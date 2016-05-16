namespace potsGames
{
	partial class GameForm
	{
		/// <summary>
		/// Требуется переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
			this.mainBird = new System.Windows.Forms.PictureBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.timer3 = new System.Windows.Forms.Timer(this.components);
			this.pointWindow = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.mainBird)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.mainBird.BackColor = System.Drawing.Color.Transparent;
			this.mainBird.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.mainBird.Image = global::potsGames.Properties.Resources.GetImage("bird_straight");
			this.mainBird.Location = new System.Drawing.Point(26, 76);
			this.mainBird.Name = "pictureBox1";
			this.mainBird.Size = new System.Drawing.Size(31, 34);
			this.mainBird.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mainBird.TabIndex = 1;
			this.mainBird.TabStop = false;
			// 
			// timer1
			// 
			this.timer1.Interval = 1;
			this.timer1.Tick += new System.EventHandler(this.OnTick);
			// 
			// timer2
			// 
			this.timer2.Interval = 1;
			this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// timer3
			// 
			this.timer3.Interval = 1;
			this.timer3.Tick += new System.EventHandler(this.Timer3Tick);
			// 
			// label1
			// 
			this.pointWindow.AutoSize = true;
			this.pointWindow.BackColor = System.Drawing.Color.Transparent;
			this.pointWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.pointWindow.ForeColor = System.Drawing.Color.Red;
			this.pointWindow.Location = new System.Drawing.Point(239, 9);
			this.pointWindow.Name = "label1";
			this.pointWindow.Size = new System.Drawing.Size(26, 29);
			this.pointWindow.TabIndex = 2;
			this.pointWindow.Text = "0";
			this.pointWindow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button2
			// 
			this.button2.ForeColor = System.Drawing.Color.Maroon;
			this.button2.Location = new System.Drawing.Point(127, 225);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(210, 54);
			this.button2.TabIndex = 3;
			this.button2.Text = "START";
			this.button2.UseCompatibleTextRendering = true;
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.GameStarter);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::potsGames.Properties.Resources.GetImage("back");
			this.ClientSize = new System.Drawing.Size(482, 324);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.pointWindow);
			this.Controls.Add(this.mainBird);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "Flappy Yurii";
			this.Load += new System.EventHandler(this.InitPosition);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.GamePainter);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
			((System.ComponentModel.ISupportInitialize)(this.mainBird)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = true;
		}

		#endregion

		private System.Windows.Forms.PictureBox mainBird;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Timer timer2;
		private System.Windows.Forms.Timer timer3;
		private System.Windows.Forms.Label pointWindow;
		private System.Windows.Forms.Button button2;
	}
}

