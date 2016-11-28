using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Campus.DocumentValidator;
using FISCA.Data;

namespace SHSchool.Retake
{
    public class CourseTimetableCheck : IFieldValidator
    {
        private List<string> mCourseTimetable; //所屬課表
        private Task mTask;

        public CourseTimetableCheck()
        {
            mCourseTimetable = new List<string>();
            mTask = Task.Factory.StartNew
            (() =>
            {
                QueryHelper Helper = new QueryHelper();

                //取得重補修系統上所有課表
                DataTable Table = Helper.Select("select name from $shschool.retake.course_timetable");

                foreach (DataRow Row in Table.Rows)
                {
                    string name = Row.Field<string>("name");

                    if (!mCourseTimetable.Contains(name))
                        mCourseTimetable.Add(name);
                }
            }
            );
        }

        #region IFieldValidator 成員

        public string Correct(string Value)
        {
            return string.Empty;
        }

        public string ToString(string template)
        {
            return template;
        }

        public bool Validate(string Value)
        {
            mTask.Wait();

            return mCourseTimetable.Contains(Value);
        }

        #endregion
    }
}