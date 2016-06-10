using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
//using static Shold_Community.UploadScreenshot;
//using ShareX.HelpersLib;

namespace Shold_Community
{
    delegate int IntOperation(int x, int y);
    delegate Test UpdateLabelDelegate();
    public partial class Form1 : Form
    {
        SynchronizationContext uiContext = SynchronizationContext.Current;
        PatternService pattern = new PatternService();
        Patterns patterns = new Patterns();
        List<Pattern> patternsJSON = new List<Pattern>();
        public static PatternService addPatternObj = new PatternService();
        public Form1()
        {
            InitializeComponent();
            Initial.GetAuth();
            GetPatternsBeginWorker(patterns);
            //SynchronizationContext uiContext = SynchronizationContext.Current;
            //MyThread thread = new MyThread("поток 1",uiContext);
            //thread.Start(uiContext);
        }

        private void InitRes()
        {
            comboBox1.SelectedIndex = 1;
            pictureBox1.ImageLocation = "http://img-fotki.yandex.ru/get/35827/385420423.1/0_1956f1_4083aaca_orig";
        }

        private void _pause(int value)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < value)
                Application.DoEvents();
        }

        public void UpdateLabel(string message)
        {
            if (InvokeRequired)
            {
                //Invoke(new UpdateLabelDelegate(UpdateLabel), new object[] { message });
                return;
            }
            richTextBox1.Text = message;
        }



        public void button1_Click(object sender, EventArgs e) { }
        public void button2_Click(object sender, EventArgs e) { }
        /*
                public void button1_Click(object sender, EventArgs e)
                {
                    IntPtr iHandle = FindWindow(null, "Stronghold Kingdoms - Мир 4");
                    //IntPtr iHandle = FindWindow(null, "Stronghold Kingdoms - Мир 4");
                    if ((int)iHandle == 0) { MessageBox.Show("Игра не запущена?"); return; }
                    //IntPtr HWND_NOTOPMOST = (iHandle)-2;
                    //const int SWP_FRAMECHANGED = 0x0020;
                    ChangeGameScreenBeforeShot(iHandle);
                    _pause(500);
                    try
                    {
                        pictureBox1.Image = GetScreenshot(iHandle);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message+": не получилось сделать скрин");
                    }
                    finally
                    {
                        ChangeGameScreenAfterShot(iHandle);
                    }
                    saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                    saveFileDialog1.ShowDialog();
                    if (saveFileDialog1.FileName != "")
                    {
                        System.IO.FileStream fs =
                            (System.IO.FileStream)saveFileDialog1.OpenFile();
                        switch (saveFileDialog1.FilterIndex)
                        {
                            case 1:
                                pictureBox1.Image.Save(fs,
                                   System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;

                            case 2:
                                pictureBox1.Image.Save(fs,
                                   System.Drawing.Imaging.ImageFormat.Bmp);
                                break;

                            case 3:
                                pictureBox1.Image.Save(fs,
                                   System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                        }
                        fs.Close();
                    }
                    //if (SetForegroundWindow(iHandle))

                }

                private IntPtr getHandle(String title)
                {
                    IntPtr iHandle = new IntPtr(0);
                    Process[] procs = Process.GetProcesses();

                    foreach (Process p in procs)
                    {
                        Console.WriteLine(p.MainWindowHandle);
                        Console.WriteLine(p.MainWindowTitle);
                        //Console.WriteLine(p);
                        if (p.MainWindowTitle.Contains(title)) { iHandle = p.MainWindowHandle; listBox1.Items.Add(iHandle); }
                    }
                    //iHandle = getHandle("Stronghold Kingdoms - Мир 4");
                    return iHandle;
                }
                private void button2_Click(object sender, EventArgs e)
                {
                    //            IntPtr iHandle = new IntPtr(0);
                    //            if ((FindWindow(null, "Stronghold Kingdoms - Rise of the Wolf - Beta").ToInt32()) > 0) { iHandle = FindWindow(null, "Stronghold Kingdoms - Rise of the Wolf - Beta"); };
                    IntPtr iHandle = getHandle("Stronghold Kingdoms - ");
                    listBox1.Items.Add(iHandle);
                    //else IntPtr iHandle = new IntPtr(Convert.ToInt32("1"));
                    //IntPtr iHandle = FindWindow(null, "Stronghold Kingdoms - Мир 4");
                    ChangeGameScreenBeforeShot(iHandle);
                    _pause(500);
                    //webBrowser1.Navigate("https://oauth.yandex.ru/authorize?response_type=token&client_id=d4dac9ca4fa848ec91816792ce2b886f");
                    //richTextBox1.Text = (UploadScreenshot.GetListFromYandexApi());//UploadTo(GetScreenshot(iHandle)));
                    richTextBox1.Text = (UploadScreenshot.PostImgToYandexApi(GetScreenshot(iHandle)));//UploadTo(GetScreenshot(iHandle)));
                    ChangeGameScreenAfterShot(iHandle);
                }
        */

        //private System.ComponentModel.BackgroundWorker backgroundWorker1 = new System.ComponentModel.BackgroundWorker();

        private void InitializeBackgroundWorker()
        {
            // Attach event handlers to the BackgroundWorker object.
            BackgroundWorker backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork +=
                new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted +=
                new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }


        private void StartThread()
        {
            // This method runs on the main thread.
            //            this.WordsCounted.Text = "0";

            //                InitializeBackgroundWorker();
            // Initialize the object that the background worker calls.

            BackgroundWorker backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork +=
                new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted +=
                new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            Test gameClient = new Test();
                //client.GameClient = this.CompareString.Text;
                //WC.SourceFile = this.SourceFile.Text;

                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync(gameClient);
            
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // This event handler is where the actual work is done.
            // This method runs on the background thread.

            // Get the BackgroundWorker object that raised this event.
//            System.ComponentModel.BackgroundWorker worker;
//            worker = (System.ComponentModel.BackgroundWorker)sender;

            
            Test gameClient = (Test)e.Argument;
            gameClient.iHandle = gameClient.GetHandle("Stronghold Kingdoms - ");
            gameClient.ChangeGameScreenBeforeShot(gameClient.iHandle);
            Thread.Sleep(500);
            gameClient.screen = gameClient.GetScreenshot(gameClient.iHandle);
            gameClient.ChangeGameScreenAfterShot(gameClient.iHandle);
            e.Result = gameClient.sendToCloud(gameClient.screen);
            //Thread.Sleep(1000);
            pictureBox1.Image = gameClient.screen;
            gameClient.myResult = (String)e.Result;
            //            Thread.Sleep(1000);
            //gameClient.GetScreenshot();
            //            worker.Dispose();
            
        }

        private void backgroundWorker1_RunWorkerCompleted(
        object sender,
        System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            // Access the result through the Result property.
            //            GameClient gameClient = (GameClient)e.Arg
            String Area = (String)e.Result;
            //            .myResult = (String)e.Result;
            //my(Area);
            //MessageBox.Show("The area is: " + Area.ToString());
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(ResponseYandexFotki));
            string content = Area;
            //ResponseYandexFotki response = (ResponseYandexFotki)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(content)));
            ResponseYandexFotki response = (ResponseYandexFotki)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(content)));

            //            richTextBox1.Text = response.title;
            Clipboard.SetText(response.img.orig.href);
            
            richTextBox1.Text = response.img.orig.href;
                

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //            IntPtr iHandle = getHandle("Stronghold Kingdoms");
            //            ChangeGameScreenBeforeShot(iHandle);
            //            _pause(500);
            
            //Test mytest = new Test();
            //IntOperation op1 = MyThread.Sum;
            //int result = op1(1, 1);
            //UpdateLabelDelegate update = Test.;



            //richTextBox1.Text = "" + result;
