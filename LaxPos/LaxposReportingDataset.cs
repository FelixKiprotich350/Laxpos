namespace LaxPos
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

    [Serializable, DesignerCategory("code"), ToolboxItem(true), XmlSchemaProvider("GetTypedDataSetSchema"), XmlRoot("LaxposReportingDataset"), HelpKeyword("vs.data.DataSet")]
    public class LaxposReportingDataset : DataSet
    {
        private DataTable1DataTable tableDataTable1;
        private System.Data.SchemaSerializationMode _schemaSerializationMode;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        public LaxposReportingDataset()
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
        protected LaxposReportingDataset(SerializationInfo info, StreamingContext context) : base(info, context, false)
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
                    if (dataSet.Tables["DataTable1"] != null)
                    {
                        base.Tables.Add(new DataTable1DataTable(dataSet.Tables["DataTable1"]));
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
            LaxposReportingDataset dataset = (LaxposReportingDataset) base.Clone();
            dataset.InitVars();
            dataset.SchemaSerializationMode = this.SchemaSerializationMode;
            return dataset;
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
            LaxposReportingDataset dataset = new LaxposReportingDataset();
            XmlSchemaComplexType type = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            XmlSchemaAny item = new XmlSchemaAny {
                Namespace = dataset.Namespace
            };
            sequence.Items.Add(item);
            type.Particle = sequence;
            XmlSchema schemaSerializable = dataset.GetSchemaSerializable();
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
            base.DataSetName = "LaxposReportingDataset";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/LaxposReportingDataset.xsd";
            base.EnforceConstraints = true;
            this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableDataTable1 = new DataTable1DataTable();
            base.Tables.Add(this.tableDataTable1);
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
            this.tableDataTable1 = (DataTable1DataTable) base.Tables["DataTable1"];
            if (initTable && (this.tableDataTable1 != null))
            {
                this.tableDataTable1.InitVars();
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
                if (dataSet.Tables["DataTable1"] != null)
                {
                    base.Tables.Add(new DataTable1DataTable(dataSet.Tables["DataTable1"]));
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
        private bool ShouldSerializeDataTable1() => 
            false;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        protected override bool ShouldSerializeRelations() => 
            false;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        protected override bool ShouldSerializeTables() => 
            false;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataTable1DataTable DataTable1 =>
            this.tableDataTable1;

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
        public class DataTable1DataTable : TypedTableBase<LaxposReportingDataset.DataTable1Row>
        {
            private DataColumn columnTransNo;
            private DataColumn columnAccType;
            private DataColumn columnSubAmt;
            private DataColumn columnPayMethod;
            private DataColumn columnDate;

            [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public event LaxposReportingDataset.DataTable1RowChangeEventHandler DataTable1RowChanged;

            [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public event LaxposReportingDataset.DataTable1RowChangeEventHandler DataTable1RowChanging;

            [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public event LaxposReportingDataset.DataTable1RowChangeEventHandler DataTable1RowDeleted;

            [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public event LaxposReportingDataset.DataTable1RowChangeEventHandler DataTable1RowDeleting;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataTable1DataTable()
            {
                base.TableName = "DataTable1";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            internal DataTable1DataTable(DataTable table)
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
            protected DataTable1DataTable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                this.InitVars();
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void AddDataTable1Row(LaxposReportingDataset.DataTable1Row row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public LaxposReportingDataset.DataTable1Row AddDataTable1Row(string TransNo, string AccType, string SubAmt, string PayMethod, string Date)
            {
                LaxposReportingDataset.DataTable1Row row = (LaxposReportingDataset.DataTable1Row) base.NewRow();
                row.ItemArray = new object[] { TransNo, AccType, SubAmt, PayMethod, Date };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public override DataTable Clone()
            {
                LaxposReportingDataset.DataTable1DataTable table = (LaxposReportingDataset.DataTable1DataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override DataTable CreateInstance() => 
                new LaxposReportingDataset.DataTable1DataTable();

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override Type GetRowType() => 
                typeof(LaxposReportingDataset.DataTable1Row);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                LaxposReportingDataset dataset = new LaxposReportingDataset();
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
                    FixedValue = dataset.Namespace
                };
                type.Attributes.Add(attribute);
                XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                    Name = "tableTypeName",
                    FixedValue = "DataTable1DataTable"
                };
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                XmlSchema schemaSerializable = dataset.GetSchemaSerializable();
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
                this.columnTransNo = new DataColumn("TransNo", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnTransNo);
                this.columnAccType = new DataColumn("AccType", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAccType);
                this.columnSubAmt = new DataColumn("SubAmt", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnSubAmt);
                this.columnPayMethod = new DataColumn("PayMethod", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnPayMethod);
                this.columnDate = new DataColumn("Date", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDate);
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            internal void InitVars()
            {
                this.columnTransNo = base.Columns["TransNo"];
                this.columnAccType = base.Columns["AccType"];
                this.columnSubAmt = base.Columns["SubAmt"];
                this.columnPayMethod = base.Columns["PayMethod"];
                this.columnDate = base.Columns["Date"];
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public LaxposReportingDataset.DataTable1Row NewDataTable1Row() => 
                (LaxposReportingDataset.DataTable1Row) base.NewRow();

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => 
                new LaxposReportingDataset.DataTable1Row(builder);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.DataTable1RowChanged != null)
                {
                    this.DataTable1RowChanged(this, new LaxposReportingDataset.DataTable1RowChangeEvent((LaxposReportingDataset.DataTable1Row) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.DataTable1RowChanging != null)
                {
                    this.DataTable1RowChanging(this, new LaxposReportingDataset.DataTable1RowChangeEvent((LaxposReportingDataset.DataTable1Row) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.DataTable1RowDeleted != null)
                {
                    this.DataTable1RowDeleted(this, new LaxposReportingDataset.DataTable1RowChangeEvent((LaxposReportingDataset.DataTable1Row) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.DataTable1RowDeleting != null)
                {
                    this.DataTable1RowDeleting(this, new LaxposReportingDataset.DataTable1RowChangeEvent((LaxposReportingDataset.DataTable1Row) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void RemoveDataTable1Row(LaxposReportingDataset.DataTable1Row row)
            {
                base.Rows.Remove(row);
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataColumn TransNoColumn =>
                this.columnTransNo;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataColumn AccTypeColumn =>
                this.columnAccType;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataColumn SubAmtColumn =>
                this.columnSubAmt;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataColumn PayMethodColumn =>
                this.columnPayMethod;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataColumn DateColumn =>
                this.columnDate;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0"), Browsable(false)]
            public int Count =>
                base.Rows.Count;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public LaxposReportingDataset.DataTable1Row this[int index] =>
                (LaxposReportingDataset.DataTable1Row) base.Rows[index];
        }

        public class DataTable1Row : DataRow
        {
            private LaxposReportingDataset.DataTable1DataTable tableDataTable1;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            internal DataTable1Row(DataRowBuilder rb) : base(rb)
            {
                this.tableDataTable1 = (LaxposReportingDataset.DataTable1DataTable) base.Table;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public bool IsAccTypeNull() => 
                base.IsNull(this.tableDataTable1.AccTypeColumn);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public bool IsDateNull() => 
                base.IsNull(this.tableDataTable1.DateColumn);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public bool IsPayMethodNull() => 
                base.IsNull(this.tableDataTable1.PayMethodColumn);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public bool IsSubAmtNull() => 
                base.IsNull(this.tableDataTable1.SubAmtColumn);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public bool IsTransNoNull() => 
                base.IsNull(this.tableDataTable1.TransNoColumn);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void SetAccTypeNull()
            {
                base[this.tableDataTable1.AccTypeColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void SetDateNull()
            {
                base[this.tableDataTable1.DateColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void SetPayMethodNull()
            {
                base[this.tableDataTable1.PayMethodColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void SetSubAmtNull()
            {
                base[this.tableDataTable1.SubAmtColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void SetTransNoNull()
            {
                base[this.tableDataTable1.TransNoColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public string TransNo
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableDataTable1.TransNoColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("The value for column 'TransNo' in table 'DataTable1' is DBNull.", exception);
                    }
                    return str;
                }
                set => 
                    base[this.tableDataTable1.TransNoColumn] = value;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public string AccType
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableDataTable1.AccTypeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("The value for column 'AccType' in table 'DataTable1' is DBNull.", exception);
                    }
                    return str;
                }
                set => 
                    base[this.tableDataTable1.AccTypeColumn] = value;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public string SubAmt
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableDataTable1.SubAmtColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("The value for column 'SubAmt' in table 'DataTable1' is DBNull.", exception);
                    }
                    return str;
                }
                set => 
                    base[this.tableDataTable1.SubAmtColumn] = value;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public string PayMethod
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableDataTable1.PayMethodColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("The value for column 'PayMethod' in table 'DataTable1' is DBNull.", exception);
                    }
                    return str;
                }
                set => 
                    base[this.tableDataTable1.PayMethodColumn] = value;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public string Date
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableDataTable1.DateColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("The value for column 'Date' in table 'DataTable1' is DBNull.", exception);
                    }
                    return str;
                }
                set => 
                    base[this.tableDataTable1.DateColumn] = value;
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        public class DataTable1RowChangeEvent : EventArgs
        {
            private LaxposReportingDataset.DataTable1Row eventRow;
            private DataRowAction eventAction;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataTable1RowChangeEvent(LaxposReportingDataset.DataTable1Row row, DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public LaxposReportingDataset.DataTable1Row Row =>
                this.eventRow;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataRowAction Action =>
                this.eventAction;
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        public delegate void DataTable1RowChangeEventHandler(object sender, LaxposReportingDataset.DataTable1RowChangeEvent e);
    }
}

