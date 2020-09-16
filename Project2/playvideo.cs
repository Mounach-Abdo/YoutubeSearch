using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubeFiverr
{
    public partial class playvideo : Form
    {
        public playvideo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string html = "<html><head>";
            html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
            html += "</head><body style='position:absolute;width:100%;height:100%'><iframe style='position:absolute;width:100%;height:100%' border:0;id='video' src= 'https://www.youtube.com/embed/{0}' style='position:absolute; width=100% ;height=100%; frameborder=0;' allowfullscreen></iframe>";
            html += "</body></html>";
            this.webBrowser1.DocumentText = string.Format(html,searchYoutubeForm.varUrl.Split('=')[1]);
        }

        private void playvideo_Load(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }
    }
}
