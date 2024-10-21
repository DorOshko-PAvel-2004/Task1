namespace Task1
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
            buttonFiles = new Button();
            UniteButton = new Button();
            BoxDeletingRow = new TextBox();
            label1 = new Label();
            buttonClearFiles = new Button();
            buttonDbMove = new Button();
            TextDbLoaded = new Label();
            buttonCalculSumMedian = new Button();
            labelSum = new Label();
            labelMedian = new Label();
            folderBrowserDialog1 = new FolderBrowserDialog();
            SuspendLayout();
            // 
            // buttonFiles
            // 
            buttonFiles.Location = new Point(67, 62);
            buttonFiles.Name = "buttonFiles";
            buttonFiles.Size = new Size(75, 23);
            buttonFiles.TabIndex = 0;
            buttonFiles.Text = "Create Files";
            buttonFiles.UseVisualStyleBackColor = true;
            buttonFiles.Click += buttonFiles_ClickAsync;
            // 
            // UniteButton
            // 
            UniteButton.Location = new Point(67, 108);
            UniteButton.Name = "UniteButton";
            UniteButton.Size = new Size(75, 23);
            UniteButton.TabIndex = 1;
            UniteButton.Text = "Unite files";
            UniteButton.UseVisualStyleBackColor = true;
            UniteButton.Click += UniteButton_Click;
            // 
            // BoxDeletingRow
            // 
            BoxDeletingRow.Location = new Point(334, 111);
            BoxDeletingRow.MaxLength = 10;
            BoxDeletingRow.Name = "BoxDeletingRow";
            BoxDeletingRow.Size = new Size(154, 23);
            BoxDeletingRow.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(172, 116);
            label1.Name = "label1";
            label1.Size = new Size(146, 15);
            label1.TabIndex = 3;
            label1.Text = "Symbols for line's deleting";
            // 
            // buttonClearFiles
            // 
            buttonClearFiles.Location = new Point(515, 112);
            buttonClearFiles.Name = "buttonClearFiles";
            buttonClearFiles.Size = new Size(75, 23);
            buttonClearFiles.TabIndex = 4;
            buttonClearFiles.Text = "Clear files";
            buttonClearFiles.UseVisualStyleBackColor = true;
            buttonClearFiles.Click += buttonClearFiles_Click;
            // 
            // buttonDbMove
            // 
            buttonDbMove.Location = new Point(67, 163);
            buttonDbMove.Name = "buttonDbMove";
            buttonDbMove.Size = new Size(116, 23);
            buttonDbMove.TabIndex = 5;
            buttonDbMove.Text = "Move to Database";
            buttonDbMove.UseVisualStyleBackColor = true;
            buttonDbMove.Click += buttonDbMove_ClickAsync;
            // 
            // TextDbLoaded
            // 
            TextDbLoaded.AutoSize = true;
            TextDbLoaded.Location = new Point(514, 157);
            TextDbLoaded.Name = "TextDbLoaded";
            TextDbLoaded.Size = new Size(0, 15);
            TextDbLoaded.TabIndex = 6;
            // 
            // buttonCalculSumMedian
            // 
            buttonCalculSumMedian.Location = new Point(67, 206);
            buttonCalculSumMedian.Name = "buttonCalculSumMedian";
            buttonCalculSumMedian.Size = new Size(139, 42);
            buttonCalculSumMedian.TabIndex = 7;
            buttonCalculSumMedian.Text = "Calculate Sum and Median from database";
            buttonCalculSumMedian.UseVisualStyleBackColor = true;
            buttonCalculSumMedian.Click += buttonCalculSumAndMedian_Click;
            // 
            // labelSum
            // 
            labelSum.AutoSize = true;
            labelSum.Location = new Point(91, 270);
            labelSum.Name = "labelSum";
            labelSum.Size = new Size(0, 15);
            labelSum.TabIndex = 8;
            // 
            // labelMedian
            // 
            labelMedian.AutoSize = true;
            labelMedian.Location = new Point(91, 308);
            labelMedian.Name = "labelMedian";
            labelMedian.Size = new Size(0, 15);
            labelMedian.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelMedian);
            Controls.Add(labelSum);
            Controls.Add(buttonCalculSumMedian);
            Controls.Add(TextDbLoaded);
            Controls.Add(buttonDbMove);
            Controls.Add(buttonClearFiles);
            Controls.Add(label1);
            Controls.Add(BoxDeletingRow);
            Controls.Add(UniteButton);
            Controls.Add(buttonFiles);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonFiles;
        private Button UniteButton;
        private TextBox BoxDeletingRow;
        private Label label1;
        private Button buttonClearFiles;
        private Button buttonDbMove;
        private Label TextDbLoaded;
        private Button buttonCalculSumMedian;
        private Label labelSum;
        private Label labelMedian;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}
