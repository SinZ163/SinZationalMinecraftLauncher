namespace SinZational_Minecraft_Launcher {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.userText = new System.Windows.Forms.TextBox();
            this.passText = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.userLabel = new System.Windows.Forms.Label();
            this.passLabel = new System.Windows.Forms.Label();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.consoleBox = new System.Windows.Forms.CheckBox();
            this.updateBox = new System.Windows.Forms.CheckBox();
            this.rememberBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // userText
            // 
            this.userText.Location = new System.Drawing.Point(549, 508);
            this.userText.Name = "userText";
            this.userText.Size = new System.Drawing.Size(100, 20);
            this.userText.TabIndex = 0;
            // 
            // passText
            // 
            this.passText.Location = new System.Drawing.Point(549, 534);
            this.passText.Name = "passText";
            this.passText.Size = new System.Drawing.Size(100, 20);
            this.passText.TabIndex = 1;
            this.passText.UseSystemPasswordChar = true;
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(666, 508);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(111, 46);
            this.loginButton.TabIndex = 2;
            this.loginButton.Text = "Launch!";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(485, 508);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(58, 13);
            this.userLabel.TabIndex = 3;
            this.userLabel.Text = "Username:";
            this.userLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // passLabel
            // 
            this.passLabel.AutoSize = true;
            this.passLabel.Location = new System.Drawing.Point(487, 538);
            this.passLabel.Name = "passLabel";
            this.passLabel.Size = new System.Drawing.Size(56, 13);
            this.passLabel.TabIndex = 4;
            this.passLabel.Text = "Password:";
            this.passLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(12, 12);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(760, 490);
            this.webBrowser.TabIndex = 5;
            this.webBrowser.Url = new System.Uri("http://sinzationalminecraft.mca.d3s.co", System.UriKind.Absolute);
            // 
            // consoleBox
            // 
            this.consoleBox.AutoSize = true;
            this.consoleBox.Location = new System.Drawing.Point(12, 508);
            this.consoleBox.Name = "consoleBox";
            this.consoleBox.Size = new System.Drawing.Size(70, 17);
            this.consoleBox.TabIndex = 6;
            this.consoleBox.Text = "Console?";
            this.consoleBox.UseVisualStyleBackColor = true;
            // 
            // updateBox
            // 
            this.updateBox.AutoSize = true;
            this.updateBox.Location = new System.Drawing.Point(12, 531);
            this.updateBox.Name = "updateBox";
            this.updateBox.Size = new System.Drawing.Size(121, 17);
            this.updateBox.TabIndex = 7;
            this.updateBox.Text = "Check for Updates?";
            this.updateBox.UseVisualStyleBackColor = true;
            // 
            // rememberBox
            // 
            this.rememberBox.AutoSize = true;
            this.rememberBox.Location = new System.Drawing.Point(140, 531);
            this.rememberBox.Name = "rememberBox";
            this.rememberBox.Size = new System.Drawing.Size(101, 17);
            this.rememberBox.TabIndex = 8;
            this.rememberBox.Text = "Remember Me?";
            this.rememberBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 560);
            this.Controls.Add(this.rememberBox);
            this.Controls.Add(this.updateBox);
            this.Controls.Add(this.consoleBox);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.passLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.passText);
            this.Controls.Add(this.userText);
            this.Name = "MainForm";
            this.Text = "SinZational Minecraft Launcher";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userText;
        private System.Windows.Forms.TextBox passText;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label passLabel;
        public System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.CheckBox consoleBox;
        private System.Windows.Forms.CheckBox updateBox;
        private System.Windows.Forms.CheckBox rememberBox;
    }
}

