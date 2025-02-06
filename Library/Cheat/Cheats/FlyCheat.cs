using System.Collections.Generic;
using System.Linq;
using Library.Cheat.Abstractions;
using Library.Cheat.Attributes;
using Library.Cheat.Patches;
using UnityEngine;

namespace Library.Cheat.Cheats
{
    [Cheat(name: nameof(FlyCheat))]
    public class FlyCheat : CheatContract
    {
        /// <summary>
        /// Represents a collection of key bindings used within the FlyCheat functionality.
        /// This property maps input actions to their corresponding keybind configurations.
        /// </summary>
        private static Dictionary<InputAction, Keybind> Keybinds { get; set; }

        /// <summary>
        /// Represents the key binding associated with moving forward in the context of the FlyCheat functionality.
        /// This property retrieves the key associated with the "forward" input action from the defined keybinds.
        /// </summary>
        private static KeyCode ForwardKey => Keybinds[(InputAction)0].key;

        private static KeyCode BackwardKey => Keybinds[(InputAction)1].key;
        private static KeyCode JumpKey => Keybinds[(InputAction)6].key;
        private static KeyCode LeftStrafeKey => Keybinds[(InputAction)2].key;
        private static KeyCode RightStrafeKey => Keybinds[(InputAction)3].key;

        /// <summary>
        /// Represents the FlyCheat class that is annotated with the CheatAttribute
        /// to signify its association with a specific cheat functionality in the library.
        /// </summary>
        public FlyCheat()
        {
            Keybinds = Clone(SettingsManager.Keybinds);
        }

        /// <summary>
        /// Creates a new dictionary by cloning the provided source dictionary.
        /// Each Keybind in the source is duplicated to ensure deep copying of its values.
        /// </summary>
        /// <param name="source">The source dictionary containing InputAction keys and Keybind values to be cloned.</param>
        /// <returns>A new dictionary with InputAction keys and cloned Keybind values.</returns>
        private static Dictionary<InputAction, Keybind> Clone(Dictionary<InputAction, Keybind> source) =>
            source.ToDictionary(kvp => kvp.Key, kvp => new Keybind(kvp.Value));

        /// <summary>
        /// Activates the cheat functionality by enabling specific game behavior or characteristics.
        /// This method is meant to be overridden by derived classes to implement cheat-specific activation logic.
        /// </summary>
        /// <param name="arguments">An object containing arguments or parameters required for activating the cheat. Can be null if no arguments are needed.</param>
        protected override void Activate(object arguments)
        {
            DisableUserKeys();
            
            PatchIsGrounded.IsFlying = true;
            Chat.Instance.AddText("Fly cheat activated.");
        }

        /// <summary>
        /// Deactivates the functionality associated with the implementation of this cheat feature.
        /// This method reverses any effects introduced by the Activate method, restoring the state to its original condition.
        /// </summary>
        protected internal override void Deactivate()
        {
            EnableUserKeys();
            
            PatchIsGrounded.IsFlying = false;
            Chat.Instance.AddText("Fly cheat deactivated.");
        }

        /// <summary>
        /// Enables user-defined keybindings for specific input actions.
        /// This method updates the keybindings for actions such as movement and jumping
        /// based on the predefined configuration, ensuring they match user preferences.
        /// Any changes are saved and applied through relevant game systems.
        /// </summary>
        private void EnableUserKeys()
        {
            foreach (var inputAction in new[]
                     {
                         InputAction.Forward,
                         InputAction.Backward,
                         InputAction.Jump,
                         InputAction.LeftStrafe,
                         InputAction.RightStrafe
                     })
            {
                if (Keybinds.TryGetValue(inputAction, out var userKeybind))
                {
                    SettingsManager.Keybinds[inputAction].key = userKeybind.key;
                }
            }

            SettingsManager.SaveKeySettings(SettingsManager.Keybinds.Values.ToList());
            InputManager.OnKeysUpdated();
        }

        /// <summary>
        /// Disables the user's key bindings for specific input actions by setting their associated keys to zero.
        /// Invokes necessary methods to save the updated key settings and notify the input system of the changes.
        /// </summary>
        private void DisableUserKeys()
        {
            foreach (var inputAction in new[]
                     {
                         InputAction.Forward,
                         InputAction.Backward,
                         InputAction.Jump,
                         InputAction.LeftStrafe,
                         InputAction.RightStrafe
                     })
            {
                SettingsManager.Keybinds[inputAction].key = 0;
            }

            SettingsManager.SaveKeySettings(SettingsManager.Keybinds.Values.ToList());
            InputManager.OnKeysUpdated();
        }

        /// <summary>
        /// Handles the update logic for the FlyCheat, allowing real-time movement adjustments
        /// based on player input while the cheat is active and flying mode is enabled.
        /// </summary>
        protected override void OnUpdate()
        {
            if (!IsActive) return;

            // Only proceed if the flying cheat is active
            if (!PatchIsGrounded.IsFlying) return;

            var playerEntity = Entities.Instance.me;
            if (playerEntity == null) return;

            var wrapperTransform = playerEntity.wrapper.transform;
            var movement = Vector3.zero;
            

            if (Input.GetKey(ForwardKey)) movement += wrapperTransform.forward;
            if (Input.GetKey(BackwardKey)) movement -= wrapperTransform.forward;
            if (Input.GetKey(RightStrafeKey)) movement += wrapperTransform.right;
            if (Input.GetKey(LeftStrafeKey)) movement -= wrapperTransform.right;

            if (Input.GetKey(JumpKey)) movement += Vector3.up;
            if (Input.GetKey(KeyCode.LeftControl)) movement += Vector3.down;

            movement *= 15f * Time.deltaTime;

            wrapperTransform.Translate(movement, Space.World);
            movement = Vector3.Lerp(movement, movement * 15f, Time.deltaTime * 5f);
        }
    }
}