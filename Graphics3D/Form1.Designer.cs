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
            this.shadingGroupBox = new System.Windows.Forms.GroupBox();
            this.radioButtonPhong = new System.Windows.Forms.RadioButton();
            this.radioButtonGouraud = new System.Windows.Forms.RadioButton();
            this.radioButtonFlat = new System.Windows.Forms.RadioButton();
            this.groupBoxCamera = new System.Windows.Forms.GroupBox();
            this.radioButtonDynamic = new System.Windows.Forms.RadioButton();
            this.radioButtonFollow = new System.Windows.Forms.RadioButton();
            this.radioButtonStatic = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.shadingGroupBox.SuspendLayout();
            this.groupBoxCamera.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
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
            this.tableLayoutPanel2.Controls.Add(this.button1, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.shadingGroupBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBoxCamera, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.button2, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.groupBox3, 0, 4);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(955, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 91F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 183F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(226, 755);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // shadingGroupBox
            // 
            this.shadingGroupBox.Controls.Add(this.radioButtonPhong);
            this.shadingGroupBox.Controls.Add(this.radioButtonGouraud);
            this.shadingGroupBox.Controls.Add(this.radioButtonFlat);
            this.shadingGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shadingGroupBox.Location = new System.Drawing.Point(3, 3);
            this.shadingGroupBox.Name = "shadingGroupBox";
            this.shadingGroupBox.Size = new System.Drawing.Size(220, 99);
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
            // groupBoxCamera
            // 
            this.groupBoxCamera.Controls.Add(this.radioButtonDynamic);
            this.groupBoxCamera.Controls.Add(this.radioButtonFollow);
            this.groupBoxCamera.Controls.Add(this.radioButtonStatic);
            this.groupBoxCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCamera.Location = new System.Drawing.Point(3, 108);
            this.groupBoxCamera.Name = "groupBoxCamera";
            this.groupBoxCamera.Size = new System.Drawing.Size(220, 113);
            this.groupBoxCamera.TabIndex = 10;
            this.groupBoxCamera.TabStop = false;
            this.groupBoxCamera.Text = "Camera";
            // 
            // radioButtonDynamic
            // 
            this.radioButtonDynamic.AutoSize = true;
            this.radioButtonDynamic.Location = new System.Drawing.Point(7, 66);
            this.radioButtonDynamic.Name = "radioButtonDynamic";
            this.radioButtonDynamic.Size = new System.Drawing.Size(66, 17);
            this.radioButtonDynamic.TabIndex = 2;
            this.radioButtonDynamic.Text = "Dynamic";
            this.radioButtonDynamic.UseVisualStyleBackColor = true;
            this.radioButtonDynamic.CheckedChanged += new System.EventHandler(this.radioButtonDynamic_CheckedChanged);
            // 
            // radioButtonFollow
            // 
            this.radioButtonFollow.AutoSize = true;
            this.radioButtonFollow.Location = new System.Drawing.Point(7, 43);
            this.radioButtonFollow.Name = "radioButtonFollow";
            this.radioButtonFollow.Size = new System.Drawing.Size(55, 17);
            this.radioButtonFollow.TabIndex = 1;
            this.radioButtonFollow.Text = "Follow";
            this.radioButtonFollow.UseVisualStyleBackColor = true;
            this.radioButtonFollow.CheckedChanged += new System.EventHandler(this.radioButtonFollow_CheckedChanged);
            // 
            // radioButtonStatic
            // 
            this.radioButtonStatic.AutoSize = true;
            this.radioButtonStatic.Checked = true;
            this.radioButtonStatic.Location = new System.Drawing.Point(7, 20);
            this.radioButtonStatic.Name = "radioButtonStatic";
            this.radioButtonStatic.Size = new System.Drawing.Size(52, 17);
            this.radioButtonStatic.TabIndex = 0;
            this.radioButtonStatic.TabStop = true;
            this.radioButtonStatic.Text = "Static";
            this.radioButtonStatic.UseVisualStyleBackColor = true;
            this.radioButtonStatic.CheckedChanged += new System.EventHandler(this.radioButtonStatic_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 227);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 85);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fog";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(7, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(192, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Fog (only for Phong shading model)";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.trackBar1);
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 318);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 119);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(7, 54);
            this.trackBar1.Maximum = 50;
            this.trackBar1.Minimum = -50;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(199, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(7, 20);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(69, 17);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "Reflector";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 575);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 545);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 443);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(220, 96);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rotation";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(7, 20);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(66, 17);
            this.checkBox3.TabIndex = 0;
            this.checkBox3.Text = "Rotation";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
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
            this.shadingGroupBox.ResumeLayout(false);
            this.shadingGroupBox.PerformLayout();
            this.groupBoxCamera.ResumeLayout(false);
            this.groupBoxCamera.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox shadingGroupBox;
        private System.Windows.Forms.RadioButton radioButtonPhong;
        private System.Windows.Forms.RadioButton radioButtonGouraud;
        private System.Windows.Forms.RadioButton radioButtonFlat;
        private System.Windows.Forms.GroupBox groupBoxCamera;
        private System.Windows.Forms.RadioButton radioButtonDynamic;
        private System.Windows.Forms.RadioButton radioButtonFollow;
        private System.Windows.Forms.RadioButton radioButtonStatic;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox3;
    }
}

