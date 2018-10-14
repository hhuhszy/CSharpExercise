using System;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncDemo
{
    /// <summary>
    /// Interaction logic for AnotherWindow.xaml
    /// </summary>
    public partial class AnotherWindow : Window
    {
        public AnotherWindow()
        {
            InitializeComponent();
        }

        public AnotherWindow(OriginalWindowApi api):this()
        {
            OriginalWindowApi = api;
        }

        public OriginalWindowApi OriginalWindowApi { get; set; }

        private async void btn_StartStopResume_Click(object sender, RoutedEventArgs e)
        {
            OriginalWindowApi.ProgressChanged += Handler;
            var result = await Task.Run(() =>
            {
                return OriginalWindowApi.OpenFile();
            });
        }

        private void Handler(object sender, ProgressChangedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {

                progressBar1.Maximum = e.MaxValue;
                progressBar1.Value = e.Position;
            }));
        }
    }
}
