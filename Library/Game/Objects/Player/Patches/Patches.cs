using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace Library.Game.Objects.Player.Patches
{
    [HarmonyPatch(typeof(global::Player), nameof(global::Player.Respawn))]
    public static class PatchPlayerRespawn
    {
        [HarmonyPrefix]
        public static void Prefix(ref ComEntity comEntity)
        {
            // Only respawn at the location you died if it's you.
            if (comEntity.name == PlayerObject.Me.name)
            {
                comEntity.posX = PlayerObject.Me.position.x;
                comEntity.posY = PlayerObject.Me.position.y;
                comEntity.posZ = PlayerObject.Me.position.z;
                comEntity.rotY = PlayerObject.Me.rotation.eulerAngles.y;
            }
        }
    }
}