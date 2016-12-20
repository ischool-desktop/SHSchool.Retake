using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA;
using FISCA.Presentation;
using FISCA.Permission;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using SHSchool.Retake.DAO;
using System.Windows.Forms;
using Campus.DocumentValidator;
using System.ComponentModel;

namespace SHSchool.Retake
{
    /// <summary>
    /// 客製重補修系統
    /// </summary>
    public class Program
    {
        static BackgroundWorker _bgLLoadUDT = new BackgroundWorker();
        [MainMethod()]
        public static void Main()
        {
            // 更新 UDS UDT 方式             
            //if (!FISCA.RTContext.IsDiagMode)
            //    FISCA.ServerModule.AutoManaged("http://module.ischool.com.tw/module/137/Retake_Shinmin_dep/udm.xml");

            #region 自訂驗證規則
            FactoryProvider.FieldFactory.Add(new FieldValidatorFactory());
            FactoryProvider.RowFactory.Add(new RowValidatorFactory());
            #endregion
            _bgLLoadUDT.DoWork += new DoWorkEventHandler(_bgLLoadUDT_DoWork);
            _bgLLoadUDT.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgLLoadUDT_RunWorkerCompleted);
            _bgLLoadUDT.RunWorkerAsync();
            MotherForm.AddPanel(RetakeAdmin.Instance);

            // Add ListView
            RetakeAdmin.Instance.AddView(new RetakeViewTree());

            RetakeAdmin.Instance.SelectedSourceChanged += delegate
            {
                FISCA.Presentation.MotherForm.SetStatusBarMessage("選擇「" + RetakeAdmin.Instance.SelectedSource.Count + "」個課程");
            };
            {
                // 新增課程
                Catalog catalog08A = RoleAclSource.Instance["重補修"]["編輯"];
                catalog08A.Add(new RibbonFeature("SHSchool.Retake.AddCourse", "新增課程"));

                RibbonBarItem item08A = RetakeAdmin.Instance.RibbonBarItems["編輯"];
                item08A["新增課程"].Image = Properties.Resources.新增課程;
                item08A["新增課程"].Size = RibbonBarButton.MenuButtonSize.Large;
                item08A["新增課程"].Enable = UserAcl.Current["SHSchool.Retake.AddCourse"].Executable;
                item08A["新增課程"].Click += delegate
                {
                    Form.AddCourse ac = new Form.AddCourse();
                    //ac.ShowDialog();
                };
            }
            {
                // 刪除課程
                Catalog catalog08 = RoleAclSource.Instance["重補修"]["編輯"];
                catalog08.Add(new RibbonFeature("SHSchool.Retake.DeleteCourse", "刪除課程"));

                RibbonBarItem item08 = RetakeAdmin.Instance.RibbonBarItems["編輯"];
                item08["刪除課程"].Image = Properties.Resources.刪除課程;
                item08["刪除課程"].Size = RibbonBarButton.MenuButtonSize.Large;
                item08["刪除課程"].Enable = UserAcl.Current["SHSchool.Retake.DeleteCourse"].Executable;
                item08["刪除課程"].Click += delegate
                {
                    DeleteCourse();
                };
            }
            //{ 
            //    // 重補修開課
            //    Catalog catalog04 = RoleAclSource.Instance["重補修"]["編輯"];
            //    catalog04.Add(new RibbonFeature("SHSchool.Retake.CreateCourseInfoForm", "開課"));

