
namespace ShapesUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GetDemoObject = new System.Windows.Forms.Button();
            this.textAllocator = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // GetDemoObject
            // 
            this.GetDemoObject.Location = new System.Drawing.Point(564, 47);
            this.GetDemoObject.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.GetDemoObject.Name = "GetDemoObject";
            this.GetDemoObject.Size = new System.Drawing.Size(341, 43);
            this.GetDemoObject.TabIndex = 0;
            this.GetDemoObject.Text = "Get Demo";
            this.GetDemoObject.UseVisualStyleBackColor = true;
            this.GetDemoObject.Click += new System.EventHandler(this.GetDemoObject_Click);
            // 
            // textAllocator
            // 
            this.textAllocator.Location = new System.Drawing.Point(34, 47);
            this.textAllocator.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textAllocator.Multiline = true;
            this.textAllocator.Name = "textAllocator";
            this.textAllocator.Size = new System.Drawing.Size(366, 198);
            this.textAllocator.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 330);
            this.Controls.Add(this.textAllocator);
            this.Controls.Add(this.GetDemoObject);
            this.Font = new System.Drawing.Font("Sitka Text", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetDemoObject;
        private System.Windows.Forms.TextBox textAllocator;
    }
}

