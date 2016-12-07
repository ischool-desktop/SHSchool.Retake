using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using SHSchool.Retake.DAO;

namespace SHSchool.Retake.Form
{
    public partial class ReSetSubjectDate : BaseForm
    {
        AccessHelper _accessHelper = new AccessHelper();

        List<UDTAttendanceDef> _OldAttendanceDefList = new List<UDTAttendanceDef>();
        List<UDTTimeSectionDef> _OldTimeSectionDef = new List<UDTTimeSectionDef>();
        public ReSetSubjectDate()
        {
            InitializeComponent();
        }

        private void ReSetSubjectDate_Load(object sender, EventArgs e)
        {
            //選擇一群課程
            List<UDTCourseDef> CourseList = _accessHelper.Select<UDTCourseDef>(RetakeAdmin.Instance.SelectedSource);
            bool changeBGColor = false;
            foreach (UDTCourseDef def in CourseList)
            {
                foreach (var weekDay in new string[] { "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日" })
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgData);
                    row.Tag = def;
                    row.Cells[0].Value = "" + def.SchoolYear;
                    row.Cells[1].Value = "" + def.Semester;
                    row.Cells[2].Value = "" + def.Round;
                    row.Cells[3].Value = "" + def.CourseName;
                    row.Cells[4].Value = weekDay;
                    if (changeBGColor)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            row.Cells[i].Style.BackColor = Color.LightGray;
                        }
                    }
                    dgData.Rows.Add(row);
                }
                changeBGColor = changeBGColor ? false : true;
            }

            //*如果這些課程已經有上課日期與節次資訊
            //則警告此操作,將會清除原有設定
            //List<UDTTimeSectionDef> Session = _accessHelper.Select<UDTTimeSectionDef>(RetakeAdmin.Instance.SelectedSource);
            // 上課時間區間
            _OldTimeSectionDef = UDTTransfer.UDTTimeSectionSelectByCourseIDList(RetakeAdmin.Instance.SelectedSource);
            // 課程缺曠
            _OldAttendanceDefList = UDTTransfer.UDTAttendanceSelectByCourseIDList(RetakeAdmin.Instance.SelectedSource);

            if (_OldTimeSectionDef.Count > 0)
            {
                MsgBox.Show("部份課程已有時間表\n使用本功能將會把時間表重設\n相關缺曠資料也將會遺失!!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            // 比對課程節次
            foreach (DataGridViewRow drv in dgData.Rows)
            {
                if (drv.IsNewRow)
                    continue;

                UDTCourseDef data = drv.Tag as UDTCourseDef;
                if (data != null)
                {
                    int coid = int.Parse(data.UID);
                    foreach (UDTTimeSectionDef sec in _OldTimeSectionDef.Where(x => x.CourseID == coid))
                    {
                        var weekDays = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                        if (weekDays[(int)sec.Date.DayOfWeek] == ("" + drv.Cells[4].Value))
                        {
                            int colIdx = sec.Period + 5;
                            drv.Cells[colIdx].Value = "V";
                        }
                    }
                }
            }
        }


        private void dgData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0
                && e.ColumnIndex >= colPer1.Index
                && e.ColumnIndex <= colPer8.Index)
            {
                var cell = dgData[e.ColumnIndex, e.RowIndex];
                cell.Value = (("" + cell.Value) == "V" ? "" : "V");
            }
        }

        private void dataGridViewX1_KeyDown(object sender, KeyEventArgs e)
        {
            // 使用者所選範圍內
            foreach (DataGridViewCell cell in dgData.SelectedCells)
            {
                // 當不在節次範圍內
                if (cell.ColumnIndex >= colPer1.Index && cell.ColumnIndex <= colPer8.Index)
                {
                    // 除了按A,否則都會清空資料
                    if (e.KeyCode == Keys.Tab)
                        continue;

                    if (e.KeyCode == Keys.V)
                    {
                        cell.Value = "V";
                    }
                    else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Space)
                    {
                        cell.Value = "";
                    }
                    else
                    {
                        //不動作
                    }
                }
            }
        }

        private void btnGetDate_Click(object sender, EventArgs e)
        {
            var dateForm = new ReSetSubjectDateStep2();
            if (dateForm.ShowDialog() == DialogResult.OK
                && dateForm.StartDate != null
                && dateForm.EndDate != null)
            {
                var weekDays = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                Dictionary<UDTCourseDef, Dictionary<DateTime, List<int>>> courseSesion = new Dictionary<UDTCourseDef, Dictionary<DateTime, List<int>>>();
                foreach (DataGridViewRow row in dgData.Rows)
                {
                    UDTCourseDef courseRec = row.Tag as UDTCourseDef;
                    for (int i = 1; i <= 8; i++)
                    {
                        if (("" + row.Cells[5 + i].Value) == "V")
                        {
                            if (!courseSesion.ContainsKey(courseRec))
                                courseSesion.Add(courseRec, new Dictionary<DateTime, List<int>>());
                            var runDate = dateForm.StartDate;
                            while (runDate <= dateForm.EndDate)
                            {
                                if (weekDays[(int)runDate.DayOfWeek] == ("" + row.Cells[4].Value))
                                {

                                    courseSesion[courseRec].Add(runDate, new List<int>());
                                    for (int j = i; j <= 8; j++)
                                    {
                                        if (("" + row.Cells[5 + j].Value) == "V")
                                        {
                                            courseSesion[courseRec][runDate].Add(j);
                                        }
                                    }
                                }
                                runDate = runDate.AddDays(1);
                            }
                            break;
                        }
                    }
                }
                var changeBGColor = false;
                dgData.Rows.Clear();
                colDate.Visible = true;
                colWeekDay.Visible = false;
                foreach (var courseRec in courseSesion.Keys)
                {
                    List<DateTime> dates = new List<DateTime>(courseSesion[courseRec].Keys);
                    dates.Sort();
                    foreach (var date in dates)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgData);
                        row.Tag = courseRec;
                        row.Cells[0].Value = "" + courseRec.SchoolYear;
                        row.Cells[1].Value = "" + courseRec.Semester;
                        row.Cells[2].Value = "" + courseRec.Round;
                        row.Cells[3].Value = "" + courseRec.CourseName;
                        row.Cells[5].Value = date.ToString("yyyy/MM/dd") + " (" + Utility.GetDayWeekString(date) + ")";
                        row.Cells[5].Tag = date;
                        foreach (var period in courseSesion[courseRec][date])
                        {
                            row.Cells[5 + period].Value = "V";
                        }
                        if (changeBGColor)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                row.Cells[i].Style.BackColor = Color.LightGray;
                            }
                        }

                        dgData.Rows.Add(row);
                    }
                    changeBGColor = changeBGColor ? false : true;
                }
                btnSave.Enabled = true;
                btnGetDate.Visible = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<UDTTimeSectionDef> list = new List<UDTTimeSectionDef>();
            foreach (DataGridViewRow row in dgData.Rows)
            {
                UDTCourseDef courseRec = row.Tag as UDTCourseDef;
                DateTime date = (DateTime)row.Cells[5].Tag;
                for (int i = 1; i <= 8; i++)
                {
                    if (("" + row.Cells[5 + i].Value) == "V")
                    {
                        list.Add(new UDTTimeSectionDef() { CourseID = int.Parse(courseRec.UID), Date = date, Period = i });
                    }
                }
            }
            // 刪除課程缺曠
            UDTTransfer.UDTAttendanceDelete(_OldAttendanceDefList);
            // 刪除課程上課時間
            UDTTransfer.UDTTimeSectionDelete(_OldTimeSectionDef);
            if (list.Count > 0)
                list.SaveAll();
            FISCA.Presentation.Controls.MsgBox.Show("更新完成。");
            this.Close();
        }
    }
}
