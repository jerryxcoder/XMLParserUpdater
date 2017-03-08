using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Data.Sql;
using System.Configuration;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
              
        //"C:\Users\Jerry\Desktop\samplexml.xml"
        public Form1()
        {
            InitializeComponent();
        }

        public void xmlParserTest()
        {
            XDocument dox = XDocument.Load("C:/Users/Jerry/Desktop/samplexml.xml");

            string xmlcontents = dox.ToString();
            textBox1.Text = xmlcontents;
        }

        public List<sdnProto.SdnEntry> EntryParser()
        {
            XDocument dox = XDocument.Load("C:/Users/Jerry/Desktop/sdn.xml");
            var items = dox.Descendants("sdnEntry").Select(d =>
                new
                {
                    uid = (Int32)d.Element("uid"),
                    lastName = d.Element("lastName").Value,
                    sdnType = d.Element("sdnType").Value,
                    programList = d.Descendants("programList")
                                    .Select(x =>
                                  new
                                  {
                                      program = x.Element("program").Value
                                  })
                                  .ToList()
                }).ToList();

            listBox1.DataSource = items;

            List<sdnProto.SdnEntry> ObjList = new List<sdnProto.SdnEntry>();
            foreach (var m in items)
            {
                sdnProto.SdnEntry r = new sdnProto.SdnEntry();
                r.Uid = m.uid;
                r.LastName = m.lastName;
                r.SdnType = m.sdnType;
                ObjList.Add(r);
            }

            return ObjList;
   
         }
       

        public void EntryUpdater()
        {
            var entrylist=EntryParser();
            List<sdnProto.SdnEntry> ObjList = EntryParser();
            string conn = ConfigurationManager.ConnectionStrings["ADOconnect"].ConnectionString;
            using (SqlConnection connnectionstring = new SqlConnection(conn))
            {
                connnectionstring.Open();
                SqlCommand cmd = new SqlCommand();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM ofcaSDNentry", connnectionstring);
                DataSet ds = new DataSet();

                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Fill(ds);

                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.DeleteCommand = builder.GetDeleteCommand();
                List<int> existingRecords = new List<int>();
               
                foreach (DataRow record in ds.Tables[0].Rows)
                {
                    existingRecords.Add((int)record["uid"]);
                    if(!entrylist.Select(x => x.Uid).Contains((int)record["uid"]))
                    {
                        record.Delete();
                    }
                    else 
                    {
                        var matchedRecord = entrylist.Single(x => x.Uid == (int)record["uid"]);
                        if(matchedRecord.LastName != record["lastName"].ToString())
                        {
                            record["lastName"] = matchedRecord.LastName;                           
                        }
                        if(matchedRecord.SdnType!=record["sdnType"].ToString())
                        {
                            record["sdnType"] = matchedRecord.SdnType;
                        }

                    }
                }

                foreach(var newRecord in entrylist.Where(x => !existingRecords.Contains(x.Uid)))
                {
                    var newRow = ds.Tables[0].NewRow();
                    newRow["uid"] = newRecord.Uid;
                    newRow["lastName"] = newRecord.LastName;
                    newRow["sdnType"] = newRecord.SdnType;
                    ds.Tables[0].Rows.Add(newRow);
                }

                adapter.Update(ds);
                
             }
         }

        private void button1_Click(object sender, EventArgs e)
        {
            //xmlParserTest();
            //EntryParser();
            EntryUpdater();
          }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
