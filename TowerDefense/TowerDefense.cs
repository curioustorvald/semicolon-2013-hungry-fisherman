using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TDBase;

namespace TowerDefense
{
    public partial class TowerDefense : Form
    {
        private Timer timer = new Timer() { Interval = 33 };
        private SceneManager sceneManager;
        private Main main = new Main();

        public TowerDefense()
        {
            Paint += TowerDefense_Paint;
            timer.Tick += timer_Tick;

            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);


            sceneManager = new SceneManager();

            main.Init(sceneManager);
            timer.Start();

        }

        void TowerDefense_Paint(object sender, PaintEventArgs e)
        {
            sceneManager.Update(e.Graphics);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void TowerDefense_MouseDown(object sender, MouseEventArgs e)
        {
            sceneManager.MouseDown(e.X, e.Y, e.Button);
            MouseClass.setMouse(e.X, e.Y, e.Button, true);
        }

        private void TowerDefense_MouseMove(object sender, MouseEventArgs e)
        {
            sceneManager.MouseMove(e.X, e.Y, e.Button);
            MouseClass.setMouse(e.X, e.Y, e.Button);
        }

        private void TowerDefense_MouseUp(object sender, MouseEventArgs e)
        {
            sceneManager.MouseUp(e.X, e.Y, e.Button);
            MouseClass.setMouse(e.X, e.Y, e.Button, false);
        }

        private void TowerDefense_KeyDown(object sender, KeyEventArgs e)
        {
            sceneManager.KeyDown(e.KeyCode, e.Control, e.Alt, e.Shift);
        }

        private void TowerDefense_KeyUp(object sender, KeyEventArgs e)
        {
            sceneManager.KeyUp(e.KeyCode, e.Control, e.Alt, e.Shift);
        }

        private void TowerDefense_Load(object sender, EventArgs e)
        {

        }

    }
}
