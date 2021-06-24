using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1Tile;

namespace Image_Gallery_Demo_Ankit_Kumar
{
    public partial class ImageGallery : Form
    {
        SplitContainer splitContainer1;
        TableLayoutPanel tableLayoutPanel1;
        Panel panel1, panel2, panel3;
        TextBox _searchBox;
        PictureBox _search, _exportimage, _download;
        Tile tile1, tile2, tile3;
        Group group1;
        C1TileControl _imageTileControl;
        StatusStrip statusStrip1 = new StatusStrip();
        ToolStripProgressBar progressbar;

        PanelElement panelElement1 = new PanelElement();
        ImageElement imageElement1 = new ImageElement();
        TextElement textElement1 = new TextElement();

        C1.C1Pdf.C1PdfDocument imagePdfDocument = new C1.C1Pdf.C1PdfDocument();

        DataFetcher datafetch = new DataFetcher();
        List<ImageItem> imagesList
        {
            get;
            set;
        }

        public void setImagesList(List<ImageItem> imageList)
        {
            imagesList = imageList;
        }

        public void calladdTileControl()
        {
            addTileControl();
        }

        public void markAllTilesChecked()
        {
            foreach (Tile tile in _imageTileControl.Groups[0].Tiles)
            {
                tile.Checked = true;
            }
        }

        int checkedItems = 0;

        public ImageGallery()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximumSize = new Size(810, 810);
            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.Size = new Size(780, 800);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Image Gallery";
            Controls.Add(addSplitContainer());
        }

        private  SplitContainer addSplitContainer()
        {
            splitContainer1 = new SplitContainer();
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(2);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            splitContainer1.Size = new Size(631, 326);
            splitContainer1.SplitterDistance = 40;
            splitContainer1.TabIndex = 0;

            splitContainer1.Panel1.Controls.Add(addTableLayout());
            splitContainer1.Panel2.Controls.Add(addExportPicture());
            splitContainer1.Panel2.Controls.Add(addTileControl());
            splitContainer1.Panel2.Controls.Add(addStatus());

            group1.Visible = true;

            return splitContainer1;
        }

        TableLayoutPanel addTableLayout()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.Size = new Size(800, 40);
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            tableLayoutPanel1.Controls.Add(addPanel1(), 1, 0);
            tableLayoutPanel1.Controls.Add(addPanel2(), 2, 0);
            tableLayoutPanel1.Controls.Add(addPanel3(), 0, 0);

