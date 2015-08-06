using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NeuralNetworks;


namespace NumDetector88
{
    public partial class Form1 : Form
    {
        BackPropogationNetwork charecterNet = new BackPropogationNetwork(256, 16, 250);

        public Form1()
        {
            InitializeComponent();
        }

        void Form1_Updated(Layer l, EventArgs e)
        {
            MessageBox.Show("Hello");
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(16, 16);
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseClick += pictureBox1_MouseClick;
      
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            pictureBox1.Refresh();
        }

        public static string ArrayToStr(IEnumerable<double> arr)
        {
            string s = "";
            foreach (double d in arr)
            {
                s += d + "\n";
            }

            return s;
        }

        List<NeuralNetworks.DataSet> dataset = new List<NeuralNetworks.DataSet>();
        void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {


           // pictureBox1.Refresh();




            if (checkBox1.Checked && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                
                dataset.Add(new NeuralNetworks.DataSet() { Outputs = CharacterToNetOutput(textBoxRecognized.Text), Inputs = GetInputFromBitmap(new Bitmap(pictureBox1.Image)) });

                labelDataSetSaveNum.Text = dataset.Count + " Objects";
                //charecterNet.ApplyInput(GetInputFromBitmap(new Bitmap(pictureBox1.Image)));
                //charecterNet.CalculateOutput();

                //DetectionReturn detect = OutputToCharacter(charecterNet.ReadOutput());

                //textBoxRecognized.Text = detect.ch;
                //textBoxConfidence.Text = detect.confidence.ToString();

                //textBox3.Text = "Added" + dataset.Count;
            }

            else if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                    charecterNet.ApplyInput(GetInputFromBitmap(new Bitmap(pictureBox1.Image)));
                charecterNet.CalculateOutput();
                DetectionReturn detect = OutputToCharacter(charecterNet.ReadOutput());
                //textBox3.Text = ArrayToStr(charecterNet.Layers[charecterNet.InputIndex].GetValues());

                textBoxRecognized.Text = detect.ch;
                textBoxConfidence.Text = detect.confidence.ToString();
            }

            //Graphics.FromImage(pictureBox1.Image).Clear(Color.White);

            pictureBox1.Refresh();
        }

