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
        public SCSelectDistribution()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BackgroundWorker bkw = new BackgroundWorker() { WorkerReportsProgress = true };
            bkw.DoWork += delegate
            {
                AccessHelper accessHepler = new AccessHelper();
                #region row 欄位
                //dept.stu_dept, class.grade_year, class.class_name, student.seat_no, student.student_number, student.name
                //, scourse.subject_name, scourse.subject_level, scourse.credit
                //, session.school_year, session.semester, session.round
                //, CASE WHEN cscourse.subject_type is null THEN sscourse.subject_type ELSE cscourse.subject_type END
                //, cscourse.course_name
                //, sscourse.uid as select_subject_id, cscourse.uid as attend_course_id, student as student_id 
                #endregion
                Dictionary<string, List<DataRow>> dicStudentSelect = new Dictionary<string, List<DataRow>>();
                #region 整理取得學生須分發的選課紀錄
                {
                    FISCA.Data.QueryHelper queryHelper = new FISCA.Data.QueryHelper();
                    var dt = queryHelper.Select(@"
SELECT dept.stu_dept, class.grade_year, class.class_name, student.seat_no, student.student_number, student.name
	, scourse.subject_name, scourse.subject_level, scourse.credit
	, session.school_year, session.semester, session.round
	, CASE WHEN cscourse.subject_type is null THEN sscourse.subject_type ELSE cscourse.subject_type END
	, cscourse.course_name
	, sscourse.uid as select_subject_id, cscourse.uid as attend_course_id, student as student_id
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
			, subject.subject_type, subject.uid
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
    AND sscourse.uid is not null
    AND cscourse.uid is null
ORDER BY session.school_year desc, session.semester desc, session.round desc, stu_dept, class.grade_year desc, class.display_order, class.class_name, student.seat_no, student.id, scourse.credit desc, scourse.subject_name, scourse.subject_level
");

                    foreach (System.Data.DataRow row in dt.Rows)
                    {
                        var studentID = "" + row["student_id"];
                        if (!dicStudentSelect.ContainsKey(studentID))
                        {
                            dicStudentSelect.Add(studentID, new List<DataRow>());
                        }
                        dicStudentSelect[studentID].Add(row);
                    }
                }
                #endregion

                Dictionary<string, List<DataRow>> dicCourseList = new Dictionary<string, List<DataRow>>();
                #region 整理課程資料
                {
                    FISCA.Data.QueryHelper queryHelper = new FISCA.Data.QueryHelper();
                    var dt = queryHelper.Select(@"
SELECT course.uid, course.subject_name, course.subject_level, course.credit
    , $shschool.retake.cdselect.dept_name as dept_name, scount.student_count
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
                        var key = "" + row["subject_name"] + "^^" + row["subject_level"] + "^^" + row["credit"] + "^^" + row["dept_name"];
                        if (!dicCourseList.ContainsKey(key))
                        {
                            dicCourseList.Add(key, new List<DataRow>());
                        }
                        dicCourseList[key].Add(row);
                    }
                }
                #endregion


            };
            bkw.RunWorkerCompleted += delegate
                {
                    FISCA.Presentation.Controls.MsgBox.Show("分發完成");
                };
            bkw.RunWorkerAsync();
        }
    }
}
