using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncDemo
{
    /// <summary>
    /// Interaction logic for MyWindow.xaml
    /// </summary>
    public partial class MyWindow : Window
    {
        public MyWindow()
        {
            InitializeComponent();
        }

        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        public OriginalWindowApi OriginalWindowApi { get; set; } = new OriginalWindowApi();

        private async void StartDownload(object sender, RoutedEventArgs e)
        {
            btn1.IsEnabled = false;
            btn2.IsEnabled = true;
            _cts = new CancellationTokenSource();//refresh the tokensource
            lbl1.Content = "Fetching...";
            try
            {
                lbl1.Content = (await DisplayWebLength(_cts.Token)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btn1.IsEnabled = true;
                btn2.IsEnabled = false;
                if (_cts.IsCancellationRequested)
                {
                    lbl1.Content = "label"; 
                }
                _cts.Dispose();
            }
        }

        private void CancelDownload(object sender , RoutedEventArgs e)
        {
            _cts.Cancel();
            btn2.IsEnabled = false;
        }

        private async void DelayTaskAsync(object o , RoutedEventArgs e)
        {
            btn3.IsEnabled = false;
            lbl1.Content = "label";

            var task = DownloadWithTiemDelay(1000);

            lbl1.Content = (await task).Length;
            btn3.IsEnabled = true;
        }

        private void AnotherModelWindow(object o , RoutedEventArgs e)
        {
            var anotherWindow = new AnotherWindow(OriginalWindowApi);
            anotherWindow.ShowDialog();
        }

        #region Helper
        private async Task<int> DisplayWebLength(CancellationToken cancellationToken)
        {
            using (var httpClient = new HttpClient())
            {
                var resultTask = httpClient.GetStringAsync("https://www.baidu.com/");
                await Task.Delay(2000);
                cancellationToken.ThrowIfCancellationRequested();
                var result = "";
                try
                {
                    result = await resultTask;
                }
                catch (System.Exception)
                { }
                return result.Length;
            }
        }

        private async Task<string> DownloadWithTiemDelay(int delay)
        {
            var result = "";

            using (var client = new HttpClient())
            {
                var downloadTask = client.GetStringAsync("http://www.baidu.com");
                var timeoutTask = Task.Delay(delay);

                var completedTask = await Task.WhenAny(downloadTask, timeoutTask);
                if (completedTask == timeoutTask)
                {
                    result = "TiemOut";
                }
                else
                {
                    result = await downloadTask;
                }

                return result;
                
            }
        }


        #endregion

        #region private fields
        private CancellationTokenSource _cts;
        #endregion
    }
}