//            Test op2 = new Test();
//            InitializeBackgroundWorker();
            StartThread();
            //Test result = op2();
            //richTextBox1.Text = "" + result;
            //            this.Invoke((MethodInvoker)delegate {
            //                richTextBox1.Text = mytest.my; // runs on UI thread
            //            });
            //            MyThread t1 = new MyThread("Поток 1", uiContext);

            //            ChangeGameScreenAfterShot(iHandle);

        }


        private void InitializeBackgroundWorkerSave()
        {
            // Attach event handlers to the BackgroundWorker object.
            BackgroundWorker backgroundWorkerSave = new BackgroundWorker();
            backgroundWorkerSave.DoWork +=
                new System.ComponentModel.DoWorkEventHandler(backgroundWorkerSave_DoWork);
            backgroundWorkerSave.RunWorkerCompleted +=
                new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorkerSave_RunWorkerCompleted);
        }


        private void StartThreadMakeSave()
        {
            // This method runs on the main thread.
            //            this.WordsCounted.Text = "0";

            //                InitializeBackgroundWorker();
            // Initialize the object that the background worker calls.

            BackgroundWorker backgroundWorkerSave = new BackgroundWorker();
            backgroundWorkerSave.DoWork +=
                new System.ComponentModel.DoWorkEventHandler(backgroundWorkerSave_DoWork);
            backgroundWorkerSave.RunWorkerCompleted +=
                new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorkerSave_RunWorkerCompleted);
            Test gameClient = new Test();
            //client.GameClient = this.CompareString.Text;
            //WC.SourceFile = this.SourceFile.Text;

            // Start the asynchronous operation.
            backgroundWorkerSave.RunWorkerAsync(gameClient);


        }

        private void backgroundWorkerSave_DoWork(object sender, DoWorkEventArgs e)
        {
            // This event handler is where the actual work is done.
            // This method runs on the background thread.

            // Get the BackgroundWorker object that raised this event.
            //            System.ComponentModel.BackgroundWorker worker;
            //            worker = (System.ComponentModel.BackgroundWorker)sender;


            Test gameClient = (Test)e.Argument;
            gameClient.iHandle = gameClient.GetHandle("Stronghold Kingdoms - ");
            gameClient.ChangeGameScreenBeforeShot(gameClient.iHandle);
            Thread.Sleep(500);
            gameClient.screen = gameClient.GetScreenshot(gameClient.iHandle);
            gameClient.ChangeGameScreenAfterShot(gameClient.iHandle);
            //e.Result = gameClient.sendToCloud(gameClient.screen);
            //Thread.Sleep(1000);
            pictureBox1.Image = gameClient.screen;
            //gameClient.myResult = (String)e.Result;
            //            Thread.Sleep(1000);
            //gameClient.GetScreenshot();
            //            worker.Dispose();

        }

        private void backgroundWorkerSave_RunWorkerCompleted(
        object sender,
        System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            // Access the result through the Result property.
            //            GameClient gameClient = (GameClient)e.Arg
            //String Area = (String)e.Result;
            //            .myResult = (String)e.Result;
            //my(Area);
            //MessageBox.Show("The area is: " + Area.ToString());
            //DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(ResponseYandexFotki));
            //string content = Area;
            //ResponseYandexFotki response = (ResponseYandexFotki)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(content)));
            //ResponseYandexFotki response = (ResponseYandexFotki)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(content)));

            //            richTextBox1.Text = response.title;
            //Clipboard.SetText(response.img.orig.href);

            //richTextBox1.Text = response.img.orig.href;
            saveFileDialog1.Filter = "Png Image|*.png|Bitmap Image|*.bmp|Gif Image|*.gif|JPeg Image|*.jpg";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        pictureBox1.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Png);
                        break;

                    case 2:
                        pictureBox1.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        pictureBox1.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case 4:
                        pictureBox1.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                }
                fs.Close();
            }


        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            StartThreadMakeSave();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //String my = "";
            String my2 = "";
            //my = Initial.GetAuth();
            InitRes();
            my2 = Initial.GetMe();
            //listBox1.Items.Add(my);
            listBox1.Items.Add(my2);
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //numericUpDown1.Value = listBox1.SelectedIndex;

            

        }

        private void numericUpDown1_MouseUp(object sender, MouseEventArgs e)
        {
            object Item = listBox1.SelectedItem;
            int ItemIndex = listBox1.SelectedIndex;
            object Item2 = listBox2.SelectedItem;
            int ItemIndex2 = listBox2.SelectedIndex;
            //numericUpDown1.Value = ItemIndex;
            numericUpDown1.Value = -numericUpDown1.Value;
            listBox1.Items.Remove(Item);
            listBox1.Items.Insert(ItemIndex + (int)numericUpDown1.Value, Item);
            //listBox2.Items.Remove(Item2);
            //listBox2.Items.Insert(ItemIndex2 + (int)numericUpDown1.Value, Item2);

            listBox1.Focus();
            listBox1.SetSelected(ItemIndex + (int)numericUpDown1.Value, true);
            //listBox2.SetSelected(ItemIndex2 + (int)numericUpDown1.Value, true);
            numericUpDown1.Value = 0;
        }


        private void GetPatternsBeginWorker(Patterns patterns)
        {
            BackgroundWorker workerPatterns = new BackgroundWorker();
            workerPatterns.DoWork += new System.ComponentModel.DoWorkEventHandler(workerPatterns_DoWork);
            workerPatterns.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(workerPatterns_RunWorkerCompleted);
            //PatternsHandler patterns = new PatternsHandler();
            workerPatterns.RunWorkerAsync(patterns);
        }

        private void workerPatterns_DoWork(object sender, DoWorkEventArgs e)
        {
            //PatternsHandler patterns = (PatternsHandler)e.Argument;
            //e.Result = patterns.getFromServer();
            e.Result = Initial.GetPatterns();
            
        }

        private void workerPatterns_RunWorkerCompleted(
        object sender,
        System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //String patterns = (String)e.Result;
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<Pattern>));
            string content = (String)e.Result; 
            patternsJSON = (List<Pattern>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(content)));
