using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Library.Abstractions;
using Library.Plugins.Abstractions;
using Library.Plugins.UI;
using UnityEngine;

namespace Library.Plugins
{
    public delegate void PluginLoadDelegate(QPlugin plugin);

    public class PluginManager : ManagerContract<PluginManager>
    {
        /// <summary>
        /// Represents a static collection of plugins of type QPlugin managed by the PluginManager.
        /// This collection serves as the internal storage for all plugins loaded in the system.
        /// The plugins are instances of classes deriving from QPlugin that implement functionality
        /// such as loading, unloading, and providing metadata (e.g., name, description, and author).
        /// </summary>
        private static List<QPlugin> _plugins = new List<QPlugin>();

        /// <summary>
        /// Represents the directory path used by the PluginManager for storing and loading plugins.
        /// This path is combined with the application's base directory and defaults to a subdirectory named "Quest".
        /// It is used to dynamically manage plugin files and ensure they are organized in a specific location.
        /// </summary>
        private static string DirectoryPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Quest");
        
        /// <summary>
        /// Fires when a plugin is loaded.
        /// </summary>
        public static event PluginLoadDelegate PluginLoaded;
        /// <summary>
        /// Fires when a plugin is unloaded.
        /// </summary>
        public static event PluginLoadDelegate PluginUnloaded;

        public PluginManager()
        {
            if (!Directory.Exists(DirectoryPath)) 
                Directory.CreateDirectory(DirectoryPath);
            
            // Listens for plugin loads and unloads
            PluginManagerInterface.Instance.ListenForPluginLoadsAndUnloads();
        }
    
        /// <summary>
        /// Registers all plugins from the specified directory.
        /// </summary>
        public void RegisterAllPlugins()
        {
            try
            {
                var pluginAssemblies = Directory.GetFiles(DirectoryPath, "*.dll");
                foreach (var pluginAssembly in pluginAssemblies)
                {
                    var asm = Assembly.LoadFrom(pluginAssembly);
                    var type = asm.DefinedTypes.FirstOrDefault(t => t.IsSubclassOf(typeof(QPlugin)));
                    if (type == null) continue;
                
                    var plugin = Activator.CreateInstance(type) as QPlugin;
                    plugin?.Load();
                    _plugins.Add(plugin);
                    PluginLoaded?.Invoke(plugin);
                }
            } 
            catch (Exception exception)
            {
                Debug.LogError($"Error registering plugins: {exception.Message}");
            }   
        }

        /// <summary>
        /// Unloads the specified plugin by its name, removes it from the managed collection,
        /// and invokes the PluginUnloaded event.
        /// </summary>
        /// <param name="pluginName">The name of the plugin to unload.</param>
        public void Unload(string pluginName)
        {
            var plugin = _plugins.FirstOrDefault(p => p.Name == pluginName);
            if (plugin == null) return;
            
            plugin.Unload();
            _plugins.Remove(plugin);
            PluginUnloaded?.Invoke(plugin);
        }
    }
}