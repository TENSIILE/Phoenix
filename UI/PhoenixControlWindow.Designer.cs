namespace Phoenix.UI
{
    partial class PhoenixControlWindow
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menu = new System.Windows.Forms.Panel();
            this.pictureIcon = new System.Windows.Forms.PictureBox();
            this.labelCaption = new System.Windows.Forms.Label();
            this.pictureMinimize = new System.Windows.Forms.PictureBox();
            this.pictureMaximize = new System.Windows.Forms.PictureBox();
            this.pictureClose = new System.Windows.Forms.PictureBox();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMaximize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureClose)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menu.BackColor = System.Drawing.Color.Black;
            this.menu.Controls.Add(this.pictureIcon);
            this.menu.Controls.Add(this.labelCaption);
            this.menu.Controls.Add(this.pictureMinimize);
            this.menu.Controls.Add(this.pictureMaximize);
            this.menu.Controls.Add(this.pictureClose);
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(333, 30);
            this.menu.TabIndex = 0;
            // 
            // pictureIcon
            // 
            this.pictureIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureIcon.Location = new System.Drawing.Point(0, 0);
            this.pictureIcon.Name = "pictureIcon";
            this.pictureIcon.Size = new System.Drawing.Size(30, 30);
            this.pictureIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureIcon.TabIndex = 4;
            this.pictureIcon.TabStop = false;
            // 
            // labelCaption
            // 
            this.labelCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCaption.ForeColor = System.Drawing.Color.White;
            this.labelCaption.Location = new System.Drawing.Point(0, 0);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(261, 30);
            this.labelCaption.TabIndex = 3;
            this.labelCaption.Text = "Название окна";
            this.labelCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelCaption.DoubleClick += new System.EventHandler(this.LabelCaption_DoubleClick);
            this.labelCaption.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LabelCaption_MouseDown);
            this.labelCaption.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LabelCaption_MouseMove);
            this.labelCaption.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LabelCaption_MouseUp);
            // 
            // pictureMinimize
            // 
            this.pictureMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureMinimize.Image = global::Phoenix.Properties.Resources.icon_minimize;
            this.pictureMinimize.Location = new System.Drawing.Point(261, 0);
            this.pictureMinimize.Name = "pictureMinimize";
            this.pictureMinimize.Size = new System.Drawing.Size(24, 30);
            this.pictureMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureMinimize.TabIndex = 2;
            this.pictureMinimize.TabStop = false;
            this.pictureMinimize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureMinimize_MouseDown);
            this.pictureMinimize.MouseEnter += new System.EventHandler(this.PictureMinimize_MouseEnter);
            this.pictureMinimize.MouseLeave += new System.EventHandler(this.PictureMinimize_MouseLeave);
            this.pictureMinimize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureMinimize_MouseUp);
            // 
            // pictureMaximize
            // 
            this.pictureMaximize.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureMaximize.Image = global::Phoenix.Properties.Resources.icon_maximize;
            this.pictureMaximize.Location = new System.Drawing.Point(285, 0);
            this.pictureMaximize.Name = "pictureMaximize";
            this.pictureMaximize.Size = new System.Drawing.Size(24, 30);
            this.pictureMaximize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureMaximize.TabIndex = 1;
            this.pictureMaximize.TabStop = false;
            this.pictureMaximize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureMaximize_MouseDown);
            this.pictureMaximize.MouseEnter += new System.EventHandler(this.PictureMaximize_MouseEnter);
            this.pictureMaximize.MouseLeave += new System.EventHandler(this.PictureMaximize_MouseLeave);
            this.pictureMaximize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureMaximize_MouseUp);
            // 
            // pictureClose
            // 
            this.pictureClose.BackColor = System.Drawing.Color.Transparent;
            this.pictureClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureClose.Image = global::Phoenix.Properties.Resources.icon_close;
            this.pictureClose.Location = new System.Drawing.Point(309, 0);
            this.pictureClose.Name = "pictureClose";
            this.pictureClose.Size = new System.Drawing.Size(24, 30);
            this.pictureClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureClose.TabIndex = 0;
            this.pictureClose.TabStop = false;
            this.pictureClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureClose_MouseDown);
            this.pictureClose.MouseEnter += new System.EventHandler(this.PictureClose_MouseEnter);
            this.pictureClose.MouseLeave += new System.EventHandler(this.PictureClose_MouseLeave);
            this.pictureClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureClose_MouseUp);
            // 
            // PhoenixControlWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.menu);
            this.Name = "PhoenixControlWindow";
            this.Size = new System.Drawing.Size(333, 30);
            this.menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMaximize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel menu;
        private System.Windows.Forms.PictureBox pictureMinimize;
        private System.Windows.Forms.PictureBox pictureMaximize;
        private System.Windows.Forms.PictureBox pictureClose;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.PictureBox pictureIcon;
    }
}
