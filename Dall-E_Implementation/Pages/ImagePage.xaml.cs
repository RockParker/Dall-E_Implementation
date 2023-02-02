using Dall_E_Implementation.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dall_E_Implementation.Pages
{
    /// <summary>
    /// Interaction logic for ImagePage.xaml
    /// </summary>
    public partial class ImagePage : Page
    {
        private MainWindow _mainwindow;
        private ChatBot _chatbot;
        private Page _previous;
        public ImagePage(ChatBot bot, MainWindow mw, Page previous)
        {
            _mainwindow = mw;
            _chatbot = bot;
            _previous = previous;
            InitializeComponent();
        }

        private async void btnSubmit_click(object sender, RoutedEventArgs e)
        {

            ChatBotTextBox tbox = new ChatBotTextBox(ChatBotTextBox.ChatSender.USER, spMain);
            tbox.Text = tbInput.Text;

            spMain.Children.Add(tbox);

            tbox = new(ChatBotTextBox.ChatSender.CHATGPT, spMain);
            tbox.Text = "Waiting...";
            scrollDown();
            spMain.Children.Add(tbox);


            string result = await _chatbot.getImageResult(tbInput.Text);
            tbox.Text = result;
            var temp = await getImageFromURL(result);
            temp.MaxWidth = 512;
            temp.MaxHeight = 512;
            spMain.Children.Add(temp);
            scrollDown();

        }


        private async Task<Image> getImageFromURL(string url)
        {
            BitmapImage img = new BitmapImage(new Uri(url));

            Image image = new Image();
            image.Source = img;

            return image;
        }

        private void tbInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void tbInput_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSubmit_click(sender, e);
            }
        }

        private void scrollDown()
        {
            svChatWindow.ScrollToBottom();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            _mainwindow.MainFrame.Navigate(_previous);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            _mainwindow.MainFrame.Navigate(new SettingPage(this, _mainwindow));
        }
    }
}
