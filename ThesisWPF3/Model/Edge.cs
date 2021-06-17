using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ThesisWPF3.Model
{
    public class Edge : INotifyPropertyChanged
    {
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Vertex source;
        private Vertex target;

        public Edge(Vertex source, Vertex target)
        {
            this.source = source;
            this.target = target;
        }

        public Vertex Source => this.source;
        public Vertex Target => this.target;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}