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
using ThesisWPF3.Service;
using ThesisWPF3.ViewModel;

namespace ThesisWPF3.View
{
    /// <summary>
    /// Interaction logic for CodonUserControl.xaml
    /// </summary>
    public sealed partial class CodonUserControl : UserControl
    {
        public CodonUserControl()
        {
            this.InitializeComponent();
            ViewModel = new CodonUserControlViewModel();
        }

        public event EventHandler LeftSelectionChanged;

        public event EventHandler RightSelectionChanged;

        public CodonUserControlViewModel ViewModel { get; }

        private void OnLeftSelectionChanged()
        {
            EventHandler handler = LeftSelectionChanged;
            handler?.Invoke(this, null);
        }

        private void OnRightSelectionChanged()
        {
            EventHandler handler = RightSelectionChanged;
            handler?.Invoke(this, null);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBoxName = (sender as CheckBox).Name;
            var code = checkBoxName.Split('x')[0];
            var isLeftSide = checkBoxName.Split('x')[1].Equals("left");

            if (isLeftSide)
            {
                if (!this.ViewModel.LeftSelectedCodons.Any(x => x.FirstBase == code[0] && x.SecondBase == code[1] && x.ThirdBase == code[2]))
                {
                    var codon = new ParserService().CodeToCodon(code);
                    this.ViewModel.LeftSelectedCodons.Add(codon);
                }
                OnLeftSelectionChanged();
            }
            else
            {
                if (!this.ViewModel.RightSelectedCodons.Any(x => x.FirstBase == code[0] && x.SecondBase == code[1] && x.ThirdBase == code[2]))
                {
                    var codon = new ParserService().CodeToCodon(code);
                    this.ViewModel.RightSelectedCodons.Add(codon);
                }
                OnRightSelectionChanged();
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkBoxName = (sender as CheckBox).Name;
            var code = checkBoxName.Split('x')[0];
            var isLeftSide = checkBoxName.Split('x')[1].Equals("left");

            if (isLeftSide)
            {
                if (this.ViewModel.LeftSelectedCodons.Any(x => x.FirstBase == code[0] && x.SecondBase == code[1] && x.ThirdBase == code[2]))
                {
                    var codonToRemove = this.ViewModel.LeftSelectedCodons.Where(x => x.FirstBase == code[0] && x.SecondBase == code[1] && x.ThirdBase == code[2]).FirstOrDefault();
                    this.ViewModel.LeftSelectedCodons.Remove(codonToRemove);
                }
                OnLeftSelectionChanged();
            }
            else
            {
                if (this.ViewModel.RightSelectedCodons.Any(x => x.FirstBase == code[0] && x.SecondBase == code[1] && x.ThirdBase == code[2]))
                {
                    var codonToRemove = this.ViewModel.RightSelectedCodons.Where(x => x.FirstBase == code[0] && x.SecondBase == code[1] && x.ThirdBase == code[2]).FirstOrDefault();
                    this.ViewModel.RightSelectedCodons.Remove(codonToRemove);
                }
                OnRightSelectionChanged();
            }
        }
    }
}