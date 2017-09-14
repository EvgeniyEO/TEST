using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestMAP
{
    public static class LogError
    {
        public static void MessageInfo(string caption, string msg)
        {
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void MessageError(string caption, string msg)
        {
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void MessageError(Exception _ex, string caption)
        {
            MessageBox.Show(_ex.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //WriteLog(_ex.Message + "\n************************\n" + _ex.StackTrace + "\n******************\n\n");
             string mes = "Ошибка: " + _ex.Message +
                  "\nОбъект: " + _ex.Source +
                  "\nМетод, вызвавший исключение: " + _ex.TargetSite +
                  "\nСтэк: " + _ex.StackTrace;
            WriteLog(mes);
        }

        public static void MessageError(Exception _ex, string message, string caption, bool IsShow)
        {
            if (IsShow)
                MessageBox.Show(string.IsNullOrEmpty(message) ? _ex.Message : message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //WriteLog(_ex.Message + "\n************************\n" + _ex.StackTrace + "\n******************\n\n");
            string mes = "Ошибка: " + _ex.Message +
                 "\nОбъект: " + _ex.Source +
                 "\nМетод, вызвавший исключение: " + _ex.TargetSite +
                 "\nСтэк: " + _ex.StackTrace;
            WriteLog(mes);
        }

        public static void ShowError(Exception _ex, string caption)
        {
            MessageBox.Show(_ex.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowError(string txt, string caption)
        {
            MessageBox.Show(txt, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult MessageQuery(string caption, string msg)
        {
            return MessageBox.Show(msg, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static void WriteLog(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return;
            string path = Directory.GetCurrentDirectory() + "\\log.txt";
            using (var outfile = new StreamWriter(path, true, Encoding.UTF8))
            {
                outfile.WriteLine();
                //outfile.WriteLine();
                //outfile.WriteLine("***********************");
                outfile.WriteLine("дата: {0}", DateTime.Now);
                outfile.WriteLine();
                outfile.WriteLine(msg);
                outfile.WriteLine("====================================");
            }
        }
    }
}
