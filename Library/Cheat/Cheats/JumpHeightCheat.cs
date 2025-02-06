using Library.Cheat.Abstractions;
using Library.Cheat.Attributes;
using Library.Cheat.Patches;

namespace Library.Cheat.Cheats
{
    [Cheat(name: nameof(JumpHeightCheat))]
    public class JumpHeightCheat : CheatContract
    {
        /// <summary>
        /// Represents the default multiplier used to modify the height value associated with
        /// the jump functionality in the cheat. This serves as the baseline multiplier value,
        /// ensuring the standard jump height is maintained unless overridden.
        /// </summary>
        private const float DefaultHeightMultiplier = 1.0f;

        /// <summary>
        /// Activates the jump height cheat by setting the jump height multiplier.
        /// This method overrides the base functionality to apply the specific cheat behavior.
        /// </summary>
        /// <param name="arguments">
        /// The value to set for the jump height multiplier. If null, a default value will be used.
        /// Expected to be of type float.
        /// </param>
        protected override void Activate(object arguments)
        {
            var heightMultiplier = arguments as float? ?? DefaultHeightMultiplier;

            JumpHeightPatch.HeightMultiplier = heightMultiplier;
            Chat.Instance.AddText($"Jump height multiplier set to {heightMultiplier}.");
        }

        /// <summary>
        /// Resets the jump height multiplier to its default value.
        /// This method is used to deactivate the jump height cheat
        /// and restore the game behavior to its original state.
        /// </summary>
        protected internal override void Deactivate()
        {
            JumpHeightPatch.HeightMultiplier = DefaultHeightMultiplier;
        }
    }
}