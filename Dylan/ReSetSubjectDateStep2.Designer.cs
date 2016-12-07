namespace SHSchool.Retake.Form
{
    partial class ReSetSubjectDateStep2
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
            this.dtEndDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.dtBeginDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnFinish = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBeginDate)).BeginInit();
            this.SuspendLayout();
            // 
            // dtEndDate
            // 
            this.dtEndDate.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.dtEndDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtEndDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEndDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtEndDate.ButtonDropDown.Visible = true;
            this.dtEndDate.ButtonFreeText.Checked = true;
            this.dtEndDate.FreeTextEntryMode = true;
            this.dtEndDate.IsPopupCalendarOpen = false;
            this.dtEndDate.Location = new System.Drawing.Point(86, 41);
            this.dtEndDate.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dtEndDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // 
            // 
            this.dtEndDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtEndDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtEndDate.MonthCalendar.BackgroundStyle.Class = "";
            this.dtEndDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEndDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtEndDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEndDate.MonthCalendar.DayNames = new string[] {
        "日",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六"};
            this.dtEndDate.MonthCalendar.DisplayMonth = new System.DateTime(2012, 8, 1, 0, 0, 0, 0);
            this.dtEndDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtEndDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtEndDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtEndDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtEndDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtEndDate.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtEndDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEndDate.MonthCalendar.TodayButtonVisible = true;
            this.dtEndDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(135, 25);
            this.dtEndDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtEndDate.TabIndex = 37;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(12, 43);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(74, 21);
            this.labelX3.TabIndex = 36;
            this.labelX3.Text = "結束日期：";
            // 
            // dtBeginDate
            // 
            this.dtBeginDate.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.dtBeginDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtBeginDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtBeginDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtBeginDate.ButtonDropDown.Visible = true;
            this.dtBeginDate.ButtonFreeText.Checked = true;
            this.dtBeginDate.FreeTextEntryMode = true;
            this.dtBeginDate.IsPopupCalendarOpen = false;
            this.dtBeginDate.Location = new System.Drawing.Point(86, 10);
            this.dtBeginDate.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dtBeginDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // 
            // 
            this.dtBeginDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtBeginDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtBeginDate.MonthCalendar.BackgroundStyle.Class = "";
            this.dtBeginDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtBeginDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtBeginDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtBeginDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtBeginDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtBeginDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtBeginDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtBeginDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtBeginDate.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtBeginDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtBeginDate.MonthCalendar.DayNames = new string[] {
        "日",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六"};
            this.dtBeginDate.MonthCalendar.DisplayMonth = new System.DateTime(2012, 8, 1, 0, 0, 0, 0);
            this.dtBeginDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtBeginDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtBeginDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtBeginDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtBeginDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtBeginDate.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtBeginDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtBeginDate.MonthCalendar.TodayButtonVisible = true;
            this.dtBeginDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtBeginDate.Name = "dtBeginDate";
            this.dtBeginDate.Size = new System.Drawing.Size(135, 25);
            this.dtBeginDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtBeginDate.TabIndex = 35;
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
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(74, 21);
            this.labelX1.TabIndex = 34;
            this.labelX1.Text = "開始日期：";
            // 
            // btnFinish
            // 
            this.btnFinish.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFinish.BackColor = System.Drawing.Color.Transparent;
            this.btnFinish.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFinish.Location = new System.Drawing.Point(146, 72);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(75, 25);
            this.btnFinish.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFinish.TabIndex = 41;
            this.btnFinish.Text = "完成";
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // ReSetSubjectDateStep2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 107);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.dtEndDate);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.dtBeginDate);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "ReSetSubjectDateStep2";
            this.Text = "選取日期區間";
            this.Load += new System.EventHandler(this.ReSetSubjectDateStep2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBeginDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtEndDate;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtBeginDate;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnFinish;
    }
}