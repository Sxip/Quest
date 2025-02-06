using Library.Interface.UI;
using Library.Plugins;
using UnityEngine;

namespace Library.Quest
{
    public static class Loader
    {
        
        /// <summary>
        /// Represents a Unity GameObject, a fundamental entity in the Unity Scene.
        /// Used as a container component that can hold scripts, behaviors, and other components in the scene hierarchy.
        /// </summary>
        private static readonly GameObject GameObject = new GameObject();

        /// <summary>
        /// Initializes and attaches a Boot component to a persistent GameObject.
        /// Ensures that the GameObject is not destroyed when loading a new scene.
        /// </summary>
        public static void Load()
        {
            Bootstrap.Instance = GameObject.AddComponent<Bootstrap>();
            Object.DontDestroyOnLoad(GameObject);
        }
        
        /// <summary>
        /// Removes the Boot component from the persistent GameObject and destroys it.
        /// </summary>
        public static void Unload()
        {
            Root.Instance.ResetUi();
            Root.Instance.Dispose();
            
            Object.Destroy(GameObject);
        }
    }
}