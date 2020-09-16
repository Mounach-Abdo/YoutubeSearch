using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project2.Properties;
using YouTubeSearch;

namespace YoutubeFiverr
{
    public partial class searchYoutubeForm : Form
    {
        public searchYoutubeForm()
        {
            InitializeComponent();
        }
        public static string varUrl;
        public int currentRow = 0;
        private void createItem(string title, string author, string duration, string viewsCount, byte[] imgThubmnail, int index, int column, string url)
        {
            Panel p = new Panel();
            p.Name = "panel" + index;
            p.Margin = new Padding(10, 10, 10, 10);
            p.Dock = DockStyle.Fill;

            PictureBox picture = new PictureBox();
            picture.Name = "picture" + index;
            picture.Dock = DockStyle.Top;
            picture.Height = 107;
            p.Location = new Point(10, 10);
            p.BorderStyle = BorderStyle.FixedSingle;
            picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            picture.Tag = url;
            picture.DoubleClick += (sender, e) =>
            {
                varUrl = picture.Tag + "";
                playvideo video = new playvideo();
                video.Show();
            };
            using (MemoryStream ms = new MemoryStream(imgThubmnail))
            {
                if (imgThubmnail.Length > 10)
                    picture.Image = Image.FromStream(ms);
                else
                    picture.Image = pictureBox1.Image;
            }
            picture.Size = new Size(249, 144);
            picture.Location = new Point(0, 0);
            p.Controls.Add(picture);

            //Title label

            Label labelTitle = new Label();
            labelTitle.Name = "lblTitle" + index;
            labelTitle.Text = title;
            labelTitle.AutoSize = false;
            labelTitle.Size = new Size(242, 53);
            labelTitle.ForeColor = Color.White;
            labelTitle.Anchor =
            labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            labelTitle.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelTitle.Location = new Point(4, 149);
            p.Controls.Add(labelTitle);

            //Author label

            Label labelAuthor = new Label();
            labelAuthor.Name = "lblAuthor" + index;
            labelAuthor.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelAuthor.ForeColor = System.Drawing.Color.Gainsboro;
            labelAuthor.Size = new System.Drawing.Size(42, 15);
            labelAuthor.TabIndex = 3;
            labelAuthor.Text = "Author: " + author;
            labelAuthor.Size = new Size(42, 15);
            labelAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            labelAuthor.Location = new Point(5, 200);
            p.Controls.Add(labelAuthor);

            //Duration label

            Label labelDuration = new Label();
            labelDuration.Name = "lblDuration" + index;
            labelDuration.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelDuration.ForeColor = System.Drawing.Color.Gainsboro;
            labelDuration.Size = new System.Drawing.Size(42, 15);
            //labelDuration.TabIndex = 3;
            labelDuration.Text = "Video Duration: " + duration;
            labelDuration.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelDuration.Location = new Point(5, 220);
            p.Controls.Add(labelDuration);

            //Viewers count label

            Label labelviews = new Label();
            labelviews.Name = "lblviews" + index;
            labelviews.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelviews.ForeColor = System.Drawing.Color.Gainsboro;
            labelviews.Size = new System.Drawing.Size(42, 15);
            labelviews.Text = "Views: " + viewsCount;
            labelviews.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelviews.Location = new Point(6, 240);
            p.Controls.Add(labelviews);
            if (column == 3 )
            {
                column = 0;
                this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 290F));
                currentRow++;
            }
                this.table.Controls.Add(p, column, currentRow);

        }
        TableLayoutPanel table;
        private void searchYoutubeForm_Load(object sender, EventArgs e)
        {

        }

        public void getTable()
        {
            table = new TableLayoutPanel();
            table.AutoScroll = true;
            table.ColumnCount = 3;
            table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            table.Dock = System.Windows.Forms.DockStyle.Fill;
            table.Location = new System.Drawing.Point(0, 0);
            table.Name = "main";
            table.RowCount = 1;
            table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 290F));
            table.Size = new System.Drawing.Size(810, 451);
            table.TabIndex = 7;
            mainPanel.Controls.Add(table);
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            VideoSearch items = new VideoSearch();
            List<video> list = new List<video>();
            int pageNumber = 1;
            var column = 0;
            currentRow = 0;
            table?.Controls.Clear();
            mainPanel.Controls.Clear();
            table = null;
            getTable();
            var videos = await items.GetVideos(txtUrl.Text, 1);
            foreach (var item in videos)
            {
                if (column == 4)
                    column = 0;
                createItem(item.getTitle(), item.getAuthor(), item.getDuration(), item.getViewCount(), new WebClient().DownloadData(item.getThumbnail()), pageNumber++, column++, item.getUrl());

            }

        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
