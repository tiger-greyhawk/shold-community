using Shold_Community.Entities;
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
    public partial class ManageVillagesForm : Form
    {
        public ManageVillagesForm()
        {
            Random rnd = new Random();
            InitializeComponent();
            idVillageTextBox.Text = Convert.ToString(rnd.Next(999999));
            //Initial.GetVillages();
            populateListView(listView1, Initial.GetVillages());

        }

        private void addVillageButton_Click(object sender, EventArgs e)
        {
            Village village = new Village();
            village.idInWorld = Convert.ToInt32(idVillageTextBox.Text);
            village.name = nameVillageTextBox.Text;
            Initial.PostVillage(village);
            populateListView(listView1, Initial.GetVillages());
        }

        private void populateListView(ListView listView, List<Village> villages)
        {
            listView1.Items.Clear();

            foreach (Village item in villages)
            {
                //string nick = Initial.GetPlayerFriends(item.playerId).nick;
                //string nick = allPlayers.Find(allPlayers => item.playerId == allPlayers.id).nick;
                ListViewItem listViewItem = new ListViewItem(item.idInWorld.ToString());
                listViewItem.Tag = item;
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, item.name));
                
                listView1.Items.Add(listViewItem);
            }
        }

        private void deleteVillageButton_Click(object sender, EventArgs e)
        {
            Village toDelete = new Village();
            toDelete = (Village)listView1.SelectedItems[0].Tag;
            Initial.DeleteVillageOne(toDelete.id);
            listView1.Items.Remove(listView1.SelectedItems[0]);
                
        }
    }
}
