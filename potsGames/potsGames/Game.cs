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

	public class Pipe {
		//{ TopWidth = value; Propities[0] = value; }
		private bool exist = false;
		
		private int _TopWidth;
		private int _TopLength;
		private int _BottomWidth;
		private int _BottomLength;

		public int TopWidth { get { return _TopWidth; } set { _TopWidth = value; PropitiesInit(); Propities[0] = value; } }
		public int TopLength { get { return _TopLength; } set { _TopLength = value; PropitiesInit(); Propities[1] = value; } }
		public int BottomWidth { get { return _BottomWidth; } set { _BottomWidth = value; PropitiesInit(); Propities[2] = value; } }
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

		public Pipe (int windowHeight , int windowWidth, int distanceBetweenPipes) // To do make normal params to generate pipe 
		{
			PropitiesInit();
			Random random = new Random();
			_TopWidth = windowWidth;
			_TopLength = random.Next(40, (windowHeight - distanceBetweenPipes)); // refact 40 to pipe length min
			_BottomWidth = windowWidth;
			_BottomLength = TopLength + distanceBetweenPipes;
			Propities[0] = _TopWidth;
			Propities[1] = _TopLength;
			Propities[2] = _BottomWidth;
			Propities[3] = _BottomLength;
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
						_TopWidth = value;
						break;
					case 1 :
						_TopLength = value;
						break;
					case 2 :
						_BottomWidth = value;
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
		Pipe FirstPipe;
		Pipe SecondPipe;
		//List<int> SecondPipe = new List<int>();
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
			//FirstPipe.Clear();
		}

		private void StartGame()
		{
			ResetPipes = false;
			timer1.Enabled = true;
			timer2.Enabled = true;
			timer3.Enabled = true;
			//Random random = new Random();
			//int num = random.Next(40, Height - PipeDifferentY);
			//int num1 = num + PipeDifferentY;
	
			//Console.WriteLine("num : {0} , num1 : {1} , Height : {2}, H - pdf :  {3}; width : {4}", num, num1, Height, Height - PipeDifferentY, Width);
			//Console.WriteLine("num : {0} , num1 : {1} , Height : {2}, H - pdf :  {3}", num, num1, Height, Height - PipeDifferentY);
			
			
			//FirstPipe.Clear();
			//FirstPipe.Add(Width);
			//FirstPipe.Add(num);
			//FirstPipe.Add(Width);
			//FirstPipe.Add(num1);
			
			//num = random.Next(40, (Height - PipeDifferentY));
			//num1 = num + PipeDifferentY;
			//SecondPipe.Clear();
			//SecondPipe.Add(Width + PipeDifferentX);
			//SecondPipe.Add(num);
			//SecondPipe.Add(Width + PipeDifferentX);
			//SecondPipe.Add(num1);
			FirstPipe = new Pipe(Height, Width , PipeDifferentY);
			SecondPipe = new Pipe(Height, Width + PipeDifferentX, PipeDifferentY);
			button2.Visible = false;
			button2.Enabled = false;
			Running = true;
			Focus();
		}
		public void InitialPipes()
		{
			if (FirstPipe[0] + PipeWidth <= 0 | Start == true)
			{
				/*Random rnd = new Random();
				int px = this.Width;
				int py = rnd.Next(40, (this.Height - PipeDifferentY));

				var p2y = py + PipeDifferentY;

				Console.WriteLine("num : {0} , num1 : {1} , Height : {2}, H - pdf :  {3}", py, p2y, Height, Height - PipeDifferentY);
				
				
				var p2x = px;
				
				FirstPipe.Clear();
				FirstPipe.Add(px);
				FirstPipe.Add(py);
				FirstPipe.Add(p2x);
				FirstPipe.Add(p2y);*/
				FirstPipe = new Pipe(Height, Width, PipeDifferentY);
			}
			else
			{
				FirstPipe[0] = FirstPipe[0] - 2;
				FirstPipe[2] = FirstPipe[2] - 2;
			}
			if (SecondPipe[0] + PipeWidth <= 0)
			{
				/*Random rnd = new Random();
				int px = this.Width;
				int py = rnd.Next(40, (this.Height - PipeDifferentY));
				var p2x = px;
				var p2y = py + PipeDifferentY;
				Console.WriteLine("num : {0} , num1 : {1} , Height : {2}, H - pdf :  {3}", py, p2y, Height, Height - PipeDifferentY);
				int[] p1 = { px, py, p2x, p2y };
				SecondPipe.Clear();
				SecondPipe.Add(px);
				SecondPipe.Add(py);
				SecondPipe.Add(p2x);
				SecondPipe.Add(p2y);*/
				SecondPipe = new Pipe(Height, Width, PipeDifferentY);
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
				// Первый верхний
				e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(FirstPipe[0], 0, PipeWidth, FirstPipe[1]));
				e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(FirstPipe[0] - 10, FirstPipe[3] - PipeDifferentY, 75, 15));
				// первый нижний
				e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(FirstPipe[2], FirstPipe[3], PipeWidth, this.Height - FirstPipe[3]));
				e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(FirstPipe[2] - 10, FirstPipe[3], 75, 15));
				// второй верхний
				e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(SecondPipe[0], 0, PipeWidth, SecondPipe[1]));
				e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(SecondPipe[0] - 10, SecondPipe[3] - PipeDifferentY, 75, 15));
				// втрой нижний
				e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(SecondPipe[2], SecondPipe[3], PipeWidth, this.Height - SecondPipe[3]));
				e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(SecondPipe[2] - 10, SecondPipe[3], 75, 15));

			}

		}
	}
}

