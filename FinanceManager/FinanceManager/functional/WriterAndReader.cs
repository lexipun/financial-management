using System;
using System.IO;
using IOFiles;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.functional
{
    class WriterAndReader: Binder
    {
        private string _path = "./resourses/mainData/";
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
            string rez = "\t\t" + DateTime.Now + "\n";
            foreach (var expenseOrIncomeWithCause in _historyOfIncomesAndExpenses)
            {
                rez += (expenseOrIncomeWithCause) + "\n";
            }

            IO.Write(path, rez);
        }

        public void WriteToFullHistory()
        {
            Writer(_path + "FullHistory.txt");
        }

        public void WriteHistoryWithLifeTimeOneMonth()
        {
            string lokalPath = "partOfHistory.txt";

            if(Directory.Exists(_path + lokalPath) && Directory.GetLastAccessTime(_path + lokalPath).Month != DateTime.Now.Month)
            {
                Directory.Delete(_path + lokalPath);
            }

            Writer(_path + lokalPath);

        }

        public void UserDataSaver()
        {
            IO.BinarySerialize(MainWindow.myFinance, _path + "currentFinances.xml");
        }

        public void UserDataLoader()
        {
            IO.BinaryDeserialize(ref MainWindow.myFinance, _path + "currentFinances.xml");
        }
    }
}
