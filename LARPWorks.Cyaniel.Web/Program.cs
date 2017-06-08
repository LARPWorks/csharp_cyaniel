using System;
using Nancy.Hosting.Self;

namespace LARPWorks.Cyaniel.Web
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
#if DEBUG
            using (var host = new NancyHost(new Uri("http://localhost:5000")))
            {
                host.Start();
                Console.WriteLine("Running on http://localhost:5000");
                Console.ReadLine();
            }
#endif
        }
    }
}
