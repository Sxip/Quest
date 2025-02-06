using System;
using HarmonyLib;
using Library.Harmony.Attributes;
using Newtonsoft.Json;
using UnityEngine;

namespace Library.PacketSniffer.Patches
{
    /// <summary>
    /// A Harmony patch class that intercepts and processes outgoing requests
    /// from the AEC component's sendRequest method. This class facilitates
    /// capturing and serializing network requests for packet-sniffing purposes.
    /// </summary>
    [LazyHarmony]
    [HarmonyPatch(typeof(AEC), nameof(AEC.sendRequest))]
    public class AecPatch
    {
        /// <summary>
        /// Intercepts and processes outgoing network requests via the AEC component's sendRequest method.
        /// Captures, serializes, and forwards the request for packet-sniffing purposes.
        /// </summary>
        /// <param name="__instance">The current instance of the AEC class where this method is invoked.</param>
        /// <param name="r">The network request object that is being sent.</param>
        [HarmonyPrefix]
        public static void SendRequest(AEC __instance, Request r)
        {
            if (!PacketSniffer.IsRunning) return;
            
            try
            {
                var serializedRequest =
                    JsonConvert.SerializeObject(r, Formatting.None, PacketSniffer.SerializerSettings);

                PacketSniffer.SendRequest(serializedRequest);
            }
            catch (Exception exception)
            {
                Debug.LogError($"Error intercepting sendRequest: {exception.Message}");
            }
        }
    }
}