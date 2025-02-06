using System;
using System.IO;
using System.Reflection;

namespace Library.Resolver
{
    public static class EmbeddedAssemblyResolver
    {
        public static void Register()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => args.Name.StartsWith("0Harmony")
                ? LoadEmbeddedAssembly(Resource._0Harmony)
                : null;
        }

        private static Assembly LoadEmbeddedAssembly(byte[] assemblyBytes)
        {
            if (assemblyBytes == null || assemblyBytes.Length == 0) 
                throw new Exception("Embedded assembly is empty or missing!");

            return Assembly.Load(assemblyBytes);
        }
    }
}