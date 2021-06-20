using Microsoft.VisualStudio.TestTools.UnitTesting;
using NanoByte.SatSolver;
using System;
using System.Linq;
using ThesisWPF3.Model;
using ThesisWPF3.Service;

namespace UnitTestCodonComparer
{
    [TestClass]
    public class FormulaCreationTests
    {
        private static ParserService parserService = new ParserService();
        private static Solver<string> solver = new Solver<string>();

        [TestMethod]
        public void TestMethod1()
        {
            var graph1 = new Graph(0);
            graph1.Vertices.Add(new Vertex("1", "c1"));
            graph1.Vertices.Add(new Vertex("2", "c1"));
            graph1.Vertices.Add(new Vertex("3", "c1"));
            graph1.Vertices.Add(new Vertex("4", "c2"));
            graph1.Vertices.Add(new Vertex("5", "c2"));
            graph1.Vertices.Add(new Vertex("6", "c2"));
            graph1.Vertices.Add(new Vertex("7", "c3"));
            graph1.Vertices.Add(new Vertex("8", "c3"));
            graph1.Vertices.Add(new Vertex("9", "c3"));
            graph1.Edges.Add(new Edge(graph1.Vertices.FirstOrDefault(x => x.Description.Equals("1")), graph1.Vertices.FirstOrDefault(x => x.Description.Equals("2"))));

            var graph2 = new Graph(0);
            graph2.Vertices.Add(new Vertex("A", "c1"));
            graph2.Vertices.Add(new Vertex("B", "c1"));
            graph2.Vertices.Add(new Vertex("C", "c1"));
            graph2.Vertices.Add(new Vertex("D", "c2"));
            graph2.Vertices.Add(new Vertex("E", "c2"));
            graph2.Vertices.Add(new Vertex("F", "c2"));
            graph2.Vertices.Add(new Vertex("G", "c3"));
            graph2.Vertices.Add(new Vertex("H", "c3"));
            graph2.Vertices.Add(new Vertex("I", "c3"));

            var formula = parserService.GraphsToFormula(graph1, graph2);

            Assert.IsFalse(formula.ContainsEmptyClause);
            Assert.AreEqual(42, formula.Count());
            Assert.IsFalse(solver.IsSatisfiable(formula));
        }

        [TestMethod]
        public void TestMethod2()
        {
            var graph1 = new Graph(0);
            graph1.Vertices.Add(new Vertex("1", "c1"));
            graph1.Vertices.Add(new Vertex("2", "c1"));
            graph1.Vertices.Add(new Vertex("3", "c1"));
            graph1.Vertices.Add(new Vertex("4", "c2"));
            graph1.Vertices.Add(new Vertex("5", "c2"));
            graph1.Vertices.Add(new Vertex("6", "c2"));
            graph1.Vertices.Add(new Vertex("7", "c3"));
            graph1.Vertices.Add(new Vertex("8", "c3"));
            graph1.Vertices.Add(new Vertex("9", "c3"));

            var graph2 = new Graph(0);
            graph2.Vertices.Add(new Vertex("A", "c1"));
            graph2.Vertices.Add(new Vertex("B", "c1"));
            graph2.Vertices.Add(new Vertex("C", "c1"));
            graph2.Vertices.Add(new Vertex("D", "c2"));
            graph2.Vertices.Add(new Vertex("E", "c2"));
            graph2.Vertices.Add(new Vertex("F", "c2"));
            graph2.Vertices.Add(new Vertex("G", "c3"));
            graph2.Vertices.Add(new Vertex("H", "c3"));
            graph2.Vertices.Add(new Vertex("I", "c3"));
            graph2.Edges.Add(new Edge(graph2.Vertices.FirstOrDefault(x => x.Description.Equals("C")), graph2.Vertices.FirstOrDefault(x => x.Description.Equals("B"))));

            var formula = parserService.GraphsToFormula(graph1, graph2);

            Assert.IsFalse(formula.ContainsEmptyClause);
            Assert.AreEqual(42, formula.Count());
            Assert.IsFalse(solver.IsSatisfiable(formula));
        }

        [TestMethod]
        public void TestMethod3()
        {
            var graph1 = new Graph(0);
            graph1.Vertices.Add(new Vertex("1", "c1"));
            graph1.Vertices.Add(new Vertex("2", "c1"));
            graph1.Vertices.Add(new Vertex("3", "c1"));
            graph1.Edges.Add(new Edge(graph1.Vertices.FirstOrDefault(x => x.Description.Equals("1")), graph1.Vertices.FirstOrDefault(x => x.Description.Equals("2"))));
            graph1.Edges.Add(new Edge(graph1.Vertices.FirstOrDefault(x => x.Description.Equals("1")), graph1.Vertices.FirstOrDefault(x => x.Description.Equals("3"))));

            var graph2 = new Graph(0);
            graph2.Vertices.Add(new Vertex("A", "c1"));
            graph2.Vertices.Add(new Vertex("B", "c1"));
            graph2.Vertices.Add(new Vertex("C", "c1"));
            graph2.Edges.Add(new Edge(graph2.Vertices.FirstOrDefault(x => x.Description.Equals("A")), graph2.Vertices.FirstOrDefault(x => x.Description.Equals("C"))));

            var formula = parserService.GraphsToFormula(graph1, graph2);

            Assert.IsFalse(formula.ContainsEmptyClause);
            Assert.AreEqual(22, formula.Count());
            Assert.IsFalse(solver.IsSatisfiable(formula));
        }

        [TestMethod]
        public void TestMethod4()
        {
            var graph1 = new Graph(0);
            graph1.Vertices.Add(new Vertex("1", "c1"));
            graph1.Vertices.Add(new Vertex("2", "c1"));
            graph1.Vertices.Add(new Vertex("3", "c1"));
            graph1.Edges.Add(new Edge(graph1.Vertices.FirstOrDefault(x => x.Description.Equals("1")), graph1.Vertices.FirstOrDefault(x => x.Description.Equals("2"))));
            graph1.Edges.Add(new Edge(graph1.Vertices.FirstOrDefault(x => x.Description.Equals("1")), graph1.Vertices.FirstOrDefault(x => x.Description.Equals("3"))));

            var graph2 = new Graph(0);
            graph2.Vertices.Add(new Vertex("A", "c1"));
            graph2.Vertices.Add(new Vertex("B", "c1"));
            graph2.Vertices.Add(new Vertex("C", "c1"));
            graph2.Edges.Add(new Edge(graph2.Vertices.FirstOrDefault(x => x.Description.Equals("A")), graph2.Vertices.FirstOrDefault(x => x.Description.Equals("C"))));

            var formula = parserService.GraphsToFormula(graph2, graph1);

            Assert.IsFalse(formula.ContainsEmptyClause);
            Assert.AreEqual(22, formula.Count());
            Assert.IsFalse(solver.IsSatisfiable(formula));
        }
    }
}