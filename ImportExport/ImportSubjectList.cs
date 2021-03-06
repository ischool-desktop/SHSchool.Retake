﻿using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Campus.DocumentValidator;
using Campus.Import;
using FISCA.Data;
using FISCA.UDT;
using SHSchool.Retake.DAO;
using System.Xml;

namespace SHSchool.Retake
{
    public class ImportSubjectList : ImportWizard
    {
        private const string _SubjectName = "科目名稱";
        private const string _SubjectLevel = "級別";
        private const string _Credit = "學分數";
        private const string _DeptName = "科別";
        private const string _SubjectType = "科目類別";
        private int _SchoolYear, _Semester, _Round;
        private List<string> _IDList = new List<string>(); //找得到組合鍵值的id清單
        
        Dictionary<string, string> _SubjectDic = new Dictionary<string, string>(); //科目ID對照字典(組合鍵值,UID)

        private StringBuilder mstrLog = new StringBuilder();
        private ImportOption mOption;
        private Task mTask;
        private AccessHelper mHelper;

        /// <summary>
        /// 取得驗證規則
        /// </summary>
        /// <returns></returns>
        public override string GetValidateRule()
        {
            return Properties.Resources.SubjectList;
        }

        /// <summary>
        /// 取得支援的匯入型態
        /// </summary>
        /// <returns></returns>
        public override ImportAction GetSupportActions()
        {
            return ImportAction.Update | ImportAction.Delete;
        }

        /// <summary>
        /// 匯入前準備
        /// </summary>
        /// <param name="Option"></param>
        public override void Prepare(ImportOption Option)
        {
            mHelper = new AccessHelper();
            mOption = Option;
            mTask = Task.Factory.StartNew
            (() =>
            {
                QueryHelper Helper = new QueryHelper();

                #region 取得當前的工作學年度,學期,梯次
                UDTSessionDef data = UDTTransfer.UDTSessionGetActiveSession();
                if (!string.IsNullOrEmpty(data.UID))
                {
                    _SchoolYear = data.SchoolYear;
                    _Semester = data.Semester;
                    _Round = data.Round;
                }
                #endregion

                #region 取得指定學年度,學期,梯次的科目對照字典
                List<UDTSubjectDef> UDTSubjectList = UDTTransfer.UDTSubjectSelectByP1(_SchoolYear, _Semester, _Round);
                foreach (UDTSubjectDef elem in UDTSubjectList)
                {
                    string uid = elem.UID;
                    string subject_name = elem.SubjectName;
                    string subject_level = ""+elem.SubjecLevel;
                    string subject_credit = ""+elem.Credit;
                    string dept = elem.DeptName;

                    string subjectKey = _SchoolYear + "," + _Semester + "," + _Round + "," + subject_name + "," + subject_level + "," + subject_credit + "," + dept;

                    if (!_SubjectDic.ContainsKey(subjectKey))
                        _SubjectDic.Add(subjectKey, uid);
                }
                #endregion
            }
            );
        }