        double[] GetInputFromBitmap(Bitmap bitmap)
        {
            double[] data = new double[bitmap.Width*bitmap.Height];

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    data[(bitmap.Width*y) + i] = ThreholdPixel(bitmap.GetPixel(i, y), 0.5, true);
                }
            }

            return data;

        }

        double ThreholdPixel(Color c, double threshold, bool greaterthan)
        {
            double value = ((c.B/255)+(c.G/255)+(c.R/255))/3;

            return value;

            if (value > threshold)
            {
                if (greaterthan)
                    return 1;
                else
                    return 0;
            }

            else
            {
                if (greaterthan)
                    return 0;
                else
                    return 1;
            }

        }



        double[] CharacterToNetOutput(string c)
        {
            switch(c)
            {
                case "0":
                    return new double[]{1,0,0,0,
                                        0,0,0,0,
                                        0,0,0,0,
                                        0,0,0,0
                    };
                case "1":
                    return new double[]{0,1,0,0,
                                        0,0,0,0,
                                        0,0,0,0,
                                        0,0,0,0
                    };

                case "2":
                    return new double[]{0,0,1,0,
                                        0,0,0,0,
                                        0,0,0,0,
                                        0,0,0,0
                    };
                case "3":
                    return new double[]{0,0,0,1,
                                        0,0,0,0,
                                        0,0,0,0,
                                        0,0,0,0
                    };
                case "4":
                    return new double[]{0,0,0,0,
                                        1,0,0,0,
                                        0,0,0,0,
                                        0,0,0,0
                    };
                case "5":
                    return new double[]{0,0,0,0,
                                        0,1,0,0,
                                        0,0,0,0,
                                        0,0,0,0
                    };

                case "6":
                    return new double[]{0,0,0,0,
                                        0,0,1,0,
                                        0,0,0,0,
                                        0,0,0,0
                    };

                case "7":
                    return new double[]{0,0,0,0,
                                        0,0,0,1,
                                        0,0,0,0,
                                        0,0,0,0
                    };
                case "8":
                    return new double[]{0,0,0,0,
                                        0,0,0,0,
                                        1,0,0,0,
                                        0,0,0,0
                    };

                case "9":
                    return new double[]{0,0,0,0,
                                        0,0,0,0,
                                        0,1,0,0,
                                        0,0,0,0
                    };

                default:
                    return new double[]{0,0,0,0,
                                        0,0,0,0,
                                        0,0,0,0,
                                        0,0,0,0
                    };
            }
        }


        struct DetectionReturn
        {
            public string ch;
            public double confidence;
        }

        DetectionReturn OutputToCharacter(IEnumerable<double> output)
        {
            double top = output.Max();

            string c = "";

            switch (output.ToList().IndexOf(top))
            {
                case 0:
                    c= "0";
                    break;
                case 1:
                    c= "1";
                    break;
                case 2:
                    c= "2";
                    break;
                case 3:
                    c= "3";
                    break;
                case 4:
                    c= "4";
                    break;
                case 5:
                    c= "5";
                    break;
                case 6:
                    c= "6";
                    break;
                case 7:
                    c= "7";
                    break;
                case 8:
                    c= "8";
                    break;
                case 9:
                    c= "9";
                    break;
                case 10:
                    c= "(";
                    break;
                case 11:
                    c= ")";
                    break;
                case 12:
                    c= "*";
                    break;
                case 13:
                    c= "+";
                    break;
                case 14:
                    c= "-";
                    break;
                case 15:
                    c= "/";
                    break;
            }


            return new DetectionReturn() { ch = c, confidence = top };
        }



        void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pb = (sender as PictureBox);

            if (e.Button == MouseButtons.Right)
            {
                Graphics.FromImage(pb.Image).Clear(Color.White);
            }

            if (e.Button == MouseButtons.Left)
            {
                prevPos = new Point(e.X - pb.Location.X, e.Y - pb.Location.Y);

                Point picturePos = new Point((e.X) / (pb.Size.Width / 16) +1, (e.Y) / (pb.Size.Height / 16)+1);

                Graphics.FromImage(pb.Image).DrawEllipse(new Pen(Brushes.Black, 1), new Rectangle(picturePos, new Size(1, 1)));

                pb.Refresh();
            }
        }

        Point prevPos;
        void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pb = (sender as PictureBox);

            Point picturePos = new Point((e.X) / (pb.Size.Width / 16),  (e.Y) / (pb.Size.Height / 16));

            if (e.Button == MouseButtons.Left)
            {

                Graphics.FromImage(pb.Image).DrawEllipse(new Pen(Brushes.Black, 1), new Rectangle(picturePos, new Size(1, 1)));
                if (prevPos != null)
                {

                    //Graphics.FromImage(pb.Image).DrawLine(new Pen(Brushes.Black, 1), picturePos, prevPos);
                    //Graphics.FromImage(pb.Image).DrawEllipse(new Pen(Brushes.Black, 1), new Rectangle(picturePos, new Size(1, 1)));
                    pb.Refresh();
                }

            }

            prevPos = picturePos;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonTrain_Click(object sender, EventArgs e)
        {

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                charecterNet.BatchBackPropogate(dataset.ToArray(), int.Parse(textBoxInterations.Text), double.Parse(textBoxLearningRate.Text), double.Parse(textBoxMom.Text), worker);
            };

            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();



            statusBar1.Text = "Training in Progress";

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            statusBar1.Text = "Training in Progress: " + e.ProgressPercentage + "%";
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            statusBar1.Text = "Traning Done";

            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;

            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            SaveFileDialog sFile = new SaveFileDialog();
            sFile.OpenFile();
            sFile.AddExtension = true;
            sFile.DefaultExt = ".xml";
            sFile.ShowDialog();
         

            NeuralNetwork.SaveNetworkToFile(charecterNet.GetNetworkData(), sFile.FileName);
            MessageBox.Show("Saved");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog oFile = new OpenFileDialog();
            oFile.Multiselect = false;
            oFile.ShowDialog();

            NetworkData data = NeuralNetwork.ReadNetworkFromFile(oFile.FileName);

            charecterNet = new BackPropogationNetwork(data);
            //MessageBox.Show("Read OK");


            if(data!=null)
                button2.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("NeuralNetworks\n\nHemanth Kothoju", "NeuralNetworks");
        }

        private void textBoxLearningRate_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
