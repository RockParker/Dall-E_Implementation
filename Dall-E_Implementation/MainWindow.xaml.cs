using Dall_E_Implementation.Pages;
using Dall_E_Implementation.Properties;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
namespace Dall_E_Implementation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ChatBot bot;

        public MainWindow()
        {
            InitializeComponent();
            bot = new ChatBot();
            MainFrame.Navigate(new IndexPage(this));

        }
    }
}
