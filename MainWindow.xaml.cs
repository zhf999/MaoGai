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
        public TikuSelector tikuSelector;

        private Random random;
        public MainWindow()
        {
            InitializeComponent();
            random = new Random();

            this.questionList = new QuestionList("ti");
            this.tikuSelector = new TikuSelector();
            curQuestion = questionList.Next();
            this.InfoPanel.DataContext = questionList;
            this.QGrid.DataContext = curQuestion;
            this.TikuGrid.DataContext = tikuSelector;
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
            curQuestion.IsNotAnswered = false;
            if(submittedAnswer==curQuestion.answer)
            {
                curQuestion.ShownString = String.Format("回答正确，答案为{0}",curQuestion.answer);
                questionList.CntCorrect++;
            }
            else { 
                curQuestion.ShownString =  String.Format("回答错误，正确答案为{0}",curQuestion.answer);
                questionList.CntWrong++;
            }

        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            curQuestion = questionList.Next();
            this.QGrid.DataContext = curQuestion;
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            curQuestion = questionList.Previous();
            this.QGrid.DataContext = curQuestion;
        }

        private void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            Object selected = this.TikuCombo.SelectedItem;
            if (selected != null)
            {
                string fileName = selected as string;
                this.questionList = new QuestionList(fileName);
                this.InfoPanel.DataContext = this.questionList;
                Console.WriteLine(questionList.Questions.Count);
                Console.WriteLine(fileName);
            }
           
        }
    }
}
