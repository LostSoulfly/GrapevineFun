using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Grapevine.Server;
using System.Xml.Serialization;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        
        public RestServer server = new RestServer();
        public static List<ApiKeys> keys = new List<ApiKeys>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            server.Host = "*";
            server.Port = "8080";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!server.IsListening)
            {
                server.Start();
                this.button1.Text = "Stop Server";
            } else {
                server.Stop();
                this.button1.Text = "Start Server";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server.IsListening) server.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            using (Stream s = File.Open("API.bin", FileMode.Create))
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(s, keys);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (Stream s = File.Open("API.bin", FileMode.Open))
            {
                BinaryFormatter b = new BinaryFormatter();
                keys = b.Deserialize(s) as List<ApiKeys>;

            }
        }

        public void AddToList(string IP, string Key)
        {
            
            string[] row = { IP, Key };
            var listViewItem = new ListViewItem(row);
            listView1.Items.Add(listViewItem);
        }
    }

}
