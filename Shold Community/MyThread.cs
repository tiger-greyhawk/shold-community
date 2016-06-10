using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Shold_Community
{
    class MyThread
    {
        Thread thread;

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
                PrintWindow(handle, hdcBitmap, 0x1);
                gfxBmp.ReleaseHdc(hdcBitmap);
                gfxBmp.Dispose();
                return bmp;
            }
            else return null;
        }

        private void ChangeGameScreenBeforeShot(IntPtr handle)
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

        private void ChangeGameScreenAfterShot(IntPtr handle)
        {
            if (!handle.Equals(IntPtr.Zero))
            {
                SetWindowPlacement(handle, ref place);
            }
        }

        public MyThread(String name, SynchronizationContext uiContext)
        {
            //this.thread = new Thread(this.func);
            this.thread = new Thread(this.func);
            this.thread.Name = name;
            this.thread.Start(uiContext);
        }


        void func(object state)
        {
            /*            for (int i = 0; i < (int)num; i++)
                        {
                            Console.WriteLine(Thread.CurrentThread.Name + " выводит " + i);
                            Thread.Sleep(1000);
                        }
            */
            SynchronizationContext uiContext = state as SynchronizationContext;
            IntPtr iHandle = getHandle("Stronghold Kingdoms");
            ChangeGameScreenBeforeShot(iHandle);
            Thread.Sleep(1000);
            Bitmap bmp = GetScreenshot(iHandle);
            ChangeGameScreenAfterShot(iHandle);

//            UploadScreenshot.PostImgToYandexApi((Bitmap)bmp);
            Console.WriteLine(Thread.CurrentThread.Name + " завершился ");

            //Form1 f = new Form1();

//            uiContext.Post(UpdateUI, "Hello world!");

        }

        public static int Sum(int x, int y)
        {
            //Thread.Sleep(10000);
            return x + y;
        }

/*        private void UpdateUI(object state)
        {
            
            Form1.richTextBox1.Text = ((string)state);
        }
*/
        public IntPtr getHandle(String title)
        {
            IntPtr iHandle = new IntPtr(0);
            Process[] procs = Process.GetProcesses();

            foreach (Process p in procs)
            {
                Console.WriteLine(p.MainWindowHandle);
                Console.WriteLine(p.MainWindowTitle);
                //Console.WriteLine(p);
                if (p.MainWindowTitle.Contains(title)) { iHandle = p.MainWindowHandle; }//listBox1.Items.Add(iHandle); }
            }
            return iHandle;
        }


    }
}
