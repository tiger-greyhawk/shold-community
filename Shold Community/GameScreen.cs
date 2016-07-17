using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;

namespace Shold_Community
{
    class GameScreen
    {
        
//        public class GameClient
//        {
            public IntPtr iHandle;
            public Bitmap screen;
            public String myResult;
            public String sendToCloud(Bitmap bmp)
            {
                return UploadScreenshot.PostImgToYandexApi((Bitmap)bmp);
            }
//        }

        private void GetScreenBigArea()
        {
          //  InitializeBackgroundWorker();

//            GameClient gameClient = new GameClient();
            iHandle = GetHandle("Stronghold Kingdoms - ");
            ChangeGameScreenBeforeShot(iHandle);
            Thread.Sleep(1000);
            //            Bitmap bmp = GetScreenshot(iHandle);

            screen = GetScreenshot(iHandle);
            ChangeGameScreenAfterShot(iHandle);
         //   BackgroundWorker1.RunWorkerAsync(gameClient);
        }
/*
        private System.ComponentModel.BackgroundWorker BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();

        private void InitializeBackgroundWorker()
        {
            // Attach event handlers to the BackgroundWorker object.
            BackgroundWorker1.DoWork +=
                new System.ComponentModel.DoWorkEventHandler(BackgroundWorker1_DoWork);
            BackgroundWorker1.RunWorkerCompleted +=
                new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker1_RunWorkerCompleted);
        }

        private void BackgroundWorker1_DoWork(
            object sender,
            System.ComponentModel.DoWorkEventArgs e)
        {
//            GameClient gameClient = (GameClient)e.Argument;
            // Return the value through the Result property.
            //Thread.Sleep(10000);
            //MessageBox.Show("Делаем скриншот и грузим на Яндекс. Ждите.");
//            e.Result = gameClient.sendToCloud(gameClient.screen);
//            gameClient.myResult = (String)e.Result;
//            MessageBox.Show("The area is: " + gameClient.myResult);
        }

        private void BackgroundWorker1_RunWorkerCompleted(
            object sender,
            System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            // Access the result through the Result property.
            //            GameClient gameClient = (GameClient)e.Arg
            String Area = (String)e.Result;
//            .myResult = (String)e.Result;
            //my(Area);
            MessageBox.Show("The area is: " + Area.ToString());

        }
*/

        public GameScreen()
        {
            
//            GetGameClientArea();
        }


/*
        private void BackgroundWorker1_RunWorkerCompleted(
            object sender,
            System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            // Access the result through the Result property.
//            GameClient gameClient = (GameClient)e.Arg
            String Area = (String)e.Result;
            
            //my(Area);
            MessageBox.Show("The area is: " + Area.ToString());
            
        }
*/

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImportAttribute("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowPlacement(IntPtr hWnd, [In, Out]  ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPlacement(IntPtr hWnd, [In, Out]  ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        public static extern Boolean PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, long dwRop);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(
          string lpClassName, // class name 
          string lpWindowName // window name 
        );

        [StructLayout(LayoutKind.Sequential)]

        public struct RECT
        {
            public int Left { set; get; }
            public int Top { set; get; }
            public int Right { set; get; }
            public int Bottom { set; get; }

            /*            public static implicit operator RECT(int v)
                        {
                            throw new NotImplementedException();
                        }
            */
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public uint length { set; get; }
            public uint flags { set; get; }
            public uint showCmd { set; get; }
            public Point ptMinPosition { set; get; }
            public Point ptMaxPosition { set; get; }
            public RECT rcNormalPosition { set; get; }
        }

        public static class HWND

        {

            public static readonly IntPtr

            NOTOPMOST = new IntPtr(-2),
            BROADCAST = new IntPtr(0xffff),
            TOPMOST = new IntPtr(-1),
            TOP = new IntPtr(0),
            BOTTOM = new IntPtr(1);

        }

        public static class SWP
        {

            public static readonly uint

            NOSIZE = 0x0001,
            NOMOVE = 0x0002,
            NOZORDER = 0x0004,
            NOREDRAW = 0x0008,
            NOACTIVATE = 0x0010,
            DRAWFRAME = 0x0020,
            FRAMECHANGED = 0x0020,
            SHOWWINDOW = 0x0040,
            HIDEWINDOW = 0x0080,
            NOCOPYBITS = 0x0100,
            NOOWNERZORDER = 0x0200,
            NOREPOSITION = 0x0200,
            NOSENDCHANGING = 0x0400,
            DEFERERASE = 0x2000,
            ASYNCWINDOWPOS = 0x4000;
        }

