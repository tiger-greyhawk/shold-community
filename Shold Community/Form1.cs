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
using Shold_Community.Entities;
using System.IO;
using Shold_Community.Entities.Resources;
using Shold_Community.Forms;
using System.Media;
using System.Globalization;
using Shold_Community.Workers;
using static Shold_Community.Entities.ArmysToAttack;

//using static Shold_Community.UploadScreenshot;
//using ShareX.HelpersLib;

namespace Shold_Community
{
    delegate int IntOperation(int x, int y);
    delegate GameScreen UpdateLabelDelegate();
    public partial class Form1 : Form
    {
        SynchronizationContext uiContext = SynchronizationContext.Current;
        long lastRequest = Properties.Settings.Default.TimestampGetRes;
        public static bool online = false;
        private bool viewReqRes = false;
        SoundPlayer sp = new SoundPlayer(Properties.Resources.NewResReq);
        PatternService pattern = new PatternService();
        Patterns patterns = new Patterns();
        List<Pattern> patternsJSON = new List<Pattern>();
        List<Player> playerFriend = new List<Player>();
        List<Player> allPlayers = new List<Player>();
        List<Village> villages = new List<Village>();
        public static PatternService addPatternObj = new PatternService();
        string APPDATA = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        //SynchronizationContext uiContext = SynchronizationContext.Current;
        string LanguageOptions;
        public Form1()
        {
            if ((string)Properties.Settings.Default.Language == null)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("En");
                LanguageOptions = "En";
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
                LanguageOptions = Properties.Settings.Default.Language;
            }
            InitializeComponent();
            //Initial.GetAuth();
            //allPlayers = Initial.GetPlayers();
            //GetPatternsBeginWorker(patterns);
            
            //firstForm.TopMost = true;
            pictureBox1.Tag = "http://shold.tk/screenshoter.php";
            commercialPictureBox.Tag = "http://shold.tk/links.php";
            commercialPictureBox.Image = Properties.Resources.logo;
            //commercialPictureBox.Load("http://shold.tk/img/logo.png");
            //SynchronizationContext uiContext = SynchronizationContext.Current;
            //MyThread thread = new MyThread("поток 1",uiContext);
            //thread.Start(uiContext);
            //InitialWorker status1 = new InitialWorker();
            //String status = "";
            //status1.CheckOnlineServerBeginWorker(status);
            //status1.CheckStatusServerAsync();
            //status1.CheckStatusServerCompleted += new CheckStatusServerCompletedEventHandler();
            //Workers.MyEvent evt = new Workers.MyEvent();


            //status1.workerCheckOnlineServer_RunWorkerCompleted();
            //Workers.InitialWorker.CheckOnlineServerBeginWorker(status);

            //if (Initial.GetOnlineServer() == "online") { onlineLabel.Text = "Server is online."; StatusServerPictureBox.Image = Properties.Resources.online; }
            //if (Initial.GetOnlineServer() == "offline") { onlineLabel.Text = "Server is OFF."; StatusServerPictureBox.Image = Properties.Resources.offline; }
            CheckOnlineServerWorker();
            
            typeRequestResComboBox.SelectedIndex = 0;
            
        }


        private void CheckOnlineServerWorker()
        {
            BackgroundWorker checkOnlineServerWorker1 = new BackgroundWorker();
            checkOnlineServerWorker1.DoWork +=
                new System.ComponentModel.DoWorkEventHandler(checkOnlineServerWorker1_DoWork);
            checkOnlineServerWorker1.RunWorkerCompleted +=
                new System.ComponentModel.RunWorkerCompletedEventHandler(checkOnlineServerWorker1_RunWorkerCompleted);
            checkOnlineServerWorker1.RunWorkerAsync();
        }

