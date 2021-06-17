using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisWPF3.Model
{
    public class Graph
    {
        public List<Vertex> Vertices;
        public List<Edge> Edges;

        public Graph(int vertexNumber)
        {
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();

            for (int i = 1; i <= vertexNumber; i++)
            {
                Vertices.Add(new Vertex());
            }
        }
    }
}