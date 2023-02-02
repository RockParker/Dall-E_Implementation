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
    /// Interaction logic for IndexPage.xaml
    /// </summary>
    public partial class IndexPage : Page
    {
        private MainWindow _mainwindow;
        public IndexPage(MainWindow mw)
        {
            _mainwindow = mw;
            InitializeComponent();
        }

        private void btnCompletion_Click(object sender, RoutedEventArgs e)
        {
            _mainwindow.MainFrame.Navigate(new CompletionPage(_mainwindow.bot, _mainwindow, this));
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            _mainwindow.MainFrame.Navigate(new ImagePage(_mainwindow.bot, _mainwindow, this));
        }
    }
}
