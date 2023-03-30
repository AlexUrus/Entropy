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
using System.Windows.Shapes;

namespace Entropy
{
    /// <summary>
    /// Логика взаимодействия для CountMutualInfo.xaml
    /// </summary>
    public partial class CountMutualInfo : Window
    {
        public MainWindow MainWind { get; set; }

        public CountMutualInfo()
        {
            InitializeComponent();
            Message.Text = MainWind.Message.Text;
        }


    }
}
