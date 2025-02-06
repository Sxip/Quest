using HarmonyLib;

namespace Library.Cheat.Patches
{
    /// <summary>
    /// The PatchIsGrounded class is used to modify the behavior of the IsGrounded method
    /// in the EntityController class within the game. This class applies a Harmony patch
    /// to override the result of the IsGrounded method based on a custom IsFlying state.
    /// </summary>
    [HarmonyPatch(nameof(EntityController), nameof(EntityController.IsGrounded))]
    public class PatchIsGrounded
    {
        /// <summary>
        /// Indicates whether the entity is currently flying.
        /// </summary>
        public static bool IsFlying = false;


        /// <summary>
        /// Patches the IsGrounded method of the EntityController class to indicate whether the entity is currently flying.
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        [HarmonyPostfix]
        public static void IsGrounded(EntityController __instance, ref bool __result)
        {
            if (IsFlying)
            {
                __result = true;
            }
        }
    }

    /// <summary>
    /// The JumpHeightPatch class is designed to modify the behavior of the Jump method
    /// in the EntityController class within the game. This class applies a Harmony patch
    /// to alter the jump height of the entity based on custom logic.
    /// </summary>
    [HarmonyPatch(nameof(EntityController), "Jump")]
    public class JumpHeightPatch
    {
        /// <summary>
        /// Represents a multiplier used to adjust the height of an entity's jump.
        /// </summary>
        public static float HeightMultiplier = 1.0f;

        /// <summary>
        /// Provides a field reference to the vertical speed variable in the EntityController class.
        /// </summary>
        private static readonly AccessTools.FieldRef<EntityController, float> VerticalSpeedRef =
            AccessTools.FieldRefAccess<float>(typeof(EntityController), "verticalSpeed");
        
        [HarmonyPostfix]
        public static void Jump(EntityController __instance)
        {
            var originalSpeed = VerticalSpeedRef(__instance);
            var modifiedSpeed = originalSpeed * HeightMultiplier;
            VerticalSpeedRef(__instance) = modifiedSpeed;
        }
    }
}