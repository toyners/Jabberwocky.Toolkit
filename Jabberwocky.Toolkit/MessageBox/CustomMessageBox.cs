// -----------------------------------------------------------------------
// <copyright file="MessageBox.cs">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Jabberwocky.Toolkit.MessageBox
{
    using System;
    using System.Collections.Generic;
    using static Jabberwocky.Toolkit.MessageBox.CustomMessageBoxWindow;

    /// <summary>
    /// Displays a message box.
    /// </summary>
    public static class CustomMessageBox
    {
        public static CustomMessageBoxResult Show(string message, string caption)
        {
            return Show(message, caption, null, null, null);
        }

        public static CustomMessageBoxResult Show(string message, string caption, Dictionary<CustomMessageBoxSetupProperties, object> properties, params ButtonDetails[] buttonDetails)
        {
            return Show(message, caption, properties, null, null);
        }

        public static CustomMessageBoxResult Show(string message, string caption, Dictionary<CustomMessageBoxSetupProperties, object> baseProperties, Dictionary<CustomMessageBoxSetupProperties, object> additionalProperties,  params ButtonDetails[] buttonDetails)
        {
            var messageBox = new CustomMessageBoxWindow(message, caption, baseProperties, additionalProperties, buttonDetails);
            messageBox.ShowDialog();

            return new CustomMessageBoxResult
            {
                ButtonResult = messageBox.MessageBoxResult,
                CheckboxResult = messageBox.Option
            };
        }
    }
}
