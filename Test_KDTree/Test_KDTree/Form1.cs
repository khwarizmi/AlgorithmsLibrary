using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test_KDTree
{
    public partial class KDTreeVisualization : Form
    {
        int x, y;
        Test_KDTree.KDTree<int, int> kdTree = new KDTree<int, int>((int x, int y) => { return x - y; }, (int x, int y) => { return x - y; });

        public KDTreeVisualization()
        {
            x = y = -1;
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs ea)
        {
            Graphics e = ea.Graphics;
            e.Clear(System.Drawing.Color.Azure);

            if(x != -1 && y != -1)
            {
                Pen Black = new System.Drawing.Pen (System.Drawing.Color.Black);
                Pen Red = new System.Drawing.Pen(System.Drawing.Color.Red);
                
                e.DrawRectangle(Black, x, y, 2, 2);
                List<TreeNode<int,int>> pointsX = kdTree.getX();
                List<TreeNode<int,int>> pointsY = kdTree.getY();               

                TreeNode<int, int> node = null;
                int xStart, ystart;
                int xEnd, yEnd;

                for (int i = 0; pointsX != null && i < pointsX.Count; i++)
                {
                    node = pointsX[i];
                    xStart = node.X;
                    ystart = 0;
                    xEnd = node.X;
                    yEnd = panel1.Height;
                    kdTree.search(node, ref xStart, ref ystart, ref xEnd, ref yEnd);
                    e.DrawLine(Red, xStart, ystart, xEnd, yEnd);
                    e.DrawRectangle(Black, node.X, node.Y, 2, 2);
                }

                for (int i = 0; pointsY != null && i < pointsY.Count; i++)
                {
                    node = pointsY[i];
                    xStart = 0;
                    ystart = node.Y;
                    xEnd = panel1.Width;
                    yEnd = node.Y;
                    kdTree.search(node, ref xStart, ref ystart, ref xEnd, ref yEnd);                   
                    e.DrawLine(Black, xStart, ystart, xEnd, yEnd);
                    e.DrawRectangle(Black, node.X, node.Y, 2, 2);
                }

                kdTree.insert(x, y);
                x = y = -1;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
            panel1.Invalidate();
        }
    }
}
