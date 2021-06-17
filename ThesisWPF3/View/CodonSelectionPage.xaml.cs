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
using ThesisWPF3.Model;
using ThesisWPF3.Service;

namespace ThesisWPF3.View
{
    /// <summary>
    /// Interaction logic for CodonSelectionPage.xaml
    /// </summary>
    public partial class CodonSelectionPage : Page
    {
        private NavigationItem navigationItem;
        private CodonUserControl codonUserControl;

        public CodonSelectionPage()
        {
            this.InitializeComponent();

            codonUserControl = new CodonUserControl();
            ContentPresenterColon.Content = codonUserControl;
            Loaded += Page_Loaded;
        }

        public CodonSelectionPage(NavigationItem navigationitem)
        {
            this.InitializeComponent();
            this.navigationItem = navigationitem;
            codonUserControl = new CodonUserControl();
            ContentPresenterColon.Content = codonUserControl;
            Weiter_Button.IsEnabled = true;
            Loaded += Page_Loaded;
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (navigationItem != null)
            {
                foreach (var codon in navigationItem.FirstCodon)
                {
                    CheckBox cb = FindVisualChildren<CheckBox>(this).Where(x => x.Name.Contains("left") && x.Name.Contains(codon.Code)).FirstOrDefault();
                    cb.IsChecked = true;
                }

                foreach (var codon in navigationItem.SecondCodon)
                {
                    CheckBox cb = FindVisualChildren<CheckBox>(this).Where(x => x.Name.Contains("right") && x.Name.Contains(codon.Code)).FirstOrDefault();
                    cb.IsChecked = true;
                }
            }

            codonUserControl.LeftSelectionChanged += LeftUserControl_SelectionChanged;
            codonUserControl.RightSelectionChanged += RightUserControl_SelectionChanged;

            ContentPresenterGraphLeft.Content = new GraphUserControl(new ParserService().CodonListToGraph(codonUserControl.ViewModel.LeftSelectedCodons));
            ContentPresenterGraphRight.Content = new GraphUserControl(new ParserService().CodonListToGraph(codonUserControl.ViewModel.RightSelectedCodons));
        }

        private void LeftUserControl_SelectionChanged(object sender, EventArgs e)
        {
            ContentPresenterGraphLeft.Content = new GraphUserControl(new ParserService().CodonListToGraph(codonUserControl.ViewModel.LeftSelectedCodons));
            UpdateCheckBoxAndButtonEnable("left");
        }

        private void RightUserControl_SelectionChanged(object sender, EventArgs e)
        {
            ContentPresenterGraphRight.Content = new GraphUserControl(new ParserService().CodonListToGraph(codonUserControl.ViewModel.RightSelectedCodons));
            UpdateCheckBoxAndButtonEnable("right");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var navItem = new NavigationItem()
            {
                FirstCodon = codonUserControl.ViewModel.LeftSelectedCodons,
                SecondCodon = codonUserControl.ViewModel.RightSelectedCodons
            };

            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new ResultPage(navItem));
        }

        private void Clear_Red_Click(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox cb in FindVisualChildren<CheckBox>(this).Where(x => x.Name.Contains("left")))
            {
                cb.IsChecked = false;
            }
        }

        private void Clear_Blue_Click(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox cb in FindVisualChildren<CheckBox>(this).Where(x => x.Name.Contains("right")))
            {
                cb.IsChecked = false;
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public void UpdateCheckBoxAndButtonEnable(string side)
        {
            var mostSelected = codonUserControl.ViewModel.RightSelectedCodons.GroupBy(x => x.AcidShort).OrderByDescending(x => x.Count()).Select(g => new { AcidShort = g.Key, Count = g.Count() });
            if (side == "left")
            {
                mostSelected = codonUserControl.ViewModel.LeftSelectedCodons.GroupBy(x => x.AcidShort).OrderByDescending(x => x.Count()).Select(g => new { AcidShort = g.Key, Count = g.Count() });
            }

            //Disable all unselected acid that got selected 3 times
            foreach (var acid in mostSelected.Where(x => x.Count >= 3))
            {
                foreach (CheckBox cb in FindVisualChildren<CheckBox>(this).Where(x => x.Name.Contains(side) && x.Name.Contains(acid.AcidShort)))
                {
                    if (cb.IsChecked == false)
                    {
                        cb.IsEnabled = false;
                    }
                }
            }

            //Enable all acid that got selected less than 3 times
            foreach (var acid in mostSelected.Where(x => x.Count < 3))
            {
                foreach (CheckBox cb in FindVisualChildren<CheckBox>(this).Where(x => x.Name.Contains(side) && x.Name.Contains(acid.AcidShort)))
                {
                    cb.IsEnabled = true;
                }
            }

            if (codonUserControl.ViewModel.LeftSelectedCodons.Count != 0
                 && codonUserControl.ViewModel.LeftSelectedCodons.Count == codonUserControl.ViewModel.RightSelectedCodons.Count)
            {
                Weiter_Button.IsEnabled = true;
            }
            else
            {
                Weiter_Button.IsEnabled = false;
            }
        }
    }
}