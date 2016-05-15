using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Media;

namespace potsGames
{	

	public class PipeGeometry {
		private bool exist = false;
		
		private int _TopX; // X COORDINATE OF PIPE TOP
		private int _TopLength;
		private int _BottomX; // X CORDINATE OF PIPE BOTTOM
		private int _BottomLength;
		public int PipeWidth { get; set;}
		public int DistanceBetweenPipes { get; set; }
		public int WindowHeigh { get; set; }

		public int TopX { get { return _TopX; } set { _TopX = value; PropitiesInit(); Propities[0] = value; } }
		public int TopLength { get { return _TopLength; } set { _TopLength = value; PropitiesInit(); Propities[1] = value; } }
		public int BottomX { get { return _BottomX; } set { _BottomX = value; PropitiesInit(); Propities[2] = value; } }
		public int BottomLength { get { return _BottomLength; } set { _BottomLength = value; PropitiesInit(); Propities[3] = value; } }
		private List<int> Propities;// /{ get; set;}
		// сильно зависим от параметров окна //
		private void PropitiesInit() {
			if (!exist)
				Propities = new List<int>();
			for (var i = 0; i < 4; i++)
				Propities.Add(0);
			exist = true;
		}

		public PipeGeometry (int windowHeight , int windowWidth, int pipeWidth, int distanceBetweenPipes) // To do make normal params to generate pipe 
		{
			PropitiesInit();
			Random random = new Random();
			_TopX = windowWidth;//sreen possition
			_TopLength = random.Next(40, (windowHeight - distanceBetweenPipes)); // refact 40 to pipe length min
			_BottomX = windowWidth;//screen posirion
			_BottomLength = _TopLength + distanceBetweenPipes;
			Propities[0] = _TopX;
			Propities[1] = _TopLength;
			Propities[2] = _BottomX;
			Propities[3] = _BottomLength;
			PipeWidth = pipeWidth;
			DistanceBetweenPipes = distanceBetweenPipes;
			WindowHeigh = windowHeight;
		}

		public void DrawPipe(PaintEventArgs e)
{
				// Первый верхний
				e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(TopX, 0, PipeWidth, TopLength));
				e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(TopX - 10, BottomLength - DistanceBetweenPipes, PipeWidth + 20, 15)); // потом надо реализовать масштабирование
				// первый нижний
				e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(BottomX, BottomLength, PipeWidth, WindowHeigh - BottomLength));
				e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(BottomX - 10, BottomLength, PipeWidth + 20, 15));
		}

		public bool Any()
		{
			return Propities.Any();
		}

		public int this[int i]
		{
			get{return Propities[i];}
			set
			{
				Propities[i] = value;
				switch (i)
				{
					case 0 :
						_TopX = value;
						break;
					case 1 :
						_TopLength = value;
						break;
					case 2 :
						_BottomX = value;
						break;
					case 3 :
						_BottomLength = value;
						break;
 
				}
			}
		}


		
	}

	/*public class Pipes {
		List<int> FirstPipe = new List<int>();
		List<int> SecondPipe = new List<int>();
		int PipeWidth {get; set; }         //= 55;
		int PipeDifferentY {get; set; }    //= 140;
		int PipeDifferentX { get; set; }   //= 180;
		
		public void SetPipes
	
	}*/
	public partial class GameForm
	{
		PipeGeometry FirstPipe;
		PipeGeometry SecondPipe;
		int PipeWidth = 55;
		int PipeDifferentY = 140;
		int PipeDifferentX = 180;
		bool Start = true;
		bool Running;
		int Step = 5;
		int Originalx, Originaly;
		bool ResetPipes = false;
		int points;
		bool inPipe = false;

		private void Die()
		{
			Running = false;
			timer2.Enabled = false;
			timer3.Enabled = false;
			button2.Visible = true;
			button2.Enabled = true;
			points = 0;
			mainBird.Location = new Point(Originalx, Originaly);
			ResetPipes = true;
			FirstPipe = null;
		}

		private void StartGame()
		{
			ResetPipes = false;
			timer1.Enabled = true;
			timer2.Enabled = true;
			timer3.Enabled = true;
			
			FirstPipe = new PipeGeometry(Height, Width ,PipeWidth, PipeDifferentY);
			SecondPipe = new PipeGeometry(Height, Width + PipeDifferentX,PipeWidth, PipeDifferentY);
			button2.Visible = false;
			button2.Enabled = false;
			Running = true;
			Focus();
		}
		public void InitialPipes()
		{
			if (FirstPipe[0] + PipeWidth <= 0 | Start == true)
			{
			
				FirstPipe = new PipeGeometry(Height, Width, PipeWidth , PipeDifferentY);
			}
			else
			{
				FirstPipe[0] = FirstPipe[0] - 2;
				FirstPipe[2] = FirstPipe[2] - 2;
			}
			if (SecondPipe[0] + PipeWidth <= 0)
			{
				SecondPipe = new PipeGeometry(Height, Width, PipeWidth, PipeDifferentY);
			}
			else
			{
				SecondPipe[0] = SecondPipe[0] - 2;
				SecondPipe[2] = SecondPipe[2] - 2;
			}

			if (Start)
				Start = false;
		}

		private void CheckForPoint()
		{
			Rectangle rec = mainBird.Bounds;
			Rectangle rec1 = new Rectangle(FirstPipe[2] + 20, FirstPipe[3] - PipeDifferentY, 15, PipeDifferentY);
			Rectangle rec2 = new Rectangle(SecondPipe[2] + 20, SecondPipe[3] - PipeDifferentY, 15, PipeDifferentY);
			Rectangle intersect1 = Rectangle.Intersect(rec, rec1);
			Rectangle intersect2 = Rectangle.Intersect(rec, rec2);
			if (!ResetPipes | Start)
			{
				if (intersect1 != Rectangle.Empty | intersect2 != Rectangle.Empty)
				{
					if (!inPipe)
					{
						points++;
						PlaySound("point");
						inPipe = true;
					}
				}
				else
				{
					inPipe = false;
				}
			}
		}

		private void CheckForCollision()
		{
			Rectangle rec = mainBird.Bounds;
			Rectangle rec1 = new Rectangle(FirstPipe[0], 0, PipeWidth, FirstPipe[1]);
			Rectangle rec2 = new Rectangle(FirstPipe[2], FirstPipe[3], PipeWidth, this.Height - FirstPipe[3]);
			Rectangle rec3 = new Rectangle(SecondPipe[0], 0, PipeWidth, SecondPipe[1]);
			Rectangle rec4 = new Rectangle(SecondPipe[2], SecondPipe[3], PipeWidth, this.Height - SecondPipe[3]);
			Rectangle intersect1 = Rectangle.Intersect(rec, rec1);
			Rectangle intersect2 = Rectangle.Intersect(rec, rec2);
			Rectangle intersect3 = Rectangle.Intersect(rec, rec3);
			Rectangle intersect4 = Rectangle.Intersect(rec, rec4);
			if (!ResetPipes | Start)
				if (intersect1 != Rectangle.Empty |
					intersect2 != Rectangle.Empty |
					intersect3 != Rectangle.Empty |
					intersect4 != Rectangle.Empty
					)
				{
					PlaySound("collision");
					Die();
				}
		}


		private void DrawPipes(PaintEventArgs e)
		{
			if (!ResetPipes && FirstPipe != null &&  SecondPipe != null && FirstPipe.Any() && SecondPipe.Any())
			{
				FirstPipe.DrawPipe(e);
				SecondPipe.DrawPipe(e);

			}

		}
	}
}

