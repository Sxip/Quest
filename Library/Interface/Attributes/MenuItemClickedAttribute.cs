using System;

namespace Library.Interface.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class MenuItemClickedAttribute : Attribute
    {
        public MenuItemClickedAttribute()
        {
            
        }
    }
}