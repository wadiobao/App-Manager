using System.Windows.Forms;
using System;

namespace BTLNET
{
    partial class CaiDat
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
            this.radioButtonLight = new System.Windows.Forms.RadioButton();
            this.radioButtonDark = new System.Windows.Forms.RadioButton();
            this.radioButtonVietnamese = new System.Windows.Forms.RadioButton();
            this.radioButtonEnglish = new System.Windows.Forms.RadioButton();
            this.btnApply = new System.Windows.Forms.Button();
            this.groupBoxTheme = new System.Windows.Forms.GroupBox();
            this.groupBoxLanguage = new System.Windows.Forms.GroupBox();
            this.groupBoxPassword = new System.Windows.Forms.GroupBox();
            this.lblCurrentPassword = new System.Windows.Forms.Label();
            this.txtCurrentPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.groupBoxTheme.SuspendLayout();
            this.groupBoxLanguage.SuspendLayout();
            this.groupBoxPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonLight
            // 
            this.radioButtonLight.AutoSize = true;
            this.radioButtonLight.Location = new System.Drawing.Point(30, 30);
            this.radioButtonLight.Name = "radioButtonLight";
            this.radioButtonLight.Size = new System.Drawing.Size(60, 20);
            this.radioButtonLight.TabIndex = 1;
            this.radioButtonLight.Text = "Sáng";
            // 
            // radioButtonDark
            // 
            this.radioButtonDark.AutoSize = true;
            this.radioButtonDark.Location = new System.Drawing.Point(200, 30);
            this.radioButtonDark.Name = "radioButtonDark";
            this.radioButtonDark.Size = new System.Drawing.Size(48, 20);
            this.radioButtonDark.TabIndex = 2;
            this.radioButtonDark.Text = "Tối";
            // 
            // radioButtonVietnamese
            // 
            this.radioButtonVietnamese.AutoSize = true;
            this.radioButtonVietnamese.Location = new System.Drawing.Point(30, 30);
            this.radioButtonVietnamese.Name = "radioButtonVietnamese";
            this.radioButtonVietnamese.Size = new System.Drawing.Size(89, 20);
            this.radioButtonVietnamese.TabIndex = 4;
            this.radioButtonVietnamese.Text = "Tiếng Việt";
            // 
            // radioButtonEnglish
            // 
            this.radioButtonEnglish.AutoSize = true;
            this.radioButtonEnglish.Location = new System.Drawing.Point(200, 30);
            this.radioButtonEnglish.Name = "radioButtonEnglish";
            this.radioButtonEnglish.Size = new System.Drawing.Size(72, 20);
            this.radioButtonEnglish.TabIndex = 5;
            this.radioButtonEnglish.Text = "English";
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnApply.FlatAppearance.BorderSize = 0;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.ForeColor = System.Drawing.Color.White;
            this.btnApply.Location = new System.Drawing.Point(150, 360);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(100, 30);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "Áp dụng";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // groupBoxTheme
            // 
            this.groupBoxTheme.Controls.Add(this.radioButtonLight);
            this.groupBoxTheme.Controls.Add(this.radioButtonDark);
            this.groupBoxTheme.Location = new System.Drawing.Point(20, 20);
            this.groupBoxTheme.Name = "groupBoxTheme";
            this.groupBoxTheme.Size = new System.Drawing.Size(360, 80);
            this.groupBoxTheme.TabIndex = 0;
            this.groupBoxTheme.TabStop = false;
            this.groupBoxTheme.Text = "Giao diện";
            // 
            // groupBoxLanguage
            // 
            this.groupBoxLanguage.Controls.Add(this.radioButtonVietnamese);
            this.groupBoxLanguage.Controls.Add(this.radioButtonEnglish);
            this.groupBoxLanguage.Location = new System.Drawing.Point(20, 120);
            this.groupBoxLanguage.Name = "groupBoxLanguage";
            this.groupBoxLanguage.Size = new System.Drawing.Size(360, 80);
            this.groupBoxLanguage.TabIndex = 3;
            this.groupBoxLanguage.TabStop = false;
            this.groupBoxLanguage.Text = "Ngôn ngữ";
            // 
            // groupBoxPassword
            // 
            this.groupBoxPassword.Controls.Add(this.lblCurrentPassword);
            this.groupBoxPassword.Controls.Add(this.txtCurrentPassword);
            this.groupBoxPassword.Controls.Add(this.lblNewPassword);
            this.groupBoxPassword.Controls.Add(this.txtNewPassword);
            this.groupBoxPassword.Controls.Add(this.btnChangePassword);
            this.groupBoxPassword.Location = new System.Drawing.Point(20, 220);
            this.groupBoxPassword.Name = "groupBoxPassword";
            this.groupBoxPassword.Size = new System.Drawing.Size(360, 134);
            this.groupBoxPassword.TabIndex = 6;
            this.groupBoxPassword.TabStop = false;
            this.groupBoxPassword.Text = "Đổi mật khẩu";
            // 
            // lblCurrentPassword
            // 
            this.lblCurrentPassword.AutoSize = true;
            this.lblCurrentPassword.Location = new System.Drawing.Point(30, 30);
            this.lblCurrentPassword.Name = "lblCurrentPassword";
            this.lblCurrentPassword.Size = new System.Drawing.Size(109, 16);
            this.lblCurrentPassword.TabIndex = 7;
            this.lblCurrentPassword.Text = "Mật khẩu hiện tại:";
            // 
            // txtCurrentPassword
            // 
            this.txtCurrentPassword.Location = new System.Drawing.Point(150, 27);
            this.txtCurrentPassword.Name = "txtCurrentPassword";
            this.txtCurrentPassword.Size = new System.Drawing.Size(180, 22);
            this.txtCurrentPassword.TabIndex = 8;
            this.txtCurrentPassword.UseSystemPasswordChar = true;
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Location = new System.Drawing.Point(30, 60);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(89, 16);
            this.lblNewPassword.TabIndex = 9;
            this.lblNewPassword.Text = "Mật khẩu mới:";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(150, 57);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(180, 22);
            this.txtNewPassword.TabIndex = 10;
            this.txtNewPassword.UseSystemPasswordChar = true;
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Location = new System.Drawing.Point(150, 85);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(180, 43);
            this.btnChangePassword.TabIndex = 11;
            this.btnChangePassword.Text = "Đổi mật khẩu";
            this.btnChangePassword.Click += new System.EventHandler(this.BtnChangePassword_Click);
            // 
            // CaiDat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(400, 420);
            this.Controls.Add(this.groupBoxTheme);
            this.Controls.Add(this.groupBoxLanguage);
            this.Controls.Add(this.groupBoxPassword);
            this.Controls.Add(this.btnApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CaiDat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cài đặt";
            this.groupBoxTheme.ResumeLayout(false);
            this.groupBoxTheme.PerformLayout();
            this.groupBoxLanguage.ResumeLayout(false);
            this.groupBoxLanguage.PerformLayout();
            this.groupBoxPassword.ResumeLayout(false);
            this.groupBoxPassword.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonLight;
        private System.Windows.Forms.RadioButton radioButtonDark;
        private System.Windows.Forms.RadioButton radioButtonVietnamese;
        private System.Windows.Forms.RadioButton radioButtonEnglish;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.GroupBox groupBoxTheme;
        private System.Windows.Forms.GroupBox groupBoxLanguage;
        private System.Windows.Forms.Label lblCurrentPassword;
        private System.Windows.Forms.TextBox txtCurrentPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.GroupBox groupBoxPassword;
    }
}