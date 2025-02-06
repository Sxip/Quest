namespace Library.Cheat.Abstractions
{
    public abstract class CheatContract
    {
        /// <summary>
        /// Indicates whether the cheat is currently active or inactive.
        /// When set to true, the cheat is active; when set to false, the cheat is inactive.
        /// This property is managed internally and is updated by the Toggle method,
        /// which activates or deactivates the cheat depending on its current state.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Toggles the activation state of the cheat. If the cheat is active, it will be deactivated.
        /// If the cheat is inactive, it will be activated using the provided arguments.
        /// </summary>
        /// <param name="arguments">The arguments used when activating the cheat.</param>
        public void Toggle(object arguments)
        {
            if (IsActive)
            {
                IsActive = false;
                Deactivate();
            }
            else
            {
                IsActive = true;
                Activate(arguments);
            }
        }

        /// <summary>
        /// Updates the state of the cheat if it is currently active by invoking the OnUpdate method.
        /// This method is intended to be called periodically, such as during a game loop.
        /// </summary>
        public void Update()
        {
            if (IsActive)
            {
                OnUpdate();
            }
        }

        /// <summary>
        /// Activates the cheat using the provided arguments. This method is meant to be overridden in derived classes to define the specific behavior of the cheat activation.
        /// </summary>
        /// <param name="arguments">The arguments required for activating the cheat.</param>
        protected abstract void Activate(object arguments);

        /// <summary>
        /// Deactivates the cheat. This method is intended to be overridden in derived classes
        /// to define the specific behavior when the cheat is deactivated.
        /// </summary>
        protected internal abstract void Deactivate();

        /// <summary>
        /// Defines the behavior that should occur during each update cycle when the cheat is active.
        /// This method is intended to be overridden in derived classes to implement custom update logic.
        /// </summary>
        protected virtual void OnUpdate()
        {
            
        }
    }
}