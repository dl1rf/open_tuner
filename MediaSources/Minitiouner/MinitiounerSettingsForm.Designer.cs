namespace opentuner.MediaSources.Minitiouner
{
    partial class MinitiounerSettingsForm
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
            this.groupHardwareInterface = new System.Windows.Forms.GroupBox();
            this.txtIpAddress = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboHardwareInterface = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ComboDefaultRFInput = new System.Windows.Forms.ComboBox();
            this.comboSupplyBDefault = new System.Windows.Forms.ComboBox();
            this.comboSupplyADefault = new System.Windows.Forms.ComboBox();
            this.txtTuner2FreqOffset = new System.Windows.Forms.TextBox();
            this.txtTuner1FreqOffset = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtTuner1Frequency = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTuner1SymbolRate = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTuner2Frequency = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTuner2SymbolRate = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupHardwareInterface.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupHardwareInterface
            // 
            this.groupHardwareInterface.Controls.Add(this.txtIpAddress);
            this.groupHardwareInterface.Controls.Add(this.label2);
            this.groupHardwareInterface.Controls.Add(this.comboHardwareInterface);
            this.groupHardwareInterface.Controls.Add(this.label1);
            this.groupHardwareInterface.Location = new System.Drawing.Point(16, 15);
            this.groupHardwareInterface.Margin = new System.Windows.Forms.Padding(4);
            this.groupHardwareInterface.Name = "groupHardwareInterface";
            this.groupHardwareInterface.Padding = new System.Windows.Forms.Padding(4);
            this.groupHardwareInterface.Size = new System.Drawing.Size(437, 101);
            this.groupHardwareInterface.TabIndex = 0;
            this.groupHardwareInterface.TabStop = false;
            this.groupHardwareInterface.Text = "Hardware Interface";
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Enabled = false;
            this.txtIpAddress.Location = new System.Drawing.Point(179, 62);
            this.txtIpAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(212, 22);
            this.txtIpAddress.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(21, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "IP Address: ";
            // 
            // comboHardwareInterface
            // 
            this.comboHardwareInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboHardwareInterface.FormattingEnabled = true;
            this.comboHardwareInterface.Items.AddRange(new object[] {
            "Always Ask",
            "FTDI Module",
            "PicoTuner"});
            this.comboHardwareInterface.Location = new System.Drawing.Point(179, 30);
            this.comboHardwareInterface.Margin = new System.Windows.Forms.Padding(4);
            this.comboHardwareInterface.Name = "comboHardwareInterface";
            this.comboHardwareInterface.Size = new System.Drawing.Size(212, 24);
            this.comboHardwareInterface.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Default Interface: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtTuner2SymbolRate);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtTuner2Frequency);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtTuner1SymbolRate);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtTuner1Frequency);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.ComboDefaultRFInput);
            this.groupBox1.Controls.Add(this.comboSupplyBDefault);
            this.groupBox1.Controls.Add(this.comboSupplyADefault);
            this.groupBox1.Controls.Add(this.txtTuner2FreqOffset);
            this.groupBox1.Controls.Add(this.txtTuner1FreqOffset);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(16, 124);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(437, 338);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tuner Properties";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 300);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 16);
            this.label7.TabIndex = 9;
            this.label7.Text = "Default RF Input:";
            // 
            // ComboDefaultRFInput
            // 
            this.ComboDefaultRFInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboDefaultRFInput.FormattingEnabled = true;
            this.ComboDefaultRFInput.Items.AddRange(new object[] {
            "Tuner 1 = A, Tuner 2 = A",
            "Tuner 1 = A, Tuner 2 = B",
            "Tuner 1 = B, Tuner 2 = A",
            "Tuner 1 = B, Tuner 2 = B"});
            this.ComboDefaultRFInput.Location = new System.Drawing.Point(179, 297);
            this.ComboDefaultRFInput.Margin = new System.Windows.Forms.Padding(4);
            this.ComboDefaultRFInput.Name = "ComboDefaultRFInput";
            this.ComboDefaultRFInput.Size = new System.Drawing.Size(212, 24);
            this.ComboDefaultRFInput.TabIndex = 8;
            // 
            // comboSupplyBDefault
            // 
            this.comboSupplyBDefault.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSupplyBDefault.FormattingEnabled = true;
            this.comboSupplyBDefault.Items.AddRange(new object[] {
            "Off",
            "13V Vertical",
            "18V Horizontal"});
            this.comboSupplyBDefault.Location = new System.Drawing.Point(179, 256);
            this.comboSupplyBDefault.Margin = new System.Windows.Forms.Padding(4);
            this.comboSupplyBDefault.Name = "comboSupplyBDefault";
            this.comboSupplyBDefault.Size = new System.Drawing.Size(212, 24);
            this.comboSupplyBDefault.TabIndex = 7;
            // 
            // comboSupplyADefault
            // 
            this.comboSupplyADefault.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSupplyADefault.FormattingEnabled = true;
            this.comboSupplyADefault.Items.AddRange(new object[] {
            "Off",
            "13V Vertical",
            "18V Horizontal"});
            this.comboSupplyADefault.Location = new System.Drawing.Point(179, 224);
            this.comboSupplyADefault.Margin = new System.Windows.Forms.Padding(4);
            this.comboSupplyADefault.Name = "comboSupplyADefault";
            this.comboSupplyADefault.Size = new System.Drawing.Size(212, 24);
            this.comboSupplyADefault.TabIndex = 6;
            // 
            // txtTuner2FreqOffset
            // 
            this.txtTuner2FreqOffset.Location = new System.Drawing.Point(179, 152);
            this.txtTuner2FreqOffset.Margin = new System.Windows.Forms.Padding(4);
            this.txtTuner2FreqOffset.Name = "txtTuner2FreqOffset";
            this.txtTuner2FreqOffset.Size = new System.Drawing.Size(212, 22);
            this.txtTuner2FreqOffset.TabIndex = 5;
            // 
            // txtTuner1FreqOffset
            // 
            this.txtTuner1FreqOffset.Location = new System.Drawing.Point(179, 53);
            this.txtTuner1FreqOffset.Margin = new System.Windows.Forms.Padding(4);
            this.txtTuner1FreqOffset.Name = "txtTuner1FreqOffset";
            this.txtTuner1FreqOffset.Size = new System.Drawing.Size(212, 22);
            this.txtTuner1FreqOffset.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 259);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "LNB B Supply Default:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 227);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "LNB A Supply Default:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 155);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Tuner 2 Freq Offset:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 56);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tuner 1 Freq Offset:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(353, 470);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(245, 470);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtTuner1Frequency
            // 
            this.txtTuner1Frequency.Location = new System.Drawing.Point(179, 23);
            this.txtTuner1Frequency.Margin = new System.Windows.Forms.Padding(4);
            this.txtTuner1Frequency.Name = "txtTuner1Frequency";
            this.txtTuner1Frequency.Size = new System.Drawing.Size(212, 22);
            this.txtTuner1Frequency.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 26);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 16);
            this.label8.TabIndex = 11;
            this.label8.Text = "Tuner 1 Freqeuency:";
            // 
            // txtTuner1SymbolRate
            // 
            this.txtTuner1SymbolRate.Location = new System.Drawing.Point(179, 83);
            this.txtTuner1SymbolRate.Margin = new System.Windows.Forms.Padding(4);
            this.txtTuner1SymbolRate.Name = "txtTuner1SymbolRate";
            this.txtTuner1SymbolRate.Size = new System.Drawing.Size(212, 22);
            this.txtTuner1SymbolRate.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 86);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(136, 16);
            this.label9.TabIndex = 13;
            this.label9.Text = "Tuner 1 Symbol Rate:";
            // 
            // txtTuner2Frequency
            // 
            this.txtTuner2Frequency.Location = new System.Drawing.Point(179, 122);
            this.txtTuner2Frequency.Margin = new System.Windows.Forms.Padding(4);
            this.txtTuner2Frequency.Name = "txtTuner2Frequency";
            this.txtTuner2Frequency.Size = new System.Drawing.Size(212, 22);
            this.txtTuner2Frequency.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 125);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 16);
            this.label10.TabIndex = 15;
            this.label10.Text = "Tuner 2 Frequency:";
            // 
            // txtTuner2SymbolRate
            // 
            this.txtTuner2SymbolRate.Location = new System.Drawing.Point(179, 182);
            this.txtTuner2SymbolRate.Margin = new System.Windows.Forms.Padding(4);
            this.txtTuner2SymbolRate.Name = "txtTuner2SymbolRate";
            this.txtTuner2SymbolRate.Size = new System.Drawing.Size(212, 22);
            this.txtTuner2SymbolRate.TabIndex = 16;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 185);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(136, 16);
            this.label11.TabIndex = 17;
            this.label11.Text = "Tuner 2 Symbol Rate:";
            // 
            // MinitiounerSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(473, 506);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupHardwareInterface);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MinitiounerSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Minitiouner Settings";
            this.groupHardwareInterface.ResumeLayout(false);
            this.groupHardwareInterface.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupHardwareInterface;
        private System.Windows.Forms.MaskedTextBox txtIpAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboHardwareInterface;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox ComboDefaultRFInput;
        private System.Windows.Forms.ComboBox comboSupplyBDefault;
        private System.Windows.Forms.ComboBox comboSupplyADefault;
        private System.Windows.Forms.TextBox txtTuner2FreqOffset;
        private System.Windows.Forms.TextBox txtTuner1FreqOffset;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTuner1Frequency;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTuner2SymbolRate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTuner2Frequency;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTuner1SymbolRate;
    }
}