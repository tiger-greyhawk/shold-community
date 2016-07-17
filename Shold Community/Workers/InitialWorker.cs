using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;

namespace Shold_Community.Workers
{
    class InitialWorker
    {
        private string status;

        public string Status { get; }

        public void CheckStatusServer()
        {

        }
        
        public event CheckStatusServerCompletedEventHandler CheckStatusServerCompleted;
        
        public void CheckStatusServerAsync(object sender, DoWorkEventArgs e) {
            BackgroundWorker bw = sender as BackgroundWorker;
            int arg = (int)e.Argument;
            e.Result = Initial.GetOnlineServer();
        }

        

    }

    public delegate void CheckStatusServerCompletedEventHandler(object sender,
        CheckStatusServerCompletedEventArgs e);

    public class CheckStatusServerCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {
        public CheckStatusServerCompletedEventArgs(Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
        {

        }

        public string Result { get; }
    }

}
