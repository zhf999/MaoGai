using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MaoGai
{
    public class AnswerDisplay:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string shownString;

        
        public string ShownString
        {
            get { return shownString; }
            set
            {
                shownString = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ShownString"));
            }
        }
    }
}
