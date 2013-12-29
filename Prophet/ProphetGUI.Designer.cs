namespace Prophet {
    partial class ProphetGUI {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.prophetPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // prophetPropertyGrid
            // 
            this.prophetPropertyGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.prophetPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.prophetPropertyGrid.Name = "prophetPropertyGrid";
            this.prophetPropertyGrid.Size = new System.Drawing.Size(533, 472);
            this.prophetPropertyGrid.TabIndex = 0;
            this.prophetPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.prophetPropertyGrid_PropertyValueChanged);
            // 
            // ProphetGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 475);
            this.Controls.Add(this.prophetPropertyGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(16, 38);
            this.Name = "ProphetGUI";
            this.Text = "Prophet";
            this.Load += new System.EventHandler(this.ProphetGUI_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid prophetPropertyGrid;
    }
}