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
        public QuestionList QuestionList
        {
            get { return this.InfoPanel.DataContext as QuestionList; }
            set { this.InfoPanel.DataContext = value; this.CurQuestion = value.Current(); }
        }
        public Question CurQuestion
        {
            get { return this.QGrid.DataContext as Question; }
            set { this.QGrid.DataContext = value; }
        }
        public TikuSelector TikuSelector
        {
            get { return this.TikuGrid.DataContext as TikuSelector; }
            set { this.TikuGrid.DataContext = value; }
        }


        public MainWindow()
        {
            InitializeComponent();

            this.QuestionList = new QuestionList("ti");
            this.TikuSelector = new TikuSelector();
            //this.InfoPanel.DataContext = QuestionList;
            //this.QGrid.DataContext = CurQuestion;
            //this.TikuGrid.DataContext = TikuSelector;
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {   
            string submittedAnswer="";
            foreach(Selection selection in CurQuestion.Candidates.Choices)
            {
                if(selection.IsSelected==true)
                {
                    submittedAnswer+=selection.ID;
                }
                
            }
            Console.WriteLine(submittedAnswer);
            CurQuestion.IsNotAnswered = false;
            if(submittedAnswer==CurQuestion.answer)
            {
                CurQuestion.ShownString = String.Format("回答正确，答案为{0}",CurQuestion.answer);
                QuestionList.CntCorrect++;
            }
            else { 
                CurQuestion.ShownString =  String.Format("回答错误，正确答案为{0}",CurQuestion.answer);
                QuestionList.CntWrong++;
                QuestionList.Wrong.Add(CurQuestion);
            }

        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            CurQuestion = QuestionList.Next();
            //.QGrid.DataContext = CurQuestion;
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            CurQuestion = QuestionList.Previous();
           // this.QGrid.DataContext = CurQuestion;
        }

        private void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            Object selected = this.TikuCombo.SelectedItem;
            if (selected != null)
            {
                string fileName = selected as string;
                this.QuestionList = new QuestionList(fileName);
                //this.InfoPanel.DataContext = this.QuestionList;
                Console.WriteLine(QuestionList.Questions.Count);
                Console.WriteLine(fileName);
                this.NextButton_Click(sender, e);
            }
           
        }

        private void WrongButton_Click(object sender, RoutedEventArgs e)
        {
            this.QuestionList = new QuestionList(this.QuestionList.Wrong);
            this.CurQuestion = this.QuestionList.Current();
        }
    }
}
