using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace SHSchool.Retake.DAO
{
    /// <summary>
    /// 重補修期間
    /// </summary>
    [TableName("shschool.retake.session")]
    public class UDTSessionDef : ActiveRecord
    {
        ///<summary>
        /// 學年度
        ///</summary>
        [Field(Field = "school_year", Indexed = false)]
        public int SchoolYear { get; set; }

        ///<summary>
        /// 學期
        ///</summary>
        [Field(Field = "semester", Indexed = false)]
        public int Semester { get; set; }

        ///<summary>
        /// 梯次
        ///</summary>
        [Field(Field = "round", Indexed = false)]
        public int Round { get; set; }

        ///<summary>
        /// 名稱
        ///</summary>
        [Field(Field = "name", Indexed = false)]
        public string Name { get; set; }

        /////<summary>
        ///// 開始日期
        /////</summary>
        //[Field(Field = "begin_date", Indexed = false)]
        //public DateTime BeginDate { get; set; }

        /////<summary>
        ///// 結束日期
        /////</summary>
        //[Field(Field = "end_date", Indexed = false)]
        //public DateTime EndDate { get; set; }

        /// <summary>
        /// 開放登記(選課)
        /// </summary>
        [Field(Field = "registration_open", Indexed = false)]
        public DateTime? RegistrationOpen { get; set; }

        /// <summary>
        /// 登記截止(選課)
        /// </summary>
        [Field(Field = "registration_close", Indexed = false)]
        public DateTime? RegistrationClose { get; set; }

        ///<summary>
        /// 正在開放
        ///</summary>
        [Field(Field = "active", Indexed = false)]
        public Boolean Active { get; set; }

    }
}
