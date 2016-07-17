using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shold_Community.Forms
{
    public partial class AmountStoragesForm : Form
    {
        public AmountStoragesForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.GameStorageAmount = Convert.ToInt32(storageBox.Text);
            Properties.Settings.Default.GameBanquetsAmount = Convert.ToInt32(banquetsBox.Text);
            Properties.Settings.Default.GameGranaryAmount = Convert.ToInt32(granaryBox.Text);
            Properties.Settings.Default.GameTavernAmount = Convert.ToInt32(tavernBox.Text);
            Properties.Settings.Default.GameArmoryAmount = Convert.ToInt32(armoryBox.Text);
            Properties.Settings.Default.Save();
            this.Close();
            this.Dispose();

        }

        private void AmountStoragesForm_Load(object sender, EventArgs e)
        {
            storageBox.Text = Convert.ToString(Properties.Settings.Default.GameStorageAmount);
            banquetsBox.Text = Convert.ToString(Properties.Settings.Default.GameBanquetsAmount);
            granaryBox.Text = Convert.ToString(Properties.Settings.Default.GameGranaryAmount);
            tavernBox.Text = Convert.ToString(Properties.Settings.Default.GameTavernAmount);
            armoryBox.Text = Convert.ToString(Properties.Settings.Default.GameArmoryAmount);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