        private void checkOnlineServerWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Initial.GetOnlineServer();
        }

        private void checkOnlineServerWorker1_RunWorkerCompleted(
        object sender,
        System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            string status = (String)e.Result;
            if (status == "online") { onlineLabel.Text = "Server is online."; StatusServerPictureBox.Image = Properties.Resources.online; }
            if (status == "offline") { onlineLabel.Text = "Server is OFF."; StatusServerPictureBox.Image = Properties.Resources.offline; }
        }

        private void AuthWorker()
        {
            BackgroundWorker authWorker1 = new BackgroundWorker();
            authWorker1.DoWork +=
                new System.ComponentModel.DoWorkEventHandler(authWorker1_DoWork);
            authWorker1.RunWorkerCompleted +=
                new System.ComponentModel.RunWorkerCompletedEventHandler(authWorker1_RunWorkerCompleted);
            authWorker1.RunWorkerAsync();
        }

        private void authWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Initial.Logout();
            e.Result = Initial.GetAuth();
        }

        private void authWorker1_RunWorkerCompleted(
        object sender,
        System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //string status = (String)e.Result;
            //MessageBox.Show(status);
            Player test = new Player();
            test = null;
            test = Initial.GetMe();

            if (test != null) Auth();
            else
            {
                this.Text = "Stronghold Community";
                loginLabel.Text = "Check auth data!";
                statusLoginPictureBox.Image = Properties.Resources.offline;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            AuthWorker();
        }

        private void Auth()
        {
            //Initial.GetAuth();
            GetPatternsBeginWorker(patterns);
            PopulateVillages();
            this.Text = this.Text +" - "+ Initial.GetMe().nick;
            comboBox1.SelectedIndex = 1;
            RequestNewResTimer.Enabled = true;
            AdminMessageTimer.Enabled = true;
            if (online == true) loginLabel.Text = "Online";
            statusLoginPictureBox.Image = Properties.Resources.online;
            checkOnlineServerTimer.Enabled = false;
            Font selfFont = (Font)onlineLabel.Font.Clone();
            //listBox1.Font = new Font(selfFont, FontStyle.Regular);
            onlineLabel.Font = new Font(selfFont, FontStyle.Bold);
        }

        private void PopulateVillages()
        {
            
            villages = Initial.GetVillages();
            foreach (Village item in villages)
            {
                villageComboBox.Items.Add(item.name);
            }
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
            GameScreen gameClient = new GameScreen();
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

            
            GameScreen gameClient = (GameScreen)e.Argument;
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
            GameScreen gameClient = new GameScreen();
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


            GameScreen gameClient = (GameScreen)e.Argument;
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
            //String my2 = "";
            //my = Initial.GetAuth();
            InitRes();
            //my2 = Initial.GetMe();
            //listBox1.Items.Add(my);
            //listBox1.Items.Add(my2);
            
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
            string temp = "";
            DataContractJsonSerializer patternsTemp = new DataContractJsonSerializer(typeof(List<Pattern>));
            DataContractJsonSerializer patternsFriendsTemp = new DataContractJsonSerializer(typeof(List<Pattern>));
            if (checkBox1.Checked)
            {
                List<Pattern> patternsJSONTemp = (List<Pattern>)patternsTemp.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(Initial.GetPatterns())));
                List<Pattern> patternsFriendsJSONTemp = (List<Pattern>)patternsFriendsTemp.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(Initial.GetPatternsFriends())));
                patternsJSONTemp.AddRange(patternsFriendsJSONTemp);
                playerFriend.Clear();
                foreach (Pattern patterns in patternsFriendsJSONTemp)
                {
                    
                    playerFriend.Add(Initial.GetPlayer(patterns.playerId));
                }
                //temp = patternsJSONTemp.ToString();
                MemoryStream stream1 = new MemoryStream();
                patternsFriendsTemp.WriteObject(stream1, patternsJSONTemp);
                stream1.Position = 0;
                StreamReader sr = new StreamReader(stream1);
                temp = sr.ReadToEnd();
            }
            //temp = Initial.GetPatterns() + Initial.GetPatternsFriends();
            else 
            temp = Initial.GetPatterns();
            e.Result = temp;
            //e.Result = Initial.GetPatterns();
            
        }

        private void workerPatterns_RunWorkerCompleted(
        object sender,
        System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //String patterns = (String)e.Result;
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<Pattern>));
            string content = (String)e.Result; 
            patternsJSON = (List<Pattern>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(content)));
            comboBox1_SelectedValueChanged(sender, e);
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
                Pattern patternNew = new Pattern();
                PatternPhoto patternPhotoNew = new PatternPhoto();
                PatternFormation patternFormationNew = new PatternFormation();

                Stream myStream = null;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = APPDATA+"\\Firefly Studios\\Stronghold Kingdoms\\";
                openFileDialog1.Filter = "файлы построений (AttackSetup_*.cas)|AttackSetup_*.cas";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            using (myStream)
                            {

                                // Insert code to read the stream here.
                                byte[] fileBytes = new byte[(int)myStream.Length];
                                myStream.Position = 0;
                                myStream.Read(fileBytes, 0, (int)myStream.Length);

                                //Encoding.UTF8.GetBytes
                                patternFormationNew.file = (Convert.ToBase64String(fileBytes));
                                patternFormationNew.fileName = openFileDialog1.SafeFileName;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }
                else
                {
                    addPattern1.DialogResult = DialogResult.Cancel;
                    return;
                }

                
                patternNew.name = addPattern1.textBox1.Text;
                patternNew.typeCastle = addPattern1.comboBox1.Text;
                patternNew.accessFrom = 1001;
                patternNew.comment = "";



                patternNew.playerId = Initial.GetMe().id;

                string postPattern = Initial.PostPattern(patternNew);
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Pattern));
                patternNew = (Pattern)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(postPattern)));

                patternPhotoNew.patternId = patternNew.id;
                patternPhotoNew.photo = addPattern1.textBox2.Text;
                patternPhotoNew.photoName = "noname";

                string postPatternPhoto = Initial.PostPatternPhoto(patternPhotoNew);
                DataContractJsonSerializer jsonPhoto = new DataContractJsonSerializer(typeof(PatternPhoto));
                patternPhotoNew = (PatternPhoto)jsonPhoto.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(postPatternPhoto)));

                patternFormationNew.patternId = patternNew.id;



                string postPatternFormation = Initial.PostPatternFormation(patternFormationNew);
                DataContractJsonSerializer jsonFormation = new DataContractJsonSerializer(typeof(PatternFormation));
                patternFormationNew = (PatternFormation)jsonFormation.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(postPatternFormation)));

                patternsJSON.Add(patternNew);
                MessageBox.Show("Добавлено");
                patternsJSON.Clear();
                GetPatternsBeginWorker(patterns);
                comboBox1_SelectedValueChanged(sender, e);
                //MessageBox.Show(postPattern);
                //MessageBox.Show(addPattern1.textBox1.Text);
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
            if ((string)(sender as ListBox).SelectedItem != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                listBox2.SelectedIndex = listBox1.SelectedIndex;
                //string id = listBox2.SelectedItem.ToString();
                //string temp = Initial.GetPatternOne(listBox2.SelectedItem.ToString());
                //DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Pattern));
                //string temp = Initial.GetPatternOne(patternsJSON[listBox1.SelectedIndex].id);
                Pattern clear = Initial.GetPatternOne(Convert.ToInt32(listBox2.SelectedItem));
                //Pattern clear = (Pattern)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(temp)));
                richTextBox2.Text = clear.comment;
                DataContractJsonSerializer jsonPhoto = new DataContractJsonSerializer(typeof(PatternPhoto));
                //string temp = Initial.GetPatternOne(patternsJSON[listBox1.SelectedIndex].id);
                string tempPhoto = Initial.GetPatternPhotoOne(clear.id);
                PatternPhoto clearPhoto = (PatternPhoto)jsonPhoto.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(tempPhoto)));

                DataContractJsonSerializer jsonFormation1 = new DataContractJsonSerializer(typeof(PatternFormation));
                //string temp = Initial.GetPatternOne(patternsJSON[listBox1.SelectedIndex].id);
                string tempFormation = Initial.GetPatternFormationOne(clear.id);
                
                PatternFormation clearFormation = (PatternFormation)jsonFormation1.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(tempFormation)));
                string text = clearFormation.fileName.Substring(0, clearFormation.fileName.Length - 4);
                fileName.Text = text.Substring(12);

                //if ((clearPhoto.photo == "") || (clearPhoto.photo == "")) pictureBox1.Load("http://neskuchnij.net/nophoto.jpg");
                try
                {
                    pictureBox1.Load(clearPhoto.photo);
                    pictureBox1.Tag = clearPhoto.photo;
                }
                catch (Exception)
                {

                    pictureBox1.Load("http://neskuchnij.net/nophoto.jpg");
                    //throw;
                } 
                /*
                if (clear.accessFrom == 1001)
                {
                    //pictureBox1.Image = null;
                    pictureBox1.Load("https://img-fotki.yandex.ru/get/60380/385420423.1/0_194e00_16e955e8_L.jpg");
                }
                if (clear.accessFrom == 1002)
                {
                    ///pictureBox1.Image = null;
                    pictureBox1.Load("https://img-fotki.yandex.ru/get/53078/385420423.1/0_194eba_93f3e696_L.jpg");
                }
                if (clear.accessFrom == 1003)
                {
                    //pictureBox1.Image = null;
                    pictureBox1.Load("https://img-fotki.yandex.ru/get/31412/385420423.1/0_194fb2_4f754935_L.jpg");
                }
                */
                //MessageBox.Show(temp);
                //MessageBox.Show("1");
            }
            Cursor.Current = Cursors.Default;
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
                    listBox2.Items.Add(pattern.id);
                    //Font selfFont = (Font)listBox1.Font.Clone();
                    
                    if (pattern.playerId == Initial.GetMe().id)
                    {
                        //.BackColor = Color.White;
                        //listBox1.Font = new Font(selfFont, FontStyle.Regular);
                        listBox1.Items.Add(pattern.name);
                    }
                    else
                    {
                        //string playerName = Initial.GetPlayerName(pattern.playerId);
                        Player player = playerFriend.Find(playerFriend => playerFriend.id == pattern.playerId);
                        //listBox1.Font = new Font(selfFont, FontStyle.Italic);
                        listBox1.Items.Add(pattern.name+"  ("+player.nick+")");
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            patternsJSON.Clear();
            GetPatternsBeginWorker(patterns);
            //comboBox1_SelectedValueChanged(this, e);
        }

        private void PatternToGame_Click(object sender, EventArgs e)
        {
            //DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Pattern));
            //string temp = Initial.GetPatternOne(patternsJSON[listBox1.SelectedIndex].id);
            Pattern clear = Initial.GetPatternOne(Convert.ToInt32(listBox2.SelectedItem));
            //Pattern clear = (Pattern)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(temp)));
            DataContractJsonSerializer jsonFormation = new DataContractJsonSerializer(typeof(PatternFormation));
            //string temp = Initial.GetPatternOne(patternsJSON[listBox1.SelectedIndex].id);
            string tempFormation = Initial.GetPatternFormationOne(clear.id);
            PatternFormation clearFormation = (PatternFormation)jsonFormation.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(tempFormation)));
            byte[] fileFormation = new byte[clearFormation.file.Length];
            fileFormation = Convert.FromBase64String(clearFormation.file);
            
            
            File.WriteAllBytes(APPDATA+"\\Firefly Studios\\Stronghold Kingdoms\\"+clearFormation.fileName, fileFormation);
        }

        private void savePatternButton_Click(object sender, EventArgs e)
        {
            Pattern patternToUpdate = new Pattern();
            patternToUpdate = Initial.GetPatternOne(Convert.ToInt32(listBox2.SelectedItem));
            //patternToUpdate.name = listBox1.SelectedItem.ToString();
            //patternToUpdate.playerId
            patternToUpdate.comment = richTextBox2.Text;
            Initial.PutPattern(patternToUpdate);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            System.Diagnostics.Process.Start((sender as PictureBox).Tag.ToString());
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            Confirm confirm = new Confirm();
            confirm.ShowDialog();
            if (confirm.DialogResult == DialogResult.OK)
            {
                
                Initial.DeletePatternOne(Convert.ToInt32(listBox2.SelectedItem));
                patternsJSON.Clear();
                GetPatternsBeginWorker(patterns);
                comboBox1_SelectedValueChanged(sender, e);
            }
            else confirm.Dispose();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://shold.tk/screenshoter.php");
            
        }


        private void commercialPictureBox_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start((sender as PictureBox).Tag.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пока не реализовано");
        }

        private void richTextBox2_Enter(object sender, EventArgs e)
        {
            if (richTextBox2.Text == "Описание построения")
                richTextBox2.Text = "";
            
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            List<RequestRes> request = new List<RequestRes>();
            request = Initial.GetRequestResources();
            //            MessageBox.Show("");
            
            populateListView(listView1, request);
            /*foreach (RequestRes item in request)
            {
                string nick = Initial.GetPlayerFriends(item.playerId).nick;
                //Player player = playerFriend.Find(playerFriend => playerFriend.id == item.playerId);
                listBox3.Items.Add("В деревню " + item.villageId + " нужно " + item.amount + " " + item.name + ". Максимальная вместимость " + item.max_quantum + " ( "+nick+" )");

                //listView1.DoubleBuffering(true);

                
                

            }*/
        }

        private void populateListView(ListView listView, List<RequestRes> requestRes)
        {
            listView1.Items.Clear();
            
            foreach (RequestRes item in requestRes)
            {
                //string nick = Initial.GetPlayerFriends(item.playerId).nick
                item.translateFromBD(Thread.CurrentThread.CurrentUICulture.ToString());
                allPlayers = Initial.GetPlayers();
                string nick = allPlayers.Find(allPlayers => item.playerId == allPlayers.id).nick; 
                ListViewItem listViewItem = new ListViewItem(item.worldId.ToString());
                listViewItem.Tag = item;
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, Convert.ToString(item.villageId)));
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, (Convert.ToString(item.onWay)+"/"+Convert.ToString(item.amount))));
                //listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, translateFromBD(item).name));
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, item.name));
                
                //listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, typeRequestResComboBox.Items.));
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, nick));
                listView1.Items.Add(listViewItem);
            }
        }

        private RequestRes translateFromBD(RequestRes requestRes)
        {
            switch (requestRes.name) {
                case "дерево":
                    requestRes.name = "wood";
                    break;
                case "сыр":
                    requestRes.name = "cheese";
                    break;
            }
            return requestRes;
        }

        private void createRequestButton_Click(object sender, EventArgs e)
        {
            List<RequestRes> request = new List<RequestRes>();
            
            //allPlayers = Initial.GetPlayers();
            
            RequestRes requestRes = new RequestRes();
            
            requestRes.amount = Convert.ToInt32(amountRequestResTextBox.Text);
            requestRes.max_quantum = Convert.ToInt32(maxAmountTextBox.Text);
            requestRes.name = typeRequestResComboBox.SelectedIndex.ToString()+nameRequestResComboBox.SelectedIndex.ToString();
            requestRes.type = typeRequestResComboBox.Text;
            requestRes.villageId = villages[villageComboBox.SelectedIndex].id;
            requestRes.worldId = 1;
            requestRes.playerId = Convert.ToInt32(Initial.GetMe().id);
            requestRes.timestamp = (new DateTime( ).Ticks);
            Initial.PostRequestRes(requestRes);
            request = Initial.GetRequestResources();
            populateListView(listView1, request);
        }

        private void authToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsAuthenticationForm auth = new OptionsAuthenticationForm();
            auth.loginBox.Text = Properties.Settings.Default.Login;
            auth.passBox.Text = Properties.Settings.Default.Password;
            auth.serverBox.Text = Properties.Settings.Default.Server;
            auth.portBox.Text = Properties.Settings.Default.Port;
            auth.ShowDialog();

            if (auth.DialogResult == DialogResult.OK)
            {
                Properties.Settings.Default.Login = auth.loginBox.Text;
                Properties.Settings.Default.Password = auth.passBox.Text;
                Properties.Settings.Default.Server = auth.serverBox.Text;
                Properties.Settings.Default.Port = auth.portBox.Text;
                Properties.Settings.Default.Save();
                //Auth();
                AuthWorker();
                MessageBox.Show("Saved");
            }
            else
            {
                MessageBox.Show("Canceled");
            }

            //auth.Dispose();
        }

        private void amountStoragesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AmountStoragesForm amount = new AmountStoragesForm();
            amount.ShowDialog();
