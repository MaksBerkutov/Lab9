using System;
using System.Collections.Generic;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Semaphore sem = new Semaphore(3, 13);
        private Random random = new Random();
        private static int count = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                var th = new Thread(OutputArrTh) { Name = $"Reader[{count++}]" };
                th.Start(getRandArr(random.Next(3, 10)));
            }
            this.Title = (count-1).ToString();
        }
        private int[] getRandArr(int Len)
        {
            int[] arr = new int[Len];
            for (int i = 0; i < Len; i++)
                arr[i] = random.Next(0, 100);
            return arr;
        }
        private void OutputArrTh(object arr)
        {
            sem.WaitOne();
            (string, string) tmp = (Thread.CurrentThread.Name, Thread.CurrentThread.ManagedThreadId.ToString());
            this.Dispatcher.Invoke(() => Text.Text += $"{tmp.Item1} [{tmp.Item2}]: {String.Join(" ", ((int[])arr))}\n");
          
            Thread.Sleep(random.Next(2000, 5000));
            sem.Release();
        }

       
    }
}
