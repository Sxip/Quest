using System;

namespace Library.Cheat.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CheatAttribute : Attribute
    {
        /// Gets the name associated with the Cheat attribute.
        /// This property holds the descriptive name designated for the attribute
        /// during its declaration.
        public string Name { get; }

        /// <summary>
        /// Specifies a custom attribute that can be applied at the class level.
        /// This attribute indicates that the class is associated with a "cheat" feature
        /// and includes a user-defined name for identification purposes.
        /// </summary>
        public CheatAttribute(string name)
        {
            Name = name;
        }
    }
}