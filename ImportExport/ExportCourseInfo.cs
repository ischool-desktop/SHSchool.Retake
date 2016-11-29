using Aspose.Cells;
using FISCA.UDT;
using K12.Data;
using SHSchool.Retake.DAO;
using SmartSchool.API.PlugIn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace SHSchool.Retake.ImportExport
{
    class ExportCourseInfo : SmartSchool.API.PlugIn.Export.Exporter
    {
        List<string> _CourseIDList; //課程id清單
        List<UDTCourseDef> _CourseDefList; //課程清單
        Dictionary<string, string> _TeacherDic; //教師名稱字典

        public ExportCourseInfo(List<string> courseIDList)
        {
            //初始化
            this.Image = null;
            this.Text = "匯出課程基本資料";
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
            FieldList.Add("科目類別");
            FieldList.Add("科別");
            FieldList.Add("科目名稱");
            FieldList.Add("學分數");
            FieldList.Add("科目級別");
            FieldList.Add("授課教師");

            wizard.ExportableFields.AddRange(FieldList);
            wizard.ExportPackage += (sender, e) =>
            {
                GetData();

                foreach (UDTCourseDef elem in _CourseDefList)
                {
                    RowData row = new RowData();
                    row.ID = elem.UID;

                    foreach (string field in e.ExportFields)
                    {
                        if (wizard.ExportableFields.Contains(field))
                        {
                            switch (field)
                            {
                                case "課程名稱": row.Add(field, "" + elem.CourseName); break;
                                case "學年度": row.Add(field, "" + elem.SchoolYear); break;
                                case "學期": row.Add(field, "" + elem.Semester); break;
                                case "梯次": row.Add(field, "" + elem.Round); break;
                                case "科目類別": row.Add(field, elem.SubjectType); break;
                                case "科別": row.Add(field, elem.DeptName); break;
                                case "科目名稱": row.Add(field, elem.SubjectName); break;
                                case "學分數": row.Add(field, "" + elem.Credit); break;
                                case "科目級別": row.Add(field, "" + elem.SubjectLevel); break;
                                case "授課教師":
                                    string teacherName;
                                    try
                                    {
                                        teacherName = _TeacherDic[elem.RefTeacherID.ToString()];
                                    }
                                    catch
                                    {
                                        teacherName = "";
                                    }
                                    row.Add(field, teacherName);
                                    break;
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
            _CourseDefList = new List<UDTCourseDef>();
            foreach (UDTCourseDef rec in UDTTransfer.UDTCourseSelectUIDs(_CourseIDList))
            {
                _CourseDefList.Add(rec);
            }

            //排序
            _CourseDefList.Sort(SortCourse);

            //取得教師資料
            _TeacherDic = new Dictionary<string, string>();
            List<string> teacherIDs = new List<string>();
            foreach(UDTCourseDef elem in _CourseDefList)
            {
                string id = elem.RefTeacherID.ToString();
                if (!teacherIDs.Contains(id))
                {
                    teacherIDs.Add(id);
                }
            }

            FISCA.Data.QueryHelper helper = new FISCA.Data.QueryHelper();
            string ids = string.Join("','",teacherIDs);
            DataTable data = helper.Select("select id,teacher_name from teacher where id in ('" + ids +"')");
            foreach (DataRow row in data.Rows)
            {
                string id = row["id"].ToString();
                string name = row["teacher_name"].ToString();
                if (!_TeacherDic.ContainsKey(id))
                {
                    //建立教師字典
                    _TeacherDic.Add(id, name);
                }
            }
        }

        //排序方法
        private int SortCourse(UDTCourseDef x, UDTCourseDef y)
        {
            string xx = x.SchoolYear.ToString().PadLeft(4, '0');
            xx += x.Semester.ToString().PadLeft(2, '0');
            xx += x.Round.ToString().PadLeft(3, '0');
            xx += x.CourseName.PadLeft(20, '0');

            string yy = y.SchoolYear.ToString().PadLeft(4, '0');
            yy += y.Semester.ToString().PadLeft(2, '0');
            yy += y.Round.ToString().PadLeft(3, '0');
            yy += y.CourseName.PadLeft(20, '0');

            return xx.CompareTo(yy);
        }
    }
}
