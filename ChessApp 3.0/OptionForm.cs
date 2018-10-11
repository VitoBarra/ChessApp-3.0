using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace ChessApp_3._0
{

    public partial class OptionForm : Form
    {


        public OptionForm()
        {
            InitializeComponent();
        }

        private void OptionForm_Load(object sender, EventArgs e)
        {
            DimentionSquareTextBox.Text = Global.Conf.AppSettings.Settings["Dimension"].Value;
            #region ComboBox Selected
            if (Global.Conf.AppSettings.Settings["GameMode"].Value == "1")
                GameModeComboBox.SelectedIndex = 0;
            else if (Global.Conf.AppSettings.Settings["GameMode"].Value == "2")
                GameModeComboBox.SelectedIndex = 1;
            else if (Global.Conf.AppSettings.Settings["GameMode"].Value == "3")
                GameModeComboBox.SelectedIndex = 2;
            else if (Global.Conf.AppSettings.Settings["GameMode"].Value == "4")
                GameModeComboBox.SelectedIndex = 3;

            if (Global.Conf.AppSettings.Settings["DifficultyWhiteAi"].Value == "2")
                DifficultyWCombo.SelectedIndex = 0;
            else if (Global.Conf.AppSettings.Settings["DifficultyWhiteAi"].Value == "3")
                DifficultyWCombo.SelectedIndex = 1;
            else if (Global.Conf.AppSettings.Settings["DifficultyWhiteAi"].Value == "4")
                DifficultyWCombo.SelectedIndex = 2;

            if (Global.Conf.AppSettings.Settings["DifficultyBlackAi"].Value == "2")
                DifficultyBCombo.SelectedIndex = 0;
            else if (Global.Conf.AppSettings.Settings["DifficultyBlackAi"].Value == "3")
                DifficultyBCombo.SelectedIndex = 1;
            else if (Global.Conf.AppSettings.Settings["DifficultyBlackAi"].Value == "4")
                DifficultyBCombo.SelectedIndex = 2;

            if (Global.Conf.AppSettings.Settings["ThemeW"].Value == "#f4e4b5" && Global.Conf.AppSettings.Settings["ThemeB"].Value == "#744b44")
                ThemeComboBox.SelectedIndex = 0;
            if (Global.Conf.AppSettings.Settings["ThemeW"].Value == "#e6e6e6" && Global.Conf.AppSettings.Settings["ThemeB"].Value == "#666666")
                ThemeComboBox.SelectedIndex = 1;
            if (Global.Conf.AppSettings.Settings["ThemeW"].Value == "#ffe6ee" && Global.Conf.AppSettings.Settings["ThemeB"].Value == "#ff6699")
                ThemeComboBox.SelectedIndex = 2;
            #endregion
        }

        #region Button
        private void OKButton_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                Global.width_Height = int.Parse(DimentionSquareTextBox.Text);
                Global.Conf.AppSettings.Settings["Dimension"].Value = DimentionSquareTextBox.Text;
                Global.Conf.Save();
                this.Close();
            }
        }

        private void ApplicaButton_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                Global.width_Height = int.Parse(DimentionSquareTextBox.Text);
                Global.Conf.AppSettings.Settings["Dimension"].Value = DimentionSquareTextBox.Text;
                Global.Conf.Save(ConfigurationSaveMode.Minimal);
            }
        }

        private void CancellaButton_Click(object sender, EventArgs e) { this.Close(); }
        #endregion




        private void GameModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GameModeComboBox.SelectedIndex == 0)
            {
                DifficultyWCombo.Hide(); DifficultyBCombo.Hide();
                Global.Conf.AppSettings.Settings["GameMode"].Value = "1";
            }
            else if (GameModeComboBox.SelectedIndex == 1)
            {
                DifficultyWCombo.Hide(); DifficultyBCombo.Show();
                Global.Conf.AppSettings.Settings["GameMode"].Value = "2";
            }
            else if (GameModeComboBox.SelectedIndex == 2)
            {
                DifficultyWCombo.Show(); DifficultyBCombo.Hide();
                Global.Conf.AppSettings.Settings["GameMode"].Value = "3";
            }
            else if (GameModeComboBox.SelectedIndex == 3)
            {
                DifficultyWCombo.Show(); DifficultyBCombo.Show();
                Global.Conf.AppSettings.Settings["GameMode"].Value = "4";
            }
        }

        private void DifficultyWCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DifficultyWCombo.SelectedIndex == 0)
                Global.Conf.AppSettings.Settings["DifficultyWhiteAi"].Value = "2";
            else if (DifficultyWCombo.SelectedIndex == 1)
                Global.Conf.AppSettings.Settings["DifficultyWhiteAi"].Value = "3";
            else if (DifficultyWCombo.SelectedIndex == 2)
                Global.Conf.AppSettings.Settings["DifficultyWhiteAi"].Value = "4";
        }

        private void DifficultyBcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DifficultyBCombo.SelectedIndex == 0)
                Global.Conf.AppSettings.Settings["DifficultyBlackAi"].Value = "2";
            else if (DifficultyBCombo.SelectedIndex == 1)
                Global.Conf.AppSettings.Settings["DifficultyBlackAi"].Value = "3";
            else if (DifficultyBCombo.SelectedIndex == 2)
                Global.Conf.AppSettings.Settings["DifficultyBlackAi"].Value = "4";;
        }

        private void ThemeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ThemeComboBox.SelectedIndex == 0)
            {
                Global.Conf.AppSettings.Settings["ThemeW"].Value = "#f4e4b5";
                Global.Conf.AppSettings.Settings["ThemeB"].Value = "#744b44";
            }
            else if (ThemeComboBox.SelectedIndex == 1)
            {
                Global.Conf.AppSettings.Settings["ThemeW"].Value = "#e6e6e6";
                Global.Conf.AppSettings.Settings["ThemeB"].Value = "#666666";
            }
            else if (ThemeComboBox.SelectedIndex == 2)
            {
                Global.Conf.AppSettings.Settings["ThemeW"].Value = "#ffe6ee";
                Global.Conf.AppSettings.Settings["ThemeB"].Value = "#ff6699";
            }
        }

        private void DimentionSquareTextBox_Validating(object sender, CancelEventArgs e)
        {
            ErrorProvider error = new ErrorProvider();
            if (!(int.Parse(DimentionSquareTextBox.Text) >= 60 && int.Parse(DimentionSquareTextBox.Text) <= 100))
            {
                e.Cancel = true;
                DimentionSquareTextBox.Focus();
                error.SetError(DimentionSquareTextBox, "number between 50 and 100");
            }
            else
            {
                e.Cancel = false;
                error.SetError(DimentionSquareTextBox, null);
            }
        }
    }
}
