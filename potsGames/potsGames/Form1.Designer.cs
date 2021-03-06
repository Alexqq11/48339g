﻿namespace potsGames
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
			System.Windows.Forms.Label LivesPanel;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
			this.mainBird = new System.Windows.Forms.PictureBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.timer3 = new System.Windows.Forms.Timer(this.components);
			this.pointWindow = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.Coins = new System.Windows.Forms.Label();
			this.FirstPots = new System.Windows.Forms.Button();
			this.SecondPots = new System.Windows.Forms.Button();
			this.LivesPanel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.mainBird)).BeginInit();
			this.SuspendLayout();
			// 
			// LivesPanel
			// 
			this.LivesPanel.AutoSize = true;
			this.LivesPanel.BackColor = System.Drawing.Color.Transparent;
			this.LivesPanel.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)), true);
			this.LivesPanel.ForeColor = System.Drawing.Color.Crimson;
			this.LivesPanel.Location = new System.Drawing.Point(-2, 8);
			this.LivesPanel.Name = "LivesPanel";
			this.LivesPanel.Size = new System.Drawing.Size(0, 31);
			this.LivesPanel.TabIndex = 4;
			// 
			// mainBird
			// 
			this.mainBird.BackColor = System.Drawing.Color.Transparent;
			this.mainBird.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.mainBird.Enabled = false;
			this.mainBird.Location = new System.Drawing.Point(25, 157);
			this.mainBird.Name = "mainBird";
			this.mainBird.Size = new System.Drawing.Size(69, 72);
			this.mainBird.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mainBird.TabIndex = 1;
			this.mainBird.TabStop = false;
			this.mainBird.Visible = false;
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
			// pointWindow
			// 
			this.pointWindow.AutoSize = true;
			this.pointWindow.BackColor = System.Drawing.Color.Transparent;
			this.pointWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.pointWindow.ForeColor = System.Drawing.Color.Black;
			this.pointWindow.Location = new System.Drawing.Point(454, 10);
			this.pointWindow.Name = "pointWindow";
			this.pointWindow.Size = new System.Drawing.Size(0, 29);
			this.pointWindow.TabIndex = 2;
			this.pointWindow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button2
			// 
			this.button2.Enabled = false;
			this.button2.ForeColor = System.Drawing.Color.Maroon;
			this.button2.Location = new System.Drawing.Point(204, 392);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(210, 54);
			this.button2.TabIndex = 3;
			this.button2.Text = "START";
			this.button2.UseCompatibleTextRendering = true;
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Visible = false;
			this.button2.Click += new System.EventHandler(this.GameStarter);
			// 
			// Coins
			// 
			this.Coins.AutoSize = true;
			this.Coins.BackColor = System.Drawing.Color.Transparent;
			this.Coins.Font = new System.Drawing.Font("Comic Sans MS", 16F);
			this.Coins.ForeColor = System.Drawing.Color.Gold;
			this.Coins.Location = new System.Drawing.Point(221, 8);
			this.Coins.Name = "Coins";
			this.Coins.Size = new System.Drawing.Size(0, 30);
			this.Coins.TabIndex = 5;
			// 
			// FirstPots
			// 
			this.FirstPots.BackColor = System.Drawing.Color.Transparent;
			this.FirstPots.BackgroundImage = global::potsGames.Properties.Resources.pots_1_1_protected;
			this.FirstPots.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.FirstPots.Font = new System.Drawing.Font("Comic Sans MS", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FirstPots.ForeColor = System.Drawing.Color.Crimson;
			this.FirstPots.Location = new System.Drawing.Point(117, 146);
			this.FirstPots.Name = "FirstPots";
			this.FirstPots.Size = new System.Drawing.Size(163, 156);
			this.FirstPots.TabIndex = 6;
			this.FirstPots.Text = "Select";
			this.FirstPots.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.FirstPots.UseVisualStyleBackColor = false;
			this.FirstPots.Click += new System.EventHandler(this.SelectFirstPots);
			// 
			// SecondPots
			// 
			this.SecondPots.BackColor = System.Drawing.Color.Transparent;
			this.SecondPots.BackgroundImage = global::potsGames.Properties.Resources.pots_2_1_protected;
			this.SecondPots.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.SecondPots.Font = new System.Drawing.Font("Comic Sans MS", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.SecondPots.ForeColor = System.Drawing.Color.Crimson;
			this.SecondPots.Location = new System.Drawing.Point(348, 146);
			this.SecondPots.Name = "SecondPots";
			this.SecondPots.Size = new System.Drawing.Size(154, 156);
			this.SecondPots.TabIndex = 7;
			this.SecondPots.Text = "Select";
			this.SecondPots.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.SecondPots.UseVisualStyleBackColor = false;
			this.SecondPots.Click += new System.EventHandler(this.SelectSecondPots);
			// 
			// GameForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::potsGames.Properties.Resources.back;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(625, 486);
			this.Controls.Add(this.SecondPots);
			this.Controls.Add(this.FirstPots);
			this.Controls.Add(this.Coins);
			this.Controls.Add(this.LivesPanel);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.pointWindow);
			this.Controls.Add(this.mainBird);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "GameForm";
			this.Text = "Flappy Yurii";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.GamePainter);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
			((System.ComponentModel.ISupportInitialize)(this.mainBird)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox mainBird;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Timer timer2;
		private System.Windows.Forms.Timer timer3;
		private System.Windows.Forms.Label pointWindow;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label Coins;
		private System.Windows.Forms.Button FirstPots;
		private System.Windows.Forms.Button SecondPots;
		private System.Windows.Forms.Label LivesPanel;
	}
}

