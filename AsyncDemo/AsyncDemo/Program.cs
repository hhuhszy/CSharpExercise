using System;
using System.Windows;
using System.Threading.Tasks;

namespace AsyncDemo
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var app = new Application();
            app.Run(new MyWindow());

            //Run();

            //Console.ReadLine();
        }

        //static async void Run()
        //{
        //    var tcs = new TaskCompletionSource<bool>();

        //    var fireAndForgetTask = Task.Delay(3000).ContinueWith((t) =>
        //    {
        //        Console.WriteLine("!");
        //        tcs.SetResult(true);
        //    });

        //    await tcs.Task;
        //}
    }
}