/*            listBox1.Items.Clear();
            listBox2.Items.Clear();
            foreach (var item in patternsJSON)
            {
                //PatternsHandler pattern = new PatternsHandler();
                pattern.id = item.id;
                pattern.name = item.name;
                pattern.playerId = item.playerId;
                pattern.typeCastle = item.typeCastle;
                //richTextBox2.Text += item.name+"\r\n";
                if (pattern.typeCastle == comboBox1.Text)
                {
                    listBox1.Items.Add(pattern.name);
                    listBox2.Items.Add(pattern.id);
                }
            }
*/
            //richTextBox1.Text += response.Property.name;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form2 addPattern1 = new Form2();
//            Form2 addPattern2 = new Form2();
//            Form2 addPattern3 = new Form2();
//            Form2 addPattern4 = new Form2();
            addPatternObj = null;
            addPattern1.ShowDialog();
            if (addPattern1.DialogResult == DialogResult.OK)
            {
                Pattern myNew = new Pattern();
                myNew.name = addPattern1.textBox1.Text;
                myNew.typeCastle = addPattern1.comboBox1.Text;
                patternsJSON.Add(myNew);
                MessageBox.Show(addPattern1.textBox1.Text);
                //MessageBox.Show(addPatternObj.ToString());
            }
            else if (addPattern1.DialogResult == DialogResult.Cancel)
            {
                //MessageBox.Show("cancel");
            }
            else addPattern1.Dispose();


        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 0)
            {
                listBox2.SelectedIndex = listBox1.SelectedIndex;
                //string id = listBox2.SelectedItem.ToString();
                //string temp = Initial.GetPatternOne(listBox2.SelectedItem.ToString());
                string temp = Initial.GetPatternOne(patternsJSON[listBox1.SelectedIndex].id);
                //MessageBox.Show(temp);
                //MessageBox.Show("1");
            }
        }

        

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            foreach (var item in patternsJSON)
            {
                //PatternsHandler pattern = new PatternsHandler();
                pattern.id = item.id;
                pattern.name = item.name;
                pattern.playerId = item.playerId;
                pattern.typeCastle = item.typeCastle;
                //richTextBox2.Text += item.name+"\r\n";
                if (pattern.typeCastle == comboBox1.Text)
                {
                    listBox1.Items.Add(pattern.name);
                    listBox2.Items.Add(pattern.id);
                }
            }
        }
    }
}
