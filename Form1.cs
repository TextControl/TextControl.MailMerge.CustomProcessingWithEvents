using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TXTextControl;

namespace tx_reporting_customprocessing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds.ReadXml("sample_db.xml", XmlReadMode.Auto);

            mailMerge1.Merge(ds.Tables["Person_Address"]);
        }

        private void mailMerge1_DataRowMerged(object sender, TXTextControl.DocumentServer.MailMerge.DataRowMergedEventArgs e)
        {
            using (TXTextControl.ServerTextControl tx = new TXTextControl.ServerTextControl())
            {
                tx.Create();

                TXTextControl.LoadSettings ls = new LoadSettings { ApplicationFieldFormat = ApplicationFieldFormat.MSWord };
                tx.Load(e.MergedRow, TXTextControl.BinaryStreamType.InternalUnicodeFormat, ls);

                foreach (Table table in tx.Tables)
                {
                    table.AutoSize(tx);
                }

                byte[] data;

                tx.Save(out data, BinaryStreamType.InternalUnicodeFormat);
                e.MergedRow = data;
            }
        }


    }
}
