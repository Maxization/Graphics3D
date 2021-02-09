using Graphics3D.LightModels;
using Graphics3D.Scene;
using Graphics3D.ShadingModels;
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
        Camera staticCamera;
        Camera followCamera;
        Camera dynamicCamera;
        DateTime previousDate;
        Fog fog;
        ShadingModelEnum shadingType;
        ILightModel lightModel;
        public Form1()
        {
            InitializeComponent();
            bmp = new DirectBitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp.Bitmap;
            device = new Device(bmp);
            meshes = device.LoadJSONFile("scene.babylon");
            fog = new Fog(false, 500);

            shadingType = ShadingModelEnum.Flat;
            lightModel = new PhongLightModel(0.1, 0.5, 0.5, 20);

            meshes[0].Rotation = new Vector3D(meshes[0].Rotation.X, meshes[0].Rotation.Y + 0.00001f, meshes[0].Rotation.Z);
            meshes[2].Rotation = new Vector3D(meshes[2].Rotation.X + Math.PI / 2, meshes[2].Rotation.Y, meshes[2].Rotation.Z);
            meshes[3].Rotation = new Vector3D(meshes[3].Rotation.X + Math.PI / 2, meshes[3].Rotation.Y, meshes[3].Rotation.Z);
            meshes[4].Rotation = new Vector3D(meshes[4].Rotation.X + Math.PI / 2, meshes[4].Rotation.Y, meshes[4].Rotation.Z);

            meshes[0].Color = Color.Green;
            meshes[1].Color = Color.Red;
            meshes[2].Color = Color.Blue;
            meshes[3].Color = Color.Blue;
            meshes[4].Color = Color.Blue;

            staticCamera = new Camera();
            staticCamera.Position = new Vector3D(0, 3, 5);
            staticCamera.Target = new Vector3D(0, 0, 0);

            followCamera = new Camera();
            followCamera.Position = new Vector3D(0, 3, 8);
            followCamera.Target = meshes[1].Position;

            dynamicCamera = new Camera();
            dynamicCamera.Position = new Vector3D(meshes[1].Position.X - 10, 15, meshes[1].Position.Z);
            dynamicCamera.Target = new Vector3D(5, 10, 0);

            camera = staticCamera;
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

            device.Render(camera, shadingType, lightModel, fog, meshes);

            pictureBox1.Invalidate();
        }


        double pos_dx = 0.1f;
        double rot_dz = 0.05f;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(fog.isFog)
            {
                fog.ChangeFog();
            }

            double oldX = meshes[1].Position.X;
            if (oldX > 6 || oldX < -6)
            {
                pos_dx = -pos_dx;
                rot_dz = -rot_dz;
            }

            meshes[1].Position = new Vector3D(oldX + pos_dx, 0.5, 0);
            meshes[1].Rotation = new Vector3D(meshes[0].Rotation.X, meshes[0].Rotation.Y, meshes[0].Rotation.Z + rot_dz);
            followCamera.Target = meshes[1].Position;
            dynamicCamera.Position = new Vector3D(meshes[1].Position.X - 10, 15, meshes[1].Position.Z);

            UpdateScreen();
        }
        private void radioButtonFlat_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if(button.Checked)
            {
                shadingType = ShadingModelEnum.Flat;
            }
        }

        private void radioButtonGouraud_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button.Checked)
            {
                shadingType = ShadingModelEnum.Gouraud;
            }
        }

        private void radioButtonPhong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button.Checked)
            {
                shadingType = ShadingModelEnum.Phong;
            }
        }

        private void radioButtonStatic_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button.Checked)
            {
                camera = staticCamera;
            }
        }

        private void radioButtonFollow_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button.Checked)
            {
                camera = followCamera;
            }
        }

        private void radioButtonDynamic_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button.Checked)
            {
                camera = dynamicCamera;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox button = sender as CheckBox;
            fog.ChangeState(button.Checked);
        }
    }
}
