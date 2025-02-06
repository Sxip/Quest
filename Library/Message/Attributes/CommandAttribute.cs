using System;

namespace Library.Message.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CommandAttribute : Attribute
    {
        public string CommandName { get; private set; }
        public string Description { get; private set; }
        
        
        public CommandAttribute(string commandName, string description = "")
        {
            CommandName = commandName;
            Description = description;
        }
    }
}