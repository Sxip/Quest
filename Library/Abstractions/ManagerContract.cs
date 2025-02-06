using System;

namespace Library.Abstractions
{
    public class ManagerContract<T> where T : new()
    {
        /// <summary>
        /// Represents the internally stored singleton instance of the HookManager utilized by the HarmonyManager.
        /// This variable is a thread-safe, lazy-initialized instance, ensuring that the object is only created when it is first needed.
        /// </summary>
        private static readonly Lazy<T> _instance = new Lazy<T>(() => new T());

        /// <summary>
        /// Provides access to the singleton instance of the HookManager used by the HarmonyManager.
        /// This property ensures thread-safe, lazy initialization, creating the instance only when accessed for the first time.
        /// </summary>
        public static T Instance => _instance.Value;
    }
}