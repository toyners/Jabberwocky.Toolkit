using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Jabberwocky.Toolkit.MessageBox
{
    /// <summary>
    /// Interaction logic for ModalDialog.xaml
    /// </summary>
    public partial class CustomMessageBoxWindow : Window
    {
        public enum CustomMessageBoxSetupProperties
        {
            WindowStartupLocation,
            WindowStyle,
            WindowIcon,
            WindowOwner,
            ShowInTaskbar,
            Topmost,
            ResizeMode,
            SizeToContent,
            MaxHeight,
            MinHeight,
            MaxWidth,
            MinWidth,
            Image,
            DefaultChoiceWhenClosingWindow,
            CheckBoxOption,
        }

        #region Fields
        private Dictionary<string, int> returnValuesByCaption = new Dictionary<string, int>();
        private string defaultChoiceWhenClosingWindow;
        #endregion

        #region Construction
        public CustomMessageBoxWindow(string message, string caption, Dictionary<CustomMessageBoxSetupProperties, object> properties, params ButtonDetails[] buttonDetails) :
            this(message, caption, properties, null, buttonDetails)
        {
        }

        public CustomMessageBoxWindow(string message, string caption, Dictionary<CustomMessageBoxSetupProperties, object> baseProperties, Dictionary<CustomMessageBoxSetupProperties, object> additionalProperties, params ButtonDetails[] buttonDetails)
        {
            this.InitializeComponent();

            if (baseProperties != null && baseProperties.Count > 0)
                this.SetProperties(baseProperties);

            if (additionalProperties != null && additionalProperties.Count > 0)
                this.SetProperties(additionalProperties);

            if (buttonDetails != null && buttonDetails.Length > 0)
            {
                var buttons = new List<ButtonViewModel>();
                foreach (var button in buttonDetails)
                {
                    buttons.Add(new ButtonViewModel(button));
                    this.returnValuesByCaption.Add(button.Caption, (int)button.Result);
                }

                this.Buttons = new ReadOnlyCollection<ButtonViewModel>(buttons);
            }
            else
            {
                // default button (OK)
                this.defaultChoiceWhenClosingWindow = "OK";
                var defaultButton = new ButtonDetails { Caption = "OK", IsDefault = true, IsCancel = true, Result = 1 };
                this.returnValuesByCaption.Add(defaultButton.Caption, (int)defaultButton.Result);
                var buttons = new List<ButtonViewModel>();
                buttons.Add(new ButtonViewModel(defaultButton));
                this.Buttons = new ReadOnlyCollection<ButtonViewModel>(buttons);
            }

            this.Items.ItemsSource = this.Buttons;

            this.Caption = caption;
            this.Message = message;
        }
        #endregion

        #region Properties
        public ReadOnlyCollection<ButtonViewModel> Buttons { get; set; }
        public int MessageBoxResult { get; private set; } = -1;

        public bool? Option
        {
            get { return this.CheckBox_Grid.Visibility == Visibility.Visible ? this.CheckBox_Option.IsChecked : null; }
            private set { this.CheckBox_Option.IsChecked = value; }
        }

        private string Caption
        {
            get { return this.Title; }
            set { this.Title = value; }
        }

        private string Message
        {
            get { return this.TextBlock_Message.Text; }
            set { this.TextBlock_Message.Text = value; }
        }
        #endregion

        #region Methods
        private void SetProperties(Dictionary<CustomMessageBoxSetupProperties, object> properties)
        {
            foreach (var kv in properties)
            {
                var key = kv.Key;
                var value = kv.Value;
                switch (key)
                {
                    case CustomMessageBoxSetupProperties.CheckBoxOption:
                    {
                        Tuple<string, bool> optionDetails = null;
                        if (value != null && (optionDetails = value as Tuple<string, bool>) != null)
                        {
                            var checkBoxCaption = optionDetails.Item1;
                            if (!string.IsNullOrWhiteSpace(checkBoxCaption))
                            {
                                this.CheckBox_Grid.Visibility = Visibility.Visible;
                                this.CheckBox_Option.Content = checkBoxCaption;
                                this.CheckBox_Option.IsChecked = optionDetails.Item2;
                            }
                        }
                        
                        break;
                    }
                    case CustomMessageBoxSetupProperties.Image:
                    {
                        if (value is MessageBoxImage)
                            this.DisplayImage((MessageBoxImage)value);
                        break;
                    }
                    case CustomMessageBoxSetupProperties.ResizeMode:
                    {
                        if (Enum.TryParse<ResizeMode>(value.ToString(), out var result))
                            this.ResizeMode = result;
                        break;
                    }
                    case CustomMessageBoxSetupProperties.WindowIcon:
                    {
                        this.Icon = new BitmapImage(new Uri(value.ToString(), UriKind.Relative));
                        break;
                    }
                    case CustomMessageBoxSetupProperties.WindowOwner:
                    {
                        this.Owner = (Window)value;
                        break;
                    }
                    case CustomMessageBoxSetupProperties.WindowStartupLocation:
                    {
                        if (Enum.TryParse<WindowStartupLocation>(value.ToString(), out var result))
                            this.WindowStartupLocation = result;
                        break;
                    }
                    case CustomMessageBoxSetupProperties.ShowInTaskbar:
                    {
                        if (value is bool)
                            this.ShowInTaskbar = (bool)value;
                        break;
                    }
                    case CustomMessageBoxSetupProperties.DefaultChoiceWhenClosingWindow:
                    {
                        this.defaultChoiceWhenClosingWindow = value.ToString();
                        break;
                    }
                }
            }
        }

        private void DisplayImage(MessageBoxImage image)
        {
            Icon icon;

            switch (image)
            {
                case MessageBoxImage.Exclamation:       // Enumeration value 48 - also covers "Warning"
                    icon = SystemIcons.Exclamation;
                    break;
                case MessageBoxImage.Error:             // Enumeration value 16, also covers "Hand" and "Stop"
                    icon = SystemIcons.Hand;
                    break;
                case MessageBoxImage.Information:       // Enumeration value 64 - also covers "Asterisk"
                    icon = SystemIcons.Information;
                    break;
                case MessageBoxImage.Question:
                    icon = SystemIcons.Question;
                    break;
                default:
                    icon = SystemIcons.Information;
                    break;
            }

            this.Image_MessageBox.Source = icon.ToImageSource();
            this.Image_MessageBox.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var label = (Label)((Button)sender).Content;
            this.MessageBoxResult = this.returnValuesByCaption[label.Content.ToString()];
            this.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (this.MessageBoxResult == -1 && 
                this.defaultChoiceWhenClosingWindow != null &&
                this.returnValuesByCaption.TryGetValue(this.defaultChoiceWhenClosingWindow, out var returnValue))
            {
                this.MessageBoxResult = returnValue;
            }
        }
        #endregion

        #region Structs
        public class ButtonViewModel
        {
            private ButtonDetails buttonDetails;
            public ButtonViewModel(ButtonDetails buttonDetails) => this.buttonDetails = buttonDetails;

            public string Caption { get { return this.buttonDetails.Caption; } }
            public bool IsCancel { get { return this.buttonDetails.IsCancel; } }
            public bool IsDefault { get { return this.buttonDetails.IsDefault; } }
        }

        public struct ButtonDetails
        {
            public string Caption;
            public bool IsDefault;
            public bool IsCancel;
            public string ImageName;
            public uint Result; // Results can only be positive, -1 is reserved (no choice made)
        }
        #endregion
    }
}
