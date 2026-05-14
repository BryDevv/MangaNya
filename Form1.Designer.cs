namespace MangaNya
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            button8 = new Button();
            txtNit = new TextBox();
            label11 = new Label();
            txtNombre = new TextBox();
            label10 = new Label();
            label9 = new Label();
            button3 = new Button();
            label2 = new Label();
            Data2 = new DataGridView();
            button2 = new Button();
            button1 = new Button();
            label1 = new Label();
            DataP = new DataGridView();
            tabPage2 = new TabPage();
            button6 = new Button();
            button5 = new Button();
            DataP_admin = new DataGridView();
            label8 = new Label();
            textBox5 = new TextBox();
            label7 = new Label();
            textBox4 = new TextBox();
            label6 = new Label();
            textBox3 = new TextBox();
            label5 = new Label();
            textBox2 = new TextBox();
            label4 = new Label();
            button4 = new Button();
            textBox1 = new TextBox();
            label3 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Data2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DataP).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataP_admin).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(8, 14);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(915, 790);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button8);
            tabPage1.Controls.Add(txtNit);
            tabPage1.Controls.Add(label11);
            tabPage1.Controls.Add(txtNombre);
            tabPage1.Controls.Add(label10);
            tabPage1.Controls.Add(label9);
            tabPage1.Controls.Add(button3);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(Data2);
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(DataP);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(907, 757);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Productos";
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            // 
            // button8
            // 
            button8.Location = new Point(587, 618);
            button8.Name = "button8";
            button8.Size = new Size(94, 29);
            button8.TabIndex = 12;
            button8.Text = "Imprimir";
            button8.UseVisualStyleBackColor = true;
            button8.Visible = false;
            button8.Click += button8_Click;
            // 
            // txtNit
            // 
            txtNit.Location = new Point(587, 564);
            txtNit.Name = "txtNit";
            txtNit.Size = new Size(125, 27);
            txtNit.TabIndex = 11;
            txtNit.Visible = false;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(586, 541);
            label11.Name = "label11";
            label11.Size = new Size(35, 20);
            label11.TabIndex = 10;
            label11.Text = "NIT:";
            label11.Visible = false;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(587, 496);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(125, 27);
            txtNombre.TabIndex = 9;
            txtNombre.Visible = false;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(586, 473);
            label10.Name = "label10";
            label10.Size = new Size(67, 20);
            label10.TabIndex = 8;
            label10.Text = "Nombre:";
            label10.Visible = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(586, 355);
            label9.Name = "label9";
            label9.Size = new Size(45, 20);
            label9.TabIndex = 7;
            label9.Text = "Total:";
            // 
            // button3
            // 
            button3.Location = new Point(587, 378);
            button3.Name = "button3";
            button3.Size = new Size(237, 29);
            button3.TabIndex = 6;
            button3.Text = "Eliminar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(587, 83);
            label2.Name = "label2";
            label2.Size = new Size(139, 20);
            label2.TabIndex = 5;
            label2.Text = "Carrito de compras:";
            // 
            // Data2
            // 
            Data2.AllowUserToAddRows = false;
            Data2.AllowUserToDeleteRows = false;
            Data2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Data2.Location = new Point(587, 121);
            Data2.Name = "Data2";
            Data2.ReadOnly = true;
            Data2.RowHeadersWidth = 51;
            Data2.Size = new Size(237, 216);
            Data2.TabIndex = 4;
            // 
            // button2
            // 
            button2.Location = new Point(586, 413);
            button2.Name = "button2";
            button2.Size = new Size(237, 29);
            button2.TabIndex = 3;
            button2.Text = "Generar factura";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(38, 469);
            button1.Name = "button1";
            button1.Size = new Size(405, 29);
            button1.TabIndex = 2;
            button1.Text = "Agregar al carrito";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 70);
            label1.Name = "label1";
            label1.Size = new Size(167, 20);
            label1.TabIndex = 1;
            label1.Text = "Productos en existencia:";
            // 
            // DataP
            // 
            DataP.AllowUserToAddRows = false;
            DataP.AllowUserToDeleteRows = false;
            DataP.AllowUserToResizeColumns = false;
            DataP.AllowUserToResizeRows = false;
            DataP.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataP.Location = new Point(38, 112);
            DataP.Name = "DataP";
            DataP.ReadOnly = true;
            DataP.RowHeadersWidth = 51;
            DataP.Size = new Size(405, 323);
            DataP.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(button6);
            tabPage2.Controls.Add(button5);
            tabPage2.Controls.Add(DataP_admin);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(textBox5);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(textBox4);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(textBox3);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(textBox2);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(button4);
            tabPage2.Controls.Add(textBox1);
            tabPage2.Controls.Add(label3);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(907, 757);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Control";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(660, 63);
            button6.Name = "button6";
            button6.Size = new Size(168, 29);
            button6.TabIndex = 14;
            button6.Text = "Más Vendidos";
            button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(37, 586);
            button5.Name = "button5";
            button5.Size = new Size(187, 29);
            button5.TabIndex = 13;
            button5.Text = "Modificar";
            button5.UseVisualStyleBackColor = true;
            // 
            // DataP_admin
            // 
            DataP_admin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataP_admin.Location = new Point(297, 43);
            DataP_admin.Name = "DataP_admin";
            DataP_admin.RowHeadersWidth = 51;
            DataP_admin.Size = new Size(337, 505);
            DataP_admin.TabIndex = 12;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(37, 467);
            label8.Name = "label8";
            label8.Size = new Size(48, 20);
            label8.TabIndex = 11;
            label8.Text = "Stock:";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(37, 506);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(187, 27);
            textBox5.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(37, 359);
            label7.Name = "label7";
            label7.Size = new Size(114, 20);
            label7.TabIndex = 9;
            label7.Text = "Precio de venta:";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(37, 398);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(187, 27);
            textBox4.TabIndex = 8;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(37, 259);
            label6.Name = "label6";
            label6.Size = new Size(53, 20);
            label6.TabIndex = 7;
            label6.Text = "Marca:";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(37, 298);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(187, 27);
            textBox3.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(37, 160);
            label5.Name = "label5";
            label5.Size = new Size(61, 20);
            label5.TabIndex = 5;
            label5.Text = "Código:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(37, 199);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(187, 27);
            textBox2.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(37, 75);
            label4.Name = "label4";
            label4.Size = new Size(67, 20);
            label4.TabIndex = 3;
            label4.Text = "Nombre:";
            // 
            // button4
            // 
            button4.Location = new Point(37, 551);
            button4.Name = "button4";
            button4.Size = new Size(187, 29);
            button4.TabIndex = 2;
            button4.Text = "Agregar";
            button4.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(37, 114);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(187, 27);
            textBox1.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(37, 16);
            label3.Name = "label3";
            label3.Size = new Size(185, 28);
            label3.TabIndex = 0;
            label3.Text = "Agregar producto:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(930, 816);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Data2).EndInit();
            ((System.ComponentModel.ISupportInitialize)DataP).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DataP_admin).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button button3;
        private Label label2;
        private DataGridView Data2;
        private Button button2;
        private Button button1;
        private Label label1;
        private DataGridView DataP;
        private Label label6;
        private TextBox textBox3;
        private Label label5;
        private TextBox textBox2;
        private Label label4;
        private Button button4;
        private TextBox textBox1;
        private Label label3;
        private Label label8;
        private TextBox textBox5;
        private Label label7;
        private TextBox textBox4;
        private Button button6;
        private Button button5;
        private DataGridView DataP_admin;
        private Label label9;
        private TextBox txtNit;
        private Label label11;
        private TextBox txtNombre;
        private Label label10;
        private Button button8;
        private Label label12;
        private TextBox textBox6;
    }
}
    

