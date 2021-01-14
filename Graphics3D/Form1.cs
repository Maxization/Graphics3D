using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics3D
{
    public partial class Form1 : Form
    {
        DirectBitmap bmp;
        Device device;
        Mesh[] meshes;
        Camera camera;
        public Form1()
        {
            InitializeComponent();
            bmp = new DirectBitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp.Bitmap;
            camera = new Camera();
            device = new Device(bmp);
            meshes = device.LoadJSONFileAsync("monkey.babylon");

            camera.Position = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 5 });
            camera.Target = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0 });

            UpdateScreen();
        }
        
        void UpdateScreen()
        {
            device.Clear();

            foreach (var mesh in meshes)
            {
                mesh.Rotation = Vector<double>.Build.DenseOfArray(new double[] { mesh.Rotation[0]+1.5,
                                                                    mesh.Rotation[1], mesh.Rotation[2] - 1.6});
            }

            device.Render(camera, meshes);

            pictureBox1.Invalidate();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateScreen();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;
            UpdateScreen();
        }
    }
}
