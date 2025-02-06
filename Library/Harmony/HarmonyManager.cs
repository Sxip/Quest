using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Library.Abstractions;
using Library.Harmony.Attributes;
using Library.PacketSniffer.UI;
using Library.Utilities;
using UnityEngine;

namespace Library.Harmony
{
    public class HarmonyManager : ManagerContract<HarmonyManager>
    {
        /// <summary>
        /// Gets the instance of the Harmony library used to manage and apply runtime patches.
        /// The HarmonyInstance is initialized with a unique identifier ("com.quest.harmony") to manage dependencies and avoid conflicts.
        /// This property is read-only and provides access to the underlying Harmony functionality.
        /// </summary>
        private HarmonyLib.Harmony HarmonyInstance { get; set; } = new HarmonyLib.Harmony("com.quest.harmony");

        /// <summary>
        /// Stores a list of applied patches for each assembly.
        /// This is used to track which patches have been applied and avoid duplicate patches.
        /// </summary>
        private readonly Dictionary<string, List<MethodBase>> _appliedPatches =
            new Dictionary<string, List<MethodBase>>();

        /// <summary>
        /// Stores a list of lazy-loaded patches for each assembly.
        /// This is used to track which lazy patches have been loaded and avoid duplicate patches.
        /// </summary>
        private readonly Dictionary<string, Type> _lazyPatchClasses = new Dictionary<string, Type>();

        /// <summary>
        /// Iterates through all non-blacklisted assemblies in the current application domain
        /// and applies patches to them using the Harmony instance.
        /// </summary>
        public void ApplyPatches()
        {
            var assembliesWithHarmony =
                ReflectionsUtility.GetAssembliesWithClassesWithAttributes<LazyHarmonyAttribute, HarmonyAttribute>(AppDomain.CurrentDomain
                    .GetAssemblies());

            foreach (var assembly in assembliesWithHarmony)
            {
                Debug.LogWarning($"Applying patches from assembly: {assembly.FullName}");
                ApplyPatchesFromAssembly(assembly);
            }
        }

        /// <summary>
        /// Applies patches from the specified assembly using the Harmony instance.
        /// </summary>
        /// <param name="assembly">The assembly from which to apply patches.</param>
        private void ApplyPatchesFromAssembly(Assembly assembly)
        {
            try
            {
                HarmonyInstance.PatchAll(assembly);
                Debug.LogWarning($"Patches applied from assembly: {assembly.GetName().FullName}");
            }
            catch (Exception exception)
            {
                Debug.LogError($"Error applying patches from assembly {assembly.GetName().Name}: {exception}");
            }
        }
    }
}