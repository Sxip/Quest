using HarmonyLib;

namespace Library.Message.Patches
{
    [HarmonyPatch(nameof(Chat), nameof(Chat.OnChatSubmit))]
    public static class PatchChat
    {
        /// <summary>
        /// Patches the OnChatSubmit method of the Message class to intercept
        /// </summary>
        /// <param name="msg">The chat message submitted by the user.</param>
        [HarmonyPrefix]
        public static bool OnChatSubmit(string msg) => MessageManager.Instance.ProcessMessage(msg);
    }
}