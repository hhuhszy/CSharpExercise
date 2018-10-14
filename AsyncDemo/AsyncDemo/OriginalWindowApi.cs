using System;
using System.Threading;

namespace AsyncDemo
{
    public class OriginalWindowApi
    {
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        public string OpenFile()
        {
            var pa = new ProgressChangedEventArgs();
            pa.MaxValue = new Random().Next(10, 20);
            for (int i = 0; i < pa.MaxValue; i++)
            {
                if (!pa.IsCancel)
                {
                    Thread.Sleep(200);//simulate some cpu-bound work
                    pa.Position = i + 1;
                    pa.Caption = $"Max{pa.MaxValue}--Status{pa.Position}";
                    ProgressChanged(this, pa);
                }
            }
            if (pa.IsCancel)
            {
                return "cancelled";
            }
            return "finished";
        }
    }

    public class ProgressChangedEventArgs
    {
        public int Position { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public string Caption { get; set; }
        public bool IsCancel { get; set; } = false;

        public void Cancel()
        {
            IsCancel = true;
        }
    }
}
