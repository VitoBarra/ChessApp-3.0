namespace ChessApp_3._0
{
    partial class Board
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Board
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Board";
            this.Load += new System.EventHandler(this.Board_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Board_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Board_DragEnter);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Board_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
