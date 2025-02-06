using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Library.Abstractions;
using Library.Mono.Attributes;
using Library.Utilities;
using UnityEngine;

namespace Library.Mono
{
    public class MonoManager : ManagerContract<MonoManager>
    {
        /// <summary>
        /// A collection of methods to be executed during the Mono update process.
        /// This list is populated with methods that have been decorated with the
        /// <see cref="MonoUpdateAttribute"/> attribute and registered through reflection.
        /// The methods in this list are intended to be invoked in the MonoManager's update cycle
        /// to implement periodic or repeated behavior.
        /// </summary>
        private readonly List<Action> _updateMethods = new List<Action>();

        /// <summary>
        /// Registers all methods marked with the <see cref="MonoUpdateAttribute"/> from all loaded assemblies.
        /// This method scans all assemblies available in the current application domain, identifies those
        /// containing methods decorated with the <see cref="MonoUpdateAttribute"/>, and processes them for Mono updates.
        /// </summary>
        public void RegisterAllMonoUpdates()
        {
            var assembliesWithMonoUpdates =
                ReflectionsUtility.GetAssembliesWithMethodsWithAttribute<MonoUpdateAttribute>(AppDomain.CurrentDomain
                    .GetAssemblies());

            foreach (var assembly in assembliesWithMonoUpdates)
            {
                RegisterMonoUpdatesFromAssembly(assembly);
            }
        }

        /// <summary>
        /// Executes all registered update methods in the Mono update cycle.
        /// This method iterates through the collection of methods (populated during the registration process),
        /// invoking each one. If an exception is thrown during the execution of any update method, it is caught,
        /// and an error message is logged using the Chat system.
        /// </summary>
        public void SubscribeUpdate()
        {
            foreach (var method in _updateMethods)
            {
                try
                {
                    method();
                }
                catch (Exception exception)
                {
                    Debug.LogError($"Error executing update method: {exception.Message}");
                }
            }
        }


        /// <summary>
        /// Registers Mono update methods from a specified assembly.
        /// This method scans the provided assembly for methods marked with the <see cref="MonoUpdateAttribute"/>,
        /// creates instances of their declaring types, and adds the corresponding actions to the Mono update collection.
        /// </summary>
        /// <param name="assembly">
        /// The assembly to scan for methods marked with the <see cref="MonoUpdateAttribute"/>.
        /// </param>
        private void RegisterMonoUpdatesFromAssembly(Assembly assembly)
        {
            var updateMethods = ReflectionsUtility.GetMethodsWithAttribute<MonoUpdateAttribute>(assembly);
            
            foreach (var method in updateMethods)
            {
                var action = method.IsStatic
                    ? CreateStaticDelegate(method)
                    : CreateInstanceDelegate(method);

                if (action != null) _updateMethods.Add(action);
            }
        }

        /// <summary>
        /// Creates a delegate for a static method. This method generates an <see cref="Action"/>
        /// delegate for the provided static method <see cref="MethodInfo"/> allowing the method
        /// to be later invoked without an instance.
        /// </summary>
        /// <param name="method">The <see cref="MethodInfo"/> representing the static method for which the delegate is to be created.</param>
        /// <returns>
        /// An <see cref="Action"/> delegate that wraps the specified static method. Returns null if the delegate could not be created.
        /// </returns>
        private Action CreateStaticDelegate(MethodInfo method)
        {
            return (Action)Delegate.CreateDelegate(typeof(Action), method);
        }

        /// <summary>
        /// Creates a delegate for an instance method marked with the <see cref="MonoUpdateAttribute"/>.
        /// This method ensures that the delegate is created for the appropriate instance of the method's declaring type.
        /// </summary>
        /// <param name="method">The method information for which a delegate will be created. The method must belong to a type that supports instantiation and should not be static.</param>
        /// <returns>A delegate for the specified instance method, or null if the instance could not be created or the method is invalid.</returns>
        private Action CreateInstanceDelegate(MethodInfo method)
        {
            if (method.DeclaringType == null) return null;

            var instance = Activator.CreateInstance(method.DeclaringType);
            return instance != null
                ? (Action)Delegate.CreateDelegate(typeof(Action), instance, method)
                : null;
        }
    }
}