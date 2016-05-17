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
		public ObstaclesMap(int windowHeight, int windowWidth, int obstracleWidth, int horizontInterval, int verticalInterval)
		{
			WindowHeight = windowHeight;
			WindowWidth = windowWidth;
			Count = (windowWidth - obstracleWidth * (windowWidth / horizontInterval)) / horizontInterval + 1; // in the future write normal stabilization
			ObstacleWidth = obstracleWidth;
			ObstaclesHorizontInterval = horizontInterval;
			ObstacleVerticalInterval = verticalInterval;
			MakeObstacles();
			MakeBonuses();
		}
		private void MakeBonuses()
		{
			LevelBonus = new List<Bonus>();
			var bonusTypes = new List<String>();
			bonusTypes.Add("armor");
			bonusTypes.Add("coins");
			bonusTypes.Add("heart");
			bonusTypes.Add("speedBuf");
			for (var i = 0; i < 20; i++)
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