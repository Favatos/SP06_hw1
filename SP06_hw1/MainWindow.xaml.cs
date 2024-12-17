using System.Windows;
using System.Windows.Controls;

namespace SP06_hw1
{
    public partial class MainWindow : Window
    {
        private static CancellationTokenSource _cts;
        public MainWindow()
        {
            InitializeComponent();
        }

        public static void MoveProgress(ProgressBar progress, CancellationToken token)
        {
            for (int i = 0; i < 100; i++)
            {
                if (token.IsCancellationRequested) return;
                progress.Dispatcher.Invoke(() => progress.Value = i + 1);
                Thread.Sleep(20);
            }

        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            _cts.Cancel();
        }

        private async void Button_Start(object sender, RoutedEventArgs e)
        {
            List<Task> tasks = new List<Task>();
            _cts = new();
            CancellationToken token = _cts.Token;
            ProgressBar[] progresses = {
                p1,
                p2,
                p3,
                p4,
            };

            for (int i = 0; i < progresses.Length; i++)
            {
                int iCopy = i;
                Task task = Task.Run(() => MoveProgress(progresses[iCopy], token));
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            MessageBox.Show("Всё скопировано!");
        }
    }
}
