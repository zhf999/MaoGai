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
using System.Reflection;

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
                CurQuestion.ShownString = String.Format("回答正确，正确答案为{0}，您的答案为{1}",CurQuestion.answer,submittedAnswer);
                QuestionList.CntCorrect++;
            }
            else { 
                CurQuestion.ShownString =  String.Format("回答错误，正确答案为{0}，您的答案为{1}",CurQuestion.answer,submittedAnswer);
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
            MessageBoxResult boxResult = MessageBox.Show(this,"切换题库将彻底覆盖当前题库，是否继续？", "警告",
                MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if(boxResult==MessageBoxResult.OK)
            {
                this.SwitchTiku(sender,e);
            }
        }

        private void SwitchTiku(object sender, RoutedEventArgs e)
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
            MessageBoxResult boxResult = MessageBox.Show(this, "切换至错题将彻底覆盖当前题库，是否继续？", "警告",
            MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if(boxResult==MessageBoxResult.OK)
            {
                this.QuestionList = new QuestionList(this.QuestionList.Wrong);
                this.CurQuestion = this.QuestionList.Current();
            }
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("本软件基于MIT协议开源于https://github.com/zhf999/MaoGai\n" +
                "请宣传时保证对方zhf-xdu.top下载以维护原作者利益\n" +
                "by 周洪锋","关于", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
