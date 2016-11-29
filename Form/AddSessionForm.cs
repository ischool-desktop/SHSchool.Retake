using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SHSchool.Retake.DAO;
using System.Xml.Linq;

namespace SHSchool.Retake.Form
{
    /// <summary>
    /// 新增重補修期間
    /// </summary>
    public partial class AddSessionForm : FISCA.Presentation.Controls.BaseForm
    {
        BackgroundWorker _bgWorker = new BackgroundWorker();
        List<UDTSessionDef> _AllUDTSession = new List<UDTSessionDef>();
        public AddSessionForm()
        {
            InitializeComponent();
            // 取得UDT所有名冊設定
            _AllUDTSession = UDTTransfer.UDTSessionSelectAll();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FISCA.Presentation.Controls.MsgBox.Show("資料新增完成");
            btnSave.Enabled = true;
            this.Close();
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 取得目前期間
            UDTSessionDef actData = UDTTransfer.UDTSessionGetActiveTrue1();

            // 新增建議名單
            InsertData(actData.UID);
        }

        private void AddSessionForm_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.MinimumSize = this.Size;
            int dsc = int.Parse(K12.Data.School.DefaultSchoolYear);
            int dss = int.Parse(K12.Data.School.DefaultSemester);
            iptSchoolYear.Value = dsc;
            iptSemester.Value = dss;
            iptRound.Value = 1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = iptSchoolYear.Value + "學年度第" + iptSemester.Value + "學期" + iptRound.Value + "梯次";

            bool pass = true;
            // 檢查是否有相同名冊
            foreach (UDTSessionDef data in _AllUDTSession)
            {
                // 先設成 false 確認可以新增時，更新狀態使用
                data.Active = false;

                if (data.SchoolYear == iptSchoolYear.Value && data.Semester == iptSemester.Value && data.Round == iptRound.Value)
                    pass = false;
            }

            if (pass)
            {
                btnSave.Enabled = false;
                List<UDTSessionDef> dataList = new List<UDTSessionDef>();
                UDTSessionDef newData = new UDTSessionDef();
                newData.SchoolYear = iptSchoolYear.Value;
                newData.Semester = iptSemester.Value;
                newData.Round = iptRound.Value;
                newData.Name = name;
                newData.Active = true;
                dataList.Add(newData);

                // 更新原有目前學期
                //UDTTransfer.UDTSessionUpdate(_AllUDTSession);

                // 新增資料
                UDTTransfer.UDTSessionInsert(dataList);
                // 新增資料
                _bgWorker.RunWorkerAsync();
           
            }
            else
            {
                FISCA.Presentation.Controls.MsgBox.Show("新增失敗! 系統內已有相同學年度、學期、梯次的名冊。");
                return;
            }


        }

        private void InsertData(string UID)
        {
            if (!string.IsNullOrEmpty(UID))
            {
                // 取得最新
                DataTable dtTable = QueryData.GetRetakeList1();

                List<UDTSuggestListDef> insertDataList = new List<UDTSuggestListDef>();
                // 收集資料
                Dictionary<int, XElement> insertDataDict = new Dictionary<int, XElement>();
                foreach (DataRow dr in dtTable.Rows)
                {
                    int sid = int.Parse(dr["StudentID"].ToString());
                    if (!insertDataDict.ContainsKey(sid))
                    {
                        XElement elm = new XElement("Subjects");
                        insertDataDict.Add(sid, elm);
                    }

                    XElement subElm = new XElement("Subject");
                    subElm.SetAttributeValue("Name", dr["科目"].ToString());
                    subElm.SetAttributeValue("Level", dr["級別"].ToString());
                    subElm.SetAttributeValue("Credit", dr["學分"].ToString());
                    subElm.SetAttributeValue("Type", dr["重補修"].ToString());
                    subElm.SetAttributeValue("SchoolYear", dr["學年度"].ToString());
                    subElm.SetAttributeValue("Semester", dr["學期"].ToString());
                    subElm.SetAttributeValue("GradeYear", dr["成績年級"].ToString());
                    subElm.SetAttributeValue("Score", dr["成績"].ToString());
                    subElm.SetAttributeValue("Required", dr["必選修"].ToString());
                    subElm.SetAttributeValue("CheckCourse1", dr["本學期修課"].ToString());
                    subElm.SetAttributeValue("StudentStatus", dr["學生狀態"].ToString());
                    insertDataDict[sid].Add(subElm);
                }

                // 寫入資料
                foreach (KeyValuePair<int, XElement> data in insertDataDict)
                {
                    UDTSuggestListDef newData = new UDTSuggestListDef();
                    newData.RefStudentID = data.Key;
                    newData.RefSessionID = int.Parse(UID);
                    newData.SubjectContent = data.Value.ToString();
                    insertDataList.Add(newData);
                }
                UDTTransfer.UDTSuggestListInsert(insertDataList);                
            }
        }

        private void iptSchoolYear_ValueChanged(object sender, EventArgs e)
        {

        }

        private void AddSessionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnSave.Enabled == false)
                e.Cancel = true;
        }

    }
}
