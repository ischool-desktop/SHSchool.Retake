using Aspose.Cells;
using K12.Data;
using SHSchool.Retake.DAO;
using SmartSchool.API.PlugIn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace SHSchool.Retake.ImportExport
{
    class ExportSCAttend : SmartSchool.API.PlugIn.Export.Exporter
    {
        List<string> _CourseIDList; //課程id清單
        Dictionary<int, StudentRecord> _StudDict; //學生Record字典
        Dictionary<int, UDTCourseDef> _CourseDic; //課程字典
        List<UDTScselectDef> _CourseStudents; //修課學生清單

        public ExportSCAttend(List<string> courseIDList)
        {
            //初始化
            this.Image = null;
            this.Text = "匯出修課學生";
            _CourseIDList = courseIDList;
        }

        //覆寫
        public override void InitializeExport(SmartSchool.API.PlugIn.Export.ExportWizard wizard)
        {
            List<string> FieldList = new List<string>();
            FieldList.Add("課程名稱");
            FieldList.Add("學年度");
            FieldList.Add("學期");
            FieldList.Add("梯次");
            FieldList.Add("學號");
            FieldList.Add("姓名");
            FieldList.Add("課程座號");
            FieldList.Add("重補修");

            wizard.ExportableFields.AddRange(FieldList);
            wizard.ExportPackage += (sender, e) =>
            {
                GetData();

                foreach (UDTScselectDef elem in _CourseStudents)
                {
                    RowData row = new RowData();
                    row.ID = elem.UID;

                    foreach (string field in e.ExportFields)
                    {
                        if (wizard.ExportableFields.Contains(field))
                        {
                            switch (field)
                            {
                                case "課程名稱": row.Add(field, "" + _CourseDic[elem.CourseID].CourseName); break;
                                case "學年度": row.Add(field, "" + _CourseDic[elem.CourseID].SchoolYear); break;
                                case "學期": row.Add(field, "" + _CourseDic[elem.CourseID].Semester); break;
                                case "梯次": row.Add(field, "" + _CourseDic[elem.CourseID].Round); break;
                                case "學號": row.Add(field, _StudDict[elem.StudentID].StudentNumber); break;
                                case "姓名": row.Add(field, _StudDict[elem.StudentID].Name); break;
                                case "課程座號": row.Add(field, "" + elem.SeatNo); break;
                                case "重補修": row.Add(field, "" + elem.Type); break;
                            }
                        }
                    }
                    e.Items.Add(row);
                }
            };
        }

        private void GetData()
        {
            //取得課程資料
            _CourseDic = new Dictionary<int, UDTCourseDef>();
            foreach (UDTCourseDef rec in UDTTransfer.UDTCourseSelectUIDs(_CourseIDList))
            {
                int uid = int.Parse(rec.UID);
                if(!_CourseDic.ContainsKey(uid))
                {
                    _CourseDic.Add(uid, rec);
                }
            }

            //取得修課學生資料
            _CourseStudents = new List<UDTScselectDef>();
            foreach (UDTScselectDef data in UDTTransfer.UDTSCSelectByCourseIDList(_CourseIDList))
            {
                _CourseStudents.Add(data);
            }

            //排序
            _CourseStudents.Sort(SortCourseStudents);

            //建立學生record字典
            _StudDict = new Dictionary<int, StudentRecord>();
            List<string> sidList = (from data in _CourseStudents select data.StudentID.ToString()).Distinct().ToList();
            foreach (StudentRecord rec in Student.SelectByIDs(sidList))
            {
                int id = int.Parse(rec.ID);
                if (!_StudDict.ContainsKey(id))
                    _StudDict.Add(id, rec);
            }
        }

        private int SortCourseStudents(UDTScselectDef x, UDTScselectDef y)
        {
            string xx = _CourseDic[x.CourseID].SchoolYear.ToString().PadLeft(4, '0');
            xx += _CourseDic[x.CourseID].Semester.ToString().PadLeft(2, '0');
            xx += _CourseDic[x.CourseID].Round.ToString().PadLeft(3, '0');
            xx += _CourseDic[x.CourseID].CourseName.PadLeft(20, '0');
            xx += x.SeatNo.ToString().PadLeft(3, '0');

            string yy = _CourseDic[y.CourseID].SchoolYear.ToString().PadLeft(4, '0');
            yy += _CourseDic[y.CourseID].Semester.ToString().PadLeft(2, '0');
            yy += _CourseDic[y.CourseID].Round.ToString().PadLeft(3, '0');
            yy += _CourseDic[y.CourseID].CourseName.PadLeft(20, '0');
            yy += y.SeatNo.ToString().PadLeft(3, '0');

            return xx.CompareTo(yy);
        }
    }
}
