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
	public partial class GameForm {

		private static void PlaySound(string sound)
		{
			var soundtrack = Properties.Resources.GetSound(sound);
			SoundPlayer sp = new SoundPlayer(soundtrack);
			sp.Play();
		}

		private System.Drawing.Bitmap GetImage(string image)
		{
			return Properties.Resources.GetImage(image);
		}

	}
}
