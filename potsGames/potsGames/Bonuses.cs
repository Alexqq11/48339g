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
		private int lives; //{public get; set;}
		private int armor; //{get; private set;}
		private int coins; //{get; private set;}
		private int speedBuf; //{get; private set;}
		public void Taken(){
			if (Taked)
			{
				X = -5;
				Y = -5;
				Width = 0;
				Height = 0;
				Texture = null;
			}
		}

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

				case "heart":
					lives = Rnd.GetRandomNumber(1 , 5);
					break;
				case "armor":
					armor = 1;
					break;
				case "coins":
					coins = Rnd.GetRandomNumber(10, 100);
					break;
				case "speedBuf":
					speedBuf = 2;
					break;
			}
				
		}


	}


}