using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dall_E_Implementation.CustomControls
{
    public class ChatBotTextBox : TextBox
    {
        public enum ChatSender { USER, CHATGPT }

        public ChatBotTextBox(ChatSender sender, StackPanel parent)
        {

            this.FontSize = 17;
            this.Margin = new Thickness(5);
            this.BorderBrush = new SolidColorBrush(Colors.Transparent);
            this.TextWrapping = TextWrapping.Wrap;
            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3c3c4a"));
            this.Foreground = new SolidColorBrush(Colors.AntiqueWhite);
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.MaxWidth = parent.ActualWidth * 0.75;

            if (sender == ChatSender.USER)
            {

                this.Margin = new Thickness(5, 50, 5, 5);
                this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3c3c4a"));
                this.Foreground = new SolidColorBrush(Colors.GhostWhite);
                this.HorizontalAlignment = HorizontalAlignment.Right;
            }
        }
    }
}