            //    RibbonBarItem item04 = RetakeAdmin.Instance.RibbonBarItems["編輯"];
            //    item04["批次開課"].Image = Properties.Resources.開課;
            //    item04["批次開課"].Size = RibbonBarButton.MenuButtonSize.Medium;
            //    item04["批次開課"].Enable = UserAcl.Current["SHSchool.Retake.CourseTimetableForm"].Executable;
            //    item04["批次開課"].Click += delegate
            //    {
            //        Form.CreateCourseInfoForm ccif = new Form.CreateCourseInfoForm();
            //        ccif.ShowDialog();
            //    };
            //}
            {
                // 重補修課表管理
                Catalog catalog03 = RoleAclSource.Instance["重補修"]["編輯"];
                catalog03.Add(new RibbonFeature("SHSchool.Retake.CourseTimetableForm", "開課科別管理"));

                RibbonBarItem item03 = RetakeAdmin.Instance.RibbonBarItems["編輯"];
                item03["開課科別管理"].Image = Properties.Resources.重補修課表管理_schedule_write_64;
                item03["開課科別管理"].Size = RibbonBarButton.MenuButtonSize.Medium;
                item03["開課科別管理"].Enable = UserAcl.Current["SHSchool.Retake.CourseTimetableForm"].Executable;
                item03["開課科別管理"].Click += delegate
                {
                    Form.CourseTimetableForm ctf = new Form.CourseTimetableForm();
                    ctf.ShowDialog();
                };
            }
            {
                //時間表設定
                Catalog catalog21 = RoleAclSource.Instance["重補修"]["編輯"];
                catalog21.Add(new RibbonFeature("SHSchool.Retake.ReSetSubjectDate", "課程時間表設定"));
                RibbonBarItem item11 = RetakeAdmin.Instance.RibbonBarItems["編輯"];
                item11["課程時間表設定"].Enable = UserAcl.Current["SHSchool.Retake.ReSetSubjectDate"].Executable;
                item11["課程時間表設定"].Size = RibbonBarButton.MenuButtonSize.Medium;
                item11["課程時間表設定"].Image = Properties.Resources.lesson_planning_clock_64;
                item11["課程時間表設定"].Click += delegate
                {
                    if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                    {
                        Form.ReSetSubjectDate ccif = new Form.ReSetSubjectDate();
                        ccif.ShowDialog();
                    }
                    else
                    {
                        MsgBox.Show("請選擇課程!!");
                    }
                };
            }
            {
                // 建議重補修名單
                Catalog catalog01 = RoleAclSource.Instance["重補修"]["重補修選課"];
                catalog01.Add(new RibbonFeature("SHSchool.Retake.SuggestListForm", "梯次及重補修名單"));

                RibbonBarItem item01 = RetakeAdmin.Instance.RibbonBarItems["重補修選課"];
                item01["梯次及重補修名單"].Image = Properties.Resources.建議重補修名單_filter_data_add_64;
                item01["梯次及重補修名單"].Size = RibbonBarButton.MenuButtonSize.Medium;
                item01["梯次及重補修名單"].Enable = UserAcl.Current["SHSchool.Retake.SuggestListForm"].Executable;
                item01["梯次及重補修名單"].Click += delegate
                {
                    Form.SuggestListForm slf = new Form.SuggestListForm();
                    slf.ShowDialog();
                };
            }
            {
                // 重補修科目管理
                Catalog catalog02 = RoleAclSource.Instance["重補修"]["重補修選課"];
                catalog02.Add(new RibbonFeature("SHSchool.Retake.SubjectListForm", "開放選課科目管理"));

                RibbonBarItem item02 = RetakeAdmin.Instance.RibbonBarItems["重補修選課"];
                item02["開放選課科目管理"].Image = Properties.Resources.重補修科目管理_project_64;
                item02["開放選課科目管理"].Size = RibbonBarButton.MenuButtonSize.Medium;
                item02["開放選課科目管理"].Enable = UserAcl.Current["SHSchool.Retake.SubjectListForm"].Executable;
                item02["開放選課科目管理"].Click += delegate
                {
                    Form.SubjectListForm slf = new Form.SubjectListForm();
                    if (slf._isShowForm)
                        slf.ShowDialog();
                    else
                        slf.Close();
                };
            }
            {

                //選課開放時間
                Catalog catalog05 = RoleAclSource.Instance["重補修"]["重補修選課"];
                catalog05.Add(new RibbonFeature("SHSchool.Retake.RetakeJoinForm", "選課開放時間"));

                RibbonBarItem item05 = RetakeAdmin.Instance.RibbonBarItems["重補修選課"];
                item05["選課開放時間"].Enable = UserAcl.Current["SHSchool.Retake.RetakeJoinForm"].Executable;
                item05["選課開放時間"].Size = RibbonBarButton.MenuButtonSize.Medium;
                item05["選課開放時間"].Image = Properties.Resources.time_frame_refresh_128;
                item05["選課開放時間"].Click += delegate
                {
                    Form.RetakeJoinForm ccif = new Form.RetakeJoinForm();
                    ccif.ShowDialog();
                };
            }
            {

                //匯出選課結果
                Catalog catalog05 = RoleAclSource.Instance["重補修"]["重補修選課"];
                catalog05.Add(new RibbonFeature("D087192D-9B83-4D25-B5F5-A337E4F4A907", "匯出選課結果"));

                RibbonBarItem item05 = RetakeAdmin.Instance.RibbonBarItems["重補修選課"];
                item05["匯出選課結果"].Enable = UserAcl.Current["D087192D-9B83-4D25-B5F5-A337E4F4A907"].Executable;
                item05["匯出選課結果"].Size = RibbonBarButton.MenuButtonSize.Medium;
                item05["匯出選課結果"].Image = Properties.Resources.匯出;
                item05["匯出選課結果"].Click += delegate
                {
                    FISCA.Data.QueryHelper queryHelper = new FISCA.Data.QueryHelper();
                    var dt = queryHelper.Select(@"
SELECT dept.stu_dept, class.grade_year, class.class_name, student.seat_no, student.student_number, student.name
	, scourse.subject_name, scourse.subject_level, scourse.credit
	, session.school_year, session.semester, session.round
	, CASE WHEN cscourse.subject_type is null THEN sscourse.subject_type ELSE cscourse.subject_type END
	, cscourse.course_name
	, sscourse.uid as select_subject_id, cscourse.uid as attend_course_id
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
ORDER BY session.school_year desc, session.semester desc, session.round desc, stu_dept, class.grade_year, class.display_order, class.class_name, student.seat_no, student.id, scourse.subject_name, scourse.subject_level
");
                    Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook();
                    int colIndex, rowIndex;
                    rowIndex = 0;
                    colIndex = 0;
                    {
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("科別");
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("年級");
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("班級");
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("座號");
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("學號");
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("姓名");
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("科目");
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("級別");
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("學分數");
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("學年度");
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("學期");
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("梯次");
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("科目類別");
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("重補修課程名稱");
                        rowIndex++;
                    }

                    foreach (System.Data.DataRow row in dt.Rows)
                    {
                        colIndex = 0;
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("" + row["stu_dept"]);
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("" + row["grade_year"]);
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("" + row["class_name"]);
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("" + row["seat_no"]); 
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("" + row["student_number"]);
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("" + row["name"]);
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("" + row["subject_name"]);
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("" + row["subject_level"]);
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("" + row["credit"]);
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("" + row["school_year"]);
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("" + row["semester"]);
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("" + row["round"]);
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("" + row["subject_type"]);
                        wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue("" + row["course_name"]);
                        rowIndex++;
                    }
                    wb.Worksheets[0].AutoFitColumns();


                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.FileName = "匯出重補修學生選課結果";
                    saveFileDialog1.Filter = "Excel (*.xls)|*.xls";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        wb.Save(saveFileDialog1.FileName);
                        System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                    }
                };
            }
            {
                //選課開放時間
                Catalog catalog05 = RoleAclSource.Instance["重補修"]["重補修選課"];
                catalog05.Add(new RibbonFeature("A754CBCD-F002-40BB-BDD7-86A8B259E613", "選課課程分發"));

                RibbonBarItem item05 = RetakeAdmin.Instance.RibbonBarItems["重補修選課"];
                item05["選課課程分發"].Enable = UserAcl.Current["A754CBCD-F002-40BB-BDD7-86A8B259E613"].Executable;
                item05["選課課程分發"].Size = RibbonBarButton.MenuButtonSize.Medium;
                item05["選課課程分發"].Image = Properties.Resources.time_frame_refresh_128;
                item05["選課課程分發"].Click += delegate
                {
                    new Form.SCSelectDistribution().ShowDialog();
                };
            }
            {
                //匯入課程
                Catalog catalog09 = RoleAclSource.Instance["重補修"]["資料統計"];
                catalog09.Add(new RibbonFeature("SHSchool.Retake.ImportCourse", "匯入課程基本資料"));

                RibbonBarItem item09 = RetakeAdmin.Instance.RibbonBarItems["資料統計"];
                item09["匯入"].Image = Properties.Resources.Import_Image;
                item09["匯入"].Size = RibbonBarButton.MenuButtonSize.Large;
                item09["匯入"]["匯入課程基本資料"].Enable = UserAcl.Current["SHSchool.Retake.ImportCourse"].Executable;
                item09["匯入"]["匯入課程基本資料"].Click += delegate
                {
                    new ImportCourseExtension().Execute();
                    RetakeEvents.RaiseAssnChanged();
                };
            }
            {
                //匯入學生修課
                Catalog catalog20 = RoleAclSource.Instance["重補修"]["資料統計"];
                catalog20.Add(new RibbonFeature("SHSchool.Retake.ImportSCAttend", "匯入學生修課"));

                RibbonBarItem item10 = RetakeAdmin.Instance.RibbonBarItems["資料統計"];
                item10["匯入"]["匯入修課學生"].Enable = UserAcl.Current["SHSchool.Retake.ImportSCAttend"].Executable;
                item10["匯入"]["匯入修課學生"].Click += delegate
                {
                    new ImportSCAttend().Execute();
                    RetakeEvents.RaiseAssnChanged();
                };
            }
            {
                // 報表 課程點名單
                Catalog catalog22 = RoleAclSource.Instance["重補修"]["資料統計"];
                catalog22.Add(new RibbonFeature("SHSchool.Retake.Report.CourseStudentSCReport", "課程點名單"));
                RibbonBarItem itemRpt01 = RetakeAdmin.Instance.RibbonBarItems["資料統計"];
                itemRpt01["報表"]["課程點名單"].Enable = UserAcl.Current["SHSchool.Retake.Report.CourseStudentSCReport"].Executable;
                itemRpt01["報表"].Image = Properties.Resources.Report;
                itemRpt01["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
                itemRpt01["報表"]["課程點名單"].Click += delegate
                {
                    if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                    {
                        Report.CourseStudentSCReportForm cssf = new Report.CourseStudentSCReportForm(RetakeAdmin.Instance.SelectedSource);
                        cssf.ShowDialog();
                    }
                    else
                    {
                        MsgBox.Show("請選擇課程!");
                    }
                };
            }
            {

                // 報表 課程缺曠名單
                Catalog catalog23 = RoleAclSource.Instance["重補修"]["資料統計"];
                catalog23.Add(new RibbonFeature("SHSchool.Retake.Report.CourseStudentAttendReport", "課程缺曠名單"));

                RibbonBarItem itemRpt02 = RetakeAdmin.Instance.RibbonBarItems["資料統計"];
                itemRpt02["報表"]["課程缺曠名單"].Enable = UserAcl.Current["SHSchool.Retake.Report.CourseStudentAttendReport"].Executable;
                itemRpt02["報表"].Image = Properties.Resources.Report;
                itemRpt02["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
                itemRpt02["報表"]["課程缺曠名單"].Click += delegate
                {
                    if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                    {
                        Report.CourseStudentAttendReportForm csscf = new Report.CourseStudentAttendReportForm(RetakeAdmin.Instance.SelectedSource);
                        csscf.ShowDialog();
                    }
                    else
                    {
                        MsgBox.Show("請選擇課程!");
                    }
                };
            }
            {
                // 報表 缺曠通知單
                Catalog catalog24 = RoleAclSource.Instance["重補修"]["資料統計"];
                catalog24.Add(new RibbonFeature("SHSchool.Retake.Report.StudentAttendanceReportForm", "缺曠通知單"));
                RibbonBarItem itemRpt03 = RetakeAdmin.Instance.RibbonBarItems["資料統計"];
                itemRpt03["報表"]["缺曠通知單"].Enable = UserAcl.Current["SHSchool.Retake.Report.StudentAttendanceReportForm"].Executable;
                itemRpt03["報表"].Image = Properties.Resources.Report;
                itemRpt03["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
                itemRpt03["報表"]["缺曠通知單"].Click += delegate
                {
                    if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                    {
                        Report.StudentAttendanceReportForm sarf = new Report.StudentAttendanceReportForm(RetakeAdmin.Instance.SelectedSource);
                        sarf.ShowDialog();

                    }
                    else
                    {
                        MsgBox.Show("請選擇課程!");
                    }
                };
            }
            {

                // 報表 及格人數
                Catalog catalog28 = RoleAclSource.Instance["重補修"]["資料統計"];
                catalog28.Add(new RibbonFeature("SHSchool.Retake.Report.StudentPassReport", "及格人數"));

                RibbonBarItem itemRpt04 = RetakeAdmin.Instance.RibbonBarItems["資料統計"];
                itemRpt04["報表"]["及格人數"].Enable = UserAcl.Current["SHSchool.Retake.Report.StudentPassReport"].Executable;
                itemRpt04["報表"].Image = Properties.Resources.Report;
                //itemRpt04["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
                itemRpt04["報表"]["及格人數"].Click += delegate
                {
                    Report.StudentPassReport spr = new Report.StudentPassReport();
                    spr.Main();
                };
            }
            {

                // 報表 課程缺曠統計表
                Catalog catalog29 = RoleAclSource.Instance["重補修"]["資料統計"];
                catalog29.Add(new RibbonFeature("SHSchool.Retake.Report.CourseAttendanceRpt", "課程缺曠統計表"));

                RibbonBarItem itemRpt05 = RetakeAdmin.Instance.RibbonBarItems["資料統計"];
                itemRpt05["報表"]["課程缺曠統計表"].Enable = UserAcl.Current["SHSchool.Retake.Report.CourseAttendanceRpt"].Executable;
                itemRpt05["報表"].Image = Properties.Resources.Report;
                itemRpt05["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
                itemRpt05["報表"]["課程缺曠統計表"].Click += delegate
                {
                    if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                    {
                        Report.CourseAttendanceRpt car = new Report.CourseAttendanceRpt(RetakeAdmin.Instance.SelectedSource);
                        car.Run();

                    }
                    else
                    {
                        MsgBox.Show("請選擇課程!");
                    }
                };
            }

            {
                // 成績 評量設定
                Catalog catalog25 = RoleAclSource.Instance["重補修"]["成績"];
                catalog25.Add(new RibbonFeature("SHSchool.Retake.GradingProjectConfigForm", "評量設定"));

                RibbonBarItem Results = RetakeAdmin.Instance.RibbonBarItems["成績"];
                Results["評量設定"].Size = RibbonBarButton.MenuButtonSize.Medium;
                Results["評量設定"].Image = Properties.Resources.barchart_64;
                Results["評量設定"].Enable = UserAcl.Current["SHSchool.Retake.GradingProjectConfigForm"].Executable;
                Results["評量設定"].Click += delegate
                {
                    Form.GradingProjectConfigForm gpcf = new Form.GradingProjectConfigForm();
                    gpcf.ShowDialog();
                };
            }
            {

                //成績輸入區間
                Catalog catalog06 = RoleAclSource.Instance["重補修"]["成績"];
                catalog06.Add(new RibbonFeature("SHSchool.Retake.ResultsInputForm", "成績輸入區間"));

                RibbonBarItem Results = RetakeAdmin.Instance.RibbonBarItems["成績"];
                RibbonBarItem item06 = RetakeAdmin.Instance.RibbonBarItems["成績"];
                item06["成績輸入區間"].Enable = UserAcl.Current["SHSchool.Retake.ResultsInputForm"].Executable;
                item06["成績輸入區間"].Size = RibbonBarButton.MenuButtonSize.Medium;
                item06["成績輸入區間"].Image = Properties.Resources.time_frame_refresh_128;
                item06["成績輸入區間"].Click += delegate
                {
                    Form.ResultsInputForm ccif = new Form.ResultsInputForm();
                    ccif.ShowDialog();
                };
            }
            {

                // 成績 成績輸入
                Catalog catalog26 = RoleAclSource.Instance["重補修"]["成績"];
                catalog26.Add(new RibbonFeature("SHSchool.Retake.RetakeResultsInputForm", "成績輸入"));

                RibbonBarItem Results = RetakeAdmin.Instance.RibbonBarItems["成績"];
                Results["成績輸入"].Size = RibbonBarButton.MenuButtonSize.Medium;
                Results["成績輸入"].Image = Properties.Resources.marker_fav_64;
                Results["成績輸入"].Enable = UserAcl.Current["SHSchool.Retake.RetakeResultsInputForm"].Executable;
                Results["成績輸入"].Click += delegate
                {
                    if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                    {
                        Form.RetakeResultsInputForm rrif = new Form.RetakeResultsInputForm(RetakeAdmin.Instance.SelectedSource);
                        rrif.ShowDialog();
                    }
                    else
                    {
                        MsgBox.Show("請選擇課程!");
                    }
                };
            }
            {
                // 成績 學期結算
                Catalog catalog27 = RoleAclSource.Instance["重補修"]["成績"];
                catalog27.Add(new RibbonFeature("SHSchool.Retake.ClearingForm", "成績結算"));

                RibbonBarItem Results = RetakeAdmin.Instance.RibbonBarItems["成績"];
                Results["成績結算"].Size = RibbonBarButton.MenuButtonSize.Medium;
                Results["成績結算"].Image = Properties.Resources.brand_write_64;
                Results["成績結算"].Enable = UserAcl.Current["SHSchool.Retake.ClearingForm"].Executable;
                Results["成績結算"].Click += delegate
                {
                    Form.ClearingForm cf = new Form.ClearingForm();
                    cf.ShowDialog();
                };
            }

            {

                //課程缺曠登錄
                Catalog catalog07 = RoleAclSource.Instance["重補修"]["缺曠"];
                catalog07.Add(new RibbonFeature("SHSchool.Retake.AttendanceForm", "缺曠登錄"));

                RibbonBarItem item07 = RetakeAdmin.Instance.RibbonBarItems["缺曠"];
                item07["缺曠登錄"].Enable = UserAcl.Current["SHSchool.Retake.AttendanceForm"].Executable;
                item07["缺曠登錄"].Size = RibbonBarButton.MenuButtonSize.Medium;
                item07["缺曠登錄"].Image = Properties.Resources.desk_64;
                item07["缺曠登錄"].Click += delegate
                {
                    Form.AttendanceForm ccif = new Form.AttendanceForm();
                    ccif.ShowDialog();
                };
            }
            {

                //課程扣考查詢
                Catalog catalog07a = RoleAclSource.Instance["重補修"]["缺曠"];
                catalog07a.Add(new RibbonFeature("SHSchool.Retake.StudentNotExamSearchForm", "扣考查詢"));

                RibbonBarItem item07a = RetakeAdmin.Instance.RibbonBarItems["缺曠"];
                item07a["扣考查詢"].Enable = UserAcl.Current["SHSchool.Retake.StudentNotExamSearchForm"].Executable;
                item07a["扣考查詢"].Size = RibbonBarButton.MenuButtonSize.Medium;
                item07a["扣考查詢"].Image = Properties.Resources.desk_64;
                item07a["扣考查詢"].Click += delegate
                {
                    if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                    {
                        Form.StudentNotExamSearchForm snesf = new Form.StudentNotExamSearchForm(RetakeAdmin.Instance.SelectedSource);
                        snesf.ShowDialog();
                    }
                    else
                    {
                        MsgBox.Show("請選擇課程!!");
                        return;
                    }

                };
            }
            {

                //匯出課程成績
                Catalog catalogSc01 = RoleAclSource.Instance["重補修"]["資料統計"];
                catalogSc01.Add(new RibbonFeature("SHSchool.Retake.ExportCourseScore", "課程學期成績匯入檔"));

                RibbonBarItem itemEp01 = RetakeAdmin.Instance.RibbonBarItems["資料統計"];
                itemEp01["匯出"].Image = Properties.Resources.匯出;
                itemEp01["匯出"].Size = RibbonBarButton.MenuButtonSize.Large;
                itemEp01["匯出"]["課程學期成績匯入檔"].Enable = UserAcl.Current["SHSchool.Retake.ExportCourseScore"].Executable;
                itemEp01["匯出"]["課程學期成績匯入檔"].Click += delegate
                {
                    if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                    {
                        ImportExport.ExportCourseScore ecs = new ImportExport.ExportCourseScore(RetakeAdmin.Instance.SelectedSource);
                    }
                    else
                        FISCA.Presentation.Controls.MsgBox.Show("請選擇課程!");
                };
            }
            {
                //匯出學生選課清單
                Catalog catalogSc02 = RoleAclSource.Instance["重補修"]["資料統計"];
                catalogSc02.Add(new RibbonFeature("SHSchool.Retake.ExportStudentCourseSelect", "學生選課清單"));
                RibbonBarItem itemEp02 = RetakeAdmin.Instance.RibbonBarItems["資料統計"];
                itemEp02["匯出"].Image = Properties.Resources.匯出;
                itemEp02["匯出"].Size = RibbonBarButton.MenuButtonSize.Large;
                itemEp02["匯出"]["學生選課清單"].Enable = UserAcl.Current["SHSchool.Retake.ExportStudentCourseSelect"].Executable;
                itemEp02["匯出"]["學生選課清單"].Click += delegate
                {
                    if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                    {
                        ImportExport.ExportStudentCourseSelect escs = new ImportExport.ExportStudentCourseSelect(RetakeAdmin.Instance.SelectedSource);
                    }
                    else
                        FISCA.Presentation.Controls.MsgBox.Show("請選擇課程!");
                };
            }
            {
                //匯出課程
                Catalog catalogSc03 = RoleAclSource.Instance["重補修"]["資料統計"];
                catalogSc03.Add(new RibbonFeature("SHSchool.Retake.ExportCourseInfo", "匯出課程基本資料"));

                RibbonBarItem itemEp03 = RetakeAdmin.Instance.RibbonBarItems["資料統計"];
                itemEp03["匯出"].Image = Properties.Resources.匯出;
                itemEp03["匯出"].Size = RibbonBarButton.MenuButtonSize.Large;
                itemEp03["匯出"]["匯出課程基本資料"].Enable = UserAcl.Current["SHSchool.Retake.ExportCourseInfo"].Executable;
                itemEp03["匯出"]["匯出課程基本資料"].Click += delegate
                {
                    if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                    {
                        ImportExport.ExportCourseInfo exporter = new ImportExport.ExportCourseInfo(RetakeAdmin.Instance.SelectedSource);

                        ExportStudentV2 wizard = new ExportStudentV2(exporter.Text, exporter.Image, RetakeAdmin.Instance.SelectedSource);
                        exporter.InitializeExport(wizard);
                        wizard.ShowDialog();
                    }
                    else
                        FISCA.Presentation.Controls.MsgBox.Show("請選擇課程!");
                };
            }
            {
                //匯出修課學生
                Catalog catalogSc04 = RoleAclSource.Instance["重補修"]["資料統計"];
                catalogSc04.Add(new RibbonFeature("SHSchool.Retake.ExportSCAttend", "匯出修課學生"));

                RibbonBarItem itemEp04 = RetakeAdmin.Instance.RibbonBarItems["資料統計"];
                itemEp04["匯出"].Image = Properties.Resources.匯出;
                itemEp04["匯出"].Size = RibbonBarButton.MenuButtonSize.Large;
                itemEp04["匯出"]["匯出修課學生"].Enable = UserAcl.Current["SHSchool.Retake.ExportSCAttend"].Executable;
                itemEp04["匯出"]["匯出修課學生"].Click += delegate
                {
                    if (RetakeAdmin.Instance.SelectedSource.Count > 0)
                    {
                        ImportExport.ExportSCAttend exporter = new ImportExport.ExportSCAttend(RetakeAdmin.Instance.SelectedSource);

                        ExportStudentV2 wizard = new ExportStudentV2(exporter.Text, exporter.Image, RetakeAdmin.Instance.SelectedSource);
                        exporter.InitializeExport(wizard);
                        wizard.ShowDialog();
                    }
                    else
                        FISCA.Presentation.Controls.MsgBox.Show("請選擇課程!");
                };
            }
            {
                //其它
                MenuButton MenuItem09 = RetakeAdmin.Instance.ListPaneContexMenu["刪除課程"];
                MenuItem09.Enable = UserAcl.Current["SHSchool.Retake.DeleteCourse"].Executable;
                MenuItem09.Click += delegate
                {
                    DeleteCourse();
                };
            }
            {
                // 報表 重補修缺課(含扣考)通知單
                Catalog catalogP01 = RoleAclSource.Instance["重補修"]["功能按鈕"];
                catalogP01.Add(new RibbonFeature("SHSchool.Retake.Report.StudentCourseAttendanceRptForm", "重補修缺課(含扣考)通知單"));

                // 報表 重補修缺課(含扣考)通知單( 在學生>
                var studItemRpt01 = K12.Presentation.NLDPanels.Student.RibbonBarItems["資料統計"]["報表"]["重補修報表"]["重補修缺課(含扣考)通知單"];
                studItemRpt01.Enable = UserAcl.Current["SHSchool.Retake.Report.StudentCourseAttendanceRptForm"].Executable;
                studItemRpt01.Image = Properties.Resources.Report;
                studItemRpt01.Click += delegate
                {
                    if (K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0)
                    {
                        Report.StudentCourseAttendanceRptForm scarf = new Report.StudentCourseAttendanceRptForm(K12.Presentation.NLDPanels.Student.SelectedSource);
                        scarf.ShowDialog();
                    }
                    else
                    {
                        MsgBox.Show("請選擇學生!");
                    }
                };
            }

            // 課程基本資料 資料項目
            FeatureAce UserPermission = FISCA.Permission.UserAcl.Current["SHSchool.Retake.DetailContent.CourseInfoContent"];
            if (UserPermission.Editable)
                RetakeAdmin.Instance.AddDetailBulider(new DetailBulider<DetailContent.CourseInfoContent>());

            // 課程修課學生 資料項目
            UserPermission = FISCA.Permission.UserAcl.Current["SHSchool.Retake.DetailContent.CourseStudentContent"];
            if (UserPermission.Editable)
                RetakeAdmin.Instance.AddDetailBulider(new DetailBulider<DetailContent.CourseStudentContent>());

            // 課程時間表 資料項目
            UserPermission = FISCA.Permission.UserAcl.Current["SHSchool.Retake.DetailContent.CourseTimeSectionViewContent"];
            if (UserPermission.Editable)
                RetakeAdmin.Instance.AddDetailBulider(new DetailBulider<DetailContent.CourseTimeSectionViewContent>());

            
            // 資料項目權限註冊
            Catalog detailItem = RoleAclSource.Instance["重補修"]["資料項目"];
            detailItem.Add(new DetailItemFeature("SHSchool.Retake.DetailContent.CourseInfoContent", "課程基本資料"));
            detailItem.Add(new DetailItemFeature("SHSchool.Retake.DetailContent.CourseStudentContent", "課程修課學生"));
            detailItem.Add(new DetailItemFeature("SHSchool.Retake.DetailContent.CourseTimeSectionViewContent", "課程時間表"));
        }

        static void _bgLLoadUDT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                FISCA.Presentation.Controls.MsgBox.Show("重補修 UDT Table 載入過程發生錯誤" + e.Error.Message);
            }
        }

        static void _bgLLoadUDT_DoWork(object sender, DoWorkEventArgs e)
        {
            // 載入UDT table
            //// 成績輸入時間
            //UDTTransfer.UDTScoreInputDateSelect();
            //// 學生選課
            //UDTTransfer.UDTSsselectLoad();
            UDTTransfer.CreateRetakeUDTTable();
        }

        private static void DeleteCourse()
        {
            if (RetakeAdmin.Instance.SelectedSource.Count > 0)
            {
                DialogResult dr = MsgBox.Show("您確定要刪除課程?\n\n本功能將會串連刪除:\n(課程,修課學生,成績,缺曠)", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    AccessHelper _accessHelper = new AccessHelper();

                    //刪缺曠
                    List<UDTAttendanceDef> attendanceList = _accessHelper.Select<UDTAttendanceDef>("ref_course_id in ('" + string.Join("','", RetakeAdmin.Instance.SelectedSource) + "')");
                    _accessHelper.DeletedValues(attendanceList);

                    //時間區間
                    List<UDTTimeSectionDef> timeSectionList = _accessHelper.Select<UDTTimeSectionDef>("ref_course_id in ('" + string.Join("','", RetakeAdmin.Instance.SelectedSource) + "')");
                    _accessHelper.DeletedValues(timeSectionList);

                    //刪學生
                    List<UDTScselectDef> cselectList = _accessHelper.Select<UDTScselectDef>("ref_course_id in ('" + string.Join("','", RetakeAdmin.Instance.SelectedSource) + "')");
                    _accessHelper.DeletedValues(cselectList);

                    //刪課程
                    List<UDTCourseDef> courseList = _accessHelper.Select<UDTCourseDef>("UID in ('" + string.Join("','", RetakeAdmin.Instance.SelectedSource) + "')");
                    _accessHelper.DeletedValues(courseList);

                    MsgBox.Show("刪除成功!!");
                    RetakeEvents.RaiseAssnChanged();
                }
            }
            else
            {
                MsgBox.Show("請選擇課程!!");
            }
        }

    }
}
