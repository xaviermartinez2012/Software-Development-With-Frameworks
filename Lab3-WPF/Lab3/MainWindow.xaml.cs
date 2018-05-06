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

namespace Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            String maximizeButtonOriginalContent = (String) maximizeButton.Content;
            maximizeButton.Click += (s, e) =>
            {
                switch (Application.Current.MainWindow.WindowState)
                {
                    case WindowState.Normal:
                        Application.Current.MainWindow.WindowState = WindowState.Maximized;
                        maximizeButton.Content = "Unmaximize!";
                        break;
                    default:
                        Application.Current.MainWindow.WindowState = WindowState.Normal;
                        maximizeButton.Content = maximizeButtonOriginalContent;
                        break;
                }
            };
            somelabel.Background = Brushes.Red;
        }
    }
}