/*
            if (auth.DialogResult == DialogResult.OK)
            {
                Properties.Settings.Default.Login = auth.loginBox.Text;
                Properties.Settings.Default.Password = auth.passBox.Text;
                Properties.Settings.Default.Server = auth.serverBox.Text;
                Properties.Settings.Default.Port = auth.portBox.Text;
                Properties.Settings.Default.Save();
                Auth();
                MessageBox.Show("Сохранили");
            }
            else
            {
                MessageBox.Show("Отменили");
            }
            */
        }

        private void typeRequestResComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == 0)
            {
                nameRequestResComboBox.Items.Clear();
                object[] items = new object[] { "дерево", "камень", "железо", "смола" };
                switch (Thread.CurrentThread.CurrentUICulture.ToString()) {
                    case "en":
                        items[0] = "wood";
                        items[1] = "stone";
                        items[2] = "iron";
                        items[3] = "pitch";
                        break;
                    case "ru":
                        items[0] = "дерево";
                        items[1] = "камень";
                        items[2] = "железо";
                        items[3] = "смола";
                        break;
                    default:
                        
                        break;
                }
                nameRequestResComboBox.Items.AddRange(items);
                maxAmountTextBox.Text = Convert.ToString(Properties.Settings.Default.GameStorageAmount);
            }
            if ((sender as ComboBox).SelectedIndex == 1)
            {
                nameRequestResComboBox.Items.Clear();
                object[] items = new object[] { "оленина", "стулья", "посуда", "одежда", "вино", "соль", "специи", "шелк" };
                switch (Thread.CurrentThread.CurrentUICulture.ToString())
                {
                    case "en":
                        items[0] = "banq1";
                        items[1] = "banq2";
                        items[2] = "banq3";
                        items[3] = "banq4";
                        items[4] = "banq5";
                        items[5] = "banq6";
                        items[6] = "banq7";
                        items[7] = "banq8";
                        break;
                    case "ru":
                        items[0] = "оленина";
                        items[1] = "стулья";
                        items[2] = "посуда";
                        items[3] = "одежда";
                        items[4] = "вино";
                        items[5] = "соль";
                        items[6] = "специи";
                        items[7] = "шелк";
                        break;
                    default:

                        break;
                }
                nameRequestResComboBox.Items.AddRange(items);
                maxAmountTextBox.Text = Convert.ToString(Properties.Settings.Default.GameBanquetsAmount);
            }
            if ((sender as ComboBox).SelectedIndex == 2)
            {
                nameRequestResComboBox.Items.Clear();
                object[] items = new object[] { "яблоки", "сыр", "мясо", "хлеб", "овощи", "рыба" };
                switch (Thread.CurrentThread.CurrentUICulture.ToString())
                {
                    case "en":
                        items[0] = "apple";
                        items[1] = "cheese";
                        items[2] = "meat";
                        items[3] = "bread";
                        items[4] = "vegetables";
                        items[5] = "fish";
                        break;
                    case "ru":
                        items[0] = "яблоки";
                        items[1] = "сыр";
                        items[2] = "мясо";
                        items[3] = "хлеб";
                        items[4] = "овощи";
                        items[5] = "рыба";
                        break;
                    default:

                        break;
                }
                nameRequestResComboBox.Items.AddRange(items);
                maxAmountTextBox.Text = Convert.ToString(Properties.Settings.Default.GameGranaryAmount);
            }
            if ((sender as ComboBox).SelectedIndex == 3)
            {
                nameRequestResComboBox.Items.Clear();
                object[] items = new object[] { "эль" };
                switch (Thread.CurrentThread.CurrentUICulture.ToString())
                {
                    case "en":
                        items[0] = "ale";
                        break;
                    case "ru":
                        items[0] = "эль";
                        break;
                    default:

                        break;
                }
                nameRequestResComboBox.Items.AddRange(items);
                maxAmountTextBox.Text = Convert.ToString(Properties.Settings.Default.GameTavernAmount);
            }
            if ((sender as ComboBox).SelectedIndex == 4)
            {
                nameRequestResComboBox.Items.Clear();
                object[] items = new object[] { "луки", "бронепики", "бронемечи", "пики", "броня", "мечи", "катапульты" };
                switch (Thread.CurrentThread.CurrentUICulture.ToString())
                {
                    case "en":
                        items[0] = "bows";
                        items[1] = "armor+pickes";
                        items[2] = "armor+swords";
                        items[3] = "pickes";
                        items[4] = "armor";
                        items[5] = "swords";
                        items[6] = "catapults";
                        break;
                    case "ru":
                        items[0] = "луки";
                        items[1] = "бронепики";
                        items[2] = "бронемечи";
                        items[3] = "пики";
                        items[4] = "броня";
                        items[5] = "мечи";
                        items[6] = "катапульты";
                        break;
                    default:

                        break;
                }
                nameRequestResComboBox.Items.AddRange(items);
                maxAmountTextBox.Text = Convert.ToString(Properties.Settings.Default.GameArmoryAmount);
            }
            nameRequestResComboBox.SelectedIndex = 0;
        }

        private void manageVillagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageVillagesForm manageVillages = new ManageVillagesForm();
            manageVillages.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (online == true)
            Initial.GetAuth();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RequestRes requestRes = new RequestRes();
            //requestRes = ((RequestRes)listView1.SelectedItems[0].Tag);
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                requestRes = (RequestRes)item.Tag;
            }

            //MessageBox.Show(requestRes.name);
            sendResourcesAmountTextBox.Text = Convert.ToString(requestRes.amount);
            sendResourcesAmountTextBox.Tag = requestRes;
        }

        private void sendResourcesButton_Click(object sender, EventArgs e)
        {
            List<RequestRes> request = new List<RequestRes>();
            

            SendRes sendRes = new SendRes();
            RequestRes requestOne = (RequestRes)sendResourcesAmountTextBox.Tag;
            sendRes.amount = Convert.ToInt32(sendResourcesAmountTextBox.Text);
            sendRes.resourceId = requestOne.id;
            Initial.PostSendRes(sendRes);
            request = Initial.GetRequestResources();
            populateListView(listView1, request);
        }

        private void RequestNewResTimer_Tick(object sender, EventArgs e)
        {
            List<RequestRes> newRes = new List<RequestRes>();
            //long testing = Properties.Settings.Default.TimestampGetRes;
            //DateTime testTime = new DateTime(testing);
            newRes = Initial.GetNewRequestResources(lastRequest);
            allPlayers = Initial.GetPlayers();

            if (newRes.Count > 0)
            {
                List<RequestRes> request = new List<RequestRes>();
                request = Initial.GetRequestResources();
                populateListView(listView1, request);
                string countNewRes = "";
                string nick = allPlayers.Find(allPlayers => newRes[0].playerId == allPlayers.id).nick;
                if (newRes.Count > 1) countNewRes = "И еще других запросов: " + Convert.ToString(newRes.Count-1)+".";
                DateTime testTime = new DateTime(newRes[0].currentTimestamp);
                Properties.Settings.Default.TimestampGetRes = newRes[0].currentTimestamp;
                Properties.Settings.Default.Save();
                lastRequest = newRes[0].currentTimestamp;
                newRes[0].translateFromBD(Thread.CurrentThread.CurrentUICulture.ToString());
                notifyIcon1.BalloonTipText = Properties.Resources.NewRequest + newRes[0].name+" - "+newRes[0].amount+ " от " + nick +"\r\n"+countNewRes;
                
                sp.Play();
                notifyIcon1.ShowBalloonTip(600000);
                viewReqRes = false;
                //MessageBox.Show("Новый запрос");
            }
        }

        private void notifyIcon1_BalloonTipClosed(object sender, EventArgs e)
        {
            if (viewReqRes == false)
            {
                notifyIcon1.ShowBalloonTip(600000);
            }
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            viewReqRes = true;
            sp.Stop();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            viewReqRes = true;
            sp.Stop();
        }

        private void manageFriendsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageFriendsForm manageFriends = new ManageFriendsForm();
            manageFriends.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Initial.Logout();
        }

        private void englishToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = "En";

            Properties.Settings.Default.Save();
            MessageBox.Show("Restart the program, please.");
        }

        private void russianToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = "Ru";
            Properties.Settings.Default.Save();
            MessageBox.Show("Перезапустите программу, пожалуйста.");
        }

        private void AdminMessageTimer_Tick(object sender, EventArgs e)
        {
            List<AdminMessage> adminMessages = new List<AdminMessage>();
            adminMessages = Initial.GetAdminMessages();
            if (adminMessages.Count > 0)
                MessageBox.Show(adminMessages[0].englishMessage + "\r\n" + adminMessages[0].russianMessage);
        }

        private void checkOnlineServerTimer_Tick(object sender, EventArgs e)
        {
            //if (Initial.GetOnlineServer() == "online") { onlineLabel.Text = "Server is online."; StatusServerPictureBox.Image = Properties.Resources.online; }
            //if (Initial.GetOnlineServer() == "offline") { onlineLabel.Text = "Server is OFF."; StatusServerPictureBox.Image = Properties.Resources.offline; }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            FirstForm firstForm = new FirstForm();
            firstForm.Show();
        }

        private void addPlanAttackButton_Click(object sender, EventArgs e)
        {
            ArmyToAttack armyToAttack = new ArmyToAttack();
            string[] timeString = timeToAttack.Text.Split(':');
            int timeInt = Convert.ToInt32(timeString[0])*3600*24+ Convert.ToInt32(timeString[1])*3600+ Convert.ToInt32(timeString[2])*60+ Convert.ToInt32(timeString[3]);
            armyToAttack.timeTo = Convert.ToInt32(timeInt);
            armyToAttack.card = Convert.ToInt32(multipleCard.Text);
            armyToAttack.name = commentPlanAttack.Text;
            armyToAttack.secret = secretTextBox.Text;
            armyToAttack.currentTimestamp = new DateTime().Ticks;
            //armyToAttack.id = 0;
            //armyToAttack.playerId = 7;
            //armyToAttack.secret = "sdf";
            //Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            //Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).Ticks;
            //int unixTimestamp = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            int now = (int)(DateTime.Now).Ticks;
            int unixTimestamp = (int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
            //armyToAttack.timestamp = DateTime.Now.Ticks/10000;
            armyToAttack.timestamp = unixTimestamp;
            //armyToAttack.currentTimestamp = unixTimestamp;
            ;
            //armyToAttack.type = "type";
            //armyToAttack.villageId = 123;
            Initial.PostPlanAttack(armyToAttack);
            button6_Click(this, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<ArmyToAttack> request = new List<ArmyToAttack>();
            request = Initial.GetPlansAttack(secretTextBox.Text);
            //            MessageBox.Show("");

            populateAttacksListView(listView1, request);
        }

        private void populateAttacksListView(ListView listView, List<ArmyToAttack> armyToAttack)
        {
            listView2.Items.Clear();

            foreach (ArmyToAttack item in armyToAttack)
            {
                //string nick = Initial.GetPlayerFriends(item.playerId).nick
                //item.translateFromBD(Thread.CurrentThread.CurrentUICulture.ToString());
                allPlayers = Initial.GetPlayers();
                string nick = allPlayers.Find(allPlayers => item.playerId == allPlayers.id).nick;
                ListViewItem listViewItem = new ListViewItem(item.timeTo.ToString());
                listViewItem.Tag = item;
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, Convert.ToString(item.card)));
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, (Convert.ToString(item.timeTo))));// + "/" + Convert.ToString(item.amount))));
                //listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, translateFromBD(item).name));
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, nick));

                //listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, typeRequestResComboBox.Items.));
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, item.name));
                listView2.Items.Add(listViewItem);
                //DateTime pDate = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(item.currentTimestamp);
                //DateTime pDate = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddMilliseconds(item.currentTimestamp);
                int pDate = Initial.GetPingDelay();
                label6.Text = pDate.ToString();
                //label6.Text = pDate.ToLongTimeString();
            }
        }
    }
}
