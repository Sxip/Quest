using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Library.Abstractions;
using Library.Cheat.Abstractions;
using Library.Cheat.Attributes;
using Library.Mono.Attributes;
using Library.Utilities;
using UnityEngine;

namespace Library.Cheat
{
    public class CheatManager : ManagerContract<CheatManager>
    {
        /// <summary>
        /// A private dictionary that maps cheat names to their corresponding cheat implementations derived from AbstractCheat.
        /// Used to manage and store registered cheats within the CheatManager.
        /// </summary>
        private readonly Dictionary<string, CheatContract> Cheats = new Dictionary<string, CheatContract>();

        /// <summary>
        /// Registers all cheats from assemblies that contain classes annotated with the <see cref="CheatAttribute"/>.
        /// </summary>
        public void RegisterAllCheats()
        {
            var assemblies =
                ReflectionsUtility.GetAssembliesWithClassesWithAttribute<CheatAttribute>(AppDomain.CurrentDomain
                    .GetAssemblies());
            
            foreach (var assembly in assemblies)
            {
                RegisterCheatsFromAssembly(assembly);
            }
        }

        /// <summary>
        /// Periodically updates the state of all active cheats registered in the CheatManager.
        /// This method is automatically invoked and iterates over all cheats, calling their
        /// respective Update methods if they are active. The method is annotated with <see cref="MonoUpdateAttribute"/>
        /// to ensure it integrates with a supported update lifecycle system.
        /// </summary>
        [MonoUpdate]
        public static void Update()
        {
            foreach (var cheat in CheatManager.Instance.Cheats.Values.Where(cheat => cheat.IsActive)) 
                cheat.Update();
        }
        
        /// <summary>
        /// Toggles the specified cheat on or off. If the cheat is currently active, it will be deactivated;
        /// otherwise, it will be activated. Optional arguments can be passed to configure the target cheat during activation.
        /// </summary>
        /// <param name="name">The name of the cheat to toggle.</param>
        /// <param name="arguments">Additional arguments to be used when activating the cheat. Defaults to null if not provided.</param>
        public void ToggleCheat(string name, object arguments = null)
        {
            if (Cheats.TryGetValue(name, out var cheat))
            {
                cheat.Toggle(arguments);
            }
        }

        /// <summary>
        /// Unloads a cheat by deactivating and removing it from the internal dictionary of registered cheats.
        /// </summary>
        /// <param name="name">The name of the cheat to be unloaded.</param>
        private void Unload(string name)
        {
            if (!Cheats.TryGetValue(name, out var cheat)) return;
           
            cheat.Deactivate();
            Cheats.Remove(name);
        }
        

        /// <summary>
        /// Registers all cheats found within the specified assembly. This method identifies types in the assembly
        /// that are decorated with the <see cref="CheatAttribute"/> and initializes them as cheats.
        /// </summary>
        /// <param name="assembly">The assembly to search for types decorated with the <see cref="CheatAttribute"/>.</param>
        private void RegisterCheatsFromAssembly(Assembly assembly)
        {
            var types = ReflectionsUtility.GetTypesWithAttribute<CheatAttribute>(assembly);
            foreach (var type in types)
            {
                try
                {
                    var attribute = type.GetCustomAttribute<CheatAttribute>();
                    var instance = Activator.CreateInstance(type);

                    if (instance == null)
                    {
                        Debug.LogError($"Failed to create instance of type {type.FullName}");
                        continue;
                    }

                    if (!(instance is CheatContract cheatInstance)) continue;

                    Cheats[attribute.Name] = cheatInstance;
                    Debug.LogWarning($"Cheat loaded: {attribute.Name}, Total Cheats: {Cheats.Values.Count()}");
                }
                catch (Exception exception)
                {
                    Debug.LogError($"Failed to load cheat from type {type.FullName}: {exception.Message}");
                }
            }
        }
    }
}