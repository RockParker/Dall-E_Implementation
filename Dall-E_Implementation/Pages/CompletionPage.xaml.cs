using Dall_E_Implementation.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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
    /// Interaction logic for CompletionPage.xaml
    /// </summary>
    public partial class CompletionPage : Page
    {
        //
        private MainWindow _mainwindow;
        private ChatBot bot;
        private Page _previous;

        /// <summary>
        /// Sets the api key to the one in the settings.
        /// setups the layout content
        /// </summary>
        /// <param name="chatbot"></param>
        /// <param name="mw"></param>
        public CompletionPage(ChatBot chatbot, MainWindow mw, Page previous)
        {
            _mainwindow = mw;
            _previous = previous;
            if (Properties.Settings.Default.API_KEY == String.Empty)
            {
                MessageBox.Show("You need to add an API Key.\nYou can get one by making an account with openAI");
                _mainwindow.MainFrame.Navigate(new SettingPage(this, _mainwindow));
            }
            bot = chatbot;
            InitializeComponent();

            lblAPI_KEY.Content += Properties.Settings.Default.API_KEY;
        }

        /// <summary>
        /// Handles the Submit button for inputting a prompt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //creating a tbox with the text that the user input
            ChatBotTextBox tbox = new ChatBotTextBox(ChatBotTextBox.ChatSender.USER, spQuestionAnswerHost);
            string prompt = tbInput.Text;
            tbox.Text = prompt;

            //adding the prompt to the main stackpanel
            spQuestionAnswerHost.Children.Add(tbox);

            //set up the task
            var responseTask = bot.getCompletionResult(prompt);

            //resetting the textbox as the AI response
            tbox = new ChatBotTextBox(ChatBotTextBox.ChatSender.CHATGPT, spQuestionAnswerHost);
            tbox.Text = "Waiting....";//this is an element of UX / responsive UI

            //adding it to the main stack panel
            spQuestionAnswerHost.Children.Add(tbox);

            //another UX element(scroll to the bottom to show the recent  "post")
            svChatScroller.ScrollToBottom();

            //getting the response from the webpage and setting
            //it to the text of the ai response text box
            string response = await responseTask;
            response = format_response(response);
            tbox.Text = response;

            //more UX
            svChatScroller.ScrollToBottom();
            //more UX, having it highlight the prompt window
            //this is for a better response
            highlight_text(tbInput);//
        }

        /// <summary>
        /// removes the leading new lines from responses that don't have any edits
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private string format_response(string response)
        {

            if (response[1] != '\n')//this means there was a edit to the query
            { return response; }

            //if there was no change we want to remove the leading new lines
            int endIndex = 0;
            for (int i = 0; i < response.Length; i++)
            {
                if (response[i] != ' ' && response[i] != '\n')
                {
                    endIndex = i;
                    break;
                }
            }

            return response.Substring(endIndex);
        }

        /// <summary>
        /// triggers when the input TextBox get focus from use of keyboard controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbInput_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            highlight_text((TextBox)sender);
        }


        /// <summary>
        /// triggers when the input texxtbox gets focus from the mouse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            highlight_text((TextBox)sender);
        }

        /// <summary>
        /// highlights all text in the given TetxBox
        /// </summary>
        /// <param name="sender"></param>
        private void highlight_text(TextBox sender)
        {
            sender.SelectAll();
        }


        /// <summary>
        /// catches when inputs are put into the Textbox. if it is the enter key, it submits the query
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSubmit_Click(sender, e);
            }
        }

        /// <summary>
        /// handles the click event from the settings button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            _mainwindow.MainFrame.Navigate(new SettingPage(this, _mainwindow));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            _mainwindow.MainFrame.Navigate(_previous);
        }
    }
}
