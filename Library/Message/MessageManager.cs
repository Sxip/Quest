using System;
using System.Collections.Generic;
using System.Reflection;
using Library.Abstractions;
using Library.Message.Attributes;
using Library.Utilities;
using UnityEngine;

namespace Library.Message
{
    public class MessageManager : ManagerContract<MessageManager>
    {
        /// <summary>
        /// Represents a dictionary containing command names as keys and their corresponding
        /// actions as values. This structure is used to map string-based commands to specific
        /// functionality executed via associated delegate methods. The keys are case-insensitive.
        /// </summary>
        public readonly Dictionary<string, Action<string>> Commands = new Dictionary<string, Action<string>>(
            StringComparer.InvariantCultureIgnoreCase);


        /// <summary>
        /// Registers all commands by discovering and processing methods
        /// in the loaded assemblies that are decorated with the CommandAttribute.
        /// This method uses reflection to find assemblies and methods containing
        /// the CommandAttribute and invokes the appropriate registration logic.
        /// </summary>
        public void RegisterAllCommands()
        {
            var assembliesWithCommands =
                ReflectionsUtility.GetAssembliesWithMethodsWithAttribute<CommandAttribute>(AppDomain.CurrentDomain
                    .GetAssemblies());

            foreach (var assembly in assembliesWithCommands)
            {
                RegisterCommandsFromAssembly(assembly);
            }
        }

        /// <summary>
        /// Processes a chat message by determining whether it is a regular message
        /// or a command. If it is a regular message, it returns true to indicate
        /// further processing. If it is a command, it attempts to execute the command
        /// by looking it up in the registered commands dictionary.
        /// </summary>
        /// <param name="message">The message to process, which can either be a regular text
        /// or a command prefixed with '!' or '/'.</param>
        /// <returns>True if the message is a regular text message that requires further
        /// processing; otherwise, false if it is processed as a command or ignored.</returns>
        public bool ProcessMessage(string message)
        {
            if (!string.IsNullOrWhiteSpace(message) && !message.StartsWith("!") && !message.StartsWith("/"))
            {
                Debug.LogWarning("Regular message detected, sending to ChatInstance.");
                return true;
            }

            if (!message.StartsWith("!") || message.Length <= 1) return false;

            var commandContent = message.Substring(1).Trim();
            var commandParts = commandContent.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

            var commandName = commandParts[0].ToLower();
            var arguments = commandParts.Length > 1 ? commandParts[1] : string.Empty;

            if (Commands.TryGetValue(commandName, out var action))
            {
                try
                {
                    action(arguments);
                    return false;
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error executing command '{commandName}': {ex.Message}");
                }
            }
            else
            {
                Debug.LogWarning($"Unknown command: {commandName}");
                return false;
            }

            return false;
        }

        /// <summary>
        /// Removes a command from the dictionary of registered commands. This method
        /// identifies the command by its name and removes it if it exists, ensuring that
        /// the associated functionality is no longer accessible.
        /// </summary>
        /// <param name="commandName">The name of the command to be removed from the registry.</param>
        public void Remove(string commandName)
        {
            if (Commands.ContainsKey(commandName))
            {
                Commands.Remove(commandName);
            }
        }

        /// <summary>
        /// Registers all commands defined in the specified assembly where methods
        /// are decorated with the CommandAttribute. This method identifies methods
        /// with the CommandAttribute, extracts the command name and corresponding
        /// action, and integrates them into the Commands dictionary for execution.
        /// </summary>
        /// <param name="assembly">The assembly to scan for methods decorated with the CommandAttribute.</param>
        private void RegisterCommandsFromAssembly(Assembly assembly)
        {
            var commandMethods = ReflectionsUtility.GetMethodsWithAttribute<CommandAttribute>(assembly);
            foreach (var method in commandMethods)
            {
                if (!(method.GetCustomAttribute(typeof(CommandAttribute)) is CommandAttribute commandAttribute)) continue;

                var commandName = commandAttribute.CommandName;
                var commandAction = (Action<string>)method.CreateDelegate(typeof(Action<string>));

                if (Commands.ContainsKey(commandName)) continue;
                try
                {
                    var action = (Action<string>)Delegate.CreateDelegate(typeof(Action<string>), method);
                    Commands.Add(commandName, action);
                }
                catch (Exception exception)
                {
                    Debug.LogError(
                        $"Failed to register command '{commandName}' method '{method.Name}': {exception.Message}");
                }
            }
        }
    }
}