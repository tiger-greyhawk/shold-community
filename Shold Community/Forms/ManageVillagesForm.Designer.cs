namespace Shold_Community.Forms
{
    partial class ManageVillagesForm
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.idInWorld = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addVillageButton = new System.Windows.Forms.Button();
            this.deleteVillageButton = new System.Windows.Forms.Button();
            this.nameVillageTextBox = new System.Windows.Forms.TextBox();
            this.idVillageTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idInWorld,
            this.name});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(532, 356);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // idInWorld
            // 
            this.idInWorld.Text = "ID в игре";
            this.idInWorld.Width = 70;
            // 
            // name
            // 
            this.name.Text = "Название";
            this.name.Width = 174;
            // 
            // addVillageButton
            // 
            this.addVillageButton.Location = new System.Drawing.Point(236, 362);
            this.addVillageButton.Name = "addVillageButton";
            this.addVillageButton.Size = new System.Drawing.Size(113, 23);
            this.addVillageButton.TabIndex = 1;
            this.addVillageButton.Text = "Добавить деревню";
            this.addVillageButton.UseVisualStyleBackColor = true;
            this.addVillageButton.Click += new System.EventHandler(this.addVillageButton_Click);
            // 
            // deleteVillageButton
            // 
            this.deleteVillageButton.Location = new System.Drawing.Point(355, 362);
            this.deleteVillageButton.Name = "deleteVillageButton";
            this.deleteVillageButton.Size = new System.Drawing.Size(177, 23);
            this.deleteVillageButton.TabIndex = 2;
            this.deleteVillageButton.Text = "Удалить выбранную деревню";
            this.deleteVillageButton.UseVisualStyleBackColor = true;
            this.deleteVillageButton.Click += new System.EventHandler(this.deleteVillageButton_Click);
            // 
            // nameVillageTextBox
            // 
            this.nameVillageTextBox.Location = new System.Drawing.Point(84, 364);
            this.nameVillageTextBox.Name = "nameVillageTextBox";
            this.nameVillageTextBox.Size = new System.Drawing.Size(146, 20);
            this.nameVillageTextBox.TabIndex = 3;
            this.nameVillageTextBox.Text = "Название";
            // 
            // idVillageTextBox
            // 
            this.idVillageTextBox.Location = new System.Drawing.Point(12, 364);
            this.idVillageTextBox.Name = "idVillageTextBox";
            this.idVillageTextBox.Size = new System.Drawing.Size(66, 20);
            this.idVillageTextBox.TabIndex = 4;
            this.idVillageTextBox.Text = "925678";
            // 
            // ManageVillagesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 491);
            this.Controls.Add(this.idVillageTextBox);
            this.Controls.Add(this.nameVillageTextBox);
            this.Controls.Add(this.deleteVillageButton);
            this.Controls.Add(this.addVillageButton);
            this.Controls.Add(this.listView1);
            this.Name = "ManageVillagesForm";
            this.Text = "ManageVillagesForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader idInWorld;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.Button addVillageButton;
        private System.Windows.Forms.Button deleteVillageButton;
        private System.Windows.Forms.TextBox nameVillageTextBox;
        private System.Windows.Forms.TextBox idVillageTextBox;
    }
}