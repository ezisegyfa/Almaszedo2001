using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace TurnBasedTableStrategyGame
{
    public partial class Form1 : Form
    {
        Point topLeftCorn = new Point(70, 90);

        public Form1()
        {
            InitializeComponent();
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            System.Drawing.Graphics graphics = e.Graphics;
            HexGrid.HexGrid hg = new HexGrid.HexGrid(7,7, new Point(50, 50), 50);
            hg.Draw(graphics, topLeftCorn, new Size(800, 400));
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            topLeftCorn = new Point(topLeftCorn.X, topLeftCorn.Y + 10);
            Refresh();
        }
    }
}
