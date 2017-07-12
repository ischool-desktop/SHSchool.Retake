using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SHSchool.Retake.DAO;
using FISCA.UDT;
using FISCA.Presentation.Controls;

namespace SHSchool.Retake.Form
{

    public partial class ModifyRecord2 : FISCA.Presentation.Controls.BaseForm
    {
        private List<DataGridViewRow> _RowList = new List<DataGridViewRow>();
        private DataTable _DataTable = new DataTable();
        private  Dictionary<string, string> _DicCourseID = new Dictionary<string, string>();
        private Dictionary<string, string> _DicCourseName = new Dictionary<string, string>();
        private Dictionary<string, List<string>> _DicStudentAttendList = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> _DicCourseSection = new Dictionary<string, List<string>>();



        public ModifyRecord2(DataTable dataTable, List<DataGridViewRow> rowList)
        {
            InitializeComponent();
           
            _DataTable = dataTable;
            foreach (DataGridViewRow displayRow in rowList)
            {
                _RowList.Add(displayRow);
            }

            DataRow dataRow01 = (DataRow)_RowList[0].Tag;
            List<string> idList = new List<string>() { "-1" };
            AccessHelper accessHepler = new AccessHelper();

            comboBoxEx1.Items.Add("");

            FISCA.Data.QueryHelper queryHelper = new FISCA.Data.QueryHelper();
            var dt = queryHelper.Select(@"
SELECT course.uid, course.subject_name, course.subject_level, course.credit, course.course_name
    , $shschool.retake.cdselect.dept_name as dept_name, CASE WHEN scount.student_count is null THEN 0 ELSE scount.student_count END as student_count
FROM $shschool.retake.course course
	LEFT OUTER JOIN $shschool.retake.session session on course.school_year = session.school_year AND course.semester = session.semester AND course.round = session.round
	LEFT OUTER JOIN $shschool.retake.cdselect on $shschool.retake.cdselect.ref_course_timetable_id = course.course_timetable_id
	LEFT OUTER JOIN (
		SELECT ref_course_id, count(*) as student_count
		FROM $shschool.retake.scselect
		GROUP BY ref_course_id
	) as scount on scount.ref_course_id = course.uid
WHERE
	session.active = true
ORDER BY subject_name, subject_level, credit
");

            if (_RowList[0].Tag != null)
            {
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    if ("" + dataRow01["subject_name"] == "" + row["subject_name"]
                        && "" + dataRow01["stu_dept"] == "" + row["dept_name"]
                        && "" + dataRow01["subject_level"] == "" + row["subject_level"]
                        && "" + dataRow01["credit"] == "" + row["credit"])
                    {
                        // 符合 科目、科別、級別、學分  相同，才加入List

                        comboBoxEx1.Items.Add("" + row["course_name"]);
                        if (!idList.Contains("" + row["uid"]))
                        {
                            idList.Add("" + row["uid"]);
                        }
                        _DicCourseID.Add("" + row["course_name"], "" + row["uid"]);
                        _DicCourseName.Add("" + row["uid"], "" + row["course_name"]);
                    }
                }
            }


            foreach (var item in accessHepler.Select<UDTTimeSectionDef>("ref_course_id in (" + string.Join(",", idList) + ")"))
            {
                var courseID = "" + item.CourseID;
                var section = "" + item.Date.ToShortDateString() + "^^" + item.Period;
                if (!_DicCourseSection.ContainsKey(courseID))
                    _DicCourseSection.Add(courseID, new List<string>());
                _DicCourseSection[courseID].Add(section);

            }

            foreach (System.Data.DataRow row in _DataTable.Rows)
            {
                var studentID = "" + row["student_id"];
                if (!_DicStudentAttendList.ContainsKey(studentID))
                {
                    _DicStudentAttendList.Add(studentID, new List<string>());
                }
                else
                {
                    _DicStudentAttendList[studentID].Add("" + row["distribution_id"]);
                }
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            List<string> conflictWarningList = new List<string>();
            foreach (DataGridViewRow displayRow in _RowList)
            {

                DataRow dataRow = (DataRow)displayRow.Tag;

                string studentID = "" + dataRow["student_id"];
                string courseID = "";

                if (_DicCourseID.ContainsKey("" + comboBoxEx1.Text))
                {

                    courseID = _DicCourseID["" + comboBoxEx1.Text];
                }

                foreach (var attendCourseID in _DicStudentAttendList[studentID])
                {
                    if (_DicCourseSection.ContainsKey(courseID) && _DicCourseSection.ContainsKey(attendCourseID) && _DicCourseSection[courseID].Intersect(_DicCourseSection[attendCourseID]).Count() > 0)
                    {
                        //整理衝堂學生、課程提示訊息資料                        
                        conflictWarningList.Add("學生:" + ("" + dataRow["name"]) + "  " + "科目:" + _DicCourseName[courseID] + "與" + "科目:" + _DicCourseName[attendCourseID] + "衝堂" + "確定要分發課程?" + "\r\n");
                    }
                }
            }

            // 有衝堂狀況時
            if (conflictWarningList.Count > 0)
            {
                string warning = "";
                foreach (var warn in conflictWarningList)
                {
                    warning += warn;
                }
                if (MsgBox.Show(warning, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    //Do Nothing
                }
                else
                {
                    return;
                };
            }

            foreach (DataGridViewRow displayRow in _RowList)
            {
                DataRow dataRow = (DataRow)displayRow.Tag;

                foreach (System.Data.DataRow row in _DataTable.Rows)
                {
                    if (row["class_name"] == dataRow["class_name"] && row["seat_no"] == dataRow["seat_no"] &&
                        row["student_number"] == dataRow["student_number"] && row["name"] == dataRow["name"] &&
                        row["stu_dept"] == dataRow["stu_dept"] && row["subject_level"] == dataRow["subject_level"] &&
                        row["credit"] == dataRow["credit"] && row["fail_reason"] == dataRow["fail_reason"]
                        )
                    {
                        if (comboBoxEx1.Text != "")
                        {
                            row["distribution_id"] = _DicCourseID["" + comboBoxEx1.Text];
                        }
                        else
                        {
                            row["distribution_id"] = "";

                        }
                        row["course_name"] = "" + comboBoxEx1.Text;
                    }
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
