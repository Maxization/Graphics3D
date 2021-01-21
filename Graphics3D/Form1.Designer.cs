namespace Graphics3D
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.trackBarPosZ = new System.Windows.Forms.TrackBar();
            this.trackBarPosY = new System.Windows.Forms.TrackBar();
            this.trackBarPosX = new System.Windows.Forms.TrackBar();
            this.shadingGroupBox = new System.Windows.Forms.GroupBox();
            this.radioButtonPhong = new System.Windows.Forms.RadioButton();
            this.radioButtonGouraud = new System.Windows.Forms.RadioButton();
            this.radioButtonFlat = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPosZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPosX)).BeginInit();
            this.shadingGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.4054F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.59459F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1184, 761);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(946, 755);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.trackBarPosZ, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.trackBarPosY, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.trackBarPosX, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.shadingGroupBox, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(955, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 330F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(226, 755);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // trackBarPosZ
            // 
            this.trackBarPosZ.Location = new System.Drawing.Point(3, 428);
            this.trackBarPosZ.Maximum = 50;
            this.trackBarPosZ.Minimum = -50;
            this.trackBarPosZ.Name = "trackBarPosZ";
            this.trackBarPosZ.Size = new System.Drawing.Size(218, 45);
            this.trackBarPosZ.TabIndex = 8;
            this.trackBarPosZ.Scroll += new System.EventHandler(this.trackBarPosZ_Scroll);
            // 
            // trackBarPosY
            // 
            this.trackBarPosY.Location = new System.Drawing.Point(3, 335);
            this.trackBarPosY.Maximum = 50;
            this.trackBarPosY.Minimum = -50;
            this.trackBarPosY.Name = "trackBarPosY";
            this.trackBarPosY.Size = new System.Drawing.Size(218, 45);
            this.trackBarPosY.TabIndex = 7;
            this.trackBarPosY.Scroll += new System.EventHandler(this.trackBarPosY_Scroll);
            // 
            // trackBarPosX
            // 
            this.trackBarPosX.Location = new System.Drawing.Point(3, 258);
            this.trackBarPosX.Maximum = 50;
            this.trackBarPosX.Minimum = -50;
            this.trackBarPosX.Name = "trackBarPosX";
            this.trackBarPosX.Size = new System.Drawing.Size(218, 45);
            this.trackBarPosX.TabIndex = 6;
            this.trackBarPosX.Scroll += new System.EventHandler(this.trackBarPosX_Scroll);
            // 
            // shadingGroupBox
            // 
            this.shadingGroupBox.Controls.Add(this.radioButtonPhong);
            this.shadingGroupBox.Controls.Add(this.radioButtonGouraud);
            this.shadingGroupBox.Controls.Add(this.radioButtonFlat);
            this.shadingGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shadingGroupBox.Location = new System.Drawing.Point(3, 3);
            this.shadingGroupBox.Name = "shadingGroupBox";
            this.shadingGroupBox.Size = new System.Drawing.Size(220, 119);
            this.shadingGroupBox.TabIndex = 9;
            this.shadingGroupBox.TabStop = false;
            this.shadingGroupBox.Text = "Shading Model";
            // 
            // radioButtonPhong
            // 
            this.radioButtonPhong.AutoSize = true;
            this.radioButtonPhong.Location = new System.Drawing.Point(7, 66);
            this.radioButtonPhong.Name = "radioButtonPhong";
            this.radioButtonPhong.Size = new System.Drawing.Size(56, 17);
            this.radioButtonPhong.TabIndex = 2;
            this.radioButtonPhong.TabStop = true;
            this.radioButtonPhong.Text = "Phong";
            this.radioButtonPhong.UseVisualStyleBackColor = true;
            this.radioButtonPhong.CheckedChanged += new System.EventHandler(this.radioButtonPhong_CheckedChanged);
            // 
            // radioButtonGouraud
            // 
            this.radioButtonGouraud.AutoSize = true;
            this.radioButtonGouraud.Location = new System.Drawing.Point(7, 43);
            this.radioButtonGouraud.Name = "radioButtonGouraud";
            this.radioButtonGouraud.Size = new System.Drawing.Size(66, 17);
            this.radioButtonGouraud.TabIndex = 1;
            this.radioButtonGouraud.TabStop = true;
            this.radioButtonGouraud.Text = "Gouraud";
            this.radioButtonGouraud.UseVisualStyleBackColor = true;
            this.radioButtonGouraud.CheckedChanged += new System.EventHandler(this.radioButtonGouraud_CheckedChanged);
            // 
            // radioButtonFlat
            // 
            this.radioButtonFlat.AutoSize = true;
            this.radioButtonFlat.Checked = true;
            this.radioButtonFlat.Location = new System.Drawing.Point(7, 20);
            this.radioButtonFlat.Name = "radioButtonFlat";
            this.radioButtonFlat.Size = new System.Drawing.Size(42, 17);
            this.radioButtonFlat.TabIndex = 0;
            this.radioButtonFlat.TabStop = true;
            this.radioButtonFlat.Text = "Flat";
            this.radioButtonFlat.UseVisualStyleBackColor = true;
            this.radioButtonFlat.CheckedChanged += new System.EventHandler(this.radioButtonFlat_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPosZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPosX)).EndInit();
            this.shadingGroupBox.ResumeLayout(false);
            this.shadingGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TrackBar trackBarPosZ;
        private System.Windows.Forms.TrackBar trackBarPosY;
        private System.Windows.Forms.TrackBar trackBarPosX;
        private System.Windows.Forms.GroupBox shadingGroupBox;
        private System.Windows.Forms.RadioButton radioButtonPhong;
        private System.Windows.Forms.RadioButton radioButtonGouraud;
        private System.Windows.Forms.RadioButton radioButtonFlat;
    }
}

