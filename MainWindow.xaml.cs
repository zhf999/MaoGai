using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MaoGai
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public QuestionList questionList;
        public Question curQuestion;

        private Random random;
        public MainWindow()
        {
            InitializeComponent();
            random = new Random();
            try
            {
                using (StreamReader sr = new StreamReader(@"./data/ti.json"))
                {
                    string jsonString;
                    jsonString = sr.ReadToEnd();
                    JArray jArray = JArray.Parse(jsonString);
                    questionList = new QuestionList(jArray);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            int index = random.Next(0, questionList.Cnt);
            curQuestion = questionList[index];

            this.QCnt.DataContext = questionList;
            this.QGrid.DataContext = curQuestion;
        }


        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {   
            string submittedAnswer="";
            foreach(Selection selection in curQuestion.Candidates.Choices)
            {
                if(selection.IsSelected==true)
                {
                    submittedAnswer+=selection.ID;
                }
                
            }
            Console.WriteLine(submittedAnswer);
            int index = random.Next(random.Next(0,questionList.Cnt));
            curQuestion = questionList[index];
            questionList.RemoveAt(index);
            this.QGrid.DataContext = curQuestion;
        }
    }
}
