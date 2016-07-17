namespace Shold_Community.Forms
{
    partial class ManageFriendsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageFriendsForm));
            this.listView1 = new System.Windows.Forms.ListView();
            this.playerNick = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.playerFriendBool = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addToFriendsButton = new System.Windows.Forms.Button();
            this.deleteFromFriendsButton = new System.Windows.Forms.Button();
            this.inviteButton = new System.Windows.Forms.Button();
            this.nickPlayerTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.inviteCountLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.playerNick,
            this.playerFriendBool});
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.Name = "listView1";
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // playerNick
            // 
            resources.ApplyResources(this.playerNick, "playerNick");
            // 
            // playerFriendBool
            // 
            resources.ApplyResources(this.playerFriendBool, "playerFriendBool");
            // 
            // addToFriendsButton
            // 
            resources.ApplyResources(this.addToFriendsButton, "addToFriendsButton");
            this.addToFriendsButton.Name = "addToFriendsButton";
            this.addToFriendsButton.UseVisualStyleBackColor = true;
            this.addToFriendsButton.Click += new System.EventHandler(this.addToFriendsButton_Click);
            // 
            // deleteFromFriendsButton
            // 
            resources.ApplyResources(this.deleteFromFriendsButton, "deleteFromFriendsButton");
            this.deleteFromFriendsButton.Name = "deleteFromFriendsButton";
            this.deleteFromFriendsButton.UseVisualStyleBackColor = true;
            this.deleteFromFriendsButton.Click += new System.EventHandler(this.deleteFromFriendsButton_Click);
            // 
            // inviteButton
            // 
            resources.ApplyResources(this.inviteButton, "inviteButton");
            this.inviteButton.Name = "inviteButton";
            this.inviteButton.UseVisualStyleBackColor = true;
            this.inviteButton.Click += new System.EventHandler(this.inviteButton_Click);
            // 
            // nickPlayerTextBox
            // 
            resources.ApplyResources(this.nickPlayerTextBox, "nickPlayerTextBox");
            this.nickPlayerTextBox.Name = "nickPlayerTextBox";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // inviteCountLabel
            // 
            resources.ApplyResources(this.inviteCountLabel, "inviteCountLabel");
            this.inviteCountLabel.ForeColor = System.Drawing.Color.Red;
            this.inviteCountLabel.Name = "inviteCountLabel";
            // 
            // ManageFriendsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.inviteCountLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nickPlayerTextBox);
            this.Controls.Add(this.inviteButton);
            this.Controls.Add(this.deleteFromFriendsButton);
            this.Controls.Add(this.addToFriendsButton);
            this.Controls.Add(this.listView1);
            this.Name = "ManageFriendsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader playerNick;
        private System.Windows.Forms.ColumnHeader playerFriendBool;
        private System.Windows.Forms.Button addToFriendsButton;
        private System.Windows.Forms.Button deleteFromFriendsButton;
        private System.Windows.Forms.Button inviteButton;
        private System.Windows.Forms.TextBox nickPlayerTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label inviteCountLabel;
    }
}