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

namespace ThesisWPF3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private IBidirectionalGraph<object, IEdge<object>> _graphToVisualize;

        //public IBidirectionalGraph<object, IEdge<object>> GraphToVisualize
        //{
        //    get { return _graphToVisualize; }
        //}

        public MainWindow()
        {
            InitializeComponent();
        }

        //private void CreateGraphToVisualize()
        //{
        //    var g = new BidirectionalGraph<object, IEdge<object>>();

        //    //add the vertices to the graph
        //    string[] vertices = new string[5];
        //    for (int i = 0; i < 5; i++)
        //    {
        //        vertices[i] = i.ToString();
        //        g.AddVertex(vertices[i]);
        //    }

        //    //add some edges to the graph
        //    g.AddEdge(new Edge<object>(vertices[0], vertices[1]));
        //    g.AddEdge(new Edge<object>(vertices[1], vertices[2]));
        //    g.AddEdge(new Edge<object>(vertices[2], vertices[3]));
        //    g.AddEdge(new Edge<object>(vertices[3], vertices[1]));
        //    g.AddEdge(new Edge<object>(vertices[1], vertices[4]));

        //    g.AddEdge(new Edge<object>(vertices[1], vertices[0]));
        //    g.AddEdge(new Edge<object>(vertices[2], vertices[1]));
        //    g.AddEdge(new Edge<object>(vertices[3], vertices[2]));
        //    g.AddEdge(new Edge<object>(vertices[1], vertices[3]));
        //    g.AddEdge(new Edge<object>(vertices[4], vertices[1]));

        //    _graphToVisualize = g;
    }
}