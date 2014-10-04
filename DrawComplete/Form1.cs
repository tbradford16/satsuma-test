using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Satsuma;
using Satsuma.Drawing;

namespace DrawComplete
{
	public partial class Form1 : Form
	{
		public Bitmap img;

		public int NodeCount
		{
			get
			{
				return nodeCount;
			}
			set
			{
				nodeCount = value;
				textBox1.Text = "nodes: " + nodeCount;
			}
		}
		private int nodeCount;
		
		public Form1()
		{
			InitializeComponent();

			NodeCount = 4;

			initImg();
		}

		private void initImg()
		{
			var graph = new CompleteGraph(NodeCount, Directedness.Undirected);
			// compute a nice layout of the graph
			var layout = new ForceDirectedLayout(graph);
			layout.Run();
			// draw the graph using the computed layout
			var nodeShape = new NodeShape(NodeShapeKind.Diamond, new PointF(40, 40));
			var nodeStyle = new NodeStyle
			{
				Brush = Brushes.Yellow,
				Shape = nodeShape
			};
			var drawer = new GraphDrawer()
			{
				Graph = graph,
				NodePosition = (node => (PointF)layout.NodePositions[node]),
				NodeCaption = (node => graph.GetNodeIndex(node).ToString()),
				NodeStyle = (node => nodeStyle)
			};
			img = drawer.Draw(500, 500, Color.White);
		}

		//public 

		private void OnPaint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawImage(img, 100, 10);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			NodeCount++;
			initImg();
			Invalidate();
			Update();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			NodeCount--;

			if( NodeCount < 0 )
			{
				NodeCount = 0;
			}

			initImg();
			Invalidate();
			Update();
		}

		private void Form1Load(object sender, EventArgs e)
		{
			this.TopMost = true;
			this.WindowState = FormWindowState.Maximized;
		}
	}
}
