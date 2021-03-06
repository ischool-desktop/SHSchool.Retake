﻿namespace SHSchool.Retake.Form
{
    partial class SubjectListForm
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
            this.dgData = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colSubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubjectLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDept = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.colSubjectType = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.批次修改科別ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.批次修改科目類別ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTitle = new DevComponents.DotNetBar.LabelX();
            this.btnGetSuggestSubject = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.lblCount = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgData
            // 
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgData.BackgroundColor = System.Drawing.Color.White;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSubjectName,
            this.colSubjectLevel,
            this.colCredit,
            this.colDept,
            this.colSubjectType});
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
            this.dgData.Location = new System.Drawing.Point(7, 39);
            this.dgData.Name = "dgData";
            this.dgData.RowTemplate.Height = 24;
            this.dgData.Size = new System.Drawing.Size(817, 328);
            this.dgData.TabIndex = 0;
            this.dgData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_CellEndEdit);
            this.dgData.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgData_CellMouseDoubleClick);
            this.dgData.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgData_UserDeletingRow);
            // 
            // colSubjectName
            // 
            this.colSubjectName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSubjectName.HeaderText = "科目名稱";
            this.colSubjectName.Name = "colSubjectName";
            // 
            // colSubjectLevel
            // 
            this.colSubjectLevel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colSubjectLevel.HeaderText = "級別";
            this.colSubjectLevel.Name = "colSubjectLevel";
            this.colSubjectLevel.Width = 59;
            // 
            // colCredit
            // 
            this.colCredit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colCredit.HeaderText = "學分數";
            this.colCredit.Name = "colCredit";
            this.colCredit.Width = 72;
            // 
            // colDept
            // 
            this.colDept.DisplayMember = "Text";
            this.colDept.DropDownHeight = 106;
            this.colDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colDept.DropDownWidth = 121;
            this.colDept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colDept.HeaderText = "科別";
            this.colDept.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.colDept.IntegralHeight = false;
            this.colDept.ItemHeight = 17;
            this.colDept.Name = "colDept";
            this.colDept.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDept.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.colDept.Width = 115;
            // 
            // colSubjectType
            // 
            this.colSubjectType.DisplayMember = "Text";
            this.colSubjectType.DropDownHeight = 106;
            this.colSubjectType.DropDownWidth = 121;
            this.colSubjectType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colSubjectType.HeaderText = "科目類別";
            this.colSubjectType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.colSubjectType.IntegralHeight = false;
            this.colSubjectType.ItemHeight = 17;
            this.colSubjectType.Name = "colSubjectType";
            this.colSubjectType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSubjectType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.colSubjectType.Width = 85;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.批次修改科別ToolStripMenuItem,
            this.批次修改科目類別ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(171, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 批次修改科別ToolStripMenuItem
            // 
            this.批次修改科別ToolStripMenuItem.Name = "批次修改科別ToolStripMenuItem";
            this.批次修改科別ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.批次修改科別ToolStripMenuItem.Text = "批次修改科別";
            this.批次修改科別ToolStripMenuItem.Click += new System.EventHandler(this.批次修改科別ToolStripMenuItem_Click);
            // 
            // 批次修改科目類別ToolStripMenuItem
            // 
            this.批次修改科目類別ToolStripMenuItem.Name = "批次修改科目類別ToolStripMenuItem";
            this.批次修改科目類別ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.批次修改科目類別ToolStripMenuItem.Text = "批次修改科目類別";
            this.批次修改科目類別ToolStripMenuItem.Click += new System.EventHandler(this.批次修改科目類別ToolStripMenuItem_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblTitle.BackgroundStyle.Class = "";
            this.lblTitle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTitle.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTitle.Location = new System.Drawing.Point(7, 7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(156, 26);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "學年度　學期　梯次";
            // 
            // btnGetSuggestSubject
            // 
            this.btnGetSuggestSubject.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGetSuggestSubject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetSuggestSubject.BackColor = System.Drawing.Color.Transparent;
            this.btnGetSuggestSubject.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGetSuggestSubject.Location = new System.Drawing.Point(654, 8);
            this.btnGetSuggestSubject.Name = "btnGetSuggestSubject";
            this.btnGetSuggestSubject.Size = new System.Drawing.Size(170, 25);
            this.btnGetSuggestSubject.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnGetSuggestSubject.TabIndex = 2;
            this.btnGetSuggestSubject.Text = "取得建議重修科目";
            this.btnGetSuggestSubject.Click += new System.EventHandler(this.btnGetSuggestSubject_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(742, 400);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(654, 400);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 25);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.AutoSize = true;
            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblCount.BackgroundStyle.Class = "";
            this.lblCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblCount.Location = new System.Drawing.Point(524, 12);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(74, 21);
            this.lblCount.TabIndex = 8;
            this.lblCount.Text = "資料筆數：";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonX1.BackColor = System.Drawing.Color.Transparent;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(7, 401);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 10;
            this.buttonX1.Text = "匯出";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonX2.BackColor = System.Drawing.Color.Transparent;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(88, 401);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 11;
            this.buttonX2.Text = "匯入";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
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
            this.labelX2.Location = new System.Drawing.Point(7, 374);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(289, 21);
            this.labelX2.TabIndex = 12;
            this.labelX2.Text = "選取整列可用Delete刪除、或右鍵選單批次修改";
            // 
            // SubjectListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 431);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnGetSuggestSubject);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgData);
            this.DoubleBuffered = true;
            this.MaximizeBox = true;
            this.Name = "SubjectListForm";
            this.Text = "開放選課科目管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubjectListForm_FormClosing);
            this.Load += new System.EventHandler(this.SubjectListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgData;
        private DevComponents.DotNetBar.LabelX lblTitle;
        private DevComponents.DotNetBar.ButtonX btnGetSuggestSubject;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.LabelX lblCount;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 批次修改科別ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 批次修改科目類別ToolStripMenuItem;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubjectLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCredit;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn colDept;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn colSubjectType;
    }
}