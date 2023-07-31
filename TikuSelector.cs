using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaoGai
{
    public class TikuSelector:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<string> tikus;
        public List<string> Tikus
        {
            get { return tikus; }
            set { tikus = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tikus")); }
        }


        public TikuSelector() {
            //string[] Tikus = Directory.GetFiles(@"./data/", "*.json");
            //foreach(string each in Tikus)
            //{
            //    Console.WriteLine(each);
            //}
            DirectoryInfo directory = new DirectoryInfo(@"./data/");
            FileInfo[] fileInfos = directory.GetFiles("*.json");
            tikus = new List<string>();
            foreach (FileInfo fileInfo in fileInfos) {
                tikus.Add(fileInfo.Name.Split(".")[0]);
            }
        }
    }

}
