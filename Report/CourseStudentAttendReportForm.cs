﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using K12.Data;
using SHSchool.Retake.DAO;
using Aspose.Cells;
using System.IO;


namespace SHSchool.Retake.Report
{
    public partial class CourseStudentAttendReportForm : FISCA.Presentation.Controls.BaseForm
    {
     
        BackgroundWorker _bgWorker = new BackgroundWorker();
        BackgroundWorker _bgLoadData = new BackgroundWorker();
        List<UDTCourseDef> _CourseList = new List<UDTCourseDef>();
        /// <summary>
        /// 修課
        /// </summary>
        List<UDTScselectDef> _ScselectList = new List<UDTScselectDef>();

        /// <summary>
        /// 上課時間
        /// </summary>
        List<UDTTimeSectionDef> _TimeSectionList = new List<UDTTimeSectionDef>();

        /// <summary>
        /// 使用者所選上課時間
        /// </summary>
        List<DateTime> _SelectDateSession = new List<DateTime>();

        /// <summary>
        /// 課程缺曠資料
        /// </summary>
        List<UDTAttendanceDef> _AttendanceList = new List<UDTAttendanceDef>();

        List<string> _SelectCourseIDList = new List<string>();

        /// <summary>
        /// 班級名稱
        /// </summary>
        Dictionary<string, string> _classNameDict = new Dictionary<string, string>();
        public CourseStudentAttendReportForm(List<string> CourseIDList)
        {
            InitializeComponent();
            // 讀取所選 CourseID
            _SelectCourseIDList = CourseIDList;
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
            _bgWorker.ProgressChanged += new ProgressChangedEventHandler(_bgWorker_ProgressChanged);
            _bgWorker.WorkerReportsProgress = true;
            _bgLoadData.DoWork += new DoWorkEventHandler(_bgLoadData_DoWork);
            _bgLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgLoadData_RunWorkerCompleted);

            // 載入資料
            _bgLoadData.RunWorkerAsync();
        }

