using AnimatedWallpaper.Media;
using AnimatedWallpaper.Wallpaper;
using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace AnimatedWallpaper.Forms
{
    public partial class SettingsForm : Form
    {
        private readonly RegistryKey _rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        private readonly bool _initStartupState;

        public SettingsForm()
        {
            InitializeComponent();

            LoadData();

            if (_rkApp.GetValue("AnimatedWallpaper") is null)
            {
                startup_chk.Checked = false;
                _initStartupState = false;
            }
            else
            {
                startup_chk.Checked = true;
                _initStartupState = true;
            }

            videoFileDialog.Filter = "MP4 Files|*.mp4|WMV Files|*.wmv|WMA Files|*.wma|AVI Files|*.avi";
        }

        private void LoadData()
        {
            activateLst.Items.Clear();
            deactivateLst.Items.Clear();

            foreach (var media in MediaHandler.ActiveMedia)
            {
                activateLst.Items.Add(media.Name);
            }

            foreach (var media in MediaHandler.InactiveMedia)
            {
                deactivateLst.Items.Add(media.Name);
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (videoFileDialog.ShowDialog() != DialogResult.OK)
                return;
            
            var url = videoFileDialog.FileName;
            var name = videoFileDialog.SafeFileName;

            MediaHandler.Add(url, name);
            deactivateLst.Items.Add(name ?? string.Empty);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            MediaHandler.Save();

            WallpaperHandler.Restart();

            if (startup_chk.Checked != _initStartupState)
            {
                if (startup_chk.Checked)
                {
                    _rkApp.SetValue("AnimatedWallpaper", Application.ExecutablePath);
                }
                else
                {
                    _rkApp.DeleteValue("AnimatedWallpaper", false);
                }
            }

            Dispose();
        }

        private void upBtn_Click(object sender, EventArgs e)
        {
            var selected = activateLst.SelectedIndex;

            if (selected == 0 || selected == -1)
                return;

            var name = (string)activateLst.Items[activateLst.SelectedIndex];

            MediaHandler.MoveUp(name);

            LoadData();
        }

        private void downBtn_Click(object sender, EventArgs e)
        {
            var selected = activateLst.SelectedIndex;

            if (selected == activateLst.Items.Count - 1 || selected == -1)
                return;

            var name = (string)activateLst.Items[activateLst.SelectedIndex];

            MediaHandler.MoveDown(name);

            LoadData();
        }

        private void activateBtn_Click(object sender, EventArgs e)
        {
            if (deactivateLst.Items.Count == 0)
                return;

            var value = (string)deactivateLst.Items[deactivateLst.SelectedIndex];

            var media = MediaHandler.Get(value);
            MediaHandler.Activate(media.Name);
            MediaHandler.MoveToEnd(media.Name);

            //var selected = deactivateLst.SelectedIndex;
            //deactivateLst.Items.RemoveAt(selected);
            //activateLst.Items.Insert(activateLst.Items.Count, media.Name);
            LoadData();
        }

        private void deactivateBtn_Click(object sender, EventArgs e)
        {
            if (activateLst.Items.Count == 0)
                return;

            var value = (string)activateLst.Items[activateLst.SelectedIndex];

            var media = MediaHandler.Get(value);
            MediaHandler.Deactivate(media.Name);
            MediaHandler.MoveToStart(media.Name);

            //var selected = activateLst.SelectedIndex;
            //activateLst.Items.RemoveAt(selected);
            //deactivateLst.Items.Insert(0, media.Name);
            LoadData();
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            var selected = activateLst.SelectedIndex;
            string name;

            if (selected != -1)
            {
                name = activateLst.Items[selected].ToString();
            }
            else
            {
                selected = deactivateLst.SelectedIndex;
                if (selected == -1)
                    return;

                name = deactivateLst.Items[selected].ToString();
            }

            MediaHandler.Remove(name);

            LoadData();
        }

        private void activateLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            deactivateLst.ClearSelected();
        }

        private void deactivateLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            activateLst.ClearSelected();
        }
    }
}