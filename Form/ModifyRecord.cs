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
    public partial class ModifyRecord : FISCA.Presentation.Controls.BaseForm
    {

         List<DataGridViewRow> _RowList = new List<DataGridViewRow>();
         public DataTable _DataTable = new DataTable();

        public ModifyRecord(DataTable DataTable,List<DataGridViewRow> RowList)
        {
            InitializeComponent();

            foreach (DataGridViewRow displayRow in RowList)
            {
                _RowList.Add(displayRow);               
            }
            _DataTable = DataTable;

            buttonX1.DialogResult = DialogResult.OK;
            buttonX2.DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow displayRow in _RowList)
            {
                DataRow dataRow = (DataRow)displayRow.Tag;

                foreach (System.Data.DataRow row in _DataTable.Rows)
                {
                    if (row["class_name"] == dataRow["class_name"] && row["seat_no"] == dataRow["seat_no"]&&
                        row["student_number"] == dataRow["student_number"] && row["name"] == dataRow["name"]&&
                        row["stu_dept"] == dataRow["stu_dept"] && row["subject_level"] == dataRow["subject_level"] &&
                        row["credit"] == dataRow["credit"] && row["fail_reason"] == dataRow["fail_reason"] 
                        )
                    {
                        row["fail_reason"] = textBoxX1.Text;
                    }
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
