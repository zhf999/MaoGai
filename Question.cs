using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaoGai
{
    public enum QuestionType
        {
            SingleSelection = 1,
            MultiSelection = 2,
            Judge = 3
        }
    public class Question:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public QuestionType type;
        public string? questionTitle;
        public string QuestionTitle
        {
            get { return questionTitle; }
            set {
                questionTitle = value;
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("QuestionTitle"));
            }
        }

        public Candidates candidates;
        public Candidates Candidates {
            get { return candidates; }
            set
            {
                candidates = value;
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Candidates"));
            }
        }
        public string? answer;

        public Question(JObject jObject)
        {
            type = (QuestionType)jObject["type"].ToObject<int>();
            this.questionTitle = jObject["title"].ToString();
            this.answer = jObject["anser"].ToString();
            JArray jCandidcate = jObject["cadidate"].ToObject<JArray>();
            if (this.type != QuestionType.Judge)
                this.candidates = new Candidates(jCandidcate);
            else this.candidates = new Candidates();
        }
    }

    public class Candidates:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int cnt;
        private Selection[] choices;
        public Selection[] Choices
        {
            get { return choices; }
            set {
                choices = value;
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Choices"));
            }
        }

        public static string[] IDs = new string[] { "A", "B", "C", "D", "E", "F" };

        public Candidates()
        {
            this.cnt = 2;
            this.choices = new Selection[2] { new Selection("对"), new Selection("错") };
            this.choices[0].ID = "A";
            this.choices[1].ID = "B";
        }

        public Candidates(JArray jArray)
        {
            //Console.Write(jArray);
            this.cnt = jArray.Count;
            this.choices = new Selection[cnt];
            int i = 0;
            foreach(JValue jValue in jArray)
            {
                this.choices[i] = new Selection(jValue.ToString());
                this.choices[i].ID = IDs[i];
                i++;
            }
        }
    }
       
    public class Selection:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string ID;
        private string content;
        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Content"));
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
            }
        }

        public Selection(string content)
        {
            this.content = content;
        }
    }

    public class QuestionList:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
                this.PropertyChanged.Invoke(this,new PropertyChangedEventArgs("Cnt"));
            }
        }

        public Question this[int index]
        {
            get { return Questions[index];} 
            set { Questions[index] = value;}
        }

        public List<Question> Questions;

        public QuestionList(JArray jArray)
        {
            Questions = new List<Question>();
            foreach(JObject jObject in jArray)
            {
               Questions.Add(new Question(jObject));
                cnt++;
            }
            
        }

        public void RemoveAt(int index)
        {
            Cnt--;
            Questions.RemoveAt(index);
        }
    }
}
