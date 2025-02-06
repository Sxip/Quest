using HarmonyLib;
using UnityEngine;

namespace Library.Security.Patches
{
   /// <summary>
   /// Patches to disables all the tracking methods
   /// </summary>
   
   [HarmonyPatch(typeof(DeviceTracking), nameof(DeviceTracking.RecordDeviceData))]
   public class PatchRecordDeviceData
   {
      [HarmonyPrefix]
      public static bool RecordDeviceData(ref DeviceTracking __instance) => false;
   }

   [HarmonyPatch(typeof(DeviceTracking), nameof(DeviceTracking.RecordDeviceEvent))]
   public class PatchRecordDeviceEvent
   {
      [HarmonyPrefix]
      public static bool RecordDeviceEvent(ref DeviceTracking __instance) => false;
   }

   [HarmonyPatch(typeof(UserTracking), nameof(UserTracking.RecordUserEvent))]
   public class PatchRecordUserEvent
   {
      [HarmonyPrefix]
      public static bool RecordUserEvent(ref UserTracking __instance) => false;
   }
}