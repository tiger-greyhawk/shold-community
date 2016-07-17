namespace Shold_Community.Forms
{
    partial class AmountStoragesForm
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.storageBox = new System.Windows.Forms.TextBox();
            this.banquetsBox = new System.Windows.Forms.TextBox();
            this.granaryBox = new System.Windows.Forms.TextBox();
            this.tavernBox = new System.Windows.Forms.TextBox();
            this.armoryBox = new System.Windows.Forms.TextBox();
            this.storageLabel = new System.Windows.Forms.Label();
            this.banquetsStorLabel = new System.Windows.Forms.Label();
            this.granaryLabel = new System.Windows.Forms.Label();
            this.tavernLabel = new System.Windows.Forms.Label();
            this.armoryLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 151);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(51, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(107, 151);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // storageBox
            // 
            this.storageBox.Location = new System.Drawing.Point(82, 6);
            this.storageBox.Name = "storageBox";
            this.storageBox.Size = new System.Drawing.Size(100, 20);
            this.storageBox.TabIndex = 2;
            // 
            // banquetsBox
            // 
            this.banquetsBox.Location = new System.Drawing.Point(82, 31);
            this.banquetsBox.Name = "banquetsBox";
            this.banquetsBox.Size = new System.Drawing.Size(100, 20);
            this.banquetsBox.TabIndex = 3;
            // 
            // granaryBox
            // 
            this.granaryBox.Location = new System.Drawing.Point(82, 57);
            this.granaryBox.Name = "granaryBox";
            this.granaryBox.Size = new System.Drawing.Size(100, 20);
            this.granaryBox.TabIndex = 4;
            // 
            // tavernBox
            // 
            this.tavernBox.Location = new System.Drawing.Point(82, 83);
            this.tavernBox.Name = "tavernBox";
            this.tavernBox.Size = new System.Drawing.Size(100, 20);
            this.tavernBox.TabIndex = 5;
            // 
            // armoryBox
            // 
            this.armoryBox.Location = new System.Drawing.Point(82, 109);
            this.armoryBox.Name = "armoryBox";
            this.armoryBox.Size = new System.Drawing.Size(100, 20);
            this.armoryBox.TabIndex = 6;
            // 
            // storageLabel
            // 
            this.storageLabel.AutoSize = true;
            this.storageLabel.Location = new System.Drawing.Point(12, 9);
            this.storageLabel.Name = "storageLabel";
            this.storageLabel.Size = new System.Drawing.Size(38, 13);
            this.storageLabel.TabIndex = 7;
            this.storageLabel.Text = "Склад";
            // 
            // banquetsStorLabel
            // 
            this.banquetsStorLabel.AutoSize = true;
            this.banquetsStorLabel.Location = new System.Drawing.Point(12, 34);
            this.banquetsStorLabel.Name = "banquetsStorLabel";
            this.banquetsStorLabel.Size = new System.Drawing.Size(51, 13);
            this.banquetsStorLabel.TabIndex = 8;
            this.banquetsStorLabel.Text = "Банкеты";
            // 
            // granaryLabel
            // 
            this.granaryLabel.AutoSize = true;
            this.granaryLabel.Location = new System.Drawing.Point(12, 60);
            this.granaryLabel.Name = "granaryLabel";
            this.granaryLabel.Size = new System.Drawing.Size(26, 13);
            this.granaryLabel.TabIndex = 9;
            this.granaryLabel.Text = "Еда";
            // 
            // tavernLabel
            // 
            this.tavernLabel.AutoSize = true;
            this.tavernLabel.Location = new System.Drawing.Point(12, 86);
            this.tavernLabel.Name = "tavernLabel";
            this.tavernLabel.Size = new System.Drawing.Size(26, 13);
            this.tavernLabel.TabIndex = 10;
            this.tavernLabel.Text = "Эль";
            // 
            // armoryLabel
            // 
            this.armoryLabel.AutoSize = true;
            this.armoryLabel.Location = new System.Drawing.Point(12, 112);
            this.armoryLabel.Name = "armoryLabel";
            this.armoryLabel.Size = new System.Drawing.Size(64, 13);
            this.armoryLabel.TabIndex = 11;
            this.armoryLabel.Text = "Оружейная";
            // 
            // AmountStoragesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(201, 180);
            this.Controls.Add(this.armoryLabel);
            this.Controls.Add(this.tavernLabel);
            this.Controls.Add(this.granaryLabel);
            this.Controls.Add(this.banquetsStorLabel);
            this.Controls.Add(this.storageLabel);
            this.Controls.Add(this.armoryBox);
            this.Controls.Add(this.tavernBox);
            this.Controls.Add(this.granaryBox);
            this.Controls.Add(this.banquetsBox);
            this.Controls.Add(this.storageBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Name = "AmountStoragesForm";
            this.Text = "AmountStoragesForm";
            this.Load += new System.EventHandler(this.AmountStoragesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label storageLabel;
        private System.Windows.Forms.Label banquetsStorLabel;
        private System.Windows.Forms.Label granaryLabel;
        private System.Windows.Forms.Label tavernLabel;
        private System.Windows.Forms.Label armoryLabel;
        public System.Windows.Forms.TextBox storageBox;
        public System.Windows.Forms.TextBox banquetsBox;
        public System.Windows.Forms.TextBox granaryBox;
        public System.Windows.Forms.TextBox tavernBox;
        public System.Windows.Forms.TextBox armoryBox;
    }
}