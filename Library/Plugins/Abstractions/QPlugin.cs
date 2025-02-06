namespace Library.Plugins.Abstractions
{
    public abstract class QPlugin
    {
        /// <summary>
        /// The name of the plugin.
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// The description of the plugin.
        /// </summary>
        public abstract string Description { get; }
        /// <summary>
        /// The author of the plugin.
        /// </summary>
        public abstract string Author { get; }
        /// <summary>
        /// Called when the plugin is loaded.
        /// </summary>
        public abstract void Load();
        /// <summary>
        /// Called when the plugin is unloaded.
        /// </summary>
        public abstract void Unload();
    }
}