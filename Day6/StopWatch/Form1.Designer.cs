namespace WinFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();  // needed for Timer
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            richTextBox1 = new RichTextBox();
            richTextBox2 = new RichTextBox();
            richTextBox3 = new RichTextBox();
            timer1 = new System.Windows.Forms.Timer(components);  // ✅ declare timer
            SuspendLayout();

            // button1 - Start
            button1.Location = new Point(78, 274);
            button1.Name = "button1";
            button1.Size = new Size(126, 63);
            button1.TabIndex = 0;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;  // ✅ wire up

            // button2 - Stop
            button2.Location = new Point(345, 274);
            button2.Name = "button2";
            button2.Size = new Size(121, 63);
            button2.TabIndex = 1;
            button2.Text = "Stop";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;  // ✅ wire up

            // button3 - Reset
            button3.Location = new Point(617, 274);
            button3.Name = "button3";
            button3.Size = new Size(120, 63);
            button3.TabIndex = 2;
            button3.Text = "Reset";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;  // ✅ wire up

            // richTextBox1
            richTextBox1.Location = new Point(78, 115);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(91, 70);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";

            // richTextBox2
            richTextBox2.Location = new Point(345, 115);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(91, 70);
            richTextBox2.TabIndex = 4;
            richTextBox2.Text = "";

            // richTextBox3
            richTextBox3.Location = new Point(617, 115);
            richTextBox3.Name = "richTextBox3";
            richTextBox3.Size = new Size(91, 70);
            richTextBox3.TabIndex = 5;
            richTextBox3.Text = "";

            // timer1
            timer1.Interval = 1000;           // ✅ tick every 1 second
            timer1.Tick += timer1_Tick;        // ✅ wire up

            // Form1
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(richTextBox3);
            Controls.Add(richTextBox2);
            Controls.Add(richTextBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Stopwatch";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        private Button button1;
        private Button button2;
        private Button button3;
        private RichTextBox richTextBox1;
        private RichTextBox richTextBox2;
        private RichTextBox richTextBox3;
        private System.Windows.Forms.Timer timer1;  // ✅ declare field
    }
}