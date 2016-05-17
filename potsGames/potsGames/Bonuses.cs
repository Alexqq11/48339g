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
	public class Bonus
	{
		public bool Taked = false;
		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set;}
		public int Height {  get; set; }
		string bonusType;
		string BonusType { get { return bonusType; } set { bonusType = value; Texture = Properties.Resources.GetImage(value); } }
		public int Lives {get {return lives;}}
		public int Armor {get {return armor;}}
		public int Coins {get {return coins;}}
		public int SpeedBuf {get {return speedBuf;}}
		private int lives = 0; //{public get; set;}
		private int armor = 0; //{get; private set;}
		private int coins = 0; //{get; private set;}
		private int speedBuf = 0; //{get; private set;}
		public Bonus(int x , int y, int width, int height, string bonusType)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			BonusType = bonusType;
			SetBonusCharacteristic(bonusType);
		}
		private System.Drawing.Bitmap Texture;

		public void DrawBonus(PaintEventArgs e)
		{

			e.Graphics.DrawImage(Texture, X, Y, Width, Height);
		}
		///var topPipe = Properties.Resources.GetImage("yellow_pipe");
		//topPipe.RotateFlip(RotateFlipType.RotateNoneFlipXY);
		//e.Graphics.DrawImage(topPipe, TopX, 0, ObstacleWidth, TopY + 15);

		private void SetBonusCharacteristic(string bonusType)
		{
			switch (bonusType)
			{

				case "Lives":
					lives = 1;
					break;
				case "Armor":
					armor = 1;
					break;
				case "Coins":
					coins = Rnd.GetRandomNumber(10, 100);
					break;
				case "SpeedBuf":
					speedBuf = 2;
					break;
			}
				
		}


	}


}