namespace SHSchool.Retake.Form
{
    partial class SCSelectDistribution
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnDistribution = new DevComponents.DotNetBar.ButtonX();
            this.dgData = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.手動更正分發課程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除分發課程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.手動輸入無法分發原因ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.progressBarX1 = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDistribution
            // 
            this.btnDistribution.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDistribution.AutoSize = true;
            this.btnDistribution.BackColor = System.Drawing.Color.Transparent;
            this.btnDistribution.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDistribution.Location = new System.Drawing.Point(830, 417);
            this.btnDistribution.Name = "btnDistribution";
            this.btnDistribution.Size = new System.Drawing.Size(82, 25);
            this.btnDistribution.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDistribution.TabIndex = 8;
            this.btnDistribution.Text = "自動分發";
            this.btnDistribution.Click += new System.EventHandler(this.btnDistribution_Click);
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgData.BackgroundColor = System.Drawing.Color.White;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
            this.dgData.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgData.HighlightSelectedColumnHeaders = false;
            this.dgData.Location = new System.Drawing.Point(12, 41);
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            this.dgData.RowHeadersVisible = false;
            this.dgData.RowTemplate.Height = 24;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(988, 370);
            this.dgData.TabIndex = 9;
            this.dgData.SelectionChanged += new System.EventHandler(this.dgData_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "班級";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 90;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "座號";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "學號";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "姓名";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "科別";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 110;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "科目";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 110;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "級別";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 80;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "學分數";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 80;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column9.HeaderText = "分發課程";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.手動更正分發課程ToolStripMenuItem,
            this.清除分發課程ToolStripMenuItem,
            this.toolStripSeparator1,
            this.手動輸入無法分發原因ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(171, 98);
            // 
            // 手動更正分發課程ToolStripMenuItem
            // 
            this.手動更正分發課程ToolStripMenuItem.Enabled = false;
            this.手動更正分發課程ToolStripMenuItem.Name = "手動更正分發課程ToolStripMenuItem";
            this.手動更正分發課程ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.手動更正分發課程ToolStripMenuItem.Text = "調整分發課程";
            this.手動更正分發課程ToolStripMenuItem.Click += new System.EventHandler(this.手動更正分發課程ToolStripMenuItem_Click);
            // 
            // 清除分發課程ToolStripMenuItem
            // 
            this.清除分發課程ToolStripMenuItem.Enabled = false;
            this.清除分發課程ToolStripMenuItem.Name = "清除分發課程ToolStripMenuItem";
            this.清除分發課程ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.清除分發課程ToolStripMenuItem.Text = "清除分發課程";
            this.清除分發課程ToolStripMenuItem.Click += new System.EventHandler(this.清除分發課程ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(167, 6);
            // 
            // 手動輸入無法分發原因ToolStripMenuItem
            // 
            this.手動輸入無法分發原因ToolStripMenuItem.Enabled = false;
            this.手動輸入無法分發原因ToolStripMenuItem.Name = "手動輸入無法分發原因ToolStripMenuItem";
            this.手動輸入無法分發原因ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.手動輸入無法分發原因ToolStripMenuItem.Text = "調整無法分發原因";
            this.手動輸入無法分發原因ToolStripMenuItem.Click += new System.EventHandler(this.手動輸入無法分發原因ToolStripMenuItem_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(918, 417);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 25);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(707, 10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(157, 21);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "篩選尚未分發課程學生";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 13;
            this.labelX1.Text = "排序方式:";
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Mode";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 19;
            this.comboBoxEx1.Location = new System.Drawing.Point(93, 10);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(121, 25);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 16;
            this.comboBoxEx1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEx1_SelectedIndexChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.AutoSize = true;
            this.checkBox2.BackColor = System.Drawing.Color.Transparent;
            this.checkBox2.Location = new System.Drawing.Point(870, 10);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(131, 21);
            this.checkBox2.TabIndex = 17;
            this.checkBox2.Text = "篩選本次調整學生";
            this.checkBox2.UseVisualStyleBackColor = false;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // progressBarX1
            // 
            this.progressBarX1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.progressBarX1.BackgroundStyle.Class = "";
            this.progressBarX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progressBarX1.Location = new System.Drawing.Point(12, 418);
            this.progressBarX1.Name = "progressBarX1";
            this.progressBarX1.Size = new System.Drawing.Size(812, 23);
            this.progressBarX1.TabIndex = 18;
            this.progressBarX1.Text = "progressBarX1";
            this.progressBarX1.Visible = false;
            // 
            // labelX2
            // 
            this.labelX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 421);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(154, 21);
            this.labelX2.TabIndex = 19;
            this.labelX2.Text = "滑鼠右鍵可進行手動調整";
            // 
            // SCSelectDistribution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 454);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.comboBoxEx1);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.btnDistribution);
            this.Controls.Add(this.progressBarX1);
            this.DoubleBuffered = true;
            this.MaximizeBox = true;
            this.Name = "SCSelectDistribution";
            this.Text = "課程自動分發";
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnDistribution;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgData;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudentNumber;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 手動輸入無法分發原因ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 手動更正分發課程ToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private System.Windows.Forms.CheckBox checkBox2;
        private DevComponents.DotNetBar.Controls.ProgressBarX progressBarX1;
        private System.Windows.Forms.ToolStripMenuItem 清除分發課程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private DevComponents.DotNetBar.LabelX labelX2;
    }
}