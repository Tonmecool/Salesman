
namespace SalesmanCore.Forms
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.PaintBox = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRegister = new System.Windows.Forms.ToolStripButton();
            this.menuAuthorization = new System.Windows.Forms.ToolStripButton();
            this.menuEdit = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuReset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCircle = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCircleAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCircleMove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCircleDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLine = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLineAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLineMove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuLineDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCalc = new System.Windows.Forms.ToolStripButton();
            this.PaintBox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // PaintBox
            // 
            this.PaintBox.Controls.Add(this.statusStrip1);
            this.PaintBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PaintBox.Location = new System.Drawing.Point(0, 27);
            this.PaintBox.Name = "PaintBox";
            this.PaintBox.Size = new System.Drawing.Size(1354, 714);
            this.PaintBox.TabIndex = 2;
            this.PaintBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintBox_Paint);
            this.PaintBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PaintBox_MouseMove);
            this.PaintBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PaintBox_MouseUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusAction});
            this.statusStrip1.Location = new System.Drawing.Point(0, 688);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1354, 26);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusAction
            // 
            this.statusAction.Name = "statusAction";
            this.statusAction.Size = new System.Drawing.Size(72, 20);
            this.statusAction.Text = "Редактор";
            // 
            // menuFile
            // 
            this.menuFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileNew,
            this.menuFileOpen,
            this.menuFileSave,
            this.menuFileSaveAs});
            this.menuFile.Image = ((System.Drawing.Image)(resources.GetObject("menuFile.Image")));
            this.menuFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(59, 24);
            this.menuFile.Text = "Файл";
            // 
            // menuFileNew
            // 
            this.menuFileNew.Name = "menuFileNew";
            this.menuFileNew.Size = new System.Drawing.Size(201, 26);
            this.menuFileNew.Text = "Новый";
            this.menuFileNew.Click += new System.EventHandler(this.menuFileNew_Click);
            // 
            // menuFileOpen
            // 
            this.menuFileOpen.Name = "menuFileOpen";
            this.menuFileOpen.Size = new System.Drawing.Size(201, 26);
            this.menuFileOpen.Text = "Открыть";
            this.menuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
            // 
            // menuFileSave
            // 
            this.menuFileSave.Name = "menuFileSave";
            this.menuFileSave.Size = new System.Drawing.Size(201, 26);
            this.menuFileSave.Text = "Сохранить";
            this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
            // 
            // menuFileSaveAs
            // 
            this.menuFileSaveAs.Name = "menuFileSaveAs";
            this.menuFileSaveAs.Size = new System.Drawing.Size(201, 26);
            this.menuFileSaveAs.Text = "Сохранить как...";
            this.menuFileSaveAs.Click += new System.EventHandler(this.menuFileSaveAs_Click);
            // 
            // menuRegister
            // 
            this.menuRegister.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.menuRegister.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuRegister.Image = ((System.Drawing.Image)(resources.GetObject("menuRegister.Image")));
            this.menuRegister.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuRegister.Name = "menuRegister";
            this.menuRegister.Size = new System.Drawing.Size(100, 24);
            this.menuRegister.Text = "Регистрация";
            this.menuRegister.Click += new System.EventHandler(this.menuRegister_Click);
            // 
            // menuAuthorization
            // 
            this.menuAuthorization.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.menuAuthorization.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuAuthorization.Image = ((System.Drawing.Image)(resources.GetObject("menuAuthorization.Image")));
            this.menuAuthorization.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuAuthorization.Name = "menuAuthorization";
            this.menuAuthorization.Size = new System.Drawing.Size(105, 24);
            this.menuAuthorization.Text = "Авторизация";
            this.menuAuthorization.Click += new System.EventHandler(this.menuAuthorization_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuReset,
            this.toolStripMenuItem3,
            this.menuCircle,
            this.menuLine});
            this.menuEdit.Image = ((System.Drawing.Image)(resources.GetObject("menuEdit.Image")));
            this.menuEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(86, 24);
            this.menuEdit.Text = "Редактор";
            // 
            // menuReset
            // 
            this.menuReset.Name = "menuReset";
            this.menuReset.Size = new System.Drawing.Size(154, 26);
            this.menuReset.Text = "Сброс";
            this.menuReset.Click += new System.EventHandler(this.menuReset_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(151, 6);
            // 
            // menuCircle
            // 
            this.menuCircle.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCircleAdd,
            this.menuCircleMove,
            this.toolStripMenuItem1,
            this.menuCircleDelete});
            this.menuCircle.Name = "menuCircle";
            this.menuCircle.Size = new System.Drawing.Size(154, 26);
            this.menuCircle.Text = "Позиция";
            // 
            // menuCircleAdd
            // 
            this.menuCircleAdd.Name = "menuCircleAdd";
            this.menuCircleAdd.Size = new System.Drawing.Size(183, 26);
            this.menuCircleAdd.Text = "Добавить";
            this.menuCircleAdd.Click += new System.EventHandler(this.menuCircleAdd_Click);
            // 
            // menuCircleMove
            // 
            this.menuCircleMove.Name = "menuCircleMove";
            this.menuCircleMove.Size = new System.Drawing.Size(183, 26);
            this.menuCircleMove.Text = "Переместить";
            this.menuCircleMove.Click += new System.EventHandler(this.menuCircleMove_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 6);
            // 
            // menuCircleDelete
            // 
            this.menuCircleDelete.Name = "menuCircleDelete";
            this.menuCircleDelete.Size = new System.Drawing.Size(183, 26);
            this.menuCircleDelete.Text = "Удалить";
            this.menuCircleDelete.Click += new System.EventHandler(this.menuCircleDelete_Click);
            // 
            // menuLine
            // 
            this.menuLine.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLineAdd,
            this.menuLineMove,
            this.toolStripMenuItem2,
            this.menuLineDelete});
            this.menuLine.Name = "menuLine";
            this.menuLine.Size = new System.Drawing.Size(154, 26);
            this.menuLine.Text = "Связь";
            // 
            // menuLineAdd
            // 
            this.menuLineAdd.Name = "menuLineAdd";
            this.menuLineAdd.Size = new System.Drawing.Size(183, 26);
            this.menuLineAdd.Text = "Добавить";
            this.menuLineAdd.Click += new System.EventHandler(this.menuLineAdd_Click);
            // 
            // menuLineMove
            // 
            this.menuLineMove.Name = "menuLineMove";
            this.menuLineMove.Size = new System.Drawing.Size(183, 26);
            this.menuLineMove.Text = "Переместить";
            this.menuLineMove.Click += new System.EventHandler(this.menuLineMove_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(180, 6);
            // 
            // menuLineDelete
            // 
            this.menuLineDelete.Name = "menuLineDelete";
            this.menuLineDelete.Size = new System.Drawing.Size(183, 26);
            this.menuLineDelete.Text = "Удалить";
            this.menuLineDelete.Click += new System.EventHandler(this.menuLineDelete_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuRegister,
            this.menuAuthorization,
            this.menuEdit,
            this.toolStripSeparator1,
            this.menuCalc});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1354, 27);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // menuCalc
            // 
            this.menuCalc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuCalc.Image = ((System.Drawing.Image)(resources.GetObject("menuCalc.Image")));
            this.menuCalc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuCalc.Name = "menuCalc";
            this.menuCalc.Size = new System.Drawing.Size(75, 24);
            this.menuCalc.Text = "Решение";
            this.menuCalc.Click += new System.EventHandler(this.menuCalc_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1354, 741);
            this.Controls.Add(this.PaintBox);
            this.Controls.Add(this.toolStrip);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "FormMain";
            this.Text = "Задача коммивояжера";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.PaintBox.ResumeLayout(false);
            this.PaintBox.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Panel PaintBox;
        private System.Windows.Forms.ToolStripDropDownButton menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuFileNew;
        private System.Windows.Forms.ToolStripMenuItem menuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem menuFileSave;
        private System.Windows.Forms.ToolStripMenuItem menuFileSaveAs;
        private System.Windows.Forms.ToolStripButton menuRegister;
        private System.Windows.Forms.ToolStripButton menuAuthorization;
        private System.Windows.Forms.ToolStripDropDownButton menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuReset;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem menuCircle;
        private System.Windows.Forms.ToolStripMenuItem menuCircleAdd;
        private System.Windows.Forms.ToolStripMenuItem menuCircleMove;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuCircleDelete;
        private System.Windows.Forms.ToolStripMenuItem menuLine;
        private System.Windows.Forms.ToolStripMenuItem menuLineAdd;
        private System.Windows.Forms.ToolStripMenuItem menuLineMove;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuLineDelete;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusAction;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton menuCalc;
    }
}

