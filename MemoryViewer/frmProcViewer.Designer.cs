namespace MemoryViewer
{
    partial class frmProcViewer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.datData = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.datModules = new System.Windows.Forms.DataGridView();
            this.datMemory = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.datData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datModules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datMemory)).BeginInit();
            this.SuspendLayout();
            // 
            // datData
            // 
            this.datData.AllowUserToAddRows = false;
            this.datData.AllowUserToDeleteRows = false;
            this.datData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datData.Location = new System.Drawing.Point(0, 0);
            this.datData.Name = "datData";
            this.datData.ReadOnly = true;
            this.datData.Size = new System.Drawing.Size(244, 397);
            this.datData.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.datData);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(733, 397);
            this.splitContainer1.SplitterDistance = 244;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.datModules);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.datMemory);
            this.splitContainer2.Size = new System.Drawing.Size(485, 397);
            this.splitContainer2.SplitterDistance = 198;
            this.splitContainer2.TabIndex = 3;
            // 
            // datModules
            // 
            this.datModules.AllowUserToAddRows = false;
            this.datModules.AllowUserToDeleteRows = false;
            this.datModules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datModules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datModules.Location = new System.Drawing.Point(0, 0);
            this.datModules.Name = "datModules";
            this.datModules.ReadOnly = true;
            this.datModules.Size = new System.Drawing.Size(485, 198);
            this.datModules.TabIndex = 1;
            // 
            // datMemory
            // 
            this.datMemory.AllowUserToAddRows = false;
            this.datMemory.AllowUserToDeleteRows = false;
            this.datMemory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datMemory.DefaultCellStyle = dataGridViewCellStyle3;
            this.datMemory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datMemory.Location = new System.Drawing.Point(0, 0);
            this.datMemory.Name = "datMemory";
            this.datMemory.ReadOnly = true;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datMemory.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.datMemory.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datMemory.Size = new System.Drawing.Size(485, 195);
            this.datMemory.TabIndex = 2;
            // 
            // frmProcViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 397);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmProcViewer";
            this.Text = "frmProcViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProcViewer_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.datData)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datModules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datMemory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView datData;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView datModules;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView datMemory;
    }
}