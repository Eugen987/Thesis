using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ThesisWPF3.Model
{
    public class Codon : INotifyPropertyChanged
    {
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private char firstBase;
        private char secondBase;
        private char thirdBase;
        private string acid;
        private string acidShort;
        private string code;

        public Codon(string code, string acid)
        {
            var chars = code.ToCharArray();
            this.firstBase = chars[0];
            this.secondBase = chars[1];
            this.thirdBase = chars[2];
            this.acid = acid;
            this.code = code;
            this.acidShort = (this.acid == "Isoleucin") ? "Ile" : (this.acid == "Asparagin") ? "Asn" : (this.acid == "Stop") ? "Stop" : (this.acid == "Tryptophan ") ? "Trp" : (this.acid == "Glutamin ") ? "Gln" : new string(acid.Take(3).ToArray());
        }

        public char FirstBase => this.firstBase;

        public char SecondBase => this.secondBase;

        public char ThirdBase => this.thirdBase;

        public string Acid => this.acid;

        public string AcidShort => this.acidShort;

        public string Code => this.code;
    }
}