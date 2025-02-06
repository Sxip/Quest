using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Library.Harmony.Attributes;
using UnityEngine;

namespace Library.Utilities
{
    public static class ReflectionsUtility
    {
        /// <summary>
        /// A read-only list containing the prefixes of blacklisted assembly names.
        /// Assemblies with names that start with any of these prefixes are excluded
        /// from certain reflection-based utility methods, such as filtering types or methods.
        /// </summary>
        private static List<string> BlacklistedPrefixes { get; } = new List<string>()
        {
            "System",
            "Microsoft",
            "mscorlib",
            "netstandard",
            "Unity",
            "UnityEngine",
            "UnityEditor"
        };

        /// <summary>
        /// Filters and returns a collection of assemblies containing methods
        /// decorated with a specified attribute, excluding blacklisted assemblies.
        /// </summary>
        public static IEnumerable<Assembly> GetAssembliesWithMethodsWithAttribute<TAttribute>(
            IEnumerable<Assembly> assemblies)
            where TAttribute : Attribute
        {
            return assemblies
                .Where(assembly => !IsBlacklistedAssembly(assembly))
                .Where(assembly =>
                    SafeGetTypes(assembly).Any(type =>
                        type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
                                        BindingFlags.Instance)
                            .Any(method =>
                                method.GetCustomAttributes(typeof(TAttribute), false).Any())));
        }

        /// <summary>
        /// Searches the provided assemblies for classes that are annotated with a specific attribute,
        /// excluding blacklisted assemblies.
        /// </summary>
        public static IEnumerable<Assembly> GetAssembliesWithClassesWithAttribute<TAttribute>(
            IEnumerable<Assembly> assemblies)
            where TAttribute : Attribute
        {
            return assemblies
                .Where(assembly => !IsBlacklistedAssembly(assembly))
                .Where(assembly =>
                    SafeGetTypes(assembly)
                        .Any(type => type.GetCustomAttributes(typeof(TAttribute), false).Any()));
        }

        /// <summary>
        /// Searches the provided assemblies for types that have both of the specified attributes,
        /// excluding blacklisted assemblies.
        /// </summary>
        public static IEnumerable<Assembly> GetAssembliesWithClassesWithAttributes<TAttribute1, TAttribute2>(
            IEnumerable<Assembly> assemblies)
            where TAttribute1 : Attribute
            where TAttribute2 : Attribute
        {
            return assemblies
                .Where(assembly => !IsBlacklistedAssembly(assembly))
                .Where(assembly =>
                    SafeGetTypes(assembly).Any(type =>
                        type.GetCustomAttributes(typeof(TAttribute1), false).Any() &&
                        type.GetCustomAttributes(typeof(TAttribute2), false).Any()));
        }

        /// <summary>
        /// Retrieves a collection of methods from a specified assembly that are
        /// decorated with a specified attribute, excluding blacklisted assemblies.
        /// </summary>
        public static IEnumerable<MethodInfo> GetMethodsWithAttribute<TAttribute>(Assembly assembly)
            where TAttribute : Attribute
        {
            if (IsBlacklistedAssembly(assembly))
            {
                return Enumerable.Empty<MethodInfo>(); // Skip blacklisted assemblies
            }

            return SafeGetTypes(assembly)
                .SelectMany(type =>
                    type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
                                    BindingFlags.Instance)).Where(method =>
                    method.GetCustomAttributes(typeof(TAttribute), false).Any());
        }

        /// <summary>
        /// Retrieves all types in the specified assembly that are decorated with a specific attribute,
        /// </summary>
        public static IEnumerable<Type> GetTypesWithAttribute<TAttribute>(Assembly assembly)
            where TAttribute : Attribute
        {
            return SafeGetTypes(assembly)
                .Where(type => type.GetCustomAttributes(typeof(TAttribute), false).Any());
        }

        public static IEnumerable<Type> GetTypesWithAttributes<TAttribute, TAttribute2>(Assembly assembly)
            where TAttribute : Attribute
        {
            return SafeGetTypes(assembly)
                .Where(type =>
                    type.GetCustomAttributes(typeof(TAttribute), false).Any() &&
                    type.GetCustomAttributes(typeof(TAttribute2), false).Any());
        }
        
        private static Type[] SafeGetTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                // Log the exception and return only the successfully loaded types
                return ex.Types.Where(t => t != null).ToArray();
            }
            catch (Exception ex)
            {
                // Log unexpected exceptions and return an empty array
                Debug.LogError($"Failed to get types from assembly {assembly.FullName}: {ex}");
                return new Type[0];
            }
        }

        /// <summary>
        /// Determines whether the specified assembly is blacklisted.
        /// </summary>
        private static bool IsBlacklistedAssembly(Assembly assembly)
        {
            var assemblyName = assembly.GetName().Name;

            if (BlacklistedPrefixes.Any(prefix => assemblyName.StartsWith(prefix)))
                return true;

            if (assemblyName.Contains("Unity") || assemblyName.Contains("Mono"))
                return true;

            return assembly.GlobalAssemblyCache;
        }
    }
}