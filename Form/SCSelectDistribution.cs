using FISCA.UDT;
using SHSchool.Retake.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SHSchool.Retake.Form
{
    public partial class SCSelectDistribution : FISCA.Presentation.Controls.BaseForm
    {
        private DataTable _DataTable;
        private Dictionary<DataRow, DataGridViewRow> _DisplayRow;

        public SCSelectDistribution()
        {
            InitializeComponent();

            #region 取得學生選課及分發狀況
            FISCA.Data.QueryHelper queryHelper = new FISCA.Data.QueryHelper();
            _DataTable = queryHelper.Select(@"
SELECT dept.stu_dept, class.grade_year, class.class_name, student.seat_no, student.student_number, student.name
	, scourse.subject_name, scourse.subject_level, scourse.credit
	, session.school_year, session.semester, session.round
	, CASE WHEN cscourse.subject_type is null THEN sscourse.subject_type ELSE cscourse.subject_type END
	, cscourse.course_name, sscourse.fail_reason as fail_reason
	, sscourse.uid as select_subject_id, cscourse.uid as attend_course_id, student.id as student_id, sscourse.ssselect_id
    , null as distribution_id
FROM (
		SELECT csselect.ref_student_id, course.subject_name, course.subject_level, course.credit, course.school_year, course.semester, course.round
		FROM $shschool.retake.scselect csselect
			LEFT OUTER JOIN $shschool.retake.course course on course.uid = csselect.ref_course_id
		UNION
		SELECT ssselect.ref_student_id, subject.subject_name, subject.subject_level, subject.credit, subject.school_year, subject.semester, subject.round
		FROM $shschool.retake.ssselect ssselect
			LEFT OUTER JOIN $shschool.retake.subject subject on ssselect.ref_subject_id = subject.uid
	) as scourse
	LEFt OUTER JOIN student on student.id = scourse.ref_student_id
	LEFt OUTER JOIN class on class.id = student.ref_class_id
	LEFT OUTER JOIN (
		SELECT s.id as ref_student_id, CASE WHEN d1.name is null THEN d2.name ELSE d1.name END as stu_dept
		FROM student s
			LEFT OUTER JOIN class c on c.id = s.ref_class_id
			LEFT OUTER JOIN dept d1 on d1.id = s.ref_dept_id
			LEFT OUTER JOIN dept d2 on d2.id = c.ref_dept_id
	) as dept on student.id = dept.ref_student_id
	LEFT OUTER JOIN $shschool.retake.session session on scourse.school_year=session.school_year and scourse.semester=session.semester and scourse.round=session.round
	LEFT OUTER JOIN (
		SELECT ssselect.ref_student_id, subject.subject_name, subject.subject_level, subject.credit, subject.school_year, subject.semester, subject.round
			, subject.subject_type, subject.uid, ssselect.fail_reason, ssselect.uid as ssselect_id
		FROM $shschool.retake.ssselect ssselect
			LEFT OUTER JOIN $shschool.retake.subject subject on ssselect.ref_subject_id = subject.uid
	) as sscourse on sscourse.ref_student_id = scourse.ref_student_id 
		AND sscourse.subject_name = scourse.subject_name 
		AND (
			sscourse.subject_level = scourse.subject_level  
			OR (
				sscourse.subject_level is null
				AND
				scourse.subject_level is null
			)
		)
		AND sscourse.credit = scourse.credit
		AND sscourse.school_year = scourse.school_year
		AND sscourse.semester = scourse.semester
		AND sscourse.round = scourse.round 
	LEFT OUTER JOIN (
		SELECT csselect.ref_student_id, course.subject_name, course.subject_level, course.credit, course.school_year, course.semester, course.round
			, course.course_name, course.subject_type, course.uid
		FROM $shschool.retake.scselect csselect
			LEFT OUTER JOIN $shschool.retake.course course on course.uid = csselect.ref_course_id
	) as cscourse on cscourse.ref_student_id = scourse.ref_student_id 
		AND cscourse.subject_name = scourse.subject_name 
		AND (
			cscourse.subject_level = scourse.subject_level  
			OR (
				cscourse.subject_level is null
				AND
				scourse.subject_level is null
			)
		)
		AND cscourse.credit = scourse.credit
		AND cscourse.school_year = scourse.school_year
		AND cscourse.semester = scourse.semester
		AND cscourse.round = scourse.round
WHERE 
	session.active = true
ORDER BY session.school_year desc, session.semester desc, session.round desc, stu_dept, class.grade_year desc, class.display_order, class.class_name, student.seat_no, student.id, scourse.credit desc, scourse.subject_name, scourse.subject_level
");
            #endregion
            _DisplayRow = new Dictionary<DataRow, DataGridViewRow>();
            foreach (DataRow dataRow in _DataTable.Rows)
            {
                var displayRow = dgData.Rows[dgData.Rows.Add()];
                _DisplayRow.Add(dataRow, displayRow);
                displayRow.Tag = dataRow;

                #region dataRow 欄位
                //dept.stu_dept, class.grade_year, class.class_name, student.seat_no, student.student_number, student.name
                //, scourse.subject_name, scourse.subject_level, scourse.credit
                //, session.school_year, session.semester, session.round
                //, CASE WHEN cscourse.subject_type is null THEN sscourse.subject_type ELSE cscourse.subject_type END
                //, cscourse.course_name
                //, sscourse.uid as select_subject_id, cscourse.uid as attend_course_id, student as student_id 
                #endregion
                int cellIndex = 0;
                displayRow.Cells[cellIndex++].Value = "" + dataRow["class_name"];
                displayRow.Cells[cellIndex++].Value = "" + dataRow["seat_no"];
                displayRow.Cells[cellIndex++].Value = "" + dataRow["student_number"];
                displayRow.Cells[cellIndex++].Value = "" + dataRow["name"];
                displayRow.Cells[cellIndex++].Value = "" + dataRow["stu_dept"];
                displayRow.Cells[cellIndex++].Value = "" + dataRow["subject_name"];
                displayRow.Cells[cellIndex++].Value = "" + dataRow["subject_level"];
                displayRow.Cells[cellIndex++].Value = "" + dataRow["credit"];
                displayRow.Cells[cellIndex++].Value = "" + dataRow["course_name"];

                if (("" + dataRow["course_name"]) == "" && ("" + dataRow["fail_reason"]) != "")
                {
                    _DisplayRow[dataRow].Cells[8].Style.ForeColor = Color.Gray;
                    _DisplayRow[dataRow].Cells[8].Value = "" + dataRow["fail_reason"];
                }
            }
        }

        private void btnDistribution_Click(object s1, EventArgs e1)
        {
            BackgroundWorker bkw = new BackgroundWorker() { WorkerReportsProgress = true };
            bkw.DoWork += delegate
            {
                AccessHelper accessHepler = new AccessHelper();
                //[studentID][courseID *]
                Dictionary<string, List<string>> dicStudentAttendList = new Dictionary<string, List<string>>();
                //[studentID][row *]
                Dictionary<string, List<DataRow>> dicStudentDistributionList = new Dictionary<string, List<DataRow>>();
                #region 整理取得學生須分發的選課紀錄
                {
                    foreach (System.Data.DataRow row in _DataTable.Rows)
                    {
                        var studentID = "" + row["student_id"];
                        if (!dicStudentDistributionList.ContainsKey(studentID))
                        {
                            dicStudentDistributionList.Add(studentID, new List<DataRow>());
                            dicStudentAttendList.Add(studentID, new List<string>());
                        }
                        //select_subject_id, cscourse.uid as attend_course_id
                        if (("" + row["attend_course_id"]) == "")
                        {
                            if (("" + row["select_subject_id"]) != "")
                                dicStudentDistributionList[studentID].Add(row);
                        }
                        else
                        {
                            dicStudentAttendList[studentID].Add("" + row["attend_course_id"]);
                        }
                    }
                }
                #endregion
                //[subj^^level^^credit^^dept][row *]
                Dictionary<string, List<DataRow>> dicCourseList = new Dictionary<string, List<DataRow>>();
                //[courseID][date^^period *]
                Dictionary<string, List<string>> dicCourseSection = new Dictionary<string, List<string>>();
                //[courseID]course_name
                Dictionary<string, string> dicCourseName = new Dictionary<string, string>();
                #region 整理課程資料
                {
                    List<string> idList = new List<string>() { "-1" };
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

                    foreach (System.Data.DataRow row in dt.Rows)
                    {
                        if (!idList.Contains("" + row["uid"]))
                        {
                            idList.Add("" + row["uid"]);
                            dicCourseName.Add("" + row["uid"], "" + row["course_name"]);
                        }
                        var key = "" + row["subject_name"] + "^^" + row["subject_level"] + "^^" + row["credit"] + "^^" + row["dept_name"];
                        if (!dicCourseList.ContainsKey(key))
                        {
                            dicCourseList.Add(key, new List<DataRow>());
                        }
                        dicCourseList[key].Add(row);
                    }

                    foreach (var item in accessHepler.Select<UDTTimeSectionDef>("ref_course_id in (" + string.Join(",", idList) + ")"))
                    {
                        var courseID = "" + item.CourseID;
                        var section = "" + item.Date.ToShortDateString() + "^^" + item.Period;
                        if (!dicCourseSection.ContainsKey(courseID))
                            dicCourseSection.Add(courseID, new List<string>());
                        dicCourseSection[courseID].Add(section);
                    }
                }
                #endregion

                List<UDTScselectDef> addList = new List<UDTScselectDef>();
                #region 分發
                foreach (var studentID in dicStudentDistributionList.Keys)
                {
                    for (int i = 0; i < dicStudentDistributionList[studentID].Count; i++)
                    {
                        var row = dicStudentDistributionList[studentID][i];
                        var key = "" + row["subject_name"] + "^^" + row["subject_level"] + "^^" + row["credit"] + "^^" + row["stu_dept"];
                        if (dicCourseList.ContainsKey(key))
                        {
                            var candidateList = new List<DataRow>();
                            var dicCandidateOrder = new Dictionary<DataRow, decimal>();
                            var unpassList = new List<string>();
                            foreach (var courseRow in dicCourseList[key])
                            {
                                var courseID = "" + courseRow["uid"];
                                #region 檢查此課程與學生已修課程衝堂
                                var pass = true;
                                foreach (var attendCourseID in dicStudentAttendList[studentID])
                                {
                                    if (dicCourseSection.ContainsKey(courseID) && dicCourseSection.ContainsKey(attendCourseID) && dicCourseSection[courseID].Intersect(dicCourseSection[attendCourseID]).Count() > 0)
                                    {
                                        unpassList.Add(dicCourseName[attendCourseID]);
                                        pass = false;
                                    }
                                }
                                if (!pass) continue;
                                #endregion
                                candidateList.Add(courseRow);
                                dicCandidateOrder.Add(courseRow, 0);
                                #region 計算衝堂課程權重
                                if (dicCourseSection.ContainsKey(courseID))
                                {
                                    for (int j = i + 1; j < dicStudentDistributionList[studentID].Count; j++)
                                    {
                                        var checkRow = dicStudentDistributionList[studentID][j];
                                        var checkKey = "" + checkRow["subject_name"] + "^^" + checkRow["subject_level"] + "^^" + checkRow["credit"] + "^^" + checkRow["stu_dept"];
                                        if (dicCourseList.ContainsKey(checkKey))
                                        {
                                            foreach (var checkCourse in dicCourseList[checkKey])
                                            {
                                                var checkCourseID = "" + checkCourse["uid"];
                                                if (dicCourseSection.ContainsKey(checkCourseID))
                                                {
                                                    if (dicCourseSection[courseID].Intersect(dicCourseSection[checkCourseID]).Count() > 0)
                                                    {
                                                        //衝堂學分數做為權重值
                                                        dicCandidateOrder[courseRow] += decimal.Parse("0" + checkRow["credit"]);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                            if (candidateList.Count > 0)
                            {
                                //有可分配課程，篩選衝堂學分數最少、目前學生最少的課程。
                                var target = candidateList[0];
                                for (int j = 1; j < candidateList.Count; j++)
                                {
                                    if (dicCandidateOrder[target] > dicCandidateOrder[candidateList[j]])//比衝堂學分數(可能會影響後面課程分發)
                                        target = candidateList[j];
                                    else if (dicCandidateOrder[target] == dicCandidateOrder[candidateList[j]])
                                    {
                                        if (int.Parse("" + target["student_count"]) > int.Parse("" + candidateList[j]["student_count"]))//比學生數
                                        {
                                            target = candidateList[j];
                                        }
                                    }
                                }
                                target["student_count"] = int.Parse("" + target["student_count"]) + 1;
                                row["course_name"] = target["course_name"];
                                row["distribution_id"] = target["uid"];
                                row["fail_reason"] = "";

                                bkw.ReportProgress(0, row);
                            }
                            else
                            {
                                row["fail_reason"] = "衝堂：" + string.Join("、", unpassList) + ")";
                                bkw.ReportProgress(0, row);
                            }
                        }
                        else
                        {
                            row["fail_reason"] = "沒有開課";
                            bkw.ReportProgress(0, row);
                        }
                    }
                }
                #endregion
            };
            bkw.ProgressChanged += delegate (object sender, ProgressChangedEventArgs e)
            {
                var dataRow = e.UserState as DataRow;
                if (("" + dataRow["distribution_id"]) != "")
                {
                    _DisplayRow[dataRow].Cells[8].Style.ForeColor = Color.Red;
                    _DisplayRow[dataRow].Cells[8].Value = "" + dataRow["course_name"];
                }
                else if (("" + dataRow["fail_reason"]) != "")
                {
                    _DisplayRow[dataRow].Cells[8].Style.ForeColor = Color.Gray;
                    _DisplayRow[dataRow].Cells[8].Value = "" + dataRow["fail_reason"];
                }
                dgData.FirstDisplayedScrollingRowIndex = _DisplayRow[dataRow].Index;
            };
            bkw.RunWorkerCompleted += delegate
            {
                FISCA.Presentation.Controls.MsgBox.Show("自動分發完成");
            };
            bkw.RunWorkerAsync();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<UDTScselectDef> list = new List<UDTScselectDef>();
            Dictionary<string, string> failReason = new Dictionary<string, string>();
            foreach (DataGridViewRow displayRow in dgData.Rows)
            {
                DataRow dataRow = (DataRow)displayRow.Tag;
                if (("" + dataRow["distribution_id"]) != "")
                {
                    list.Add(new UDTScselectDef() { CourseID = int.Parse("" + dataRow["distribution_id"]), StudentID = (int.Parse("" + dataRow["student_id"])) });
                    if (("" + dataRow["fail_reason"]) != "")
                    {
                        failReason.Add("" + dataRow["ssselect_id"], "");
                    }
                }
                else if (("" + dataRow["fail_reason"]) != "")
                {
                    failReason.Add("" + dataRow["ssselect_id"], "" + dataRow["fail_reason"]);
                }
            }
            list.SaveAll();
            if (failReason.Count > 0)
            {
                List<UDTSsselectDef> selectList = new AccessHelper().Select<UDTSsselectDef>("uid in (" + string.Join(",", failReason.Keys) + ")");
                foreach (var item in selectList)
                {
                    item.FailResaon = failReason[item.UID];
                }
                selectList.SaveAll();
            }
            FISCA.Presentation.Controls.MsgBox.Show("儲存完成。");
            this.Close();
        }
    }
}
