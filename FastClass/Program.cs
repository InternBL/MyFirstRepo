using System;
using System.IO;
using System.Linq;
using CTS_Library.Extensions;
using NXOpen;
using Snap;

namespace FastClass
{
    public static class Program
    {
        public static int GetUnloadOption(string arg) { return Convert.ToInt32(Session.LibraryUnloadOption.Immediately); }

        public static void Main(string[] args)
        {
            try
            {
                foreach (var directory in Directory.GetDirectories("P:\\Visual Studio 2013\\0Released"))
                {
                    var solutionName = Path.GetFileName(directory);
                    if(solutionName == null) continue;
                    var dllsInSolution = Directory.GetFiles(directory, "*.dll", SearchOption.AllDirectories);

                    if(! dllsInSolution.Any(dll => dll.EndsWith("bin\\Debug\\" + solutionName + ".dll") || dll.EndsWith("bin\\Release\\" + solutionName + ".dll")))
                        InfoWindow.WriteLine(solutionName);
                }




                //var solutionHeaders = Directory.GetDirectories("P:\\Visual Studio 2013\\0Released")
                //    .Select(Path.GetFileName)
                //    .ToArray();

                //var files = Directory.GetFiles("P:\\Visual Studio 2013\\0Released", "*.dll", SearchOption.AllDirectories);






                //var directories = System.IO.Directory.GetDirectories("U:\\NX110\\Concept\\NX110library\\Ufunc");

                //foreach (var dir in directories)
                //{
                //    var dirName = System.IO.Path.GetFileName(dir);
                //    if(dirName == null)continue;
                //    var expectedDll = dir + "\\" + dirName + ".dll";
                //    if(!System.IO.File.Exists(expectedDll))
                //        InfoWindow.WriteLine(expectedDll);
                //}

            }
            catch (Exception ex)
            {
                ex.PrintException();
            }
        }

    }
}