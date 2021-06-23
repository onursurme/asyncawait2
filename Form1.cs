using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asyncawait2
{
    public partial class Form1 : Form
    {
        int otomatik_int;
        CancellationTokenSource source;
        CancellationToken cancelToken;
        public Form1()
        {
            InitializeComponent();
            //otomatik_int = 0;
            //TimeSpan t = TimeSpan.FromSeconds(3);
            //source = new CancellationTokenSource();
            //cancelToken = source.Token;
            //Task statisticsUploader = PeriodicFooAsync(async () =>
            //{
            //    try
            //    {
            //        FooAsync();
            //    }
            //    catch (Exception ex)
            //    {
            //        // Log the exception
            //    }
            //}, TimeSpan.FromSeconds(3), cancelToken);
            go();
        }

        public void go()
        {
            otomatik_int = 0;
            TimeSpan t = TimeSpan.FromSeconds(3);
            source = new CancellationTokenSource();
            cancelToken = source.Token;
            Task statisticsUploader = PeriodicFooAsync(async () =>
            {
                try
                {
                    FooAsync();
                }
                catch (Exception ex)
                {
                    // Log the exception
                }
            }, TimeSpan.FromSeconds(2), cancelToken);
        }

        public async Task PeriodicFooAsync(Func<Task> taskFactory,TimeSpan interval, CancellationToken cancellationToken)
        {
            while (true)
            {
                var delayTask = Task.Delay(interval, cancellationToken);
                await taskFactory();
                await delayTask;
            }
        }

        public void FooAsync()
        {
            otomatik_int++;
            Console.WriteLine(otomatik_int);
            // return otomatik_int;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            otomatik_int = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            source.Cancel();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            go();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text;
        }
    }
}
