using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Launcher
{
    internal abstract class Program
    {
        /// <summary>
        /// The name of the process to be launched or referenced by the application.
        /// This is a constant value specific to the application and is intended for internal use.
        /// </summary>
        private const string ProcessName = "aq3d";

        /// <summary>
        /// Waits for the specified game process to start and attempts to create an instance of the Injector for interaction with the process.
        /// </summary>
        /// <returns>An instance of Injector.Injector if the target process is successfully located and accessed.</returns>
        /// <exception cref="Exception">Thrown when the target process cannot be accessed or the Injector cannot be created.</exception>
        private static Injector.Injector WaitForGameProcess()
        {
            Injector.Injector monoInjector = null;

            var retryCount = 0;
            while (retryCount < 5)
            {
                var process = Process.GetProcessesByName(ProcessName).FirstOrDefault();

                if (process is null) Console.WriteLine(@"Process not found, retrying...");
                else
                {
                    Console.WriteLine(@"Process found!");

                    monoInjector = new Injector.Injector(process.Id);
                    break;
                }

                Thread.Sleep(2000);
                ++retryCount;
            }

            if (monoInjector is null) throw new Exception("Failed to create instance of MonoInjector!");
            return monoInjector;
        }

        private static byte[] ReadDllFromPath(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("DLL not found at specified path", path);
            }

            try
            {
                var loadedAssembly = File.ReadAllBytes(path);
                return loadedAssembly;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load assembly: {ex.Message}");
            }
            
            return null;
        }


        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"Welcome to the Quest Launcher!");
            Console.WriteLine();

            var monoInjector = WaitForGameProcess();
            // var assembly = ReadDllFromPath(Path.Combine(Environment.CurrentDirectory, "Library.dll"));

            var assembly = ReadDllFromPath("c:\\Dev\\Quest\\Library\\bin\\Debug\\Library.dll");
            monoInjector.Inject(assembly, "Library.Quest", "Loader", "Load");
            
            Console.WriteLine(@"Injection completed!");
            Console.WriteLine();
        }
    }
}