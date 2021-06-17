using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ThesisWPF3.Model
{
    public class Vertex : INotifyPropertyChanged
    {
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string color;
        private int id;
        private string description;
        private static int idCounter = 0;

        public Vertex()
        {
            this.id = idCounter++;
            this.Description = this.id.ToString();
            this.Color = "";
        }

        public Vertex(string description, string color)
        {
            this.id = idCounter++;
            this.Description = description;
            this.Color = color;
        }

        public string Color
        {
            get
            {
                return this.color;
            }

            set
            {
                if (value != this.color)
                {
                    this.color = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                if (value != this.description)
                {
                    this.description = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}