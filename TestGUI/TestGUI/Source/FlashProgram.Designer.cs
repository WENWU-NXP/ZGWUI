namespace ZGWUI
{
    partial class FlashProgram
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
            this.checkBoxFLASHPROGRAM5169 = new System.Windows.Forms.CheckBox();
            this.checkBoxFLASHPROGRAM5189 = new System.Windows.Forms.CheckBox();
            this.checkBoxFLASHPROGRAMERASE = new System.Windows.Forms.CheckBox();
            this.buttonFLASHPROGRAMMEROK = new System.Windows.Forms.Button();
            this.buttonFLASHPROGRAMMERCANCEL = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBoxFLASHPROGRAM5169
            // 
            this.checkBoxFLASHPROGRAM5169.AutoSize = true;
            this.checkBoxFLASHPROGRAM5169.Location = new System.Drawing.Point(49, 50);
            this.checkBoxFLASHPROGRAM5169.Name = "checkBoxFLASHPROGRAM5169";
            this.checkBoxFLASHPROGRAM5169.Size = new System.Drawing.Size(63, 17);
            this.checkBoxFLASHPROGRAM5169.TabIndex = 0;
            this.checkBoxFLASHPROGRAM5169.Text = "JN5169";
            this.checkBoxFLASHPROGRAM5169.UseVisualStyleBackColor = true;
            this.checkBoxFLASHPROGRAM5169.CheckedChanged += new System.EventHandler(this.checkBoxFLASHPROGRAM5169_CheckedChanged);
            // 
            // checkBoxFLASHPROGRAM5189
            // 
            this.checkBoxFLASHPROGRAM5189.AutoSize = true;
            this.checkBoxFLASHPROGRAM5189.Location = new System.Drawing.Point(49, 73);
            this.checkBoxFLASHPROGRAM5189.Name = "checkBoxFLASHPROGRAM5189";
            this.checkBoxFLASHPROGRAM5189.Size = new System.Drawing.Size(63, 17);
            this.checkBoxFLASHPROGRAM5189.TabIndex = 1;
            this.checkBoxFLASHPROGRAM5189.Text = "JN5189";
            this.checkBoxFLASHPROGRAM5189.UseVisualStyleBackColor = true;
            this.checkBoxFLASHPROGRAM5189.CheckedChanged += new System.EventHandler(this.checkBoxFLASHPROGRAM5189_CheckedChanged);
            // 
            // checkBoxFLASHPROGRAMERASE
            // 
            this.checkBoxFLASHPROGRAMERASE.AutoSize = true;
            this.checkBoxFLASHPROGRAMERASE.Location = new System.Drawing.Point(49, 96);
            this.checkBoxFLASHPROGRAMERASE.Name = "checkBoxFLASHPROGRAMERASE";
            this.checkBoxFLASHPROGRAMERASE.Size = new System.Drawing.Size(91, 17);
            this.checkBoxFLASHPROGRAMERASE.TabIndex = 2;
            this.checkBoxFLASHPROGRAMERASE.Text = "Erase eeprom";
            this.checkBoxFLASHPROGRAMERASE.UseVisualStyleBackColor = true;
            this.checkBoxFLASHPROGRAMERASE.CheckedChanged += new System.EventHandler(this.checkBoxFLASHPROGRAMERASE_CheckedChanged);
            // 
            // buttonFLASHPROGRAMMEROK
            // 
            this.buttonFLASHPROGRAMMEROK.Location = new System.Drawing.Point(23, 161);
            this.buttonFLASHPROGRAMMEROK.Name = "buttonFLASHPROGRAMMEROK";
            this.buttonFLASHPROGRAMMEROK.Size = new System.Drawing.Size(75, 23);
            this.buttonFLASHPROGRAMMEROK.TabIndex = 3;
            this.buttonFLASHPROGRAMMEROK.Text = "Ok";
            this.buttonFLASHPROGRAMMEROK.UseVisualStyleBackColor = true;
            this.buttonFLASHPROGRAMMEROK.Click += new System.EventHandler(this.buttonFLASHPROGRAMMEROK_Click);
            // 
            // buttonFLASHPROGRAMMERCANCEL
            // 
            this.buttonFLASHPROGRAMMERCANCEL.Location = new System.Drawing.Point(132, 161);
            this.buttonFLASHPROGRAMMERCANCEL.Name = "buttonFLASHPROGRAMMERCANCEL";
            this.buttonFLASHPROGRAMMERCANCEL.Size = new System.Drawing.Size(75, 23);
            this.buttonFLASHPROGRAMMERCANCEL.TabIndex = 4;
            this.buttonFLASHPROGRAMMERCANCEL.Text = "Cancel";
            this.buttonFLASHPROGRAMMERCANCEL.UseVisualStyleBackColor = true;
            this.buttonFLASHPROGRAMMERCANCEL.Click += new System.EventHandler(this.buttonFLASHPROGRAMMERCANCEL_Click);
            // 
            // FlashProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 224);
            this.Controls.Add(this.buttonFLASHPROGRAMMERCANCEL);
            this.Controls.Add(this.buttonFLASHPROGRAMMEROK);
            this.Controls.Add(this.checkBoxFLASHPROGRAMERASE);
            this.Controls.Add(this.checkBoxFLASHPROGRAM5189);
            this.Controls.Add(this.checkBoxFLASHPROGRAM5169);
            this.Name = "FlashProgram";
            this.Text = "FlashProgram";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxFLASHPROGRAM5169;
        private System.Windows.Forms.CheckBox checkBoxFLASHPROGRAM5189;
        private System.Windows.Forms.CheckBox checkBoxFLASHPROGRAMERASE;
        private System.Windows.Forms.Button buttonFLASHPROGRAMMEROK;
        private System.Windows.Forms.Button buttonFLASHPROGRAMMERCANCEL;
    }
}