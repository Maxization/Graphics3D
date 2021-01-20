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
        DateTime previousDate;
        public Form1()
        {
            InitializeComponent();
            bmp = new DirectBitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp.Bitmap;
            camera = new Camera();
            device = new Device(bmp);
            meshes = device.LoadJSONFile("monkey.babylon");

            camera.Position = new Vector3D(0, 0, 3);
            camera.Target = new Vector3D(0, 0, 0);

            UpdateScreen();
        }
        
        void UpdateScreen()
        {
            //Calculate FPS
            var now = DateTime.Now;
            var currentFPS = 1000.0f / (now - previousDate).TotalMilliseconds;
            previousDate = now;
            this.Text = "FPS: " + string.Format("{0:0.00}", currentFPS);

            device.Clear();

            device.Render(camera, meshes);

            pictureBox1.Invalidate();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            meshes[0].Rotation = new Vector3D(meshes[0].Rotation.X, meshes[0].Rotation.Y + 0.05f, meshes[0].Rotation.Z);
            UpdateScreen();
        }

        private void trackBarX_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;
            meshes[0].Rotation = new Vector3D(trackBar.Value/100f, meshes[0].Rotation.Y, meshes[0].Rotation.Z);
            UpdateScreen();
        }

        private void trackBarY_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;
            meshes[0].Rotation = new Vector3D(meshes[0].Rotation.X , trackBar.Value / 100f, meshes[0].Rotation.Z);
            UpdateScreen();
        }

        private void trackBarZ_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;
            meshes[0].Rotation = new Vector3D(meshes[0].Rotation.X, meshes[0].Rotation.Y, trackBar.Value / 100f);
            UpdateScreen();
        }

        private void trackBarPosX_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;
            meshes[0].Position = new Vector3D(trackBar.Value / 100f, meshes[0].Position.Y, meshes[0].Position.Z);
            UpdateScreen();
        }

        private void trackBarPosY_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;
            meshes[0].Position = new Vector3D(meshes[0].Position.X, trackBar.Value / 100f, meshes[0].Position.Z);
            UpdateScreen();
        }

        private void trackBarPosZ_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;
            meshes[0].Position = new Vector3D(meshes[0].Position.X, meshes[0].Position.Y, trackBar.Value / 100f);
            UpdateScreen();
        }
    }
}
