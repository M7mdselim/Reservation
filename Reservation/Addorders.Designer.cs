namespace Reservation
{
    partial class Addorders
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.minusbtn = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.exit = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dashboard_btn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Fetchdatabtn = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.itemmenucombo = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.quantitytxt = new System.Windows.Forms.TextBox();
            this.additembtn = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.reservationidtxt = new System.Windows.Forms.TextBox();
            this.printnpaybtn = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.paidamount = new System.Windows.Forms.TextBox();
            this.reservationnumberlabel = new System.Windows.Forms.Label();
            this.menuitemspanel = new System.Windows.Forms.Panel();
            this.totalPriceLabel = new System.Windows.Forms.Label();
            this.deletebtn = new System.Windows.Forms.Button();
            this.ReservationGridView = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.Phonenumbertxt = new System.Windows.Forms.TextBox();
            this.nametxt = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.cashiernamelabel = new System.Windows.Forms.Label();
            this.greet_user = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReservationGridView)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.minusbtn);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.exit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 35);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // minusbtn
            // 
            this.minusbtn.AutoSize = true;
            this.minusbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minusbtn.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minusbtn.ForeColor = System.Drawing.Color.White;
            this.minusbtn.Location = new System.Drawing.Point(1048, -2);
            this.minusbtn.Name = "minusbtn";
            this.minusbtn.Size = new System.Drawing.Size(20, 23);
            this.minusbtn.TabIndex = 2;
            this.minusbtn.Text = "_";
            this.minusbtn.Click += new System.EventHandler(this.minusbtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "حجوزات رمضان";
            // 
            // exit
            // 
            this.exit.AutoSize = true;
            this.exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit.ForeColor = System.Drawing.Color.White;
            this.exit.Location = new System.Drawing.Point(1079, 8);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(15, 16);
            this.exit.TabIndex = 0;
            this.exit.Text = "X";
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.panel2.Controls.Add(this.cashiernamelabel);
            this.panel2.Controls.Add(this.greet_user);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.dashboard_btn);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(169, 565);
            this.panel2.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(12, 288);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 40);
            this.button2.TabIndex = 8;
            this.button2.Text = "المدفوعات";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 225);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 40);
            this.button1.TabIndex = 7;
            this.button1.Text = "الطلبات";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Stencil", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.IndianRed;
            this.label4.Location = new System.Drawing.Point(41, 509);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "تسجيل خروج";
            // 
            // dashboard_btn
            // 
            this.dashboard_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.dashboard_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dashboard_btn.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.dashboard_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.dashboard_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.dashboard_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dashboard_btn.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dashboard_btn.ForeColor = System.Drawing.Color.White;
            this.dashboard_btn.Location = new System.Drawing.Point(12, 165);
            this.dashboard_btn.Name = "dashboard_btn";
            this.dashboard_btn.Size = new System.Drawing.Size(140, 40);
            this.dashboard_btn.TabIndex = 2;
            this.dashboard_btn.Text = "حجز";
            this.dashboard_btn.UseVisualStyleBackColor = false;
            this.dashboard_btn.Click += new System.EventHandler(this.dashboard_btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Reservation.Properties.Resources.icons8_employee_card_100px;
            this.pictureBox1.Location = new System.Drawing.Point(33, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(873, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "الاسم";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(297, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "رقم التليفون";
            // 
            // Fetchdatabtn
            // 
            this.Fetchdatabtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.Fetchdatabtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Fetchdatabtn.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.Fetchdatabtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.Fetchdatabtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.Fetchdatabtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Fetchdatabtn.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fetchdatabtn.ForeColor = System.Drawing.Color.White;
            this.Fetchdatabtn.Location = new System.Drawing.Point(367, 80);
            this.Fetchdatabtn.Name = "Fetchdatabtn";
            this.Fetchdatabtn.Size = new System.Drawing.Size(140, 40);
            this.Fetchdatabtn.TabIndex = 9;
            this.Fetchdatabtn.Text = "بحث عن عميل";
            this.Fetchdatabtn.UseVisualStyleBackColor = false;
            this.Fetchdatabtn.Click += new System.EventHandler(this.Fetchdatabtn_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel4.Font = new System.Drawing.Font("Tahoma", 2F);
            this.panel4.Location = new System.Drawing.Point(1, 138);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(931, 5);
            this.panel4.TabIndex = 10;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel5.Font = new System.Drawing.Font("Tahoma", 2F);
            this.panel5.Location = new System.Drawing.Point(591, 142);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(5, 424);
            this.panel5.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(511, 267);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 24);
            this.label12.TabIndex = 20;
            this.label12.Text = "الصنف";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // itemmenucombo
            // 
            this.itemmenucombo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemmenucombo.FormattingEnabled = true;
            this.itemmenucombo.Location = new System.Drawing.Point(318, 264);
            this.itemmenucombo.Name = "itemmenucombo";
            this.itemmenucombo.Size = new System.Drawing.Size(186, 27);
            this.itemmenucombo.TabIndex = 21;
            this.itemmenucombo.SelectedIndexChanged += new System.EventHandler(this.itemmenucombo_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(492, 210);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 24);
            this.label11.TabIndex = 22;
            this.label11.Text = "رقم الحجز";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(516, 328);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 24);
            this.label10.TabIndex = 24;
            this.label10.Text = "الكميه";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // quantitytxt
            // 
            this.quantitytxt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantitytxt.Location = new System.Drawing.Point(464, 322);
            this.quantitytxt.Multiline = true;
            this.quantitytxt.Name = "quantitytxt";
            this.quantitytxt.Size = new System.Drawing.Size(46, 30);
            this.quantitytxt.TabIndex = 25;
            this.quantitytxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // additembtn
            // 
            this.additembtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.additembtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.additembtn.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.additembtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.additembtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.additembtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.additembtn.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.additembtn.ForeColor = System.Drawing.Color.White;
            this.additembtn.Location = new System.Drawing.Point(458, 385);
            this.additembtn.Name = "additembtn";
            this.additembtn.Size = new System.Drawing.Size(114, 36);
            this.additembtn.TabIndex = 26;
            this.additembtn.Text = "اضافه";
            this.additembtn.UseVisualStyleBackColor = false;
            this.additembtn.Click += new System.EventHandler(this.additembtn_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.label9.Location = new System.Drawing.Point(487, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 24);
            this.label9.TabIndex = 27;
            this.label9.Text = "بيانات الطلب";
            // 
            // reservationidtxt
            // 
            this.reservationidtxt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reservationidtxt.Location = new System.Drawing.Point(391, 209);
            this.reservationidtxt.Multiline = true;
            this.reservationidtxt.Name = "reservationidtxt";
            this.reservationidtxt.Size = new System.Drawing.Size(91, 30);
            this.reservationidtxt.TabIndex = 28;
            this.reservationidtxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // printnpaybtn
            // 
            this.printnpaybtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.printnpaybtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.printnpaybtn.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.printnpaybtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.printnpaybtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.printnpaybtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printnpaybtn.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printnpaybtn.ForeColor = System.Drawing.Color.White;
            this.printnpaybtn.Location = new System.Drawing.Point(301, 452);
            this.printnpaybtn.Name = "printnpaybtn";
            this.printnpaybtn.Size = new System.Drawing.Size(114, 36);
            this.printnpaybtn.TabIndex = 30;
            this.printnpaybtn.Text = "طباعه ودفع";
            this.printnpaybtn.UseVisualStyleBackColor = false;
            this.printnpaybtn.Click += new System.EventHandler(this.printnpaybtn_Click_1);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(523, 458);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 24);
            this.label13.TabIndex = 31;
            this.label13.Text = "المدفوع";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // paidamount
            // 
            this.paidamount.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paidamount.Location = new System.Drawing.Point(437, 458);
            this.paidamount.Multiline = true;
            this.paidamount.Name = "paidamount";
            this.paidamount.Size = new System.Drawing.Size(79, 25);
            this.paidamount.TabIndex = 32;
            this.paidamount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.paidamount.TextChanged += new System.EventHandler(this.paidamount_TextChanged);
            // 
            // reservationnumberlabel
            // 
            this.reservationnumberlabel.AutoSize = true;
            this.reservationnumberlabel.Location = new System.Drawing.Point(631, 161);
            this.reservationnumberlabel.Name = "reservationnumberlabel";
            this.reservationnumberlabel.Size = new System.Drawing.Size(10, 13);
            this.reservationnumberlabel.TabIndex = 33;
            this.reservationnumberlabel.Text = " ";
            this.reservationnumberlabel.Click += new System.EventHandler(this.reservationnumberlabel_Click);
            // 
            // menuitemspanel
            // 
            this.menuitemspanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuitemspanel.Location = new System.Drawing.Point(19, 165);
            this.menuitemspanel.Name = "menuitemspanel";
            this.menuitemspanel.Size = new System.Drawing.Size(276, 345);
            this.menuitemspanel.TabIndex = 29;
            this.menuitemspanel.Paint += new System.Windows.Forms.PaintEventHandler(this.menuitemspanel_Paint);
            // 
            // totalPriceLabel
            // 
            this.totalPriceLabel.AutoSize = true;
            this.totalPriceLabel.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalPriceLabel.ForeColor = System.Drawing.Color.IndianRed;
            this.totalPriceLabel.Location = new System.Drawing.Point(30, 514);
            this.totalPriceLabel.Name = "totalPriceLabel";
            this.totalPriceLabel.Size = new System.Drawing.Size(0, 16);
            this.totalPriceLabel.TabIndex = 0;
            // 
            // deletebtn
            // 
            this.deletebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.deletebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deletebtn.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.deletebtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.deletebtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.deletebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deletebtn.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deletebtn.ForeColor = System.Drawing.Color.White;
            this.deletebtn.Location = new System.Drawing.Point(301, 385);
            this.deletebtn.Name = "deletebtn";
            this.deletebtn.Size = new System.Drawing.Size(114, 36);
            this.deletebtn.TabIndex = 34;
            this.deletebtn.Text = "مسح";
            this.deletebtn.UseVisualStyleBackColor = false;
            this.deletebtn.Click += new System.EventHandler(this.deletebtn_Click);
            // 
            // ReservationGridView
            // 
            this.ReservationGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ReservationGridView.Location = new System.Drawing.Point(594, 142);
            this.ReservationGridView.Name = "ReservationGridView";
            this.ReservationGridView.Size = new System.Drawing.Size(338, 421);
            this.ReservationGridView.TabIndex = 35;
            this.ReservationGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ReservationGridView_CellContentClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.Phonenumbertxt);
            this.panel3.Controls.Add(this.nametxt);
            this.panel3.Controls.Add(this.ReservationGridView);
            this.panel3.Controls.Add(this.deletebtn);
            this.panel3.Controls.Add(this.totalPriceLabel);
            this.panel3.Controls.Add(this.menuitemspanel);
            this.panel3.Controls.Add(this.reservationnumberlabel);
            this.panel3.Controls.Add(this.paidamount);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.printnpaybtn);
            this.panel3.Controls.Add(this.reservationidtxt);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.additembtn);
            this.panel3.Controls.Add(this.quantitytxt);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.itemmenucombo);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.Fetchdatabtn);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(169, 35);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(931, 565);
            this.panel3.TabIndex = 2;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.DarkRed;
            this.button3.Location = new System.Drawing.Point(6, 100);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 32);
            this.button3.TabIndex = 41;
            this.button3.Text = "مسح كل";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // Phonenumbertxt
            // 
            this.Phonenumbertxt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.Phonenumbertxt.Location = new System.Drawing.Point(47, 21);
            this.Phonenumbertxt.Name = "Phonenumbertxt";
            this.Phonenumbertxt.Size = new System.Drawing.Size(244, 26);
            this.Phonenumbertxt.TabIndex = 40;
            this.Phonenumbertxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Phonenumbertxt.TextChanged += new System.EventHandler(this.Phonenumbertxt_TextChanged);
            // 
            // nametxt
            // 
            this.nametxt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.nametxt.Location = new System.Drawing.Point(505, 22);
            this.nametxt.Name = "nametxt";
            this.nametxt.Size = new System.Drawing.Size(357, 26);
            this.nametxt.TabIndex = 39;
            this.nametxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(14, 411);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(140, 40);
            this.button5.TabIndex = 12;
            this.button5.Text = "Spot Check";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(13, 350);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(140, 40);
            this.button4.TabIndex = 11;
            this.button4.Text = "حجوزات اليوم";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // cashiernamelabel
            // 
            this.cashiernamelabel.AutoSize = true;
            this.cashiernamelabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cashiernamelabel.ForeColor = System.Drawing.Color.White;
            this.cashiernamelabel.Location = new System.Drawing.Point(93, 124);
            this.cashiernamelabel.Name = "cashiernamelabel";
            this.cashiernamelabel.Size = new System.Drawing.Size(48, 19);
            this.cashiernamelabel.TabIndex = 14;
            this.cashiernamelabel.Text = "Selim";
            // 
            // greet_user
            // 
            this.greet_user.AutoSize = true;
            this.greet_user.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.greet_user.ForeColor = System.Drawing.Color.White;
            this.greet_user.Location = new System.Drawing.Point(19, 124);
            this.greet_user.Name = "greet_user";
            this.greet_user.Size = new System.Drawing.Size(78, 19);
            this.greet_user.TabIndex = 13;
            this.greet_user.Text = "Welcome,";
            // 
            // Addorders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "Addorders";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.Home_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReservationGridView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button dashboard_btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label minusbtn;
        private System.Windows.Forms.Label exit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Fetchdatabtn;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox itemmenucombo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox quantitytxt;
        private System.Windows.Forms.Button additembtn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox reservationidtxt;
        private System.Windows.Forms.Button printnpaybtn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox paidamount;
        private System.Windows.Forms.Label reservationnumberlabel;
        private System.Windows.Forms.Panel menuitemspanel;
        private System.Windows.Forms.Label totalPriceLabel;
        private System.Windows.Forms.Button deletebtn;
        private System.Windows.Forms.DataGridView ReservationGridView;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox nametxt;
        private System.Windows.Forms.TextBox Phonenumbertxt;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label cashiernamelabel;
        private System.Windows.Forms.Label greet_user;
    }
}