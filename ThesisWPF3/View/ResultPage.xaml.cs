using NanoByte.SatSolver;
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
using ThesisWPF3.ViewModel;

namespace ThesisWPF3.View
{
    /// <summary>
    /// Interaction logic for ResultPage.xaml
    /// </summary>
    public sealed partial class ResultPage : Page
    {
        private ResultViewModel _viewModel;

        public ResultPage()
        {
            this.InitializeComponent();
            DataContext = _viewModel;
        }

        public ResultPage(NavigationItem naviItem)
        {
            this.InitializeComponent();
            DataContext = _viewModel;
            _viewModel = new ResultViewModel(naviItem);

            //FormulaTextBlock.Text = "F = ";

            foreach (var clause in _viewModel.Formula)
            {
                var clausStackPanel = new StackPanel();
                clausStackPanel.Orientation = Orientation.Horizontal;
                if (clause.Any())
                {
                    clausStackPanel.Children.Add(new TextBlock() { Text = "{", FontSize = 20 });
                    var lastLiteral = clause.Last();
                    foreach (var literal in clause)
                    {
                        if (!literal.Negated)
                        {
                            clausStackPanel.Children.Add(new TextBlock() { Text = "X", FontSize = 20 });
                            clausStackPanel.Children.Add(new TextBlock() { Text = literal.ToString(), FontSize = 12, Margin = new Thickness(0, 12, 0, 0) });
                        }
                        else
                        {
                            clausStackPanel.Children.Add(new TextBlock() { Text = "\u00ACX", FontSize = 20 });
                            clausStackPanel.Children.Add(new TextBlock() { Text = literal.ToString().Substring(1, literal.ToString().Length - 1), FontSize = 12, Margin = new Thickness(0, 12, 0, 0) });
                        }

                        if (literal != lastLiteral)
                        {
                            clausStackPanel.Children.Add(new TextBlock() { Text = ",  ", FontSize = 20 });
                        }
                    }
                    clausStackPanel.Children.Add(new TextBlock() { Text = "}", FontSize = 20 });
                }
                else
                {
                    clausStackPanel.Children.Add(new TextBlock() { Text = "{ }", FontSize = 20 });
                }

                FormaluStackPanel.Children.Add(clausStackPanel);
            }

            var solver = new Solver<string>();
            bool result = solver.IsSatisfiable(_viewModel.Formula);

            if (result)
            {
                ResultTextbox.Text = "Die Graphen sind isomorph";
                Border1.BorderBrush = new SolidColorBrush(Colors.Green);
                Border2.BorderBrush = new SolidColorBrush(Colors.Green);
                FormulaTextBlock.Text = "Folgende aussagenlogische Formel ist erfüllbar:";
            }
            else
            {
                ResultTextbox.Text = "Die Graphen sind nicht isomorph";
                Border1.BorderBrush = new SolidColorBrush(Colors.Red);
                Border2.BorderBrush = new SolidColorBrush(Colors.Red);
                FormulaTextBlock.Text = "Folgende aussagenlogische Formel ist nicht erfüllbar:";
            }
            Loaded += Page_Loaded;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ContentPresenterGraphLeft.Content = new GraphUserControl(new ParserService().CodonListToGraph(_viewModel.Navigationitem.FirstCodon));
            ContentPresenterGraphRight.Content = new GraphUserControl(new ParserService().CodonListToGraph(_viewModel.Navigationitem.SecondCodon));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new CodonSelectionPage(this._viewModel.Navigationitem));
        }
    }
}