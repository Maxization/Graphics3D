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
            mesh = new Mesh("Cube", 8);
            camera = new Camera();
            device = new Device(bmp);

            mesh.Vertices[0] = Vector<double>.Build.DenseOfArray(new double[] { -1, 1, 1, 1});
            mesh.Vertices[1] = Vector<double>.Build.DenseOfArray(new double[] { 1, 1, 1, 1 });
            mesh.Vertices[2] = Vector<double>.Build.DenseOfArray(new double[] { -1, -1, 1, 1 });
            mesh.Vertices[3] = Vector<double>.Build.DenseOfArray(new double[] { -1, -1, -1, 1 });
            mesh.Vertices[4] = Vector<double>.Build.DenseOfArray(new double[] { -1, 1, -1, 1 });
            mesh.Vertices[5] = Vector<double>.Build.DenseOfArray(new double[] { 1, 1, -1, 1 });
            mesh.Vertices[6] = Vector<double>.Build.DenseOfArray(new double[] { 1, -1, 1, 1 });
            mesh.Vertices[7] = Vector<double>.Build.DenseOfArray(new double[] { 1, -1, -1, 1 });
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

            //mesh.Rotation = Vector<double>.Build.DenseOfArray(new double[] { mesh.Rotation[0]+0.01f,
            //    mesh.Rotation[1]+0.01f, mesh.Rotation[2] });

            device.Render(camera, mesh);

            pictureBox1.Invalidate();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateScreen();
        }
    }
}
