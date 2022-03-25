using System;
using System.IO;
using IOFiles;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using FinanceManager.functional.Model;

namespace FinanceManager.functional
{
    class WriterAndReader
    {
        private string _path = "./resources/mainData/";
        IOFiles.IOFiles IO = new IOFiles.IOFiles();

        public WriterAndReader()
        {

            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        private void Writer(in string path)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                if(File.GetLastWriteTime(path).ToString("yy.MM.dd") != DateTime.Today.ToString("yy.MM.dd"))
                {
                    writer.WriteLine("\t\t" + DateTime.Today.ToString("yy.MM.dd"));
                }

                foreach (var expenseOrIncomeWithCause in MainWindow.myFinance.StoryActs)
                {
                    writer.WriteLine(expenseOrIncomeWithCause.cause + " - " + expenseOrIncomeWithCause.amount);
                }
            }
        }

        public void WriteToFullHistory()
        {
            Writer(_path + "FullHistory.txt");
        }

        public void WriteHistoryWithLifeTimeOneMonth()
        {
            string lokalPath = "partOfHistory.txt";

            if (File.Exists(_path + lokalPath) && File.GetLastAccessTime(_path + lokalPath).Month != DateTime.Now.Month)
            {
                File.Delete(_path + lokalPath);
            }

            Writer(_path + lokalPath);

        }

        public void UserDataSaver()
        {
            Finance finance = MainWindow.myFinance;
            WriteToFullHistory();
            WriteHistoryWithLifeTimeOneMonth();

            IO.BinarySerialize(finance, _path + "currentFinances.xml");
        }

        public void UserDataLoader()
        {
            if (File.Exists(_path + "currentFinances.xml"))
            {

                IO.BinaryDeserialize(ref MainWindow.myFinance, _path + "currentFinances.xml");
            }
        }
    }
}
