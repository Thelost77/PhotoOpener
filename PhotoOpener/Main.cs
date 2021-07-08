using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using PhotoOpener.Properties;

namespace PhotoOpener
{
    public partial class Main : Form
    {
        private Saver saver = new Saver(Program.FilePath);
        public bool IsMaximize
        {
            get
            {
                return Settings.Default.IsMaximize;
            }
            set
            {
                Settings.Default.IsMaximize = value;
            }
        }
        private readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        private string _filePath;
        public Main()
        {
            InitializeComponent();
            SetDeleteBtnInvisible();
            if (IsMaximize)
                WindowState = FormWindowState.Maximized;
            LoadPhoto();
        }

        private void LoadPhoto()
        {
            _filePath = saver.ReadImagePath();
            if (string.IsNullOrWhiteSpace(_filePath))
                return;
            pbMain.Image = Image.FromFile(_filePath);
            SetDeleteBtnVisible();         
           
        }
        private void SetDeleteBtnInvisible()
        {
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.FlatAppearance.BorderColor = BackColor;
            btnDelete.FlatAppearance.MouseOverBackColor = BackColor;
            btnDelete.FlatAppearance.MouseDownBackColor = BackColor;
            btnDelete.BackColor = BackColor;
            btnDelete.Text = null;
        }

        private void SetDeleteBtnVisible()
        {
            btnDelete.BackColor = Color.FromArgb(255,192,255);
            btnDelete.Text = "Usuń zdjęcie";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.FlatStyle = FlatStyle.Popup;
        }

        private void btnAddPhoto_Click(object sender, EventArgs e)
        {            
            openFileDialog1.ShowDialog();
            _filePath = openFileDialog1.FileName;
            if (_filePath == "openFileDialog1" || _filePath == string.Empty)
                return;
            if (ImageExtensions.Contains(Path.GetExtension(_filePath).ToUpper()))
            {
                pbMain.Image = Image.FromFile(_filePath);
                SetDeleteBtnVisible();
                Program.PhotoPath = _filePath;
            }
            else
            {
                MessageBox.Show("Wrong file extension!");
                openFileDialog1.Reset();
            }         
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _filePath = String.Empty;
            pbMain.Image = null;
            SetDeleteBtnInvisible();
            saver.DeleteImage();
        }       
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
                saver.SaveImage(Program.PhotoPath);

            if (WindowState == FormWindowState.Maximized)
                IsMaximize = true;
            else
                IsMaximize = false;

            Settings.Default.Save();
        }

    }
}
