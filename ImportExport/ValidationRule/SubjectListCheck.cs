using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Campus.DocumentValidator;
using FISCA.Data;
using SHSchool.Retake.DAO;

namespace SHSchool.Retake
{
    public class SubjectListCheck : IRowVaildator
    {
        private List<string> mSubjectNames; //全部組合鍵值清單
        private Dictionary<string, string> _CourseTimetableDic; //所屬課表ID對照字典
        private int _SchoolYear, _Semester, _Month;
        private Task mTask;

        public SubjectListCheck()
        {
            mTask = Task.Factory.StartNew(() =>
            {
                mSubjectNames = new List<string>();

                QueryHelper Helper = new QueryHelper();

                #region 取得組合鍵值清單
                DataTable Table = Helper.Select("select school_year,semester,month,subject_name,subject_level,credit,course_timetable_id from $shschool.retake.subject");

                foreach (DataRow Row in Table.Rows)
                {
                    string school_year = Row.Field<string>("school_year");
                    string semester = Row.Field<string>("semester");
                    string month = Row.Field<string>("month");
                    string subject_name = Row.Field<string>("subject_name");
                    string subject_level = Row.Field<string>("subject_level");
                    string subject_credit = Row.Field<string>("credit");
                    string course_timetable_id = Row.Field<string>("course_timetable_id");
                    string SubjectKey = school_year + "," + semester + "," + month + "," + subject_name + "," + subject_level + "," + subject_credit + "," + course_timetable_id;

                    if (!mSubjectNames.Contains(SubjectKey))
                        mSubjectNames.Add(SubjectKey);
                }
                #endregion

                #region 取得所屬課表對照字典
                Table = Helper.Select("select uid,name from $shschool.retake.course_timetable");
                _CourseTimetableDic = new Dictionary<string, string>();
                foreach (DataRow row in Table.Rows)
                {
                    string uid = row["uid"].ToString();
                    string name = row["name"].ToString();
                    if (!_CourseTimetableDic.ContainsKey(name))
                    {
                        _CourseTimetableDic.Add(name, uid);
                    }
                }
                #endregion

                //取得活動的學年度,學期,梯次
                UDTSessionDef data = UDTTransfer.UDTSessionGetActiveTrue1();
                if (!string.IsNullOrEmpty(data.UID))
                {
                    _SchoolYear = data.SchoolYear;
                    _Semester = data.Semester;
                    _Month = data.Round;
                }

            });
        }

        #region IRowVaildator 成員

        public bool Validate(IRowStream Value)
        {
            mTask.Wait();

            if (Value.Contains("科目名稱") && Value.Contains("級別") && Value.Contains("學分數") && Value.Contains("所屬課表"))
            {
                string subject_name = Value.GetValue("科目名稱");
                string subject_level = Value.GetValue("級別");
                string subjec_credit = Value.GetValue("學分數");
                string course_timetable_id ="";
                try
                {
                    course_timetable_id = _CourseTimetableDic[Value.GetValue("所屬課表")]; //將所屬課表名稱轉ID
                }
                catch
                {
                    course_timetable_id = "";
                }

                string SubjectKey = _SchoolYear + "," + _Semester + "," + _Month + "," + subject_name + "," + subject_level + "," + subjec_credit + "," + course_timetable_id;

                return mSubjectNames.Contains(SubjectKey);
            }
            return false;
        }

        public string Correct(IRowStream Value)
        {
            return string.Empty;
        }

        public string ToString(string template)
        {
            return template;
        }

        #endregion
    }
}