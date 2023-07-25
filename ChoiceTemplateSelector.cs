using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MaoGai
{
    public class ChoiceTemplateSelector:DataTemplateSelector
    {
        public DataTemplate MultiSelector { get; set; }
        public DataTemplate SingleSelector { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            //Console.WriteLine(item.ToString()); 
            FrameworkElement frameworkElement = container as FrameworkElement;
            if(frameworkElement != null && item !=null && item is Question)
            {
                Question question = item as Question;
                if(question.type==QuestionType.SingleSelection||question.type==QuestionType.Judge)
                {
                    Console.WriteLine("dan");
                    return SingleSelector;
                }
                else if(question.type==QuestionType.MultiSelection)
                {
                    Console.WriteLine("duo");
                    return MultiSelector;
                }
            }
            return null;
        }
    }
}
