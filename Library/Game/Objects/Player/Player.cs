using System;
using System.Linq;

namespace Library.Game.Objects.Player
{
    public static class PlayerObject
    {
        /// Gets the current instance of the player represented by "Me".
        /// </summary>
        /// <remarks>
        /// This property provides access to the player entity associated with the current context.
        /// </remarks>
        public static global::Player Me => Entities.Instance.me;

        /// <summary>
        /// Determines whether the specified player instance represents the current player.
        /// </summary>
        /// <param name="player">The player instance to check.</param>
        /// <returns>True if the specified player instance represents the current player; otherwise, false.</returns>
        public static bool IsMe(this global::Player player) => Me.isMe;
        
        /// <summary>
        /// Determines whether the specified target is a valid target for the current player.
        /// </summary>
        /// <param name="target">The target NPC to be validated.</param>
        /// <returns>True if the target is valid and can be attacked by the current player; otherwise, false.</returns>
        public static bool HasValidTarget(NPC target)
        {
            return Me.CanAttack(target) && target.HealthPercent > 0f;
        }

        /// <summary>
        /// Retrieves an inventory item by its name from the current player's inventory.
        /// </summary>
        /// <param name="name">The name of the inventory item to retrieve.</param>
        /// <returns>The inventory item that matches the specified name; otherwise, null if no matching item is found.</returns>
        public static InventoryItem GetInventoryItem(string name)
        {
            return Session.MyPlayerData.items.FirstOrDefault((InventoryItem i) =>
                ((Item)i).Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}