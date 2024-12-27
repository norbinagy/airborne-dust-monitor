namespace airborne_dust_monitor
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.particulateMatterChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.HumThresholdNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.temperatureChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.humidityChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.batteryVoltageChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.HumMinMaxResetButton = new System.Windows.Forms.Button();
            this.BVMinMaxResetButton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.TempThresholdNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.BVThresholdNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PMMinMaxResetButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.grafikonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terkepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TempMinMaxResetButton = new System.Windows.Forms.Button();
            this.PMThresholdNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BVThresholdResetButton = new System.Windows.Forms.Button();
            this.HumThresholdResetButton = new System.Windows.Forms.Button();
            this.TempThresholdResetButton = new System.Windows.Forms.Button();
            this.PMThresholdResetButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.particulateMatterChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HumThresholdNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.humidityChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.batteryVoltageChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TempThresholdNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BVThresholdNumericUpDown)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PMThresholdNumericUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // particulateMatterChart
            // 
            chartArea5.Name = "ChartArea1";
            this.particulateMatterChart.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.particulateMatterChart.Legends.Add(legend5);
            this.particulateMatterChart.Location = new System.Drawing.Point(2, 6);
            this.particulateMatterChart.Margin = new System.Windows.Forms.Padding(2);
            this.particulateMatterChart.Name = "particulateMatterChart";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.particulateMatterChart.Series.Add(series5);
            this.particulateMatterChart.Size = new System.Drawing.Size(543, 242);
            this.particulateMatterChart.TabIndex = 1;
            this.particulateMatterChart.Text = "chart1";
            // 
            // HumThresholdNumericUpDown
            // 
            this.HumThresholdNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.HumThresholdNumericUpDown.DecimalPlaces = 2;
            this.HumThresholdNumericUpDown.Location = new System.Drawing.Point(291, 624);
            this.HumThresholdNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.HumThresholdNumericUpDown.Name = "HumThresholdNumericUpDown";
            this.HumThresholdNumericUpDown.Size = new System.Drawing.Size(90, 20);
            this.HumThresholdNumericUpDown.TabIndex = 2;
            this.HumThresholdNumericUpDown.ValueChanged += new System.EventHandler(this.HumThresholdNumericUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(167, 626);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Páratartalom határérték:";
            // 
            // temperatureChart
            // 
            this.temperatureChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            chartArea6.Name = "ChartArea1";
            this.temperatureChart.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.temperatureChart.Legends.Add(legend6);
            this.temperatureChart.Location = new System.Drawing.Point(640, 3);
            this.temperatureChart.Name = "temperatureChart";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.temperatureChart.Series.Add(series6);
            this.temperatureChart.Size = new System.Drawing.Size(507, 245);
            this.temperatureChart.TabIndex = 7;
            this.temperatureChart.Text = "chart2";
            // 
            // humidityChart
            // 
            this.humidityChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            chartArea7.Name = "ChartArea1";
            this.humidityChart.ChartAreas.Add(chartArea7);
            legend7.Name = "Legend1";
            this.humidityChart.Legends.Add(legend7);
            this.humidityChart.Location = new System.Drawing.Point(5, 308);
            this.humidityChart.Name = "humidityChart";
            series7.ChartArea = "ChartArea1";
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            this.humidityChart.Series.Add(series7);
            this.humidityChart.Size = new System.Drawing.Size(540, 290);
            this.humidityChart.TabIndex = 8;
            this.humidityChart.Text = "chart3";
            // 
            // batteryVoltageChart
            // 
            this.batteryVoltageChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            chartArea8.Name = "ChartArea1";
            this.batteryVoltageChart.ChartAreas.Add(chartArea8);
            legend8.Name = "Legend1";
            this.batteryVoltageChart.Legends.Add(legend8);
            this.batteryVoltageChart.Location = new System.Drawing.Point(640, 308);
            this.batteryVoltageChart.Name = "batteryVoltageChart";
            series8.ChartArea = "ChartArea1";
            series8.Legend = "Legend1";
            series8.Name = "Series1";
            this.batteryVoltageChart.Series.Add(series8);
            this.batteryVoltageChart.Size = new System.Drawing.Size(507, 290);
            this.batteryVoltageChart.TabIndex = 9;
            this.batteryVoltageChart.Text = "chart4";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 597);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "label6";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 610);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "label7";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(678, 587);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "label8";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(678, 600);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "label9";
            // 
            // HumMinMaxResetButton
            // 
            this.HumMinMaxResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.HumMinMaxResetButton.Location = new System.Drawing.Point(5, 626);
            this.HumMinMaxResetButton.Name = "HumMinMaxResetButton";
            this.HumMinMaxResetButton.Size = new System.Drawing.Size(75, 23);
            this.HumMinMaxResetButton.TabIndex = 18;
            this.HumMinMaxResetButton.Text = "Alaphelyzet";
            this.HumMinMaxResetButton.UseVisualStyleBackColor = true;
            this.HumMinMaxResetButton.Click += new System.EventHandler(this.HumMinMaxResetButton_Click);
            // 
            // BVMinMaxResetButton
            // 
            this.BVMinMaxResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BVMinMaxResetButton.Location = new System.Drawing.Point(681, 616);
            this.BVMinMaxResetButton.Name = "BVMinMaxResetButton";
            this.BVMinMaxResetButton.Size = new System.Drawing.Size(75, 23);
            this.BVMinMaxResetButton.TabIndex = 19;
            this.BVMinMaxResetButton.Text = "Alaphelyzet";
            this.BVMinMaxResetButton.UseVisualStyleBackColor = true;
            this.BVMinMaxResetButton.Click += new System.EventHandler(this.BVMinMaxResetButton_Click);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(813, 280);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Hőmérséklet határérték:";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(838, 621);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(111, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "Feszültség határérték:";
            // 
            // TempThresholdNumericUpDown
            // 
            this.TempThresholdNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TempThresholdNumericUpDown.DecimalPlaces = 2;
            this.TempThresholdNumericUpDown.Location = new System.Drawing.Point(939, 278);
            this.TempThresholdNumericUpDown.Name = "TempThresholdNumericUpDown";
            this.TempThresholdNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.TempThresholdNumericUpDown.TabIndex = 23;
            this.TempThresholdNumericUpDown.ValueChanged += new System.EventHandler(this.TempThresholdNumericUpDown_ValueChanged);
            // 
            // BVThresholdNumericUpDown
            // 
            this.BVThresholdNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BVThresholdNumericUpDown.DecimalPlaces = 2;
            this.BVThresholdNumericUpDown.Location = new System.Drawing.Point(955, 619);
            this.BVThresholdNumericUpDown.Name = "BVThresholdNumericUpDown";
            this.BVThresholdNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.BVThresholdNumericUpDown.TabIndex = 24;
            this.BVThresholdNumericUpDown.ValueChanged += new System.EventHandler(this.BVThresholdNumericUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 250);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 263);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            // 
            // PMMinMaxResetButton
            // 
            this.PMMinMaxResetButton.Location = new System.Drawing.Point(12, 278);
            this.PMMinMaxResetButton.Margin = new System.Windows.Forms.Padding(2);
            this.PMMinMaxResetButton.Name = "PMMinMaxResetButton";
            this.PMMinMaxResetButton.Size = new System.Drawing.Size(88, 25);
            this.PMMinMaxResetButton.TabIndex = 6;
            this.PMMinMaxResetButton.Text = "Alaphelyzet";
            this.PMMinMaxResetButton.UseVisualStyleBackColor = true;
            this.PMMinMaxResetButton.Click += new System.EventHandler(this.PMMinMaxResetButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grafikonToolStripMenuItem,
            this.terkepToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1174, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // grafikonToolStripMenuItem
            // 
            this.grafikonToolStripMenuItem.Name = "grafikonToolStripMenuItem";
            this.grafikonToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.grafikonToolStripMenuItem.Text = "Grafikon";
            this.grafikonToolStripMenuItem.Click += new System.EventHandler(this.grafikonToolStripMenuItem_Click);
            // 
            // terkepToolStripMenuItem
            // 
            this.terkepToolStripMenuItem.Name = "terkepToolStripMenuItem";
            this.terkepToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.terkepToolStripMenuItem.Text = "Térkép";
            this.terkepToolStripMenuItem.Click += new System.EventHandler(this.terkepToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(659, 246);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(659, 259);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "label5";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(113, 280);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Szállópor határérték:";
            // 
            // TempMinMaxResetButton
            // 
            this.TempMinMaxResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TempMinMaxResetButton.Location = new System.Drawing.Point(662, 275);
            this.TempMinMaxResetButton.Name = "TempMinMaxResetButton";
            this.TempMinMaxResetButton.Size = new System.Drawing.Size(75, 23);
            this.TempMinMaxResetButton.TabIndex = 17;
            this.TempMinMaxResetButton.Text = "Alaphelyzet";
            this.TempMinMaxResetButton.UseVisualStyleBackColor = true;
            this.TempMinMaxResetButton.Click += new System.EventHandler(this.TempMinMaxResetButton_Click);
            // 
            // PMThresholdNumericUpDown
            // 
            this.PMThresholdNumericUpDown.DecimalPlaces = 2;
            this.PMThresholdNumericUpDown.Location = new System.Drawing.Point(223, 278);
            this.PMThresholdNumericUpDown.Name = "PMThresholdNumericUpDown";
            this.PMThresholdNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.PMThresholdNumericUpDown.TabIndex = 22;
            this.PMThresholdNumericUpDown.ValueChanged += new System.EventHandler(this.PMThresholdNumericUpDown_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.BVThresholdResetButton);
            this.panel1.Controls.Add(this.HumThresholdResetButton);
            this.panel1.Controls.Add(this.TempThresholdResetButton);
            this.panel1.Controls.Add(this.PMThresholdResetButton);
            this.panel1.Controls.Add(this.PMThresholdNumericUpDown);
            this.panel1.Controls.Add(this.particulateMatterChart);
            this.panel1.Controls.Add(this.BVThresholdNumericUpDown);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.PMMinMaxResetButton);
            this.panel1.Controls.Add(this.TempThresholdNumericUpDown);
            this.panel1.Controls.Add(this.BVMinMaxResetButton);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.batteryVoltageChart);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.TempMinMaxResetButton);
            this.panel1.Controls.Add(this.HumMinMaxResetButton);
            this.panel1.Controls.Add(this.HumThresholdNumericUpDown);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.humidityChart);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.temperatureChart);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1150, 658);
            this.panel1.TabIndex = 25;
            // 
            // BVThresholdResetButton
            // 
            this.BVThresholdResetButton.Location = new System.Drawing.Point(1081, 619);
            this.BVThresholdResetButton.Name = "BVThresholdResetButton";
            this.BVThresholdResetButton.Size = new System.Drawing.Size(75, 23);
            this.BVThresholdResetButton.TabIndex = 28;
            this.BVThresholdResetButton.Text = "Alaphelyzet";
            this.BVThresholdResetButton.UseVisualStyleBackColor = true;
            this.BVThresholdResetButton.Click += new System.EventHandler(this.BVThresholdResetButton_Click);
            // 
            // HumThresholdResetButton
            // 
            this.HumThresholdResetButton.Location = new System.Drawing.Point(386, 621);
            this.HumThresholdResetButton.Name = "HumThresholdResetButton";
            this.HumThresholdResetButton.Size = new System.Drawing.Size(75, 23);
            this.HumThresholdResetButton.TabIndex = 27;
            this.HumThresholdResetButton.Text = "Alaphelyzet";
            this.HumThresholdResetButton.UseVisualStyleBackColor = true;
            this.HumThresholdResetButton.Click += new System.EventHandler(this.HumThresholdResetButton_Click);
            // 
            // TempThresholdResetButton
            // 
            this.TempThresholdResetButton.Location = new System.Drawing.Point(1065, 275);
            this.TempThresholdResetButton.Name = "TempThresholdResetButton";
            this.TempThresholdResetButton.Size = new System.Drawing.Size(75, 23);
            this.TempThresholdResetButton.TabIndex = 26;
            this.TempThresholdResetButton.Text = "Alaphelyzet";
            this.TempThresholdResetButton.UseVisualStyleBackColor = true;
            this.TempThresholdResetButton.Click += new System.EventHandler(this.TempThresholdResetButton_Click);
            // 
            // PMThresholdResetButton
            // 
            this.PMThresholdResetButton.Location = new System.Drawing.Point(349, 278);
            this.PMThresholdResetButton.Name = "PMThresholdResetButton";
            this.PMThresholdResetButton.Size = new System.Drawing.Size(75, 23);
            this.PMThresholdResetButton.TabIndex = 25;
            this.PMThresholdResetButton.Text = "Alaphelyzet";
            this.PMThresholdResetButton.UseVisualStyleBackColor = true;
            this.PMThresholdResetButton.Click += new System.EventHandler(this.PMThresholdResetButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(12, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1160, 522);
            this.panel2.TabIndex = 25;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(16, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1141, 512);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.settingsToolStripMenuItem.Text = "Beállítások";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 697);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainWindow";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.particulateMatterChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HumThresholdNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.humidityChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.batteryVoltageChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TempThresholdNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BVThresholdNumericUpDown)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PMThresholdNumericUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart particulateMatterChart;
        private System.Windows.Forms.NumericUpDown HumThresholdNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart temperatureChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart humidityChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart batteryVoltageChart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button HumMinMaxResetButton;
        private System.Windows.Forms.Button BVMinMaxResetButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown TempThresholdNumericUpDown;
        private System.Windows.Forms.NumericUpDown BVThresholdNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button PMMinMaxResetButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem grafikonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terkepToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button TempMinMaxResetButton;
        private System.Windows.Forms.NumericUpDown PMThresholdNumericUpDown;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button PMThresholdResetButton;
        private System.Windows.Forms.Button TempThresholdResetButton;
        private System.Windows.Forms.Button HumThresholdResetButton;
        private System.Windows.Forms.Button BVThresholdResetButton;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    }
}

