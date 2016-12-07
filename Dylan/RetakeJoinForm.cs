using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using FISCA.Data;
using K12.Data;
using SHSchool.Retake.DAO;

namespace SHSchool.Retake.Form
{
    public partial class RetakeJoinForm : BaseForm
    {
        private UDTSessionDef _ActiveSession;
        AccessHelper _AccessHelper = new AccessHelper();

        public RetakeJoinForm()
        {
            InitializeComponent();
        }

        private void RetakeJoinDateTime_Load(object sender, EventArgs e)
        {
            _ActiveSession = UDTTransfer.UDTSessionGetActiveSession();
            if (!string.IsNullOrEmpty(_ActiveSession.UID))
            {
                labelX3.Text = "目前梯次：" + _ActiveSession.SchoolYear + "學年度　";
                labelX3.Text += "第" + _ActiveSession.Semester + "學期　";
                labelX3.Text += "第" + _ActiveSession.Round + "梯次";

                string startTime = _ActiveSession.RegistrationOpen.HasValue ? _ActiveSession.RegistrationOpen.Value.ToString("yyyy/MM/dd HH:mm:ss") : "";
                string endTime = _ActiveSession.RegistrationClose.HasValue ? _ActiveSession.RegistrationClose.Value.ToString("yyyy/MM/dd HH:mm:ss") : "";

                tbStartDateTime.Text = startTime;
                tbEndDateTime.Text = endTime;

                btnSave.Enabled = tbStartDateTime.Enabled = tbEndDateTime.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DateTimeParse())
            {
                if (!Compare())
                {
                    _ActiveSession.RegistrationOpen = DateTime.Parse(tbStartDateTime.Text);
                    _ActiveSession.RegistrationClose = DateTime.Parse(tbEndDateTime.Text);
                    _ActiveSession.Save();
                    MsgBox.Show("儲存成功!!");
                    this.Close();
                }
                else
                {
                    MsgBox.Show("[結束時間]不可小於[開始時間]!!");
                    return;
                }
            }
            else
            {
                MsgBox.Show("請輸入正確資料\n再進行儲存動作!!");
                return;
            }

        }

        private bool Compare()
        {
            bool a = false;
            DateTime? objStart = DateTimeHelper.Parse(tbStartDateTime.Text);
            DateTime? objEnd = DateTimeHelper.Parse(tbEndDateTime.Text);

            if (objStart.HasValue && objEnd.HasValue)
            {
                if (objStart.Value >= objEnd.Value)
                {
                    errorProvider1.SetError(tbStartDateTime, "結束時間必須在開始時間之後。");
                    errorProvider2.SetError(tbEndDateTime, "結束時間必須在開始時間之後。");
                    a = true;
                }
                else
                {
                    errorProvider1.Clear();
                    errorProvider2.Clear();

                }
            }
            return a;

        }

        private bool DateTimeParse()
        {
            return (DateTimeParseStart() & DateTimeParseEnd());
        }

        private bool DateTimeParseStart()
        {
            bool a = false;

            DateTime dt1;
            if (DateTime.TryParse(tbStartDateTime.Text, out dt1))
            {
                a = true;
                errorProvider1.Clear();
            }
            else
            {
                errorProvider1.SetError(tbStartDateTime, "請輸入正確日期格式");
            }

            return a;
        }

        private bool DateTimeParseEnd()
        {
            bool a = false;

            DateTime dt2;
            if (DateTime.TryParse(tbEndDateTime.Text, out dt2))
            {
                a = true;
                errorProvider2.Clear();
            }
            else
            {
                errorProvider2.SetError(tbEndDateTime, "請輸入正確日期格式");
            }

            return a;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbStartDateTime_Validated(object sender, EventArgs e)
        {
            if (DateTimeParseStart())
            {
                DateTime? objStart = DateTimeHelper.ParseGregorian(tbStartDateTime.Text, PaddingMethod.First);
                tbStartDateTime.Text = objStart.Value.ToString("yyyy/MM/dd HH:mm:ss");
            }
        }

        private void tbEndDateTime_Validated(object sender, EventArgs e)
        {
            if (DateTimeParseEnd())
            {
                DateTime? objStart = DateTimeHelper.ParseGregorian(tbEndDateTime.Text, PaddingMethod.First);
                tbEndDateTime.Text = objStart.Value.ToString("yyyy/MM/dd HH:mm:ss");
            }
        }

        private void tbStartDateTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbEndDateTime.Focus();
            }
        }

        private void tbEndDateTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }
    }
}
