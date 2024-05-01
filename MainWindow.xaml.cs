using Microsoft.WindowsAPICodePack.Dialogs;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace DuplicateFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void Test()
        {
            /* Code for opening multiple folders, to be used later on 
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Multiselect = true;
            */
        }

        private void AddFolder_Clicked(object sender, RoutedEventArgs e)
        {
            lblFolders.Content = "Folder Selected";
        }

        private void RemoveFolder_Clicked(object sender, RoutedEventArgs e)
        {
            lblFolders.Content = "Folder Removed";
        }
        private void OpenFile_Clicked(object sender, RoutedEventArgs e)
        {
            lblResults.Content = "File Opened";
        }

        private void FindDuplicates_Clicked(object sender, RoutedEventArgs e)
        {
            lblSelectFileTypes.Content = "Duplicates Found";
        }
    }
}