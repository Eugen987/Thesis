using NanoByte.SatSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisWPF3.Model;

namespace ThesisWPF3.Service
{
    public class ParserService
    {
        public ParserService()
        {
        }

        public Codon CodeToCodon(string code)
        {
            string acid = string.Empty;
            switch (code)
            {
                case "UUU":
                case "UUC":
                    acid = "Phenylalanin";
                    break;

                case "UUA":
                case "UUG":
                case "CUU":
                case "CUC":
                case "CUA":
                case "CUG":
                    acid = "Leucin";
                    break;

                case "AUU":
                case "AUC":
                case "AUA":
                    acid = "Isoleucin";
                    break;

                case "AUG":
                    acid = "Methionin";
                    break;

                case "GUU":
                case "GUC":
                case "GUA":
                case "GUG":
                    acid = "Valin";
                    break;

                case "UCU":
                case "UCC":
                case "UCA":
                case "UCG":
                case "AGU":
                case "AGC":
                    acid = "Serin";
                    break;

                case "CCU":
                case "CCC":
                case "CCA":
                case "CCG":
                    acid = "Prolin";
                    break;

                case "ACU":
                case "ACC":
                case "ACA":
                case "ACG":
                    acid = "Threonin";
                    break;

                case "GCU":
                case "GCC":
                case "GCA":
                case "GCG":
                    acid = "Alanin";
                    break;

                case "UAU":
                case "UAC":
                    acid = "Tyrosin";
                    break;

                case "UAA":
                case "UAG":
                case "UGA":
                    acid = "Stop";
                    break;

                case "CAU":
                case "CAC":
                    acid = "Histidin";
                    break;

                case "CAA":
                case "CAG":
                    acid = "Glutamin";
                    break;

                case "AAU":
                case "AAC":
                    acid = "Asparagin";
                    break;

                case "AAA":
                case "AAG":
                    acid = "Lysin";
                    break;

                case "GAU":
                case "GAC":
                    acid = "Asparaginsäure";
                    break;

                case "GAA":
                case "GAG":
                    acid = "Glutaminsäure";
                    break;

                case "UGU":
                case "UGC":
                    acid = "Cystein";
                    break;

                case "UGG":
                    acid = "Tryptophan";
                    break;

                case "CGU":
                case "CGC":
                case "CGA":
                case "CGG":
                case "AGA":
                case "AGG":
                    acid = "Arginin";
                    break;

                case "GGU":
                case "GGC":
                case "GGA":
                case "GGG":
                    acid = "Glycin";
                    break;

                default:
                    acid = "";
                    break;
            }

            return new Codon(code, acid);
        }

        public Formula<string> GraphsToFormula(Graph graph1, Graph graph2)
        {
            var formula = new Formula<string>();

            //Type1
            foreach (Vertex vertexGraph1 in graph1.Vertices)
            {
                var clause = new Clause<string>();
                foreach (Vertex vertexGraph2 in graph2.Vertices.Where(x => x.Color == vertexGraph1.Color))
                {
                    clause.Add(Literal.Of(vertexGraph1.Description + "," + vertexGraph2.Description));
                }

                formula.Add(clause);
            }

            //Type2
            var size = graph1.Vertices.Count();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i != j)
                    {
                        for (int k = 0; k < size; k++)
                        {
                            if (graph1.Vertices.ElementAt(i).Color == graph2.Vertices.ElementAt(k).Color && graph1.Vertices.ElementAt(j).Color == graph2.Vertices.ElementAt(k).Color)
                            {
                                formula.Add(new Clause<string>
                                {
                                    Literal.Of(graph1.Vertices.ElementAt(i).Description + "," + graph2.Vertices.ElementAt(k).Description).Negate(),
                                    Literal.Of(graph1.Vertices.ElementAt(j).Description + "," + graph2.Vertices.ElementAt(k).Description).Negate()
                                });
                            }
                        }
                    }
                }
            }

            //Type3
            for (int j = 1; j < size; j++)
            {
                for (int i = 0; i < j; i++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        for (int l = 0; l < size; l++)
                        {
                            if (l != k)
                            {
                                if (isEdge(graph1, i, j) != isEdge(graph2, k, l))
                                {
                                    if (graph1.Vertices.ElementAt(i).Color == graph2.Vertices.ElementAt(k).Color && graph1.Vertices.ElementAt(j).Color == graph2.Vertices.ElementAt(l).Color)
                                    {
                                        formula.Add(new Clause<string>
                                        {
                                            Literal.Of(graph1.Vertices.ElementAt(i).Description + "," + graph2.Vertices.ElementAt(k).Description).Negate(),
                                            Literal.Of(graph1.Vertices.ElementAt(j).Description + "," + graph2.Vertices.ElementAt(l).Description).Negate()
                                        });
                                    }
                                }

                                if (isEdge(graph1, j, k) != isEdge(graph2, i, l))
                                {
                                    if (graph1.Vertices.ElementAt(j).Color == graph2.Vertices.ElementAt(k).Color && graph1.Vertices.ElementAt(i).Color == graph2.Vertices.ElementAt(l).Color)
                                    {
                                        formula.Add(new Clause<string>
                                        {
                                            Literal.Of(graph1.Vertices.ElementAt(j).Description + "," + graph2.Vertices.ElementAt(k).Description).Negate(),
                                            Literal.Of(graph1.Vertices.ElementAt(i).Description + "," + graph2.Vertices.ElementAt(l).Description).Negate()
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return formula;
        }

        private bool isEdge(Graph graph, int a, int b)
        {
            if (graph.Edges.Any(x => x.Source == graph.Vertices.ElementAt(a) && x.Target == graph.Vertices.ElementAt(b)))
            {
                return true;
            }

            if (graph.Edges.Any(x => x.Source == graph.Vertices.ElementAt(b) && x.Target == graph.Vertices.ElementAt(a)))
            {
                return true;
            }
            return false;
        }

        public Graph CodonListToGraph(IList<Codon> codons)
        {
            int size = codons.Count();
            var graph = new Graph(0);
            foreach (var codon in codons)
            {
                graph.Vertices.Add(new Vertex(codon.Code, codon.AcidShort));
            }

            for (int i = 1; i < size; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (compareCodons(codons.ElementAt(i), codons.ElementAt(j)))
                    {
                        graph.Edges.Add(new Edge(graph.Vertices.Where(x => x.Description == codons.ElementAt(i).Code).FirstOrDefault(), graph.Vertices.Where(x => x.Description == codons.ElementAt(j).Code).FirstOrDefault()));
                    }
                }
            }

            return graph;
        }

        private bool compareCodons(Codon firstCodon, Codon secondCodon)
        {
            if (firstCodon.FirstBase == secondCodon.FirstBase && firstCodon.SecondBase == secondCodon.SecondBase && firstCodon.ThirdBase != secondCodon.ThirdBase)
            {
                return true;
            }

            if (firstCodon.FirstBase == secondCodon.FirstBase && firstCodon.SecondBase != secondCodon.SecondBase && firstCodon.ThirdBase == secondCodon.ThirdBase)
            {
                return true;
            }

            if (firstCodon.FirstBase != secondCodon.FirstBase && firstCodon.SecondBase == secondCodon.SecondBase && firstCodon.ThirdBase == secondCodon.ThirdBase)
            {
                return true;
            }

            return false;
        }
    }
}