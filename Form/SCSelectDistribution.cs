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
using System.Collections;

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
	, cscourse.course_name
	, sscourse.uid as select_subject_id
    , cscourse.uid as attend_course_id, cscourse.uid as distribution_id,cscourse.csselect_id
    , sscourse.fail_reason as fail_reason, sscourse.fail_reason as fail_reason_ori, sscourse.ssselect_id  
    
    , student.id as student_id
    
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
			, course.course_name, course.subject_type, course.uid, csselect.uid as csselect_id
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

            comboBoxEx1.Items.Add(new DataGridViewSorter("依學生", new List<DataGridViewRow>(_DisplayRow.Values)));
            comboBoxEx1.Items.Add(new DataGridViewSorter("依科目", new List<DataGridViewRow>(_DisplayRow.Values)));
            comboBoxEx1.DisplayMember = "Mode";
            comboBoxEx1.SelectedIndex = 0;
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

                Dictionary<string, int> dicStudentDistributionCreditCount = new Dictionary<string, int>();

                Dictionary<string, int> dicCourseStudentCount = new Dictionary<string, int>();
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
                        if (("" + row["distribution_id"]) == "")
                        {
                            dicStudentDistributionList[studentID].Add(row);

                            if (!dicStudentDistributionCreditCount.ContainsKey(studentID))
                                dicStudentDistributionCreditCount.Add(studentID, 0);
                            dicStudentDistributionCreditCount[studentID] += int.Parse("" + row["credit"]);
                        }
                        else
                        {
                            dicStudentAttendList[studentID].Add("" + row["distribution_id"]);
                            if (!dicCourseStudentCount.ContainsKey("" + row["distribution_id"]))
                                dicCourseStudentCount.Add("" + row["distribution_id"], 0);
                            dicCourseStudentCount["" + row["distribution_id"]]++;
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
    , $shschool.retake.cdselect.dept_name as dept_name
FROM $shschool.retake.course course
	LEFT OUTER JOIN $shschool.retake.session session on course.school_year = session.school_year AND course.semester = session.semester AND course.round = session.round
	LEFT OUTER JOIN $shschool.retake.cdselect on $shschool.retake.cdselect.ref_course_timetable_id = course.course_timetable_id
WHERE
	session.active = true
ORDER BY subject_name, subject_level, credit
");
                    dt.Columns.Add("student_count");
                    foreach (System.Data.DataRow row in dt.Rows)
                    {
                        if (dicCourseStudentCount.ContainsKey("" + row["uid"]))
                        {
                            row["student_count"] = "" + dicCourseStudentCount["" + row["uid"]];
                        }
                        else
                        {
                            row["student_count"] = "0";
                        }
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
                int progress = 0;
                #region 分發
                List<string> orderStudent = new List<string>(dicStudentDistributionCreditCount.Keys);
                orderStudent.Sort(delegate (string sid1, string sid2)
                {
                    return dicStudentDistributionCreditCount[sid2].CompareTo(dicStudentDistributionCreditCount[sid1]);
                });
                foreach (var studentID in orderStudent)
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
                            }
                            else
                            {
                                row["fail_reason"] = "衝堂：" + string.Join("、", unpassList) + ")";
                            }
                        }
                        else
                        {
                            if ("" + row["fail_reason_ori"] == "")
                            {
                                row["fail_reason"] = "沒有開課";
                            }
                        }
                    }
                    progress++;
                    bkw.ReportProgress(100 * progress / dicStudentDistributionList.Count);
                }
                #endregion
            };
            bkw.ProgressChanged += delegate (object sender, ProgressChangedEventArgs e)
            {
                labelX2.Visible = false;
                progressBarX1.Value = e.ProgressPercentage;
                progressBarX1.Visible = true;
            };
            bkw.RunWorkerCompleted += delegate
            {
                progressBarX1.Visible = false;
                labelX2.Visible = true;
                labelX2.Text = "自動分發完成";
                updateDataGridView();
                //FISCA.Presentation.Controls.MsgBox.Show("自動分發完成");
            };
            bkw.RunWorkerAsync();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AccessHelper accessHepler = new AccessHelper();

            Dictionary<string, string> dicFailReason = new Dictionary<string, string>();

            Dictionary<string, string> dicDistribution = new Dictionary<string, string>();

            List<UDTScselectDef> distributionList = new List<UDTScselectDef>();

            foreach (DataGridViewRow displayRow in dgData.Rows)
            {
                DataRow dataRow = (DataRow)displayRow.Tag;

                //分發課程有變更
                if (("" + dataRow["distribution_id"]) != "" + dataRow["attend_course_id"])
                {
                    //新增課程
                    if (("" + dataRow["attend_course_id"]) == "")
                    {
                        distributionList.Add(new UDTScselectDef() { CourseID = int.Parse("" + dataRow["distribution_id"]), StudentID = (int.Parse("" + dataRow["student_id"])) });
                    }
                    else //更改分發課程
                    {
                        dicDistribution.Add("" + dataRow["csselect_id"], "" + dataRow["distribution_id"]);
                    }
                }
                //有分發課程移除未分發原因
                if (("" + dataRow["distribution_id"]) != "")
                {
                    dataRow["fail_reason"] = "";
                }
                // 新增是由原因
                if (("" + dataRow["fail_reason"]) != "" + dataRow["fail_reason_ori"])
                {
                    dicFailReason.Add("" + dataRow["ssselect_id"], "" + dataRow["fail_reason"]);
                }
            }
            //儲存分發結果
            if (dicDistribution.Count > 0)
            {
                foreach (var item in new AccessHelper().Select<UDTScselectDef>("uid in (" + string.Join(",", dicDistribution.Keys) + ")"))
                {
                    if (dicDistribution[item.UID] == "")
                        item.Deleted = true;
                    else
                        item.CourseID = int.Parse(dicDistribution[item.UID]);
                    distributionList.Add(item);
                }
            }
            distributionList.SaveAll();

            //儲存未分發原因
            if (dicFailReason.Count > 0)
            {
                List<UDTSsselectDef> selectList = new AccessHelper().Select<UDTSsselectDef>("uid in (" + string.Join(",", dicFailReason.Keys) + ")");
                foreach (var item in selectList)
                {
                    item.FailResaon = dicFailReason[item.UID];
                }
                selectList.SaveAll();
            }

            FISCA.Presentation.Controls.MsgBox.Show("儲存完成。");
            this.Close();
        }



        private void 手動輸入無法分發原因ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> _RowList = new List<DataGridViewRow>();

            foreach (DataGridViewRow displayRow in dgData.Rows)
            {
                if (displayRow.Selected)
                {
                    _RowList.Add(displayRow);
                }
            }

            ModifyRecord MRForm = new ModifyRecord(_DataTable, _RowList);

            if (MRForm.ShowDialog() == DialogResult.OK)
            {
                updateDataGridView();
            }
        }

        private void 手動更正分發課程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> _RowList = new List<DataGridViewRow>();

            foreach (DataGridViewRow displayRow in dgData.Rows)
            {
                if (displayRow.Selected)
                {
                    _RowList.Add(displayRow);
                }
            }

            ModifyRecord2 MRForm2 = new ModifyRecord2(_DataTable, _RowList);

            if (MRForm2.ShowDialog() == DialogResult.OK)
                updateDataGridView();

        }

        private void 清除分發課程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow displayRow in dgData.Rows)
            {
                if (displayRow.Selected)
                {
                    (displayRow.Tag as DataRow)["distribution_id"] = "";
                }
            }
            updateDataGridView();
        }


        // 隨時監聽所選Rows，若不符條件，將右鍵功能disable
        private void dgData_SelectionChanged(object sender, EventArgs e)
        {
            手動輸入無法分發原因ToolStripMenuItem.Enabled = false;
            手動更正分發課程ToolStripMenuItem.Enabled = false;
            清除分發課程ToolStripMenuItem.Enabled = false;

            List<DataGridViewRow> _RowList = new List<DataGridViewRow>();

            foreach (DataGridViewRow displayRow in dgData.Rows)
            {
                if (displayRow.Selected)
                {
                    _RowList.Add(displayRow);
                }
            }

            // 全部和第一筆資料做比較，有一不同，就不給過
            if (_RowList.Count != 0)
            {
                手動輸入無法分發原因ToolStripMenuItem.Enabled = true;
                手動更正分發課程ToolStripMenuItem.Enabled = true;
                清除分發課程ToolStripMenuItem.Enabled = true;

                DataRow dataRow01 = (DataRow)_RowList[0].Tag;
                if (_RowList[0].Tag != null)
                {
                    foreach (DataGridViewRow displayRow in _RowList)
                    {
                        DataRow dataRow = (DataRow)displayRow.Tag;

                        //if (dataRow != null)
                        //{
                        //    if ("" + dataRow["fail_reason"] == "")
                        //    {
                        //        手動輸入無法分發原因ToolStripMenuItem.Enabled = false;
                        //    }
                        //}

                        if ("" + dataRow01["subject_name"] == "" + dataRow["subject_name"] && "" + dataRow01["stu_dept"] == "" + dataRow["stu_dept"] && "" + dataRow01["subject_level"] == "" + dataRow["subject_level"] && "" + dataRow01["credit"] == "" + dataRow["credit"])
                        {
                            //此乃正常課程需求完全相同狀況，不需要對右鍵ToolStripMenuItem 內容 做disable
                        }
                        else
                        {
                            手動更正分發課程ToolStripMenuItem.Enabled = false;
                        }
                        //if ("" + dataRow["fail_reason"] != "")
                        //{
                        //    手動更正分發課程ToolStripMenuItem.Enabled = false;
                        //}


                    }
                }
            }
        }

        private void updateDataGridView()
        {
            dgData.Sort((DataGridViewSorter)comboBoxEx1.SelectedItem);

            foreach (DataRow dataRow in _DataTable.Rows)
            {
                _DisplayRow[dataRow].Visible = true;
                _DisplayRow[dataRow].Cells[8].Value = "";
                _DisplayRow[dataRow].Selected = false;
                if (("" + dataRow["fail_reason"]) != "")
                {
                    _DisplayRow[dataRow].Cells[8].Style.ForeColor = Color.LightGray;
                    _DisplayRow[dataRow].Cells[8].Value = "" + dataRow["fail_reason"];
                    if (("" + dataRow["fail_reason_ori"]) != ("" + dataRow["fail_reason"]))
                        _DisplayRow[dataRow].Cells[8].Style.ForeColor = Color.Gray;
                }
                if (("" + dataRow["distribution_id"]) != "")
                {
                    _DisplayRow[dataRow].Cells[8].Style.ForeColor = _DisplayRow[dataRow].DefaultCellStyle.ForeColor;
                    _DisplayRow[dataRow].Cells[8].Value = "" + dataRow["course_name"];
                    if (("" + dataRow["distribution_id"]) != ("" + dataRow["attend_course_id"]))
                        _DisplayRow[dataRow].Cells[8].Style.ForeColor = Color.Red;
                }

                if (checkBox1.Checked)//篩選未分發的
                {
                    if ("" + dataRow["distribution_id"] != "")
                    {
                        _DisplayRow[dataRow].Visible = false;
                    }
                }
                if (checkBox2.Checked)//篩選有調整分發的
                {
                    if ("" + dataRow["distribution_id"] == "" + dataRow["attend_course_id"]
                        && "" + dataRow["fail_reason_ori"] == "" + dataRow["fail_reason"])
                    {
                        _DisplayRow[dataRow].Visible = false;
                    }
                }
            }
        }




        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Enabled = false;
            }
            else
            {
                checkBox2.Enabled = true;
            }
            updateDataGridView();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Enabled = false;
            }
            else
            {
                checkBox1.Enabled = true;
            }

            updateDataGridView();
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateDataGridView();
        }
    }

    class DataGridViewSorter : System.Collections.IComparer
    {
        private List<DataGridViewRow> _OrgIndex = new List<DataGridViewRow>();

        public string Mode { get; set; }

        public DataGridViewSorter(string mode, List<DataGridViewRow> orgIndex)
        {
            Mode = mode;
            _OrgIndex = orgIndex;
        }

        int IComparer.Compare(object x, object y)
        {
            DataGridViewRow row1 = (DataGridViewRow)x;
            DataGridViewRow row2 = (DataGridViewRow)y;
            if (Mode == "依科目")
            {
                DataRow d1 = (DataRow)row1.Tag;
                DataRow d2 = (DataRow)row2.Tag;

                List<string> desc = new List<string>() { "credit", "subject_level" };
                foreach (var key in new string[] { "credit", "subject_name", "subject_level", "stu_dept", "distribution_id" })
                {
                    if ("" + d1[key] != "" + d2[key])
                    {
                        if (desc.Contains(key))
                            return (("" + d2[key]).PadLeft(20)).CompareTo((("" + d1[key]).PadLeft(20)));
                        else
                            return (("" + d1[key]).PadLeft(20)).CompareTo((("" + d2[key]).PadLeft(20)));
                    }
                }
            }
            //Mode=依學生 或 依科目的比較條件都相等
            return _OrgIndex.IndexOf(row1).CompareTo(_OrgIndex.IndexOf(row2));
        }
    }
}
