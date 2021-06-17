using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisWPF3.Model;

namespace ThesisWPF3.ViewModel
{
    public class CodonUserControlViewModel
    {
        public CodonUserControlViewModel()
        {
            LeftSelectedCodons = new ObservableCollection<Codon>();
            RightSelectedCodons = new ObservableCollection<Codon>();
        }

        public ObservableCollection<Codon> LeftSelectedCodons
        {
            get;
            set;
        }

        public ObservableCollection<Codon> RightSelectedCodons
        {
            get;
            set;
        }
    }
}