namespace ChessApp_3._0
{
    partial class OptionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DimentionSquareTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancellaButton = new System.Windows.Forms.Button();
            this.ApplicaButton = new System.Windows.Forms.Button();
            this.DifficultyBCombo = new System.Windows.Forms.ComboBox();
            this.DifficultyWCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ThemeComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.GameModeComboBox = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // DimentionSquareTextBox
            // 
            this.DimentionSquareTextBox.Location = new System.Drawing.Point(140, 33);
            this.DimentionSquareTextBox.Name = "DimentionSquareTextBox";
            this.DimentionSquareTextBox.Size = new System.Drawing.Size(100, 20);
            this.DimentionSquareTextBox.TabIndex = 0;
            this.DimentionSquareTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.DimentionSquareTextBox_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Dimensione scacchiera";
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(201, 139);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancellaButton
            // 
            this.CancellaButton.Location = new System.Drawing.Point(39, 139);
            this.CancellaButton.Name = "CancellaButton";
            this.CancellaButton.Size = new System.Drawing.Size(75, 23);
            this.CancellaButton.TabIndex = 5;
            this.CancellaButton.Text = "Cancella";
            this.CancellaButton.UseVisualStyleBackColor = true;
            this.CancellaButton.Click += new System.EventHandler(this.CancellaButton_Click);
            // 
            // ApplicaButton
            // 
            this.ApplicaButton.Location = new System.Drawing.Point(120, 139);
            this.ApplicaButton.Name = "ApplicaButton";
            this.ApplicaButton.Size = new System.Drawing.Size(75, 23);
            this.ApplicaButton.TabIndex = 4;
            this.ApplicaButton.Text = "Applica";
            this.ApplicaButton.UseVisualStyleBackColor = true;
            this.ApplicaButton.Click += new System.EventHandler(this.ApplicaButton_Click);
            // 
            // DifficultyBCombo
            // 
            this.DifficultyBCombo.FormattingEnabled = true;
            this.DifficultyBCombo.Items.AddRange(new object[] {
            "2",
            "3",
            "4"});
            this.DifficultyBCombo.Location = new System.Drawing.Point(192, 33);
            this.DifficultyBCombo.Name = "DifficultyBCombo";
            this.DifficultyBCombo.Size = new System.Drawing.Size(48, 21);
            this.DifficultyBCombo.TabIndex = 6;
            this.DifficultyBCombo.SelectedIndexChanged += new System.EventHandler(this.DifficultyBcombo_SelectedIndexChanged);
            // 
            // DifficultyWCombo
            // 
            this.DifficultyWCombo.FormattingEnabled = true;
            this.DifficultyWCombo.Items.AddRange(new object[] {
            "2",
            "3",
            "4"});
            this.DifficultyWCombo.Location = new System.Drawing.Point(140, 33);
            this.DifficultyWCombo.Name = "DifficultyWCombo";
            this.DifficultyWCombo.Size = new System.Drawing.Size(48, 21);
            this.DifficultyWCombo.TabIndex = 7;
            this.DifficultyWCombo.SelectedIndexChanged += new System.EventHandler(this.DifficultyWCombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Difficolta Ai (W\\B)";
            // 
            // ThemeComboBox
            // 
            this.ThemeComboBox.FormattingEnabled = true;
            this.ThemeComboBox.Items.AddRange(new object[] {
            "Legno",
            "Griggio",
            "Rosa",
            "+|Theme"});
            this.ThemeComboBox.Location = new System.Drawing.Point(140, 6);
            this.ThemeComboBox.Name = "ThemeComboBox";
            this.ThemeComboBox.Size = new System.Drawing.Size(100, 21);
            this.ThemeComboBox.TabIndex = 7;
            this.ThemeComboBox.SelectedIndexChanged += new System.EventHandler(this.ThemeComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tema scacchiera";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Modalita di gioco";
            // 
            // GameModeComboBox
            // 
            this.GameModeComboBox.FormattingEnabled = true;
            this.GameModeComboBox.Items.AddRange(new object[] {
            "Player Vs Player",
            "Player Vs Ai",
            "Ai Vs Player",
            "Ai Vs Ai"});
            this.GameModeComboBox.Location = new System.Drawing.Point(140, 6);
            this.GameModeComboBox.Name = "GameModeComboBox";
            this.GameModeComboBox.Size = new System.Drawing.Size(100, 21);
            this.GameModeComboBox.TabIndex = 7;
            this.GameModeComboBox.SelectedIndexChanged += new System.EventHandler(this.GameModeComboBox_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(311, 137);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(303, 111);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Generale";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DifficultyBCombo);
            this.tabPage1.Controls.Add(this.DifficultyWCombo);
            this.tabPage1.Controls.Add(this.GameModeComboBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(303, 111);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "GamePlay";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DimentionSquareTextBox);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.ThemeComboBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(303, 111);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Aspetto";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 167);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.CancellaButton);
            this.Controls.Add(this.ApplicaButton);
            this.Controls.Add(this.OKButton);
            this.Name = "OptionForm";
            this.Text = "Option Form";
            this.Load += new System.EventHandler(this.OptionForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox DimentionSquareTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancellaButton;
        private System.Windows.Forms.Button ApplicaButton;
        private System.Windows.Forms.ComboBox DifficultyBCombo;
        private System.Windows.Forms.ComboBox DifficultyWCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ThemeComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox GameModeComboBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
    }
}