            return tableLayoutPanel1;
        }

        Panel addPanel1()
        {
            panel1 = new Panel();
            panel1.Name = "panel1";
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(477, 0);
            panel1.Size = new Size(287, 40);
           
            panel1.Paint += new PaintEventHandler(splitContainer1_Panel2_Paint);

            panel1.Controls.Add(addSearchBox());

            return panel1;
        }

        Panel addPanel2()
        {
            panel2 = new Panel();
            panel2.Name = "panel2";
            panel2.Location = new Point(479, 12);
            panel2.Margin = new Padding(2, 12, 45, 12);
            panel2.Size = new Size(40, 16);
            panel2.TabIndex = 1;

            panel2.Controls.Add(addSearchPicture());

            return panel2;
        }

        Panel addPanel3()
        {
            panel3 = new Panel();
            panel3.Name = "panel3";
            panel3.Dock = DockStyle.Fill;
            panel3.TabIndex = 1;
            panel3.Size = new Size(287, 40);

            panel3.Controls.Add(addDownloadPicture());

            return panel3;
        }

        PictureBox addDownloadPicture()
        {
            _download = new PictureBox();
            _download.Name = "_download";
            _download.Dock = DockStyle.Fill;
            _download.Size = new Size(287, 40);
            _download.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom |
                AnchorStyles.Right);
            _download.Image = global::Image_Gallery_Demo_Ankit_Kumar.Properties.Resources.download;
            _download.SizeMode = PictureBoxSizeMode.StretchImage;
            _download.Visible = false;

            _download.Click += new EventHandler(storeLocally);

            return _download;
        }

        PictureBox addSearchPicture()
        {
            _search = new PictureBox();
            _search.Name = "_search";
            _search.Dock = DockStyle.Left;
            _search.Location = new Point(0, 0);
            _search.Margin = new Padding(0);
            _search.Size = new Size(40, 16);
            _search.SizeMode = PictureBoxSizeMode.Zoom;
            _search.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom |
                AnchorStyles.Right);
            _search.Image = global::Image_Gallery_Demo_Ankit_Kumar.Properties.Resources.search_image;
            _search.Click += new EventHandler(searchClick);

            return _search;
        }

        TextBox addSearchBox()
        {
            _searchBox = new TextBox();
            _searchBox.Name = "_searchBox";
            _searchBox.BorderStyle = BorderStyle.None;
            _searchBox.Dock = DockStyle.Fill;
            _searchBox.Location = new Point(16, 9);
            _searchBox.Size = new Size(244, 16);
            _searchBox.Text = "Search Image";
            _searchBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom
                | AnchorStyles.Right);
            _searchBox.TabIndex = 0;

            return _searchBox;
        }

        PictureBox addExportPicture()
        {
            _exportimage = new PictureBox();
           _exportimage.Image = global::Image_Gallery_Demo_Ankit_Kumar.Properties.Resources.
                exportToPdf;

            _exportimage.Location = new Point(29, 3);
            _exportimage.Name = "_exportimage";
            _exportimage.Size = new Size(135, 28);
            _exportimage.SizeMode = PictureBoxSizeMode.StretchImage;
            _exportimage.TabIndex = 2;
            _exportimage.TabStop = false;
            _exportimage.Visible = false;

            _exportimage.Click += new EventHandler(exportImageClick);
            _exportimage.Paint += new PaintEventHandler(exportImagePaint);

            return _exportimage;
        }

        Tile addTile1()
        {
            tile1 = new Tile();
            tile1.BackColor = Color.LightCoral;
            tile1.Name = "tile1";
            tile1.Text = "Tile 1";
            return tile1;
        }
        Tile addTile2()
        {
            tile2 = new Tile();
            tile2.BackColor = Color.Teal;
            tile2.Name = "tile2";
            tile2.Text = "Tile 2";
            return tile2;
        }
        Tile addTile3()
        {
            tile3 = new Tile();
            tile3.BackColor = Color.SteelBlue;
            tile3.Name = "tile3";
            tile3.Text = "Tile 3";
            return tile3;
        }

        Group addGroup()
        {
            group1 = new Group();
            group1.Name = "group1";
            group1.Text = "";
            group1.Tiles.Add(addTile1());
            group1.Tiles.Add(addTile2());
            group1.Tiles.Add(addTile3());
            group1.Visible = false;
            return group1;
        }

        C1TileControl addTileControl()
        {
            _imageTileControl = new C1TileControl();
            _imageTileControl.Name = "_imageTileControl";
            _imageTileControl.AllowRearranging = true;
            _imageTileControl.CellHeight = 78;
            _imageTileControl.CellSpacing = 18;
            _imageTileControl.CellWidth = 73;
            _imageTileControl.Dock = DockStyle.Fill;
            _imageTileControl.Size = new Size(764, 718);
            _imageTileControl.SurfacePadding = new Padding(12, 4, 12, 4);
            _imageTileControl.SwipeDistance = 20;
            _imageTileControl.SwipeRearrangeDistance = 98;

            _imageTileControl.AllowChecking = true;
            panelElement1.Alignment = ContentAlignment.BottomLeft;
            panelElement1.Children.Add(imageElement1);
            panelElement1.Children.Add(textElement1);
            panelElement1.Margin = new Padding(10, 6, 10, 6);
            _imageTileControl.DefaultTemplate.Elements.Add(panelElement1);
            _imageTileControl.Groups.Add(addGroup());

            _imageTileControl.TileChecked += new EventHandler<TileEventArgs>(imageTileControlTileChecked);
            _imageTileControl.TileUnchecked += new EventHandler<TileEventArgs>(imageTileControlTileUnchecked);
            _imageTileControl.Paint += new PaintEventHandler(imageTileControlPaint);

            return _imageTileControl;
        }

        StatusStrip addStatus()
        {
            statusStrip1.Items.Add(addProgressBar());
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Visible = false;
            return statusStrip1;
        }

        ToolStripProgressBar addProgressBar()
        {
            progressbar = new ToolStripProgressBar();
            progressbar.Style = ProgressBarStyle.Marquee;
            return progressbar;
        }

        private async void searchClick(object sender, EventArgs e)
        {
            checkedItems = 0;
            _exportimage.Visible = false;
            statusStrip1.Visible = true;
            imagesList = await datafetch.GetImageData(_searchBox.Text);
            statusStrip1.Visible = false;
            AddTiles(imagesList);
            group1.Visible = true;
        }

        public void AddTiles(List<ImageItem> imageList)
        {
            _imageTileControl.Groups[0].Tiles.Clear();
            foreach (var imageitem in imageList)
            {
                Tile tile = new Tile();
                tile.HorizontalSize = 2;
                tile.VerticalSize = 2;
                _imageTileControl.Groups[0].Tiles.Add(tile);
                Image img = Image.FromStream(new
                    MemoryStream(imageitem.Base64));
                Template tl = new Template();
                ImageElement ie = new ImageElement();
                ie.ImageLayout = ForeImageLayout.Stretch;
                tl.Elements.Add(ie);
                tile.Template = tl;
                tile.Image = img;
            }
        }

        private void imageTileControlTileChecked(object sender, TileEventArgs e)
        {
            checkedItems++;
            _exportimage.Visible = true;
            _download.Visible = true;
        }

        private void imageTileControlTileUnchecked(object sender, TileEventArgs e)
        {
            checkedItems--;
            _exportimage.Visible = checkedItems > 0;
            _download.Visible = checkedItems > 0;
        }

        private void exportImageClick(object sender, EventArgs e)
        {
            List<Image> images = new List<Image>();
            foreach (Tile tile in _imageTileControl.Groups[0].Tiles)
            {
                if (tile.Checked)
                {
                    images.Add(tile.Image);
                }
            }
            ConvertToPdf(images);
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = "pdf";
            saveFile.Filter = "PDF files (*.pdf)|*.pdf*";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                imagePdfDocument.Save(saveFile.FileName);
            }
        }

        private void ConvertToPdf(List<Image> images)
        {
            RectangleF rect = imagePdfDocument.PageRectangle;
            bool firstPage = true;
            foreach (var selectedimg in images)
            {
                if (!firstPage)
                {
                    imagePdfDocument.NewPage();
                }
                firstPage = false;
                rect.Inflate(-72, -72);
                imagePdfDocument.DrawImage(selectedimg, rect);
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = _searchBox.Bounds;
            r.Inflate(3, 3);
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);
        }

        private void exportImagePaint(object sender, PaintEventArgs e)
        {
            Rectangle r = new Rectangle(_exportimage.Location.X,
                _exportimage.Location.Y, _exportimage.Width, _exportimage.Height);
            r.X -= 29;
            r.Y -= 3;
            r.Width--;
            r.Height--;
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);
            e.Graphics.DrawLine(p, new Point(0, 43), new Point(this.Width, 43));
        }

        private void imageTileControlPaint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawLine(p, 0, 43, 800, 43);
        }

        // Store Image Locally
        private void storeLocally(object sender, EventArgs e)
        {
            _Save();
        }

        public void _Save()
        {
            foreach (Tile tile in _imageTileControl.Groups[0].Tiles)
            {
                if (tile.Checked)
                {
                    //images.Add(tile.Image);
                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                    saveFile.Title = "Save an Image File";
                    saveFile.ShowDialog();
                    System.IO.FileStream fs =
                        (System.IO.FileStream)saveFile.OpenFile();
                    tile.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                    fs.Close();
                }
            }
        }

        public void _SaveAll()
        {
            foreach (Tile tile in _imageTileControl.Groups[0].Tiles)
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                saveFile.Title = "Save an Image File";
                saveFile.ShowDialog();
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFile.OpenFile();
                tile.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                fs.Close();
            }
        }
    }
}