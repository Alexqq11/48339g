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

	public class Player
	{
		public int Lives { get; set; }
		public int Coins { get; set; }
		public bool Alive { get; set; }
		public Point StartLocation { get; set; }
		public int SpeedY { get; set; }
		public int SpeedX { get; set; }
		public bool inPointZone { get; set; }
		public int Armor { get; set; }
		public Player(int speedX, int speedY, Point startLocation)
		{
			Alive = true;
			SpeedX = speedX;
			SpeedY = speedY;
			inPointZone = false;
			StartLocation = startLocation;

		}

	}
}