using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Library.Abstractions;
using Library.Interface.Attributes;
using Library.Interface.UI;
using Library.Utilities;
using UnityEngine;


namespace Library.Interface
{
    public class InterfaceManager : ManagerContract<InterfaceManager>
    {
        /// <summary>
        /// Scans all assemblies loaded in the current AppDomain for classes decorated with the
        /// <see cref="Library.Interface.Attributes.InterfaceMenuItemAttribute"/> attribute and registers
        /// menu items for each discovered class.
        /// This method leverages reflection to gather metadata and dynamically manage menu items.
        /// </summary>
        public void RegisterAllMenuItems()
        {
            var assembliesWithMenuItems = ReflectionsUtility.GetAssembliesWithClassesWithAttribute<InterfaceMenuItemAttribute>(
                AppDomain.CurrentDomain.GetAssemblies());

            foreach (var assembly in assembliesWithMenuItems)
            {
                var typesWithAttribute = ReflectionsUtility.GetTypesWithAttribute<InterfaceMenuItemAttribute>(assembly);
    
                foreach (var type in typesWithAttribute)
                {
                    RegisterMenuItemsFromType(assembly, type);
                }
            }
        }

        /// <summary>
        /// Registers menu items for the provided type from the specified assembly.
        /// This method creates the necessary ToolStripMenuItem and binds click events to discovered methods.
        /// </summary>
        /// <param name="assembly">The assembly to scan for menu item-related attributes and methods.</param>
        /// <param name="type">The type to scan for the <see cref="InterfaceMenuItemAttribute"/> and <see cref="MenuItemClickedAttribute"/>.</param>
        private void RegisterMenuItemsFromType(Assembly assembly, Type type)
        {
            var modulesMenuItem = Root.Instance.FindMenuItem("Modules") ?? CreateModulesMenuItem();

            var moduleAttribute = type.GetCustomAttribute<InterfaceMenuItemAttribute>();
            var item = new ToolStripMenuItem(moduleAttribute.Name);

            var clickedMethod = GetMenuItemClickedMethod(type);
            if (clickedMethod != null)
            {
                BindClickEvent(item, clickedMethod, Activator.CreateInstance(type));
            }

            modulesMenuItem.DropDownItems.Add(item);
        }

        /// <summary>
        /// Creates a new "Modules" menu item if it does not already exist.
        /// </summary>
        /// <returns>A new ToolStripMenuItem for the "Modules" menu.</returns>
        private ToolStripMenuItem CreateModulesMenuItem()
        {
            var modulesMenuItem = new ToolStripMenuItem("Modules");
            Root.Instance.AddMenuItem(modulesMenuItem);
            return modulesMenuItem;
        }

        /// <summary>
        /// Finds the first method in the provided type that has the <see cref="MenuItemClickedAttribute"/> applied.
        /// </summary>
        /// <param name="type">The type to scan for the <see cref="MenuItemClickedAttribute"/>.</param>
        /// <returns>The method with the <see cref="MenuItemClickedAttribute"/>, or null if no such method is found.</returns>
        private static MethodInfo GetMenuItemClickedMethod(Type type)
        {
            return type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .FirstOrDefault(m => m.GetCustomAttribute<MenuItemClickedAttribute>() != null);
        }

        /// <summary>
        /// Binds the click event for the provided menu item to the method associated with the menu item.
        /// </summary>
        /// <param name="item">The ToolStripMenuItem to bind the click event to.</param>
        /// <param name="clickedMethod">The method to invoke when the item is clicked.</param>
        /// <param name="moduleInstance">An instance of the class that contains the clicked method.</param>
        private static void BindClickEvent(ToolStripMenuItem item, MethodInfo clickedMethod, object moduleInstance)
        {
            if (clickedMethod != null)
            {
                Debug.LogWarning($"Registering menu item: {item.Text}");
                item.Click += (sender, args) =>
                {
                    Debug.LogWarning($"Invoking method: {clickedMethod.Name}");
                    if (clickedMethod.GetParameters().Length == 0)
                    {
                        Debug.LogWarning($"Invoking method: {clickedMethod.Name}");
                        try
                        {
                            clickedMethod.Invoke(moduleInstance, null);
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show($"Error invoking method: {exception.Message}", "Error");
                        }
                    }
                    else
                    {
                        MessageBox.Show("The method must not take parameters.", "Error");
                    }
                };
            }
        }
    }
}