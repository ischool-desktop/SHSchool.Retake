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
        private int _SchoolYear, _Semester, _Round;
        private Task mTask;

        public SubjectListCheck()
        {
            mTask = Task.Factory.StartNew(() =>
            {
                mSubjectNames = new List<string>();

                QueryHelper Helper = new QueryHelper();

                #region 取得組合鍵值清單
                DataTable Table = Helper.Select("select school_year,semester,round,subject_name,subject_level,credit,dept_name from $shschool.retake.subject");

                foreach (DataRow Row in Table.Rows)
                {
                    string school_year = Row.Field<string>("school_year");
                    string semester = Row.Field<string>("semester");
                    string round = Row.Field<string>("round");
                    string subject_name = Row.Field<string>("subject_name");
                    string subject_level = Row.Field<string>("subject_level");
                    string subject_credit = Row.Field<string>("credit");
                    string subject_dept = Row.Field<string>("dept_name");
                    string SubjectKey = school_year + "," + semester + "," + round + "," + subject_name + "," + subject_level + "," + subject_credit + "," + subject_dept;

                    if (!mSubjectNames.Contains(SubjectKey))
                        mSubjectNames.Add(SubjectKey);
                }
                #endregion

                //取得活動的學年度,學期,梯次
                UDTSessionDef data = UDTTransfer.UDTSessionGetActiveSession();
                if (!string.IsNullOrEmpty(data.UID))
                {
                    _SchoolYear = data.SchoolYear;
                    _Semester = data.Semester;
                    _Round = data.Round;
                }

            });
        }

        #region IRowVaildator 成員

        public bool Validate(IRowStream Value)
        {
            mTask.Wait();

            if (Value.Contains("科目名稱") && Value.Contains("級別") && Value.Contains("學分數") && Value.Contains("科別"))
            {
                string subject_name = Value.GetValue("科目名稱");
                string subject_level = Value.GetValue("級別");
                string subjec_credit = Value.GetValue("學分數");
                string subject_dept = Value.GetValue("科別");

                string SubjectKey = _SchoolYear + "," + _Semester + "," + _Round + "," + subject_name + "," + subject_level + "," + subjec_credit + "," + subject_dept;

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