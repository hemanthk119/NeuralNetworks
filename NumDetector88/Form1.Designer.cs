namespace NumDetector88
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxRecognized = new System.Windows.Forms.TextBox();
            this.textBoxConfidence = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.buttonTrain = new System.Windows.Forms.Button();
            this.textBoxInterations = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.labelDataSetSaveNum = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.textBoxLearningRate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxMom = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 354);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(576, 22);
            this.statusBar1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(161, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // textBoxRecognized
            // 
            this.textBoxRecognized.BackColor = System.Drawing.Color.Silver;
            this.textBoxRecognized.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxRecognized.Location = new System.Drawing.Point(161, 328);
            this.textBoxRecognized.Name = "textBoxRecognized";
            this.textBoxRecognized.Size = new System.Drawing.Size(100, 20);
            this.textBoxRecognized.TabIndex = 3;
            this.textBoxRecognized.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBoxConfidence
            // 
            this.textBoxConfidence.BackColor = System.Drawing.Color.Silver;
            this.textBoxConfidence.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxConfidence.Location = new System.Drawing.Point(319, 328);
            this.textBoxConfidence.Name = "textBoxConfidence";
            this.textBoxConfidence.Size = new System.Drawing.Size(98, 20);
            this.textBoxConfidence.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(158, 312);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Recognized";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(356, 312);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Confidence";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Load Net";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(12, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Save Net";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(161, 274);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(139, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Save Into Train Memory";
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // buttonTrain
            // 
            this.buttonTrain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTrain.Location = new System.Drawing.Point(436, 325);
            this.buttonTrain.Name = "buttonTrain";
            this.buttonTrain.Size = new System.Drawing.Size(128, 23);
            this.buttonTrain.TabIndex = 12;
            this.buttonTrain.Text = "Train Saved";
            this.buttonTrain.UseVisualStyleBackColor = true;
            this.buttonTrain.Click += new System.EventHandler(this.buttonTrain_Click);
            // 
            // textBoxInterations
            // 
            this.textBoxInterations.BackColor = System.Drawing.Color.Silver;
            this.textBoxInterations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxInterations.Location = new System.Drawing.Point(517, 299);
            this.textBoxInterations.Name = "textBoxInterations";
            this.textBoxInterations.Size = new System.Drawing.Size(47, 20);
            this.textBoxInterations.TabIndex = 13;
            this.textBoxInterations.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(514, 274);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Iterations";
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(436, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(128, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "Exit";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // labelDataSetSaveNum
            // 
            this.labelDataSetSaveNum.AutoSize = true;
            this.labelDataSetSaveNum.BackColor = System.Drawing.Color.Transparent;
            this.labelDataSetSaveNum.Location = new System.Drawing.Point(365, 275);
            this.labelDataSetSaveNum.Name = "labelDataSetSaveNum";
            this.labelDataSetSaveNum.Size = new System.Drawing.Size(52, 13);
            this.labelDataSetSaveNum.TabIndex = 16;
            this.labelDataSetSaveNum.Text = "0 Objects";
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(436, 41);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(128, 23);
            this.button4.TabIndex = 17;
            this.button4.Text = "About";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBoxLearningRate
            // 
            this.textBoxLearningRate.BackColor = System.Drawing.Color.Silver;
            this.textBoxLearningRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxLearningRate.Location = new System.Drawing.Point(12, 328);
            this.textBoxLearningRate.Name = "textBoxLearningRate";
            this.textBoxLearningRate.Size = new System.Drawing.Size(56, 20);
            this.textBoxLearningRate.TabIndex = 18;
            this.textBoxLearningRate.Text = "0.4";
            this.textBoxLearningRate.TextChanged += new System.EventHandler(this.textBoxLearningRate_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(12, 311);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Learning Rate";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(12, 271);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Monentum";
            // 
            // textBoxMom
            // 
            this.textBoxMom.BackColor = System.Drawing.Color.Silver;
            this.textBoxMom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxMom.Location = new System.Drawing.Point(12, 288);
            this.textBoxMom.Name = "textBoxMom";
            this.textBoxMom.Size = new System.Drawing.Size(56, 20);
            this.textBoxMom.TabIndex = 20;
            this.textBoxMom.Text = "0.2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(576, 376);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxMom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxLearningRate);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.labelDataSetSaveNum);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxInterations);
            this.Controls.Add(this.buttonTrain);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxConfidence);
            this.Controls.Add(this.textBoxRecognized);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Number Recognizer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxRecognized;
        private System.Windows.Forms.TextBox textBoxConfidence;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button buttonTrain;
        private System.Windows.Forms.TextBox textBoxInterations;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label labelDataSetSaveNum;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBoxLearningRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxMom;
    }
}

