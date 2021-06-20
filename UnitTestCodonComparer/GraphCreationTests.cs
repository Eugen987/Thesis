using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using ThesisWPF3.Model;
using ThesisWPF3.Service;

namespace UnitTestCodonComparer
{
    [TestClass]
    public class GraphCreationTests
    {
        private static ParserService parserService = new ParserService();

        [TestMethod]
        public void Test1()
        {
            Codon codon1 = new Codon("AAA", "A1");
            Codon codon2 = new Codon("AAB", "A2");

            var graph = parserService.CodonListToGraph(new List<Codon>() { codon1, codon2 });

            Assert.AreEqual(graph.Vertices.Count, 2);
            Assert.AreEqual(graph.Edges.Count, 1);
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "AAA"));
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "AAB"));
            Assert.AreEqual(graph.Vertices.FirstOrDefault(x => x.Description == "AAA").Color, "A1");
            Assert.AreEqual(graph.Vertices.FirstOrDefault(x => x.Description == "AAB").Color, "A2");
        }

        [TestMethod]
        public void Test2()
        {
            Codon codon1 = new Codon("AAA", "A1");
            Codon codon2 = new Codon("BCD", "A2");

            var graph = parserService.CodonListToGraph(new List<Codon>() { codon1, codon2 });

            Assert.AreEqual(graph.Vertices.Count, 2);
            Assert.AreEqual(graph.Edges.Count, 0);
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "AAA"));
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "BCD"));
            Assert.AreEqual(graph.Vertices.FirstOrDefault(x => x.Description == "AAA").Color, "A1");
            Assert.AreEqual(graph.Vertices.FirstOrDefault(x => x.Description == "BCD").Color, "A2");
        }

        [TestMethod]
        public void Test3()
        {
            Codon codon1 = new Codon("AAA", "A1");
            Codon codon2 = new Codon("AAB", "A2");
            Codon codon3 = new Codon("AAC", "A2");
            Codon codon4 = new Codon("AAD", "A2");
            Codon codon5 = new Codon("BCC", "A2");
            Codon codon6 = new Codon("BCD", "A2");
            Codon codon7 = new Codon("BCE", "A2");
            Codon codon8 = new Codon("BCF", "A2");

            var graph = parserService.CodonListToGraph(new List<Codon>() { codon1, codon2, codon3, codon4, codon5, codon6, codon7, codon8 });

            Assert.AreEqual(graph.Vertices.Count, 8);
            Assert.AreEqual(graph.Edges.Count, 12);
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "AAA"));
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "AAB"));
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "AAC"));
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "AAD"));
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "BCC"));
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "BCD"));
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "BCE"));
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "BCF"));
        }

        [TestMethod]
        public void Test4()
        {
            Codon codon1 = new Codon("AAA", "A1");
            Codon codon2 = new Codon("BBB", "A2");
            Codon codon3 = new Codon("CCC", "A2");
            Codon codon4 = new Codon("DDD", "A2");
            Codon codon5 = new Codon("EEE", "A2");
            Codon codon6 = new Codon("FFF", "A2");
            Codon codon7 = new Codon("GGG", "A2");
            Codon codon8 = new Codon("HHH", "A2");

            var graph = parserService.CodonListToGraph(new List<Codon>() { codon1, codon2, codon3, codon4, codon5, codon6, codon7, codon8 });

            Assert.AreEqual(graph.Vertices.Count, 8);
            Assert.AreEqual(graph.Edges.Count, 0);
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "AAA"));
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "BBB"));
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "CCC"));
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "DDD"));
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "EEE"));
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "FFF"));
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "GGG"));
            Assert.IsTrue(graph.Vertices.Any(x => x.Description == "HHH"));
        }
    }
}