        WINDOWPLACEMENT place = new WINDOWPLACEMENT();

        public void ChangeGameScreenBeforeShot(IntPtr handle)
        {
            WINDOWPLACEMENT placeTemp = new WINDOWPLACEMENT();
            //place.length = sizeof(WINDOWPLACEMENT);
            //placeTemp.length = (uint)sizeof(WINDOWPLACEMENT);
            if (!handle.Equals(IntPtr.Zero))
            {
                //ShowWindow(handle, SW_SHOWNORMAL);
                GetWindowPlacement(handle, ref place);
                placeTemp = place;
                placeTemp.showCmd = 4;
                //placeTemp.rcNormalPosition.Left; 

                SetWindowPlacement(handle, ref placeTemp);
                //_pause(1000);
                //bool success = SetWindowPos(handle, HWND.NOTOPMOST, placeTemp.rcNormalPosition.Left, placeTemp.rcNormalPosition.Top, placeTemp.rcNormalPosition.Left+2200, placeTemp.rcNormalPosition.Top+1500, SWP.NOZORDER & SWP.FRAMECHANGED);
                bool success = SetWindowPos(handle, HWND.NOTOPMOST, 0, 0, 2200, 1500, SWP.NOZORDER & SWP.FRAMECHANGED);
                //GetWindowPlacement(handle, ref placeTemp);
                //placeTemp.showCmd = place.showCmd;
                //SetWindowPlacement(handle, ref placeTemp);
                if (!success)
                {
                    MessageBox.Show("Ошибка изменения размера игры: " + Convert.ToString
                        (Marshal.GetLastWin32Error()));
                }

            }
        }

        public void ChangeGameScreenAfterShot(IntPtr handle)
        {
            if (!handle.Equals(IntPtr.Zero))
            {
                SetWindowPlacement(handle, ref place);
            }
        }
        
        public Bitmap GetScreenshot(IntPtr handle)
        {
            RECT srcRect;
            if (GetClientRect(handle, out srcRect))
            {
                int width = srcRect.Right - srcRect.Left;
                int height = srcRect.Bottom - srcRect.Top;
                Bitmap bmp = new Bitmap(width, height);
                Graphics gfxBmp = Graphics.FromImage(bmp);
                IntPtr hdcBitmap = gfxBmp.GetHdc();
                Thread.Sleep(100);
                PrintWindow(handle, hdcBitmap, 0x1);
                Bitmap bmp2 = new Bitmap(width, height-150);
                Graphics gfxBmp2 = Graphics.FromImage(bmp2);
                IntPtr hdcBitmap2 = gfxBmp2.GetHdc();
//                Thread.Sleep(100);
                BitBlt(hdcBitmap2, 0, -150, width, height, hdcBitmap, 0, 0, 0X00CC0020);//13369376);

                Bitmap bmp3 = new Bitmap(900, 100);
                Graphics gfxBmp3 = Graphics.FromImage(bmp3);
                IntPtr hdcBitmap3 = gfxBmp3.GetHdc();
//                Thread.Sleep(100);
                BitBlt(hdcBitmap2, 0, 0, 1000, 1000, hdcBitmap3, 0, 0, 0X00CC0020);//0X00CC0020);
                bmp.Dispose();
                bmp3.Dispose();
                gfxBmp.ReleaseHdc(hdcBitmap);
                gfxBmp.Dispose();
                gfxBmp2.ReleaseHdc(hdcBitmap2);
                gfxBmp2.Dispose();
                gfxBmp3.ReleaseHdc(hdcBitmap3);
                gfxBmp3.Dispose();
                return bmp2;
            }
            else return null;
        }

        public IntPtr GetHandle(String title)
        {
            IntPtr iHandle = new IntPtr(0);
            Process[] procs = Process.GetProcesses();

            foreach (Process p in procs)
            {
                Console.WriteLine(p.MainWindowHandle);
                Console.WriteLine(p.MainWindowTitle);
                //Console.WriteLine(p);
                if ((p.MainWindowTitle.Contains(title)) &(p.ProcessName == "StrongholdKingdoms")) { iHandle = p.MainWindowHandle; }//listBox1.Items.Add(iHandle); }
            }
            return iHandle;
        }
    }
}
