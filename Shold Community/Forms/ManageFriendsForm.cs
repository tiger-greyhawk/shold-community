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
    public partial class ManageFriendsForm : Form
    {
        List<Friend> friends = new List<Friend>();
        Player player = new Player();
        public ManageFriendsForm()
        {
            InitializeComponent();
            populateListView(listView1, Initial.GetPlayers());
            String text = Convert.ToString(Initial.GetMe().invite);
            inviteCountLabel.Text = Convert.ToString(Initial.GetMe().invite);
        }


        private void populateListView(ListView listView, List<Player> players)
        {
            listView1.Items.Clear();

            friends = Initial.GetPlayerFriends(Initial.GetMe().id);
            foreach (Player item in players)
            {
                
                ListViewItem listViewItem = new ListViewItem(item.nick);
                listViewItem.Tag = item;
                if (friends.Exists(friends => item.id == friends.friendId))
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(listViewItem, "да"));

                listView1.Items.Add(listViewItem);
            }
        }

        private void addToFriendsButton_Click(object sender, EventArgs e)
        {
            Initial.PostFriends(player);
            populateListView(listView1, Initial.GetPlayers());
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //requestRes = ((RequestRes)listView1.SelectedItems[0].Tag);
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                player = (Player)item.Tag;
            }
        }

        private void deleteFromFriendsButton_Click(object sender, EventArgs e)
        {
            Initial.DeleteFromFriends(player);
            populateListView(listView1, Initial.GetPlayers());
        }

        private void inviteButton_Click(object sender, EventArgs e)
        {
            Player newPlayer = new Player();
            newPlayer.nick = nickPlayerTextBox.Text;
            Initial.PostInvite(newPlayer);
            populateListView(listView1, Initial.GetPlayers());
            inviteCountLabel.Text = Convert.ToString(Initial.GetMe().invite);
            MessageBox.Show("password for new player: 'test'");
        }
    }
}

