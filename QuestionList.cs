using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaoGai
{
    public class QuestionList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int index;
        private int cnt;
        public int Cnt
        {
            get
            {
                return this.cnt;
            }
            set
            {
                this.cnt = value;
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Cnt"));
            }
        }

        private int cntCorrect;
        public int CntCorrect
        {
            get { return this.cntCorrect; }
            set { this.cntCorrect = value; this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CntCorrect")); }
        }

        private int cntWrong;
        public int CntWrong
        {
            get => this.cntWrong;
            set { this.cntWrong = value; this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CntWrong")); }
        }

        public Question this[int index]
        {
            get { return Questions[index]; }
            set { Questions[index] = value; }
        }

        public List<Question> Questions;

        public QuestionList(JArray jArray)
        {
            Questions = new List<Question>();
            foreach (JObject jObject in jArray)
            {
                Questions.Add(new Question(jObject));
                cnt++;
            }
            Shuffle();
            index = -1;

        }

        public void RemoveAt(int index)
        {
            Cnt--;
            Questions.RemoveAt(index);
        }

        public void Shuffle()
        {
            int n = Cnt;
            Random rnd = new Random();
            while (n-- >= 1)
            {
                int k = rnd.Next(n + 1);
                Question temp = Questions[k];
                Questions[k] = Questions[n];
                Questions[n] = temp;
            }
        }

        public Question Next()
        {
            if(index==cnt-1)return Questions[cnt-1];
            return Questions[++index];
        }
        public Question Previous()
        {
            if(index==0)return Questions[0];
            return Questions[--index];
        }
    }
}
