# <h2>LARPWORKS: Project Cyaniel</h2>

<h2>LARP Manager Application</h2>

<h3>Setting up a Working Dev Environment</h3>

1. Install MariaDB (latest version) ( https://downloads.mariadb.org/ )
2. Install a MySQL client of choice ( IE: MySQL Workbench )
3. Clone the project, change directory to the project root.
4. From the Script/Database directory, run the file 'build_schema.sql' on your database.
5. Create user 'larpworks_admin' with password 'larpworks'.
6. Open the project in Visual Studio
7. Make sure the compiler is in "Debug" mode, and run the project.
8. Navigate to http://localhost:5000/

<h3>Development Guidelines</h3>
1. New features should go into feature branches.
   `It's acceptable to just make a branch with your username for development.`
2. Master branch is reserved for releases. Usually at milestone completion stages.
   `Development branch is for active development.`</br>
   `Pull requests on feature branches go to development.`

<h3>Organization</h3>
All details on tasks and active tickets to be completed can be found at:
https://trello.com/b/Tn1Jpkxg/project-cyaniel-development