using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Test
{
    class Program
    {
        //public static int x = 0;

        public static string str = "D://共享文件.txt";
        public static string CurrentNumber = null;
        public static string OldNumber = null;
        public static int Statistics = 0;
        public static string str1 = "D://Send/Send.exe";
        static void Main(string[] args)
        {
            StartConsole();
            Console.ReadLine(); 
        }
        public static void StartConsole()
        {
            //x += 1;
            //Console.WriteLine("第" + x + "次");
            ReadAllTxT();
            Console.ReadLine();
        }

        /// <summary>
        /// 读取txt文件
        /// </summary>
        public static void ReadAllTxT()
        {
            Console.WriteLine("执行一次方法");
            string s = File.ReadAllText(str);
            if (OldNumber == null)
            {
                Console.WriteLine("第一次读取txt文件");
                OldNumber = s;
            }
            else
            {
                CurrentNumber = s;
                if (OldNumber == CurrentNumber)
                {
                    Statistics += 1;
                    Console.WriteLine("检查一次" + ",oldNumber:" + OldNumber + ",CurrentNumber:" + CurrentNumber + ",Statistics:" + Statistics);
                    if (Statistics >= 5)
                    {
                        Console.WriteLine("开始寻找进程，并杀死和重新开启");
                        CloseProcess();
                        Statistics = 0;
                    }
                }
                else
                {
                    Statistics = 0;
                    OldNumber = CurrentNumber;
                }
            }
            Thread.Sleep(6000);
            ReadAllTxT();
        }

        /// <summary>
        /// 打开关闭相对应的进程
        /// </summary>
        private static void CloseProcess()
        {
            try
            {
                if (Process.GetProcessesByName("Send").Length > 0)
                {
                    Process p = Process.GetProcessesByName("Send")[0];
                    p.Kill();
                }
                else
                {
                    Console.WriteLine("没找到");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Process.Start(str1);
            Statistics = 0;
        }
    }
}
