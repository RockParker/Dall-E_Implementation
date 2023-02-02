using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Page
    {
        Page _previous;
        MainWindow _mainwindow;
        public SettingPage(Page previous, MainWindow mw)
        {
            _mainwindow = mw;
            _previous = previous;
            InitializeComponent();
        }

        /// <summary>
        /// Saves the api key to the new value. 
        /// then it re initializes theh process
        /// and finally navigates to a new page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.API_KEY = tbApiKey.Text;
            Properties.Settings.Default.Save();
            btnSave.IsEnabled = false;
            _mainwindow.bot.InitAIService();
            _mainwindow.MainFrame.Navigate(new CompletionPage(_mainwindow.bot, _mainwindow, this));
        }

        /// <summary>
        /// triggers when the control gets focus through the keyboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbAPIKey_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            highlight_text(sender as TextBox);
        }

        /// <summary>
        /// triggers when the controls gets focus on a mouse double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbAPIKey_GotMouseCapture(object sender, MouseEventArgs e)
        {
            highlight_text(sender as TextBox);
        }

        /// <summary>
        /// highlight all the text in the given control
        /// </summary>
        /// <param name="sender"></param>
        private void highlight_text(TextBox sender)
        {
            sender.SelectAll();
        }


        /// <summary>
        /// allows the user to navigate to the previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            _mainwindow.MainFrame.Navigate(_previous);
        }
    }
}
