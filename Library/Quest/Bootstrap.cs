using System.Threading;
using Library.Cheat;
using Library.Harmony;
using Library.Interface.UI;
using Library.Message;
using Library.Mono;
using Library.Plugins;
using Library.Resolver;
using UnityEngine;

using Application = System.Windows.Forms.Application;

namespace Library.Quest
{
    public class Bootstrap : MonoBehaviour
    {
        /// <summary>
        /// Gets or sets the singleton instance of the <see cref="Bootstrap"/> class.
        /// This property ensures that only one instance of the Bootstrap component exists
        /// within the application, enabling centralized initialization and management.
        /// </summary>
        public static Bootstrap Instance { get; set; }

        /// <summary>
        /// Initializes the Bootstrap class and starts a new thread to run the root application.
        /// This ensures that the Root instance begins its execution independently,
        /// providing the necessary interface management and event handling for the system.
        /// </summary>
        public void Awake()
        {
            EmbeddedAssemblyResolver.Register();

            new Thread(() => Application.Run(Root.Instance)).Start();
        }

        /// <summary>
        /// Handles the initial setup process after the Bootstrap object is loaded into the scene.
        /// This method ensures that all chat commands are registered through the MessageManager and
        /// applies all necessary Harmony patches via the HarmonyManager, enabling core functionalities
        /// within the system.
        /// </summary>
        public void Start()
        {
            PluginManager.Instance.RegisterAllPlugins();
            MessageManager.Instance.RegisterAllCommands();
            CheatManager.Instance.RegisterAllCheats();
            MonoManager.Instance.RegisterAllMonoUpdates();
            HarmonyManager.Instance.ApplyPatches();
        }

        /// <summary>
        /// Updates the state of the Bootstrap object on each frame.
        /// This method is part of Unity's MonoBehaviour lifecycle and can be used to handle frame-by-frame
        /// logic, ensuring that all time-dependent or recurring operations are executed properly.
        /// </summary>
        public void Update() => MonoManager.Instance.SubscribeUpdate();
        
        
        /// <summary>
        /// Handles the termination of the Bootstrap object and its associated components.
        /// </summary>
        private void OnApplicationQuit()
        {
            Loader.Unload();
        }
    }
}