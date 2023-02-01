using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
namespace VippityVipsProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            

           
            int _numOfThreads;
            
            bool succeed = int.TryParse( textBox1.Text, out _numOfThreads);
            int numOfThreads = succeed ? _numOfThreads : 5;
            
            int _ram;
            succeed = int.TryParse(textBox2.Text, out _ram);
            int ram = succeed ? _ram : 5;
            
            int _io;
            succeed = int.TryParse(textBox3.Text, out _io);
            int io = succeed ? _io : 5;
            MessageBox.Show($"You entered, {numOfThreads} as num of threads\nYou entered, {((float)ram / (float)10).ToString()} Gb of ram\nYou entered, {io} as num of io threads\nRunning...");

            //cpu thread stuff------------------------
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < numOfThreads; i++)
            {
                threads.Add(new Thread(new ThreadStart(Run)));
            }


            //ram stuff------------------------
            List<byte[]> ramvs = new List<byte[]>();
            for (int i = 0; i < ram; i++)
            {
                ramvs.Add(new byte[100000000]);
            }



            //io threads stuff------------------------
            List<Thread> threadsio = new List<Thread>();

            for (int i = 0; i < io; i++)
            {
                var t = new Thread(() => ioTask(i + 1));
                t.Start();
                threadsio.Add(t);
            }


            Console.WriteLine("Running \n");
            Console.WriteLine("press any key to exit ...");
            Console.ReadLine();


            //clean up------------------------

            try
            {
                for (int i = 0; i < numOfThreads; i++)
                {
                    threads[i].Abort();
                }
                for (int i = 0; i < io; i++)
                {
                    threadsio[i].Abort();
                }
            }
            catch (Exception)
            {

            }





            static void Run()
            {
                int i = 0;
                while (true)
                {
                    i++;
                }
            }


            static void ioTask(int fileNum)
            {
                //int i = 0;
                string filepath = $"DummyFile{fileNum}.txt";
                FileStream fs = null;
                if (!File.Exists(filepath))
                {
                    try
                    {
                        fs = File.Create(filepath);
                        fs.Close();
                    }
                    catch (System.IO.IOException)
                    {

                    }

                }

                while (true)
                {
                    try
                    {
                        File.WriteAllText(filepath, "");
                        using (StreamWriter sw = new StreamWriter(filepath, true))
                        {
                            sw.Write("HelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHello");
                        }
                    }
                    catch (System.IO.IOException)
                    {
                    }


                }
            }
        }
    }
}
