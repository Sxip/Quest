using System;

namespace Library.Interface.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class InterfaceMenuItemAttribute : Attribute
    {
        public string Name { get; }
        
        public InterfaceMenuItemAttribute(string name)
        {
            Name = name;
        }
    }
}