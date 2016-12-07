namespace SHSchool.Retake.Form
{
    partial class ReSetSubjectDate
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgData = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnGetDate = new DevComponents.DotNetBar.ButtonX();
            this.colSchoolYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSemester = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colmo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCourse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWeekDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPer1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPer2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPer3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPer4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPer5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPer6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPer7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPer8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
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
            this.colSchoolYear,
            this.colSemester,
            this.colmo,
            this.colCourse,
            this.colWeekDay,
            this.colDate,
            this.colPer1,
            this.colPer2,
            this.colPer3,
            this.colPer4,
            this.colPer5,
            this.colPer6,
            this.colPer7,
            this.colPer8});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgData.Location = new System.Drawing.Point(15, 10);
            this.dgData.Name = "dgData";
            this.dgData.RowHeadersVisible = false;
            this.dgData.RowTemplate.Height = 24;
            this.dgData.Size = new System.Drawing.Size(816, 308);
            this.dgData.TabIndex = 0;
            this.dgData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_CellDoubleClick);
            this.dgData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewX1_KeyDown);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(756, 372);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX1.Location = new System.Drawing.Point(15, 324);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(429, 73);
            this.labelX1.TabIndex = 4;
            this.labelX1.Text = "操作說明：\r\n  1. 先設定星期節次\r\n  2. 使用轉換日期功能\r\n滑鼠雙擊節次，或者鍵盤輸入\"V\"鍵勾選、空白鍵與Delete鍵清除資料。";
            // 
            // btnGetDate
            // 
            this.btnGetDate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGetDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetDate.AutoSize = true;
            this.btnGetDate.BackColor = System.Drawing.Color.Transparent;
            this.btnGetDate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGetDate.Location = new System.Drawing.Point(675, 372);
            this.btnGetDate.Name = "btnGetDate";
            this.btnGetDate.Size = new System.Drawing.Size(75, 25);
            this.btnGetDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnGetDate.TabIndex = 5;
            this.btnGetDate.Text = "轉換日期";
            this.btnGetDate.Click += new System.EventHandler(this.btnGetDate_Click);
            // 
            // colSchoolYear
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.colSchoolYear.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSchoolYear.HeaderText = "學年度";
            this.colSchoolYear.Name = "colSchoolYear";
            this.colSchoolYear.ReadOnly = true;
            this.colSchoolYear.Width = 80;
            // 
            // colSemester
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightCyan;
            this.colSemester.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSemester.HeaderText = "學期";
            this.colSemester.Name = "colSemester";
            this.colSemester.ReadOnly = true;
            this.colSemester.Width = 60;
            // 
            // colmo
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightCyan;
            this.colmo.DefaultCellStyle = dataGridViewCellStyle3;
            this.colmo.HeaderText = "梯次";
            this.colmo.Name = "colmo";
            this.colmo.ReadOnly = true;
            this.colmo.Width = 60;
            // 
            // colCourse
            // 
            this.colCourse.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan;
            this.colCourse.DefaultCellStyle = dataGridViewCellStyle4;
            this.colCourse.HeaderText = "課程";
            this.colCourse.Name = "colCourse";
            this.colCourse.ReadOnly = true;
            // 
            // colWeekDay
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightCyan;
            this.colWeekDay.DefaultCellStyle = dataGridViewCellStyle5;
            this.colWeekDay.HeaderText = "星期";
            this.colWeekDay.Name = "colWeekDay";
            this.colWeekDay.Width = 80;
            // 
            // colDate
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightCyan;
            this.colDate.DefaultCellStyle = dataGridViewCellStyle6;
            this.colDate.HeaderText = "日期";
            this.colDate.Name = "colDate";
            this.colDate.Visible = false;
            this.colDate.Width = 125;
            // 
            // colPer1
            // 
            this.colPer1.HeaderText = "1";
            this.colPer1.Name = "colPer1";
            this.colPer1.ReadOnly = true;
            this.colPer1.Width = 30;
            // 
            // colPer2
            // 
            this.colPer2.HeaderText = "2";
            this.colPer2.Name = "colPer2";
            this.colPer2.ReadOnly = true;
            this.colPer2.Width = 30;
            // 
            // colPer3
            // 
            this.colPer3.HeaderText = "3";
            this.colPer3.Name = "colPer3";
            this.colPer3.ReadOnly = true;
            this.colPer3.Width = 30;
            // 
            // colPer4
            // 
            this.colPer4.HeaderText = "4";
            this.colPer4.Name = "colPer4";
            this.colPer4.ReadOnly = true;
            this.colPer4.Width = 30;
            // 
            // colPer5
            // 
            this.colPer5.HeaderText = "5";
            this.colPer5.Name = "colPer5";
            this.colPer5.ReadOnly = true;
            this.colPer5.Width = 30;
            // 
            // colPer6
            // 
            this.colPer6.HeaderText = "6";
            this.colPer6.Name = "colPer6";
            this.colPer6.ReadOnly = true;
            this.colPer6.Width = 30;
            // 
            // colPer7
            // 
            this.colPer7.HeaderText = "7";
            this.colPer7.Name = "colPer7";
            this.colPer7.ReadOnly = true;
            this.colPer7.Width = 30;
            // 
            // colPer8
            // 
            this.colPer8.HeaderText = "8";
            this.colPer8.Name = "colPer8";
            this.colPer8.ReadOnly = true;
            this.colPer8.Width = 30;
            // 
            // ReSetSubjectDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 400);
            this.Controls.Add(this.btnGetDate);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgData);
            this.DoubleBuffered = true;
            this.Name = "ReSetSubjectDate";
            this.Text = "時間表設定";
            this.Load += new System.EventHandler(this.ReSetSubjectDate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgData;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnGetDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSchoolYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSemester;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCourse;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWeekDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPer2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPer3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPer4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPer5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPer6;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPer7;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPer8;
    }
}