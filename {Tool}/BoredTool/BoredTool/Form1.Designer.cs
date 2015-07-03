using System;
using System.Windows.Forms;

namespace BoredTool
{
    partial class From1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.list = new System.Windows.Forms.DataGridView();
            this.Colum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ZhuJian = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Default = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AutoIncrement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TableName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Encryption = new System.Windows.Forms.Button();
            this.CreateSQL = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.CreateClass = new System.Windows.Forms.Button();
            this.used = new System.Windows.Forms.TextBox();
            this.ClassName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NameSpace = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Connection = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.list)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.list);
            this.panel1.Location = new System.Drawing.Point(2, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(710, 470);
            this.panel1.TabIndex = 0;
            // 
            // list
            // 
            this.list.CausesValidation = false;
            this.list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Colum,
            this.DataType,
            this.Length,
            this.IsNull,
            this.ZhuJian,
            this.Default,
            this.AutoIncrement,
            this.Description});
            this.list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list.Location = new System.Drawing.Point(0, 0);
            this.list.Name = "list";
            this.list.RowTemplate.Height = 23;
            this.list.Size = new System.Drawing.Size(710, 470);
            this.list.TabIndex = 0;
            this.list.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.list_CellEnter);
            this.list.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.list_CellLeave);
            this.list.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.list_EditingControlShowing);
            this.list.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.list_RowLeave);
            this.list.TabIndexChanged += new System.EventHandler(this.list_TabIndexChanged);
            // 
            // Colum
            // 
            this.Colum.Frozen = true;
            this.Colum.HeaderText = "列名";
            this.Colum.Name = "Colum";
            // 
            // DataType
            // 
            this.DataType.HeaderText = "数据类型";
            this.DataType.Items.AddRange(new object[] {
            "BigInt",
            "Binary",
            "Bit",
            "Char",
            "DateTime",
            "Decimal",
            "Float",
            "Image",
            "Int",
            "Money",
            "NChar",
            "NText",
            "NVarChar",
            "Real",
            "UniqueIdentifier",
            "SmallDateTime",
            "SmallInt",
            "SmallMoney",
            "Text",
            "Timestamp",
            "TinyInt",
            "VarBinary",
            "VarChar",
            "Variant",
            "Xml",
            "Udt",
            "Structured",
            "Date",
            "Time",
            "DateTime2",
            "DateTimeOffset"});
            this.DataType.Name = "DataType";
            this.DataType.Width = 110;
            // 
            // Length
            // 
            this.Length.HeaderText = "长度";
            this.Length.Name = "Length";
            this.Length.Width = 80;
            // 
            // IsNull
            // 
            this.IsNull.HeaderText = "能为空";
            this.IsNull.Name = "IsNull";
            this.IsNull.Width = 50;
            // 
            // ZhuJian
            // 
            this.ZhuJian.HeaderText = "主键";
            this.ZhuJian.Name = "ZhuJian";
            this.ZhuJian.Width = 50;
            // 
            // Default
            // 
            this.Default.HeaderText = "默认值";
            this.Default.Name = "Default";
            this.Default.Width = 70;
            // 
            // AutoIncrement
            // 
            this.AutoIncrement.HeaderText = "自动列";
            this.AutoIncrement.Name = "AutoIncrement";
            // 
            // Description
            // 
            this.Description.HeaderText = "描述";
            this.Description.Name = "Description";
            this.Description.Width = 107;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.TableName);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.Encryption);
            this.panel2.Controls.Add(this.CreateSQL);
            this.panel2.Controls.Add(this.Delete);
            this.panel2.Controls.Add(this.CreateClass);
            this.panel2.Controls.Add(this.used);
            this.panel2.Controls.Add(this.Connection);
            this.panel2.Controls.Add(this.ClassName);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.NameSpace);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(718, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(226, 369);
            this.panel2.TabIndex = 1;
            // 
            // TableName
            // 
            this.TableName.Location = new System.Drawing.Point(74, 11);
            this.TableName.Name = "TableName";
            this.TableName.Size = new System.Drawing.Size(130, 21);
            this.TableName.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "表名：";
            // 
            // Encryption
            // 
            this.Encryption.Location = new System.Drawing.Point(99, 292);
            this.Encryption.Name = "Encryption";
            this.Encryption.Size = new System.Drawing.Size(75, 23);
            this.Encryption.TabIndex = 5;
            this.Encryption.Text = "加密";
            this.Encryption.UseVisualStyleBackColor = true;
            this.Encryption.Click += new System.EventHandler(this.Encryption_Click);
            // 
            // CreateSQL
            // 
            this.CreateSQL.Location = new System.Drawing.Point(5, 292);
            this.CreateSQL.Name = "CreateSQL";
            this.CreateSQL.Size = new System.Drawing.Size(75, 23);
            this.CreateSQL.TabIndex = 4;
            this.CreateSQL.Text = "生成SQL语句";
            this.CreateSQL.UseVisualStyleBackColor = true;
            this.CreateSQL.Click += new System.EventHandler(this.CreateSQL_Click);
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(99, 246);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 23);
            this.Delete.TabIndex = 3;
            this.Delete.Text = "删除选";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // CreateClass
            // 
            this.CreateClass.Location = new System.Drawing.Point(5, 246);
            this.CreateClass.Name = "CreateClass";
            this.CreateClass.Size = new System.Drawing.Size(75, 23);
            this.CreateClass.TabIndex = 3;
            this.CreateClass.Text = "生成";
            this.CreateClass.UseVisualStyleBackColor = true;
            this.CreateClass.Click += new System.EventHandler(this.CreateClass_Click);
            // 
            // used
            // 
            this.used.Location = new System.Drawing.Point(3, 138);
            this.used.Multiline = true;
            this.used.Name = "used";
            this.used.Size = new System.Drawing.Size(214, 88);
            this.used.TabIndex = 2;
            this.used.Text = "using System;\r\nusing System.Data.Entity;";
            // 
            // ClassName
            // 
            this.ClassName.Location = new System.Drawing.Point(74, 65);
            this.ClassName.Name = "ClassName";
            this.ClassName.Size = new System.Drawing.Size(130, 21);
            this.ClassName.TabIndex = 1;
            this.ClassName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "附加空间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "类：";
            // 
            // NameSpace
            // 
            this.NameSpace.Location = new System.Drawing.Point(74, 38);
            this.NameSpace.Name = "NameSpace";
            this.NameSpace.Size = new System.Drawing.Size(130, 21);
            this.NameSpace.TabIndex = 1;
            this.NameSpace.Text = "Bored.Model";
            this.NameSpace.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "命名空间：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "连接字符串：";
            // 
            // Connection
            // 
            this.Connection.Location = new System.Drawing.Point(74, 92);
            this.Connection.Name = "Connection";
            this.Connection.Size = new System.Drawing.Size(130, 21);
            this.Connection.TabIndex = 1;
            this.Connection.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // From1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 368);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "From1";
            this.Text = "生成工具";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.list)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView list;
        private Panel panel2;
        private TextBox NameSpace;
        private Label label1;
        private TextBox ClassName;
        private Label label2;
        private Label label3;
        private TextBox used;
        private Button CreateClass;
        private Button Delete;
        private DataGridViewTextBoxColumn Colum;
        private DataGridViewComboBoxColumn DataType;
        private DataGridViewTextBoxColumn Length;
        private DataGridViewCheckBoxColumn IsNull;
        private DataGridViewCheckBoxColumn ZhuJian;
        private DataGridViewTextBoxColumn Default;
        private DataGridViewTextBoxColumn AutoIncrement;
        private DataGridViewTextBoxColumn Description;
        private Button CreateSQL;
        private Button Encryption;
        private TextBox TableName;
        private Label label4;
        private TextBox Connection;
        private Label label5;
    }
}

