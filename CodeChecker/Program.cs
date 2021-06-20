using NanoByte.SatSolver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisWPF3.Model;
using ThesisWPF3.Service;

namespace CodeChecker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int counter = 0;
            string line;
            var parseService = new ParserService();
            string dirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var solver = new Solver<string>();

            string filePath = dirPath + @"\Res\Codes.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            Stopwatch stopwatch = new Stopwatch();

            var outputLines = new List<string>();

            var graphList = new List<KeyValuePair<int, Graph>>();
            while ((line = file.ReadLine()) != null)
            {
                var codonList = new List<Codon>();
                foreach (var code in line.Split(' '))
                {
                    codonList.Add(parseService.CodeToCodon(code));
                }
                var graph = parseService.CodonListToGraph(codonList);
                var mostSelected = graph.Vertices.GroupBy(x => x.Color).OrderByDescending(x => x.Count()).Select(g => new { AcidShort = g.Key, Count = g.Count() });
                if (mostSelected.FirstOrDefault().Count > 3)
                {
                    var outputLine = "Code " + (counter + 1) + " erfüllt Auflagen nicht, ";
                    foreach (var acid in mostSelected.Where(x => x.Count > 3))
                    {
                        outputLine += acid.AcidShort + " (" + acid.Count + ")  ";
                    }
                    outputLine += "wird/werden zu häufig codiert.";
                    graphList.Add(new KeyValuePair<int, Graph>(counter, null));
                    outputLines.Add(outputLine);
                    Console.WriteLine(outputLine);
                }
                else
                {
                   / graphList.Add(new KeyValuePair<int, Graph>(counter, graph));
                    outputLines.Add("Code " + (counter + 1) + " erfüllt Auflagen, keine Aminosäure wird öfter als 3 mal codiert.");
                    Console.WriteLine("Code " + (counter + 1) + " erfüllt Auflagen, keine Aminosäure wird öfter als 3 mal codiert.");
                }

                counter++;
            }

            file.Close();

            var vailidGraphCount = graphList.Where(x => x.Value != null).Count();
            outputLines.Append(vailidGraphCount + " Graphen sind valide.");
            Console.WriteLine(vailidGraphCount + " Graphen sind valide.");

            TimeSpan minElapesedTime = TimeSpan.MaxValue;
            TimeSpan maxElapesedTime = TimeSpan.Zero;
            TimeSpan overallElapesedTime = TimeSpan.Zero;

            for (int i = 1; i < graphList.Count; i++)
            {
                if (graphList.Where(x => x.Key == i).FirstOrDefault().Value != null)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (graphList.Where(x => x.Key == j).FirstOrDefault().Value != null)
                        {
                            stopwatch.Start();
                            var formula = parseService.GraphsToFormula(graphList.Where(x => x.Key == j).FirstOrDefault().Value, graphList.Where(x => x.Key == i).FirstOrDefault().Value);
                            var result = solver.IsSatisfiable(formula);
                            stopwatch.Stop();

                            minElapesedTime = (minElapesedTime < stopwatch.Elapsed) ? minElapesedTime : stopwatch.Elapsed;
                            maxElapesedTime = (maxElapesedTime > stopwatch.Elapsed) ? maxElapesedTime : stopwatch.Elapsed;
                            overallElapesedTime += stopwatch.Elapsed;

                            if (result == true)
                            {
                                outputLines.Add((j + 1) + " ist isomorph zu " + (i + 1) + " (" + stopwatch.Elapsed.TotalMilliseconds + " ms)");
                                Console.WriteLine((j + 1) + " ist isomorph zu " + (i + 1) + " (" + stopwatch.Elapsed.TotalMilliseconds + " ms)");
                            }
                            else
                            {
                                outputLines.Add((j + 1) + " ist nicht isomorph zu " + (i + 1) + " (" + stopwatch.Elapsed.TotalMilliseconds + " ms)");
                                Console.WriteLine((j + 1) + " ist nicht isomorph zu " + (i + 1) + " (" + stopwatch.Elapsed.TotalMilliseconds + " ms)");
                            }

                            stopwatch.Reset();
                        }
                    }
                }
            }

            outputLines.Add("Min. Berechnungszeit: " + minElapesedTime.TotalMilliseconds + " ms.");
            Console.WriteLine("Min. Berechnungszeit: " + minElapesedTime.TotalMilliseconds + " ms.");
            outputLines.Add("Max. Berechnungszeit: " + maxElapesedTime.TotalMilliseconds + " ms.");
            Console.WriteLine("Max. Berechnungszeit: " + maxElapesedTime.TotalMilliseconds + " ms.");
            outputLines.Add("Avg. Berechnungszeit: " + overallElapesedTime.TotalMilliseconds / (vailidGraphCount * (vailidGraphCount - 1) / 2) + " ms.");
            Console.WriteLine("Avg. Berechnungszeit: " + overallElapesedTime.TotalMilliseconds / (vailidGraphCount * (vailidGraphCount - 1) / 2) + " ms.");

            File.WriteAllLines("Output.txt", outputLines.ToArray());
            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}