        /// <summary>
        /// 執行分批匯入
        /// </summary>
        /// <param name="Rows">IRowStream集合</param>
        /// <returns>回傳分批匯入執行完成訊息</returns>
        public override string Import(List<IRowStream> Rows)
        {
            mTask.Wait();

            mstrLog.Clear();

            if (mOption.SelectedKeyFields.Count >= 4 &&
                mOption.SelectedKeyFields.Contains(_SubjectName) &&
                mOption.SelectedKeyFields.Contains(_SubjectLevel) &&
                mOption.SelectedKeyFields.Contains(_Credit) &&
                mOption.SelectedKeyFields.Contains(_DeptName))
            {
                List<UDTSubjectDef> mSubjectExtensions = new List<UDTSubjectDef>(); //需要更新的record清單
                foreach (IRowStream Row in Rows)
                {
                    string subject_name = Row.GetValue(_SubjectName);
                    string subject_level = Row.GetValue(_SubjectLevel);
                    string subject_credit = Row.GetValue(_Credit);
                    string subject_dept = Row.GetValue(_DeptName);

                    string subjectKey = _SchoolYear + "," + _Semester + "," + _Round + "," + subject_name + "," + subject_level + "," + subject_credit + "," + subject_dept;

                    string uid = _SubjectDic.ContainsKey(subjectKey) ? _SubjectDic[subjectKey] : string.Empty;

                    if (!string.IsNullOrEmpty(uid))
                    {
                        _IDList.Add(uid);
                    }
                }

                string SubjectIDsCondition = string.Join("','", _IDList.ToArray());

                if (!string.IsNullOrEmpty(SubjectIDsCondition))
                    mSubjectExtensions = mHelper.Select<UDTSubjectDef>("uid in ('" + SubjectIDsCondition + "')");

                if (mOption.Action == ImportAction.Update)
                {
                    List<UDTSubjectDef> InsertRecords = new List<UDTSubjectDef>();
                    List<UDTSubjectDef> UpdateRecords = new List<UDTSubjectDef>();

                    foreach (IRowStream Row in Rows)
                    {
                        string subject_name = Row.GetValue(_SubjectName);
                        string subject_level = Row.GetValue(_SubjectLevel);
                        string subject_credit = Row.GetValue(_Credit);
                        string subject_dept = Row.GetValue(_DeptName);

                        string subjectKey = _SchoolYear + "," + _Semester + "," + _Round + "," + subject_name + "," + subject_level + "," + subject_credit + "," + subject_dept;

                        int? subjectID = null;
                        UDTSubjectDef vSubjectExtension = null;
                        if (_SubjectDic.ContainsKey(subjectKey))
                            subjectID = K12.Data.Int.ParseAllowNull(_SubjectDic[subjectKey]);

                        if (subjectID.HasValue)
                        {
                            //尋找是否有對應的資料
                            vSubjectExtension = mSubjectExtensions
                               .Find(x => x.UID.Equals(K12.Data.Int.GetString(subjectID)));

                            if (vSubjectExtension != null)
                            {
                                if (mOption.SelectedFields.Contains(_SubjectName))
                                    vSubjectExtension.SubjectName = Row.GetValue(_SubjectName);

                                if (mOption.SelectedFields.Contains(_SubjectLevel))
                                    vSubjectExtension.SubjecLevel = K12.Data.Int.ParseAllowNull(Row.GetValue(_SubjectLevel));

                                if (mOption.SelectedFields.Contains(_Credit))
                                    vSubjectExtension.Credit = decimal.Parse(Row.GetValue(_Credit));

                                if (mOption.SelectedFields.Contains(_DeptName))
                                    vSubjectExtension.DeptName = Row.GetValue(_DeptName);

                                if (mOption.SelectedFields.Contains(_SubjectType))
                                    vSubjectExtension.SubjectType = Row.GetValue(_SubjectType);
                                
                                UpdateRecords.Add(vSubjectExtension);
                            }
                        }
                        else
                        {
                            vSubjectExtension = new UDTSubjectDef();
                            vSubjectExtension.SubjectName = Row.GetValue(_SubjectName);
                            vSubjectExtension.SubjecLevel = K12.Data.Int.ParseAllowNull(Row.GetValue(_SubjectLevel));

                            if (mOption.SelectedFields.Contains(_Credit))
                                vSubjectExtension.Credit = decimal.Parse(Row.GetValue(_Credit));

                            if (mOption.SelectedFields.Contains(_DeptName))
                                vSubjectExtension.DeptName = Row.GetValue(_DeptName);

                            if (mOption.SelectedFields.Contains(_SubjectType))
                                vSubjectExtension.SubjectType = Row.GetValue(_SubjectType);
                            
                            vSubjectExtension.SchoolYear = _SchoolYear;
                            vSubjectExtension.Semester = _Semester;
                            vSubjectExtension.Round = _Round;

                            InsertRecords.Add(vSubjectExtension);
                        }
                    }

                    if (InsertRecords.Count > 0)
                    {
                        List<string> NewIDs = mHelper.InsertValues(InsertRecords);
                        mstrLog.AppendLine("已新增" + InsertRecords.Count + "筆科目資料。");
                    }
                    if (UpdateRecords.Count > 0)
                    {
                        mHelper.UpdateValues(UpdateRecords);
                        mstrLog.AppendLine("已更新" + UpdateRecords.Count + "筆科目資料。");
                    }
                }
            }
            return mstrLog.ToString();
        }
    }
}

