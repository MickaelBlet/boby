using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BobyScript
{
    // override the principal class
    public static class Console
    {
        public static void Write(string str)
        {
            BobyMultitools.Win_Script.Console.AppendText(str);
            BobyMultitools.Win_Script.Console.ScrollToEnd();
        }
        public static void WriteLine(string str)
        {
            BobyMultitools.Win_Script.Console.AppendText(str + "\r\n");
            BobyMultitools.Win_Script.Console.ScrollToEnd();
        }
    }

    public static class Environment { }

    public static class Offset { }

    public interface IBobyScript
    {
        void OnPlay();
        void OnRun();
        void OnStop();
    }


    public class BobyScript
    {
        public bool run = false;
        public void BobyScriptStop()
        {
            run = false;
        }
        public void BobyScriptPlay()
        {
            run = true;
        }

        public void Sleep(int ms)
        {
            System.Threading.Thread.Sleep(ms);
        }
        public int index = 0;
    }
}

