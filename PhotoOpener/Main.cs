using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PhotoOpener.Properties;

namespace PhotoOpener
{
    public partial class Main : Form
    {
        private Saver saver = new Saver();
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
            if (_filePath == string.Empty || _filePath == null)
                return;
            pbMain.Image = Image.FromFile(_filePath);
            SetDeleteBtnVisible();
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
            btnDelete.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            btnDelete.Text = "Usuń zdjęcie";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.FlatStyle = FlatStyle.Popup;
        }
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
                saver.SaveImage();

            if (WindowState == FormWindowState.Maximized)
                IsMaximize = true;
            else
                IsMaximize = false;

            Settings.Default.Save();
        }

    }
}
