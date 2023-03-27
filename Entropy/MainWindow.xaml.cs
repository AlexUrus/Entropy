using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Entropy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public EntropyCalculation calculates;
        public MainWindow()
        {
            InitializeComponent();
            calculates = new EntropyCalculation();
        }

        private void Message_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculates.Message = Message.Text;
            calculates.CalcAllFields();
            PrintFields();
        }

        private void PrintFields()
        {
            Ansambl.Text = calculates.Ansambl.ToString();
            EntropyValue.Text = calculates.Entropy.ToString();
            MaxEntropy.Text = calculates.MaxEntropy.ToString();
            UnderLoadAlphabet.Text = calculates.UnderLoadAlphabet.ToString();
            EntropyFirstStage.Text = calculates.EntropyFirstStage.ToString();
            TwoSymbolGrid.ItemsSource = calculates.BigramProbabilities;
            OneSymbolGrid.ItemsSource = calculates.SymbolProbabilities;
        }
    }

    
}
