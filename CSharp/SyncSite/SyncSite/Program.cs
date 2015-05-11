using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;
namespace MyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string pkgLocation;
            
           
            Package pkg;
            Application app;
            DTSExecResult pkgResults;

            pkgLocation =
            @”C:\Documents and Settings\User\My Documents\Visual Studio 2005\Projects\TheIntegration\TheIntegration\Package.dtsx”;
            app = new Application();
            pkg = (Package)app.LoadPackage(pkgLocation, true, null);
            pkgResults = pkg.Execute();

            Console.WriteLine(pkgResults.ToString());
            Console.ReadKey();
        

          
        }
    }

}