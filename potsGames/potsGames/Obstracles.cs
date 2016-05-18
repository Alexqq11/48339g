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

	public class Doors
	{
		public int X {get; set;}
		public int Y { get; set; }
		public int Width { get; set; }
		public bool Interective { get; set;}
		public int DoorWay { get; set; }
		public Point DoorWayCenter { get; set;}
		public int DoorSpeed { get; set;}
		public bool closing { get; set; }
	public 	ObstacleV2 Top { get; set;}
	public 	ObstacleV2 Bottom { get; set;}
		public Doors(string type,int x, int windowHeight)
		{
			DoorWay = 150;
			Width = 65;
			DoorWayCenter = new Point( x, Rnd.GetRandomNumber(DoorWay, windowHeight - DoorWay));
			Top = new ObstacleV2(type, DoorWayCenter.X, 0 - DoorWay / 2, Width, DoorWayCenter.Y);
			Bottom = new ObstacleV2(type, DoorWayCenter.X, DoorWayCenter.Y + DoorWay / 2, Width, windowHeight - DoorWayCenter.Y + DoorWay / 2);
		}
		  
	}

	public class LevelMap
	{
	//	int distanceX = 70;
		//int distanseY = 70;
		//int Count = 50;

	public	List<Bonus> LevelBonus {get; set;}
		public List<ObstacleV2> LevelBoxes {get;  set;}
		public List<Doors> LevelDoors {get; set;}



	public	int ObstacleWidth { get; set; }         //= 55;
	public 	int ObstacleVerticalInterval { get; set; }    //= 140;
	public 	int ObstaclesHorizontInterval { get; set; }
	public  int Count { get; set; } //= 180;
	public 	int WindowWidth { get; set; }
	public 	int WindowHeight { get; set; }
	public 	int Speed { get; set; }

		public LevelMap(int windowHeight, int windowWidth, int obstracleWidth, int horizontInterval, int verticalInterval)
		{
			WindowHeight = windowHeight;
			WindowWidth = windowWidth;
			Count = 40 ;//(windowWidth - obstracleWidth * (windowWidth / horizontInterval)) / horizontInterval + 1; // in the future write normal stabilization
			ObstacleWidth = obstracleWidth;
			ObstaclesHorizontInterval = horizontInterval;
			ObstacleVerticalInterval = verticalInterval;
			MakeDoors();
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
				var bonus = new Bonus(WindowWidth + ObstacleWidth + 10 + i * ObstaclesHorizontInterval, Rnd.GetRandomNumber(WindowHeight / 4, WindowHeight / 4 * 3), 40, 40, bonusTypes[Rnd.GetRandomNumber(0, 4)]);
				LevelBonus.Add(bonus);
			}

		}
		public void MakeBoxes()
		{
			LevelBoxes = new List<ObstacleV2>();

			for (var i = 0; i < Count; i++)
			{
				var box = new ObstacleV2("box", WindowWidth + ObstacleWidth + 80 + i * ObstaclesHorizontInterval, Rnd.GetRandomNumber(0, WindowHeight - 100), 60, 60);
				LevelBoxes.Add(box);
			}

		}


		private void MakeDoors()
		{
			var Textures = new Dictionary<int,string>();
			Textures[0]="box"; //] = boxTexture;
			Textures[1]="bluePipe";//] = bluePipeTexture;
			Textures[2]="redPipe";//] = redPipeTexture;
			Textures[3]="greenPipe";//] = greenPipeTexture;
			Textures[4]="yellowPipe";//] = yellowPipeTexture;
			LevelDoors = new List<Doors>();

			for (var i = 0; i < Count; i++)
			{
				var pipe = new Doors(Textures[Rnd.GetRandomNumber(0, 5)], WindowWidth + i * ObstaclesHorizontInterval, WindowHeight);
				LevelDoors.Add(pipe);
			}
		}
	}

	public class ObstacleV2
	{
		public bool Physic {get; set;}
		public bool Exist { get; set; }
		public int Gravity {get; set;}
		public int GameSpeed {get; set;}

		public int X {get; set;}
		public int Y {get; set;}
		public int Width {get; set;}
		public int Height {get; set;}
		public int Strength {get; set;}
		public int Damage {get; set;}
		private System.Drawing.Bitmap Texture;
		public int Points { get; private set;} 
		string Name;
		Dictionary<string, Dictionary<int, string>> Textures;

		public Player Collision(Player pots)
		{
			if (Exist)
				if (pots.Armor)
				{
					Strength = 0;
					pots.Armor = false;
					SetTexture();
					pots.Points += Points;
					Exist = false;
					Physic = true;
					Gravity = 10;

				}
				else
				{
					Strength = Strength - Damage;
					pots.Lives = pots.Lives - Damage % 4;
					if (pots.Lives > 0)
					{
						SetTexture();
						if (Strength == 0)
						{
							pots.Points += Points;
							Exist = false;
							Physic = true;
							Gravity = 10;
						}
					}
					else pots.Alive = false;

				}
			return pots;
		}
		
		private void TextureInit()
		{
			Textures = new Dictionary<string, Dictionary<int, string>>();
			var boxTexture = new Dictionary<int , string>();
				boxTexture[0] = "box_crash";
				boxTexture[1] = "box";
			var redPipeTexture = new Dictionary<int , string>();
				redPipeTexture[0] = "red_crash3"; 
				redPipeTexture[1] = "red_crash2"; 
				redPipeTexture[2] = "red_crash"; 
				redPipeTexture[3] = "red";
			var greenPipeTexture = new Dictionary<int , string>();
				greenPipeTexture[0] = "green_crash3"; 
				greenPipeTexture[1] = "green_crash2"; 
				greenPipeTexture[2] = "green_crash"; 
				greenPipeTexture[3] = "green";
			var bluePipeTexture = new Dictionary<int , string>();
				bluePipeTexture[0] = "blue_crash3"; 
				bluePipeTexture[1] = "blue_crash2"; 
				bluePipeTexture[2] = "blue_crash"; 
				bluePipeTexture[3] = "blue";
			var yellowPipeTexture = new Dictionary<int, string>();
				yellowPipeTexture[0] = "yellow_crash3";
				yellowPipeTexture[1] = "yellow_crash2";
				yellowPipeTexture[2] = "yellow_crash";
				yellowPipeTexture[3] = "yellow";
			Textures["box"] = boxTexture;
			Textures["bluePipe"] = bluePipeTexture;
			Textures["redPipe"] = redPipeTexture;
			Textures["greenPipe"] = greenPipeTexture;
			Textures["yellowPipe"] = yellowPipeTexture;

		}
 		public void  Update(int gameSpeed, int gravity,bool physic)
		{
			if (physic)
				MoveDown(gravity);
			MoveBackward(gameSpeed);
			GameSpeed = gameSpeed;
			Gravity = gravity;
			Physic = physic;
		}
		public void Update(int gameSpeed)
		{
			if (Physic)
				MoveDown(Gravity);
			MoveBackward(gameSpeed);
		}
		public void Update()
		{
			if (Physic)
				MoveDown(Gravity);
			MoveBackward(GameSpeed);
		}
		public ObstacleV2(string name, int x, int y, int width, int height)
		{
			Exist = true;
			X = x;
			Y = y;
			Width = width;
			Height = height;
			Name = name;
			TextureInit();
			setStrength(name);
			SetTexture();
			SetPoints();
		}
		private void setStrength(string name){
			switch(name){
				case "box":
					Strength = 1;
					Damage = 1;
					break;
				case "greenPipe":
					Strength = 3;
					Damage = 1;
					break;
				case "yellowPipe":
					Strength = 6;
					Damage = 2;
					break;
				case "redPipe":
					Strength = 9;
					Damage = 3;
					break;
				case "bluePipe":
					Strength = 45;
					Damage = 15;
					break;

			}
 
		}
		private void SetTexture(){
			Texture = Properties.Resources.GetImage(Textures[Name][Strength / Damage]);
		}
  
		public void DrawTexture(PaintEventArgs e )//, int windowWidth, int windowHeight)
		{	//if (Exist &&  Y > windowHeight + 10)
			e.Graphics.DrawImage(Texture,X,Y,Width,Height);
		}
		private void SetPoints()
		{
			Points = (Strength * Damage) % (Damage + Damage / 2) + 3;
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