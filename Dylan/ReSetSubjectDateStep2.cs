using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SHSchool.Retake.DAO;

namespace SHSchool.Retake.Form
{
    public partial class ReSetSubjectDateStep2 : FISCA.Presentation.Controls.BaseForm
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ReSetSubjectDateStep2()
        {
            InitializeComponent();
        }

        private void ReSetSubjectDateStep2_Load(object sender, EventArgs e)
        {
            
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (this.dtBeginDate.IsEmpty || this.dtEndDate.IsEmpty)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請選擇日期。");
                return;
            }
            this.StartDate = this.dtBeginDate.Value;
            this.EndDate = this.dtEndDate.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
