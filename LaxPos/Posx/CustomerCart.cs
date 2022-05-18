namespace LaxPos.Pos
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Threading;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable, DesignerCategory("code"), ToolboxItem(true), XmlSchemaProvider("GetTypedDataSetSchema"), XmlRoot("CustomerCart"), HelpKeyword("vs.data.DataSet")]
    public class CustomerCart : DataSet
    {
        private Table_PaymentsDataTable tableTable_Payments;
        private System.Data.SchemaSerializationMode _schemaSerializationMode;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        public CustomerCart()
        {
            this._schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            base.BeginInit();
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
            base.EndInit();
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        protected CustomerCart(SerializationInfo info, StreamingContext context) : base(info, context, false)
        {
            this._schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            if (base.IsBinarySerialized(info, context))
            {
                this.InitVars(false);
                CollectionChangeEventHandler handler2 = new CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += handler2;
                this.Relations.CollectionChanged += handler2;
            }
            else
            {
                string s = (string) info.GetValue("XmlSchema", typeof(string));
                if (base.DetermineSchemaSerializationMode(info, context) != System.Data.SchemaSerializationMode.IncludeSchema)
                {
                    base.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                }
                else
                {
                    DataSet dataSet = new DataSet();
                    dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                    if (dataSet.Tables["Table_Payments"] != null)
                    {
                        base.Tables.Add(new Table_PaymentsDataTable(dataSet.Tables["Table_Payments"]));
                    }
                    base.DataSetName = dataSet.DataSetName;
                    base.Prefix = dataSet.Prefix;
                    base.Namespace = dataSet.Namespace;
                    base.Locale = dataSet.Locale;
                    base.CaseSensitive = dataSet.CaseSensitive;
                    base.EnforceConstraints = dataSet.EnforceConstraints;
                    base.Merge(dataSet, false, MissingSchemaAction.Add);
                    this.InitVars();
                }
                base.GetSerializationData(info, context);
                CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
                base.Tables.CollectionChanged += handler;
                this.Relations.CollectionChanged += handler;
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        public override DataSet Clone()
        {
            CustomerCart cart = (CustomerCart) base.Clone();
            cart.InitVars();
            cart.SchemaSerializationMode = this.SchemaSerializationMode;
            return cart;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        protected override XmlSchema GetSchemaSerializable()
        {
            MemoryStream w = new MemoryStream();
            base.WriteXmlSchema(new XmlTextWriter(w, null));
            w.Position = 0L;
            return XmlSchema.Read(new XmlTextReader(w), null);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
        {
            CustomerCart cart = new CustomerCart();
            XmlSchemaComplexType type = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            XmlSchemaAny item = new XmlSchemaAny {
                Namespace = cart.Namespace
            };
            sequence.Items.Add(item);
            type.Particle = sequence;
            XmlSchema schemaSerializable = cart.GetSchemaSerializable();
            if (xs.Contains(schemaSerializable.TargetNamespace))
            {
                MemoryStream stream = new MemoryStream();
                MemoryStream stream2 = new MemoryStream();
                try
                {
                    XmlSchema current = null;
                    schemaSerializable.Write(stream);
                    IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        current = (XmlSchema) enumerator.Current;
                        stream2.SetLength(0L);
                        current.Write(stream2);
                        if (stream.Length == stream2.Length)
                        {
                            stream.Position = 0L;
                            stream2.Position = 0L;
                            while (true)
                            {
                                if ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                                {
                                    continue;
                                }
                                if (stream.Position != stream.Length)
                                {
                                    break;
                                }
                                return type;
                            }
                        }
                    }
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    if (stream2 != null)
                    {
                        stream2.Close();
                    }
                }
            }
            xs.Add(schemaSerializable);
            return type;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        private void InitClass()
        {
            base.DataSetName = "CustomerCart";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/CustomerCart.xsd";
            base.EnforceConstraints = true;
            this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableTable_Payments = new Table_PaymentsDataTable();
            base.Tables.Add(this.tableTable_Payments);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        protected override void InitializeDerivedDataSet()
        {
            base.BeginInit();
            this.InitClass();
            base.EndInit();
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        internal void InitVars()
        {
            this.InitVars(true);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        internal void InitVars(bool initTable)
        {
            this.tableTable_Payments = (Table_PaymentsDataTable) base.Tables["Table_Payments"];
            if (initTable && (this.tableTable_Payments != null))
            {
                this.tableTable_Payments.InitVars();
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        protected override void ReadXmlSerializable(XmlReader reader)
        {
            if (base.DetermineSchemaSerializationMode(reader) != System.Data.SchemaSerializationMode.IncludeSchema)
            {
                base.ReadXml(reader);
                this.InitVars();
            }
            else
            {
                this.Reset();
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(reader);
                if (dataSet.Tables["Table_Payments"] != null)
                {
                    base.Tables.Add(new Table_PaymentsDataTable(dataSet.Tables["Table_Payments"]));
                }
                base.DataSetName = dataSet.DataSetName;
                base.Prefix = dataSet.Prefix;
                base.Namespace = dataSet.Namespace;
                base.Locale = dataSet.Locale;
                base.CaseSensitive = dataSet.CaseSensitive;
                base.EnforceConstraints = dataSet.EnforceConstraints;
                base.Merge(dataSet, false, MissingSchemaAction.Add);
                this.InitVars();
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        private void SchemaChanged(object sender, CollectionChangeEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Remove)
            {
                this.InitVars();
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        protected override bool ShouldSerializeRelations() => 
            false;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        private bool ShouldSerializeTable_Payments() => 
            false;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        protected override bool ShouldSerializeTables() => 
            false;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Table_PaymentsDataTable Table_Payments =>
            this.tableTable_Payments;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override System.Data.SchemaSerializationMode SchemaSerializationMode
        {
            get => 
                this._schemaSerializationMode;
            set => 
                this._schemaSerializationMode = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataTableCollection Tables =>
            base.Tables;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataRelationCollection Relations =>
            base.Relations;

        [Serializable, XmlSchemaProvider("GetTypedTableSchema")]
        public class Table_PaymentsDataTable : TypedTableBase<CustomerCart.Table_PaymentsRow>
        {
            private DataColumn columnPaymode;
            private DataColumn columnAmount;
            private DataColumn columnReference;
            private DataColumn columnSecReference;
            private DataColumn columnTotal;
            private DataColumn columnBalance;

            [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public event CustomerCart.Table_PaymentsRowChangeEventHandler Table_PaymentsRowChanged;

            [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public event CustomerCart.Table_PaymentsRowChangeEventHandler Table_PaymentsRowChanging;

            [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public event CustomerCart.Table_PaymentsRowChangeEventHandler Table_PaymentsRowDeleted;

            [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public event CustomerCart.Table_PaymentsRowChangeEventHandler Table_PaymentsRowDeleting;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public Table_PaymentsDataTable()
            {
                base.TableName = "Table_Payments";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            internal Table_PaymentsDataTable(DataTable table)
            {
                base.TableName = table.TableName;
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    base.CaseSensitive = table.CaseSensitive;
                }
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    base.Locale = table.Locale;
                }
                if (table.Namespace != table.DataSet.Namespace)
                {
                    base.Namespace = table.Namespace;
                }
                base.Prefix = table.Prefix;
                base.MinimumCapacity = table.MinimumCapacity;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected Table_PaymentsDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                this.InitVars();
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void AddTable_PaymentsRow(CustomerCart.Table_PaymentsRow row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public CustomerCart.Table_PaymentsRow AddTable_PaymentsRow(string Paymode, string Amount, string Reference, string SecReference, string Total, string Balance)
            {
                CustomerCart.Table_PaymentsRow row = (CustomerCart.Table_PaymentsRow) base.NewRow();
                row.ItemArray = new object[] { Paymode, Amount, Reference, SecReference, Total, Balance };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public override DataTable Clone()
            {
                CustomerCart.Table_PaymentsDataTable table = (CustomerCart.Table_PaymentsDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override DataTable CreateInstance() => 
                new CustomerCart.Table_PaymentsDataTable();

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override Type GetRowType() => 
                typeof(CustomerCart.Table_PaymentsRow);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                CustomerCart cart = new CustomerCart();
                XmlSchemaAny item = new XmlSchemaAny {
                    Namespace = "http://www.w3.org/2001/XMLSchema",
                    MinOccurs = 0M,
                    MaxOccurs = decimal.MaxValue,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(item);
                XmlSchemaAny any2 = new XmlSchemaAny {
                    Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1",
                    MinOccurs = 1M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(any2);
                XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                    Name = "namespace",
                    FixedValue = cart.Namespace
                };
                type.Attributes.Add(attribute);
                XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                    Name = "tableTypeName",
                    FixedValue = "Table_PaymentsDataTable"
                };
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                XmlSchema schemaSerializable = cart.GetSchemaSerializable();
                if (xs.Contains(schemaSerializable.TargetNamespace))
                {
                    MemoryStream stream = new MemoryStream();
                    MemoryStream stream2 = new MemoryStream();
                    try
                    {
                        XmlSchema current = null;
                        schemaSerializable.Write(stream);
                        IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                        while (true)
                        {
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            current = (XmlSchema) enumerator.Current;
                            stream2.SetLength(0L);
                            current.Write(stream2);
                            if (stream.Length == stream2.Length)
                            {
                                stream.Position = 0L;
                                stream2.Position = 0L;
                                while (true)
                                {
                                    if ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                                    {
                                        continue;
                                    }
                                    if (stream.Position != stream.Length)
                                    {
                                        break;
                                    }
                                    return type;
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (stream != null)
                        {
                            stream.Close();
                        }
                        if (stream2 != null)
                        {
                            stream2.Close();
                        }
                    }
                }
                xs.Add(schemaSerializable);
                return type;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            private void InitClass()
            {
                this.columnPaymode = new DataColumn("Paymode", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnPaymode);
                this.columnAmount = new DataColumn("Amount", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAmount);
                this.columnReference = new DataColumn("Reference", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnReference);
                this.columnSecReference = new DataColumn("SecReference", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnSecReference);
                this.columnTotal = new DataColumn("Total", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnTotal);
                this.columnBalance = new DataColumn("Balance", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnBalance);
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            internal void InitVars()
            {
                this.columnPaymode = base.Columns["Paymode"];
                this.columnAmount = base.Columns["Amount"];
                this.columnReference = base.Columns["Reference"];
                this.columnSecReference = base.Columns["SecReference"];
                this.columnTotal = base.Columns["Total"];
                this.columnBalance = base.Columns["Balance"];
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => 
                new CustomerCart.Table_PaymentsRow(builder);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public CustomerCart.Table_PaymentsRow NewTable_PaymentsRow() => 
                (CustomerCart.Table_PaymentsRow) base.NewRow();

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.Table_PaymentsRowChanged != null)
                {
                    this.Table_PaymentsRowChanged(this, new CustomerCart.Table_PaymentsRowChangeEvent((CustomerCart.Table_PaymentsRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.Table_PaymentsRowChanging != null)
                {
                    this.Table_PaymentsRowChanging(this, new CustomerCart.Table_PaymentsRowChangeEvent((CustomerCart.Table_PaymentsRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.Table_PaymentsRowDeleted != null)
                {
                    this.Table_PaymentsRowDeleted(this, new CustomerCart.Table_PaymentsRowChangeEvent((CustomerCart.Table_PaymentsRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.Table_PaymentsRowDeleting != null)
                {
                    this.Table_PaymentsRowDeleting(this, new CustomerCart.Table_PaymentsRowChangeEvent((CustomerCart.Table_PaymentsRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void RemoveTable_PaymentsRow(CustomerCart.Table_PaymentsRow row)
            {
                base.Rows.Remove(row);
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataColumn PaymodeColumn =>
                this.columnPaymode;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataColumn AmountColumn =>
                this.columnAmount;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataColumn ReferenceColumn =>
                this.columnReference;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataColumn SecReferenceColumn =>
                this.columnSecReference;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataColumn TotalColumn =>
                this.columnTotal;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataColumn BalanceColumn =>
                this.columnBalance;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0"), Browsable(false)]
            public int Count =>
                base.Rows.Count;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public CustomerCart.Table_PaymentsRow this[int index] =>
                (CustomerCart.Table_PaymentsRow) base.Rows[index];
        }

        public class Table_PaymentsRow : DataRow
        {
            private CustomerCart.Table_PaymentsDataTable tableTable_Payments;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            internal Table_PaymentsRow(DataRowBuilder rb) : base(rb)
            {
                this.tableTable_Payments = (CustomerCart.Table_PaymentsDataTable) base.Table;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public bool IsAmountNull() => 
                base.IsNull(this.tableTable_Payments.AmountColumn);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public bool IsBalanceNull() => 
                base.IsNull(this.tableTable_Payments.BalanceColumn);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public bool IsPaymodeNull() => 
                base.IsNull(this.tableTable_Payments.PaymodeColumn);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public bool IsReferenceNull() => 
                base.IsNull(this.tableTable_Payments.ReferenceColumn);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public bool IsSecReferenceNull() => 
                base.IsNull(this.tableTable_Payments.SecReferenceColumn);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public bool IsTotalNull() => 
                base.IsNull(this.tableTable_Payments.TotalColumn);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void SetAmountNull()
            {
                base[this.tableTable_Payments.AmountColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void SetBalanceNull()
            {
                base[this.tableTable_Payments.BalanceColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void SetPaymodeNull()
            {
                base[this.tableTable_Payments.PaymodeColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void SetReferenceNull()
            {
                base[this.tableTable_Payments.ReferenceColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void SetSecReferenceNull()
            {
                base[this.tableTable_Payments.SecReferenceColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void SetTotalNull()
            {
                base[this.tableTable_Payments.TotalColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public string Paymode
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableTable_Payments.PaymodeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("The value for column 'Paymode' in table 'Table_Payments' is DBNull.", exception);
                    }
                    return str;
                }
                set => 
                    base[this.tableTable_Payments.PaymodeColumn] = value;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public string Amount
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableTable_Payments.AmountColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("The value for column 'Amount' in table 'Table_Payments' is DBNull.", exception);
                    }
                    return str;
                }
                set => 
                    base[this.tableTable_Payments.AmountColumn] = value;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public string Reference
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableTable_Payments.ReferenceColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("The value for column 'Reference' in table 'Table_Payments' is DBNull.", exception);
                    }
                    return str;
                }
                set => 
                    base[this.tableTable_Payments.ReferenceColumn] = value;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public string SecReference
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableTable_Payments.SecReferenceColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("The value for column 'SecReference' in table 'Table_Payments' is DBNull.", exception);
                    }
                    return str;
                }
                set => 
                    base[this.tableTable_Payments.SecReferenceColumn] = value;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public string Total
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableTable_Payments.TotalColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("The value for column 'Total' in table 'Table_Payments' is DBNull.", exception);
                    }
                    return str;
                }
                set => 
                    base[this.tableTable_Payments.TotalColumn] = value;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public string Balance
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableTable_Payments.BalanceColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("The value for column 'Balance' in table 'Table_Payments' is DBNull.", exception);
                    }
                    return str;
                }
                set => 
                    base[this.tableTable_Payments.BalanceColumn] = value;
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        public class Table_PaymentsRowChangeEvent : EventArgs
        {
            private CustomerCart.Table_PaymentsRow eventRow;
            private DataRowAction eventAction;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public Table_PaymentsRowChangeEvent(CustomerCart.Table_PaymentsRow row, DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public CustomerCart.Table_PaymentsRow Row =>
                this.eventRow;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataRowAction Action =>
                this.eventAction;
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        public delegate void Table_PaymentsRowChangeEventHandler(object sender, CustomerCart.Table_PaymentsRowChangeEvent e);
    }
}

