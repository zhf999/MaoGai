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
        private string? questionTitle;
        public string QuestionTitle
        {
            get { return questionTitle; }
            set {
                questionTitle = value;
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("QuestionTitle"));
            }
        }

        private Candidates candidates;
        public Candidates Candidates {
            get { return candidates; }
            set
            {
                candidates = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Candidates"));
            }
        }

        private bool isNotAnswered;
        public bool IsNotAnswered
        {
            get { return isNotAnswered;}
            set
            {
                isNotAnswered = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsNotAnswered"));
            }
        }
        private string shownString;
        public string ShownString
        {
            get { return shownString; }
            set
            {
                shownString = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ShownString"));
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

            isNotAnswered = true;
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
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Choices"));
            }
        }

        public static string[] IDs = new string[] { "A", "B", "C", "D", "E", "F" };

        public Candidates()
        {
            this.cnt = 2;
            this.choices = new Selection[2] { new Selection("对"), new Selection("错") };
            this.choices[0].ID = "对";
            this.choices[1].ID = "错";
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
            }
        }

        public Selection(string content)
        {
            this.content = content;
        }
    }

}
