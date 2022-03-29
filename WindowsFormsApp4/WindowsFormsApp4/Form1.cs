using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] cmbfill = { "akita", "beagle", "boxer", "bullterrier", "bulldog", "cattledog", "chihuahua", "chow", "collie", "corgi", "dachshund", "dalmatian", "dane", "doberman", "eskimo", "germanshepherd", "greyhound", "husky", "labrador", "malamute", "malinois", "maltese", "mastiff", "mix", "newfoundland", "ovcharka", "pekinese", "pitbull", "pointer", "pomeranian", "poodle", "pug", "retriever", "rottweiler", "samoyed", "shiba", "shihtzu", "spaniel", "terrier" };
            for(int i=0; i < cmbfill.Length; i++)
            {
                comboBox1.Items.Add(cmbfill[i].ToString());
            }
        }
        
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string link = comboBox1.GetItemText(listBox2.SelectedItem);
            webBrowser1.Navigate(new Uri(link));
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            string breed = comboBox1.GetItemText(comboBox1.SelectedItem);
            string apiUrl = String.Format("https://dog.ceo/api/breed/" + breed + "/images");
            WebClient client = new WebClient();
            string fResponse = client.DownloadString(apiUrl);
            dynamic dobj = JsonConvert.DeserializeObject<dynamic>(fResponse);
            foreach (var i in dobj["message"])
                listBox2.Items.Add(i.ToString());
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.Document.Body.SetAttribute("scroll", "no");
            var img = webBrowser1.Document.GetElementsByTagName("img")
                         .Cast<HtmlElement>().FirstOrDefault();
            var w = img.ClientRectangle.Width;
            var h = img.ClientRectangle.Height;
            img.Style = string.Format("{0}: 100%", w > h ? "Width" : "Height");
        }
    }
        
}