        void _bgLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lvwData.Items.Clear();
            List<DateTime> dtList = new List<DateTime>();
            foreach (UDTTimeSectionDef data in _TimeSectionList)
            {
                if (!dtList.Contains(data.Date))
                    dtList.Add(data.Date);
            }
            dtList.Sort();
            // 將上課時間放在畫面
            foreach (DateTime dt in dtList)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Tag = dt;
                lvi.Checked = false;                
                lvi.Text = dt.ToShortDateString();
                lvwData.Items.Add(lvi);
            }
        }

        void _bgLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            // 取得課程資料
            _CourseList = UDTTransfer.UDTCourseSelectUIDs(_SelectCourseIDList);
            // 取得上課時間表            
            _TimeSectionList = UDTTransfer.UDTTimeSectionSelectByCourseIDList(_SelectCourseIDList);

            _classNameDict.Clear();
            foreach (ClassRecord cr in Class.SelectAll())
                _classNameDict.Add(cr.ID, cr.Name);
        }

        void _bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FISCA.Presentation.MotherForm.SetStatusBarMessage("重補修課程缺曠名單產生中..", e.ProgressPercentage);
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnPrint.Enabled = true;
            Workbook wb = (Workbook)e.Result;
            FISCA.Presentation.MotherForm.SetStatusBarMessage("重補修課程缺曠名單產生完成。", 100);

            try
            {
                string FilePath = Application.StartupPath + "\\Reports\\重補修課程缺曠名單.xls";
                wb.Save(FilePath, Aspose.Cells.FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(FilePath);

            }
            catch
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = "重補修課程缺曠名單.xls";
                sd.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd.FileName, Aspose.Cells.FileFormatType.Excel2003);
                        System.Diagnostics.Process.Start(sd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _bgWorker.ReportProgress(1);
            Workbook wbTemplate = new Workbook();
            Workbook wb = new Workbook();
            wbTemplate.Open(new MemoryStream(Properties.Resources.課程缺曠名單樣板));
            wb.Open(new MemoryStream(Properties.Resources.課程缺曠名單樣板));

            Range R_Title = wbTemplate.Worksheets[1].Cells.CreateRange(0, 1, false);
            Range R_CourseName = wbTemplate.Worksheets[1].Cells.CreateRange(1, 1, false);
            Range R_Message = wbTemplate.Worksheets[1].Cells.CreateRange(2, 1, false);
            Range R_Field = wbTemplate.Worksheets[1].Cells.CreateRange(3, 1, false);
            Range R_Value = wbTemplate.Worksheets[1].Cells.CreateRange(4, 1, false);

            

            // 取得修課學生資料
            _ScselectList = UDTTransfer.UDTSCSelectByCourseIDList(_SelectCourseIDList);

            // 取得缺曠
            _AttendanceList = UDTTransfer.UDTAttendanceSelectByCourseIDList(_SelectCourseIDList);

            int RowTitleIdx = 0, RowCourseIdx = 1, RowMessageIdx = 2, RowFieldIdx = 3, RowValueIdx = 4;

            _bgWorker.ReportProgress(30);
            //取得修課學生ID
            List<string> studIDList = new List<string>();

            foreach (UDTScselectDef data in _ScselectList)
            {
                string sid = data.StudentID.ToString();
                if (!studIDList.Contains(sid))
                    studIDList.Add(sid);
            }
            Dictionary<int, StudentRecord> studRecDict = new Dictionary<int, StudentRecord>();
            foreach (StudentRecord stud in Student.SelectByIDs(studIDList))
            {
                int sid = int.Parse(stud.ID);
                studRecDict.Add(sid, stud);
            }

            // 產生報表
            foreach (UDTCourseDef courseRec in _CourseList)
            {
                int coid = int.Parse(courseRec.UID);

                // 複製樣板
                // 標題
                wb.Worksheets[0].Cells.CreateRange(RowTitleIdx, 1, false).Copy(R_Title);
                string strTitle = courseRec.SchoolYear + "學年度 第" + courseRec.Semester + "學期 第" + courseRec.Round + "梯次 課程缺曠名單";
                wb.Worksheets[0].Cells[RowTitleIdx, 0].PutValue(strTitle);

                // 課程名稱
                wb.Worksheets[0].Cells.CreateRange(RowCourseIdx, 1, false).Copy(R_CourseName);
                wb.Worksheets[0].Cells[RowCourseIdx, 0].PutValue(courseRec.CourseName);


                // 訊息
                wb.Worksheets[0].Cells.CreateRange(RowMessageIdx, 1, false).Copy(R_Message);

                Dictionary<int, int> timeSectionDict = new Dictionary<int, int>();
                // 上課日期
                wb.Worksheets[0].Cells.CreateRange(RowFieldIdx, 1, false).Copy(R_Field);
                List<UDTTimeSectionDef> timeSection = (from data in _TimeSectionList where data.CourseID == coid orderby data.Date, data.Period select data).ToList();
                int colIdx = 4;
                foreach (UDTTimeSectionDef data in timeSection)
                {
                    if (_SelectDateSession.Contains(data.Date))
                    {
                        int tid = int.Parse(data.UID);
                        if (!timeSectionDict.ContainsKey(tid))
                            timeSectionDict.Add(tid, colIdx);
                        string dtStr = data.Date.Month +"/"+string.Format("{00}", data.Date.Day);
                        wb.Worksheets[0].Cells[RowFieldIdx, colIdx].PutValue(dtStr + "\n第" + data.Period + "節");
                        colIdx++;
                    }
                }                

                // 修課學生                
                List<UDTScselectDef> scSelect = (from data in _ScselectList where data.CourseID == coid orderby data.SeatNo select data).ToList();
                foreach (UDTScselectDef data in scSelect)
                {                    
                    wb.Worksheets[0].Cells.CreateRange(RowValueIdx, 1, false).Copy(R_Value);
                 
                    if(_classNameDict.ContainsKey(studRecDict[data.StudentID].RefClassID))
                        wb.Worksheets[0].Cells[RowValueIdx, 0].PutValue(_classNameDict[studRecDict[data.StudentID].RefClassID]);
                    
                    if(studRecDict[data.StudentID].SeatNo.HasValue)
                        wb.Worksheets[0].Cells[RowValueIdx, 1].PutValue(studRecDict[data.StudentID].SeatNo.Value);

                    wb.Worksheets[0].Cells[RowValueIdx, 2].PutValue(data.SeatNo);
                    if (studRecDict.ContainsKey(data.StudentID))
                        wb.Worksheets[0].Cells[RowValueIdx, 3].PutValue(studRecDict[data.StudentID].Name);

                    // 缺曠資料
                    int attendCount = 0;
                    foreach (UDTAttendanceDef attData in _AttendanceList)
                    {
                        if (attData.CourseID == coid && attData.StudentID == data.StudentID)
                        {
                            if (timeSectionDict.ContainsKey(attData.TimeSectionID))
                            {
                                wb.Worksheets[0].Cells[RowValueIdx, timeSectionDict[attData.TimeSectionID]].PutValue("缺課");
                                attendCount++;
                            }

                        }
                    }
                    wb.Worksheets[0].Cells[RowValueIdx, 14].PutValue(attendCount);
                    RowValueIdx++;
                }

                wb.Worksheets[0].HPageBreaks.Add(RowValueIdx, 0);
                RowTitleIdx = RowValueIdx;
                RowCourseIdx = RowValueIdx + 1;
                RowMessageIdx = RowValueIdx + 2;
                RowFieldIdx = RowValueIdx + 3;
                RowValueIdx = RowFieldIdx + 1;
            }
            _bgWorker.ReportProgress(90);
            wb.Worksheets.RemoveAt(1);
            e.Result = wb;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                foreach (ListViewItem lvi in lvwData.Items)
                    lvi.Checked = true;
            }
            else
            {
                foreach (ListViewItem lvi in lvwData.Items)
                    lvi.Checked = false;
            }
        }

        private void CourseStudentAttendReportForm_Load(object sender, EventArgs e)
        {
            // 載入相關上課時間至畫面
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {            

            // 檢查使用者是否有勾選
            if (lvwData.CheckedItems.Count == 0)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請勾選上課時間");
                return;
            }

            // 檢查勾選是否超過樣板最大容納數
            // 讀取使用者勾選
            _SelectDateSession.Clear();
            foreach (ListViewItem lvi in lvwData.CheckedItems)
            {
                DateTime dt = (DateTime)lvi.Tag;
                _SelectDateSession.Add(dt);
            }

            int count = 0;
            foreach (UDTTimeSectionDef data in _TimeSectionList)
            {
                if (_SelectDateSession.Contains(data.Date))
                    count++;
            }            

            if (count > 10)
            {
                FISCA.Presentation.Controls.MsgBox.Show("勾選日期+節次共"+count+"個，已超過欄位最大10個容納數");
                return;            
            }

            btnPrint.Enabled = false;
            // 列印
            _bgWorker.RunWorkerAsync();
        }
    }
}
