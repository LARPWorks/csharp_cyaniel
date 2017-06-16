[void][system.reflection.Assembly]::LoadFrom("C:\Program Files (x86)\MySQL\MySQL Connector Net 6.9.9\Assemblies\v4.5\MySQL.Data.dll")

$mariaDbUrl = ''
$mariaDbFileName = 'mariadb-installer.msi'
$mariaDbServiceName = 'MySQL'
$schemaPath = 'Scripts\Database\build_schema.sql'
$mariaDbLink = 'https://www.dropbox.com/s/ay1mk39h5oypqny/mariadb-10.2.6-winx64.msi?dl=1'

$connectorLink = 'https://www.dropbox.com/s/ie281ob9yklr0as/mysql-connector-net-6.9.9.msi?dl=1'
$connectorFileName = 'mysql-connector.msi'
$connectorInstallPath = 'C:\Program Files (x86)\MySQL\MySQL Connector Net 6.9.9'

$OFS = "`r`n"

function DownloadFile($url, $targetFile)
{
   $uri = New-Object "System.Uri" "$url"
   $request = [System.Net.HttpWebRequest]::Create($uri)
   $request.set_Timeout(15000) #15 second timeout
   $response = $request.GetResponse()
   $totalLength = [System.Math]::Floor($response.get_ContentLength()/1024)
   $responseStream = $response.GetResponseStream()
   $targetStream = New-Object -TypeName System.IO.FileStream -ArgumentList $targetFile, Create
   $buffer = new-object byte[] 10KB
   $count = $responseStream.Read($buffer,0,$buffer.length)
   $downloadedBytes = $count

   while ($count -gt 0)
   {
       $targetStream.Write($buffer, 0, $count)
       $count = $responseStream.Read($buffer,0,$buffer.length)
       $downloadedBytes = $downloadedBytes + $count
       Write-Progress -activity "Downloading file '$($url.split('/') | Select -Last 1)'" -status "Downloaded ($([System.Math]::Floor($downloadedBytes/1024))K of $($totalLength)K): " -PercentComplete ((([System.Math]::Floor($downloadedBytes/1024)) / $totalLength)  * 100)
   }
   Write-Progress -activity "Finished downloading file '$($url.split('/') | Select -Last 1)'"

   $targetStream.Flush()
   $targetStream.Close()
   $targetStream.Dispose()
   $responseStream.Dispose()
}

Write-Output 'Beginning Project Cyaniel Development Environment setup process...'

if(!(Test-Path $schemaPath)) {
    Write-Output 'This script needs to be run from the base csharp-cyaniel directory.'
    Break
}

If (-NOT ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole(`
    [Security.Principal.WindowsBuiltInRole] “Administrator”))
{
    Write-Warning “You do not have Administrator rights to run this script!`nPlease re-run this script as an Administrator!”
    Break
}

Write-Output 'Checking for MySQL Connector.net...'
if(!(Test-Path $connectorInstallPath)) {
    Write-Output 'MySQL Connector not found!'
    if(!(Test-Path $connectorFileName)) {
        Write-Output 'Downloading mysql connector...'
        DownloadFile $connectorLink $connectorFileName
    }

    Write-Output 'Installing MySQL Connector.net...'
    Start-Process msiexec.exe -Wait -ArgumentList "/i $connectorFileName /qn"
} else {
    Write-Output 'MySQL Connector Installation Found!'
}

Write-Output 'Checking for MariaDB Installation...'
if (!(Get-Service $mariaDbServiceName -ErrorAction SilentlyContinue)) {
    Write-Output 'MariaDB Installation not found!'
    if(!(Test-Path $mariaDbFileName)) {
        Write-Output 'Downloading mariadb...'
        DownloadFile $mariaDbLink $mariaDbFileName
    }

    Write-Output 'Installing MariaDB...'
    $rootPassword = Read-Host -Prompt 'Please enter a password for the root user'

    Write-Output 'Beginning Silent Installation of MariaDB...'
    Start-Process msiexec.exe -Wait -ArgumentList "/i $mariaDbFileName PORT=3306 PASSWORD=$rootPassword SERVICENAME=$mariaDbServiceName /qn /l mariadb-install.log"

    $log = Get-Content 'mariadb-install.log'
    if($log -contains "Installation failed.") {
        Write-Output "Installation failed!"
        notepad 'mariadb-install.log'
        exit
    }

} else {
    Write-Output 'MariaDB Installation Found!'
}

Write-Output 'Connecting to Project Cyaniel Database...'
$connection = New-Object -TypeName MySql.Data.MySqlClient.MySqlConnection
if([string]::IsNullOrEmpty($rootPassword)) {
    $rootPassword = Read-Host -Prompt 'Please enter password for the root user'
}

$connection.ConnectionString = "SERVER=localhost;UID=root;PWD=$rootPassword"
$connection.Open()
Write-Output 'Connection Established!'

$command = New-Object -TypeName MySql.Data.MySqlClient.MySqlCommand
$command.Connection = $connection
$command.CommandText = 'SHOW DATABASES;'
$reader = $command.ExecuteReader()

$databases = @()
while($reader.Read()) {
    $databases += $reader["Database"]
}
$reader.Close()

if($databases -notcontains 'larpworks') {
    Write-Output "Database larpworks not found! Creating..."
    $command = New-Object -TypeName MySql.Data.MySqlClient.MySqlCommand
    $command.Connection = $connection
    $command.CommandText = 'CREATE DATABASE larpworks;'
    $command.ExecuteNonQuery()
    Write-Output 'Database created.'
} else {
    Write-Output "Database larpworks found! Wiping database."
    $command = New-Object -TypeName MySql.Data.MySqlClient.MySqlCommand
    $command.Connection = $connection
    $command.CommandText = 'DROP DATABASE larpworks;'
    $command.ExecuteNonQuery()

    $command = New-Object -TypeName MySql.Data.MySqlClient.MySqlCommand
    $command.Connection = $connection
    $command.CommandText = 'CREATE DATABASE larpworks;'
    $command.ExecuteNonQuery()
    Write-Output 'Database wiped successfully'
}

$command = New-Object -TypeName MySql.Data.MySqlClient.MySqlCommand
$command.Connection = $connection
$command.CommandText = 'SELECT User FROM mysql.user;'
$reader = $command.ExecuteReader()

$users = @()
while($reader.Read()) {
    $users += $reader["User"]
}
$reader.Close()
if($users -notcontains 'larpworks_admin') {
    Write-Output "Larpworks admin user not found! Creating..."
    $command = New-Object -TypeName MySql.Data.MySqlClient.MySqlCommand
    $command.Connection = $connection
    $command.CommandText = "CREATE USER 'larpworks_admin'@'localhost' IDENTIFIED BY 'larpworks'; GRANT ALL PRIVILEGES ON *.* TO 'larpworks_admin'@'localhost'; FLUSH PRIVILEGES;"
    $command.ExecuteNonQuery()
    Write-Output 'Larpworks administrator created!'
}

$sql_schema = Get-Content $schemaPath

$command = New-Object -TypeName MySql.Data.MySqlClient.MySqlCommand
$command.Connection = $connection
$command.CommandText = "$sql_schema"
$command.ExecuteNonQuery()
