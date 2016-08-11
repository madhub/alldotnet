using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using System.Threading;
using AForge.Video.DirectShow;

namespace Runner
{
    public class Program
    {
        static void Main(string[] args)
        {
            var numbers = Enumerable.Range(1, 1000000);
            


        }
        static void Main111(string[] args)
        {

            // enumerate video devices
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            // create video source
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            // set NewFrame event handler
            videoSource.NewFrame += (sender, eventArgs) =>
            {
                // get new frame
                //Bitmap bitmap = eventArgs.Frame;
                Console.WriteLine("Got New Frame");
                // process the frame
            };
            // start the video source
            videoSource.Start();
            // ...
            // signal to stop when you no longer need capturing
            Thread.Sleep(2000);
            videoSource.SignalToStop();
            //Console.WriteLine(Utilities.DateTimeToString(DateTime.Now));
            //CodeTest();
            Console.ReadLine();
        }

        
        


        static void CodeTest()
        {

            ManualResetEvent m = new ManualResetEvent(true); // or bool b = true
            Thread thread = new Thread(() =>
            {
                try
                {
                    while (m.WaitOne(0)) //or while(b)
                    {
                        Console.WriteLine("Inside Thread...");
                    }
                }
                finally
                {
                    //perform final operation and exit
                    Console.WriteLine("Exiting the Thread");
                }
                
                
            });

            thread.Start();
            while(!thread.IsAlive)
            {
                Thread.Sleep(5);
            }
            //do something
            Console.WriteLine("Calling Reset");
            m.Reset(); //or b = false
            Thread.Sleep(5000);
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        
    }
}
