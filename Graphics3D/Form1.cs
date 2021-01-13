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
        Mesh mesh;
        Camera camera;
        public Form1()
        {
            InitializeComponent();
            bmp = new DirectBitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp.Bitmap;
            mesh = new Mesh("Cube", 8, 12);
            camera = new Camera();
            device = new Device(bmp);

            mesh.Vertices[0] = Vector<double>.Build.DenseOfArray(new double[] { -0.5f, 0.5f, 0.5f, 1});
            mesh.Vertices[1] = Vector<double>.Build.DenseOfArray(new double[] { 0.5f, 0.5f, 0.5f, 1 });
            mesh.Vertices[2] = Vector<double>.Build.DenseOfArray(new double[] { -0.5f, -0.5f, 0.5f, 1 });
            mesh.Vertices[3] = Vector<double>.Build.DenseOfArray(new double[] { 0.5f, -0.5, 0.5f, 1 });
            mesh.Vertices[4] = Vector<double>.Build.DenseOfArray(new double[] { -0.5f, 0.5f, -0.5f, 1 });
            mesh.Vertices[5] = Vector<double>.Build.DenseOfArray(new double[] { 0.5f, 0.5f, -0.5f, 1 });
            mesh.Vertices[6] = Vector<double>.Build.DenseOfArray(new double[] { 0.5f, -0.5f, -0.5f, 1 });
            mesh.Vertices[7] = Vector<double>.Build.DenseOfArray(new double[] { -0.5f, -0.5f, -0.5f, 1 });

            mesh.Faces[0] = new Face { A = 0, B = 1, C = 2 };
            mesh.Faces[1] = new Face { A = 1, B = 2, C = 3 };
            mesh.Faces[2] = new Face { A = 1, B = 3, C = 6 };
            mesh.Faces[3] = new Face { A = 1, B = 5, C = 6 };
            mesh.Faces[4] = new Face { A = 0, B = 1, C = 4 };
            mesh.Faces[5] = new Face { A = 1, B = 4, C = 5 };

            mesh.Faces[6] = new Face { A = 2, B = 3, C = 7 };
            mesh.Faces[7] = new Face { A = 3, B = 6, C = 7 };
            mesh.Faces[8] = new Face { A = 0, B = 2, C = 7 };
            mesh.Faces[9] = new Face { A = 0, B = 4, C = 7 };
            mesh.Faces[10] = new Face { A = 4, B = 5, C = 6 };
            mesh.Faces[11] = new Face { A = 4, B = 6, C = 7 };

            mesh.Rotation = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0 });
            mesh.Position = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0 });

            camera.Position = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 10 });
            camera.Target = Vector<double>.Build.DenseOfArray(new double[] { 0, 0, 0 });

            UpdateScreen();
        }
        
        void UpdateScreen()
        {
            using(Graphics g = Graphics.FromImage(bmp.Bitmap))
            {
                g.Clear(Color.White);
            }

            mesh.Rotation = Vector<double>.Build.DenseOfArray(new double[] { mesh.Rotation[0]+0.1f,
                mesh.Rotation[1]+0.1f, mesh.Rotation[2] });

            device.Render(camera, mesh);

            pictureBox1.Invalidate();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateScreen();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;
            mesh.Position[0] = trackBar.Value;
            UpdateScreen();
        }
    }
}
