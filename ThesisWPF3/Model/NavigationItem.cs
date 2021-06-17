using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisWPF3.Model
{
    public class NavigationItem
    {
        public IList<Codon> FirstCodon
        {
            get;
            set;
        }

        public IList<Codon> SecondCodon
        {
            get;
            set;
        }

        public Graph FirstGraph { get; set; }
        public Graph SecondGraph { get; set; }
    }
}