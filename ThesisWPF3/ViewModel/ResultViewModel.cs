using NanoByte.SatSolver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ThesisWPF3.Model;
using ThesisWPF3.Service;

namespace ThesisWPF3.ViewModel
{
    public class ResultViewModel : INotifyPropertyChanged
    {
        public NavigationItem Navigationitem;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Formula<string> Formula { get; set; }

        public ResultViewModel(NavigationItem navItem)
        {
            this.Navigationitem = navItem;
            var parserService = new ParserService();
            this.Navigationitem.FirstGraph = parserService.CodonListToGraph(this.Navigationitem.FirstCodon);
            this.Navigationitem.SecondGraph = parserService.CodonListToGraph(this.Navigationitem.SecondCodon);

            Formula = parserService.GraphsToFormula(navItem.FirstGraph, navItem.SecondGraph);
        }
    }
}