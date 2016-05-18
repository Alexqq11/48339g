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
	public static class Rnd
	{
		static Random rnd = new Random();
		static public int GetRandomNumber(int start, int end)
		{

			return rnd.Next(start, end);
		}
	}
	public class LevelMap
	{
		List<Bonus> LevelBonuses;
		List<ObstacleV2> LevelObstaclesPipes;
		List<ObstacleV2> LevelFlyBoxex;
		List<ObstacleV2> LevelDoors; 
		//LevelMap  = 
	}

	public class ObstacleV2
	{
		public int X {get; set;}
		public int Y {get; set;}
		public int Width {get; set;}
		public int Height {get; set;}
		public int strength {get; set;}
		private System.Drawing.Bitmap Texture;
		int Points;
		string Name;
		Dictionary<int, String> Textures; 
		public ObstacleV2(string name, int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			Name = name;
			SetTexture(name);
		}
		
		private void SetTexture(string name){
			Texture = Properties.Resources.GetImage(name);
		}
  
		public void DrawTexture(PaintEventArgs e)
		{
			e.Graphics.DrawImage(Texture,X,Y,Width,Height);
		}

		private void Movement(int x, int y)
		{
			X = X + x;
			Y = Y + y;

		}

		public void Update(int gameSpeed, int Gravity)
		{
			Movement(-gameSpeed, Gravity);
		}
		public void MoveForward(int step)
		{
			Movement(step,0);
		}
		public void MoveBackward(int step)
		{
			Movement(-step,0);
		}
		public void  MoveUp(int step)
		{
			Movement(0, -step);
		}
		public void MoveDown(int step)
		{
			Movement(0, step);
		}


	}

	public class Obstacle
	{
		public int ObstacleWidth { get; set; }
		public int ObstacleVerticalInterval { get; set; }
		public int WindowHeight { get; set; }

		public int TopX { get; set; }
		public int TopY { get; set; }
		public int BottomX { get; set; }
		public int BottomLength { get; set; }
		// сильно зависим от параметров окна //

		public Obstacle(int windowHeight, int windowWidth, int pipeWidth, int distanceBetweenPipes) // To do make normal params to generate pipe 
		{
			TopX = windowWidth;//sreen possition



			TopY = Rnd.GetRandomNumber(40, (windowHeight - distanceBetweenPipes)); // refact 40 to pipe length min
			Console.WriteLine(TopY);
			BottomX = windowWidth;//screen posirion
			BottomLength = TopY + distanceBetweenPipes;
			ObstacleWidth = pipeWidth;
			ObstacleVerticalInterval = distanceBetweenPipes;
			WindowHeight = windowHeight;
		}

		public void DrawPipe(PaintEventArgs e)
		{
			var bottomPipe = Properties.Resources.GetImage("yellow_pipe");
			var topPipe = Properties.Resources.GetImage("yellow_pipe");
			topPipe.RotateFlip(RotateFlipType.RotateNoneFlipXY);
			e.Graphics.DrawImage(topPipe, TopX, 0, ObstacleWidth, TopY + 15);
			e.Graphics.DrawImage(bottomPipe, BottomX, BottomLength, ObstacleWidth, WindowHeight - BottomLength);

		}
		public void DrawTest(PaintEventArgs e)
		{
			//e.Graphics.DrawImage(Image i);
		}
		//public void DrawBox(PaintEventArgs)

	}

	public class ObstaclesMap
	{

		int ObstacleWidth { get; set; }         //= 55;
		int ObstacleVerticalInterval { get; set; }    //= 140;
		int ObstaclesHorizontInterval { get; set; }
		public int Count { get; set; } //= 180;
		int WindowWidth { get; set; }
		int WindowHeight { get; set; }
		int Speed { get; set; }

		public List<Obstacle> LevelObstacles { get; set; }
		public List<Bonus> LevelBonus { get; set; } 
		public List<ObstacleV2> LevelBoxes {get; set;}
		public void MakeBoxes(){
		
		
			LevelBoxes = new List<ObstacleV2>();

			for (var i = 0; i < Count; i++)
			{
				var box = new ObstacleV2("box",WindowWidth + ObstacleWidth + 80 + i * ObstaclesHorizontInterval, Rnd.GetRandomNumber(0 , WindowHeight - 100 ), 60 , 60 );
				LevelBoxes.Add(box);
			}
		
		}
			
		public ObstaclesMap(int windowHeight, int windowWidth, int obstracleWidth, int horizontInterval, int verticalInterval)
		{
			WindowHeight = windowHeight;
			WindowWidth = windowWidth;
			Count = 40 ;//(windowWidth - obstracleWidth * (windowWidth / horizontInterval)) / horizontInterval + 1; // in the future write normal stabilization
			ObstacleWidth = obstracleWidth;
			ObstaclesHorizontInterval = horizontInterval;
			ObstacleVerticalInterval = verticalInterval;
			MakeObstacles();
			MakeBonuses();
			MakeBoxes();
		}
		private void MakeBonuses()
		{
			LevelBonus = new List<Bonus>();
			var bonusTypes = new List<String>();
			bonusTypes.Add("armor");
			bonusTypes.Add("coins");
			bonusTypes.Add("heart");
			bonusTypes.Add("speedBuf");
			for (var i = 0; i < Count; i++)
			{
				var bonus  = new Bonus(WindowWidth + ObstacleWidth + 10 + i * ObstaclesHorizontInterval, Rnd.GetRandomNumber(WindowHeight / 4, WindowHeight / 4 * 3 ), 40, 40, bonusTypes[Rnd.GetRandomNumber(0,4)]);
				LevelBonus.Add(bonus);
			}

		}

		private void MakeObstacles()
		{

			LevelObstacles = new List<Obstacle>();

			for (var i = 0; i < Count; i++)
			{
				var pipe = new Obstacle(WindowHeight, WindowWidth + i * ObstaclesHorizontInterval, ObstacleWidth, ObstacleVerticalInterval);
				LevelObstacles.Add(pipe);
			}
		}
		public void UpdateObstracles()
		{
			for (var i = 0; i < Count; i++)
			{
				if (LevelObstacles.Any())
					LevelObstacles[i] = new Obstacle(WindowHeight, WindowWidth + i * ObstaclesHorizontInterval, ObstacleWidth, ObstacleVerticalInterval);
			}
		}

		public Obstacle this[int i] // write exception
		{
			get { return LevelObstacles[i]; }
			set { LevelObstacles[i] = value; }
		}
	}

}