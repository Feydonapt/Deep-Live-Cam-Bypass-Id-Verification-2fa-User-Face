using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
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
using Point = System.Drawing.Point;

namespace RemoteLiveC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {/// <summary>/// 根据RGB，计算灰度值/// </summary>/// <param name="posClr">Color值</param>/// <returns>灰度值，整型</returns>
        private int GetGrayNumColor(System.Drawing.Color posClr){   
            int i = (int)(0.299 * posClr.R + 0.587 * posClr.G + 0.114 * posClr.B);  
            int i2=(posClr.R * 19595 + posClr.G * 38469 + posClr.B * 7472) >> 16;  
            return i2;
        }

        Rect GetMinEill(Point [] ps)
        {

        }

        static private byte[] recvBuf = new byte[10];
        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        public static extern bool GetCursorPos(ref Point lpPoint);
        static TcpClient tcl = new TcpClient();
        static NetworkStream sl = null;
        static bool connected = false;
        private static ImageCodecInfo GetEncoder(string format)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; j++)
            {
                if (encoders[j].MimeType == format)
                    return encoders[j];
            }

            return null;
        }
        static private byte[] CompressionImage(Bitmap bm, long quality)
        {

            ImageCodecInfo CodecInfo = GetEncoder("image/jpeg");
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
            myEncoderParameters.Param[0] = myEncoderParameter;
            using (MemoryStream ms = new MemoryStream())
            {
                bm.Save(ms, CodecInfo, myEncoderParameters);
                myEncoderParameters.Dispose();
                myEncoderParameter.Dispose();
                return ms.ToArray();
            }


        }
        public MainWindow()
        {
            InitializeComponent();
           
            Loaded += (e, v) => 
            {
                var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                int camera_index = 1;
                foreach (FilterInfo device in videoDevices)
                {
                    Console.WriteLine(device.Name + " " + camera_index.ToString());
                    camera_index++;
                }

                var selected = 2;//;Convert.ToInt32(Console.ReadLine(), 10);

                

                var buffer = new WriteableBitmap(1280, 720, 96, 96, PixelFormats.Pbgra32, null);
                var plant = new Bitmap(1280, 720, buffer.BackBufferStride, System.Drawing.Imaging.PixelFormat.Format32bppPArgb, buffer.BackBuffer);

                var pen = Graphics.FromImage(plant);
                OPT.Source = buffer;
                VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[selected].MonikerString);
                //    videoSource.VideoResolution = videoSource.VideoCapabilities[0];
                videoSource.VideoResolution = videoSource.VideoCapabilities[1];
                MessageBox.Show(videoSource.VideoCapabilities[1].FrameRate.ToString());
                videoSource.NewFrame += (F, C) =>
                {
                    this.Dispatcher.Invoke(()=> {
                        buffer.Lock();
                        pen.Clear(System.Drawing.Color.White);

                        var Cbn = C.Frame.Clone(new System.Drawing.Rectangle(180,100,800,580),System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                        var lc = new Bitmap(400,290);
                        Graphics bv = Graphics.FromImage(lc);
                        bv.DrawImage(Cbn, new System.Drawing.Rectangle(0,0,400,290), new System.Drawing.Rectangle(0, 0, 800, 580), GraphicsUnit.Pixel);

                        int[,] pos = new int[400, 290];
                        for(int i=0;i<400;i++)
                            for(int j = 0; j < 290; j++)
                            {
                                var tc = lc.GetPixel(i, j);
                                if(GetGrayNumColor(tc) < 65)
                                {
                                    lc.SetPixel(i,j, System.Drawing.Color.Red);
                                }
                            }

                        pen.DrawImage(lc, new Point(0,0));
                        buffer.AddDirtyRect(new Int32Rect(0, 0, 1280, 720));
                        buffer.Unlock();

                        bv.Dispose();
                        lc.Dispose();
                        Cbn.Dispose();
                    });     
                    


                    /*
                    if (!connected) return;
                    int buf_size = (int)(tcl.SendBufferSize / 3d);

                    System.Drawing.Point p2 = new System.Drawing.Point();
                    GetCursorPos(ref p2);

                    Graphics gw = Graphics.FromImage(C.Frame);
                    gw.DrawEllipse(new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.Black)), new System.Drawing.Rectangle(p2.X - 6, p2.Y - 6, 12, 12));
                    gw.Dispose();

                    Bitmap n_buffer = new Bitmap(1600, 900);
                    Graphics g = Graphics.FromImage(n_buffer);
                    g.DrawImage(C.Frame, 0, 0, 1600, 900);
                    var vceData = CompressionImage(n_buffer, 42);

                    g.Dispose();
                    n_buffer.Dispose();

                    for (int i = 0; i < vceData.Length; i += buf_size)
                    {
                        var lbuf = vceData.Skip(i).Take(buf_size).ToArray();
                        try
                        {
                            sl.Write(lbuf, 0, lbuf.Length);
                            int sc = sl.Read(recvBuf, 0, recvBuf.Length);
                            if (sc == 0) { connected = false; return; }
                        }
                        catch (Exception)
                        {
                            connected = false; return;
                        }
                    }
                    try
                    {
                        sl.Write(new byte[4] { 104, 105, 56, 33 }, 0, 4);
                        int size = sl.Read(recvBuf, 0, recvBuf.Length);
                    }
                    catch (Exception)
                    {
                        connected = false; return;
                    }
                    */
                };

                videoSource.Start();

                Closed += (e, v) =>
                {
                    videoSource.SignalToStop();
                };
            };
        }
    }
}
