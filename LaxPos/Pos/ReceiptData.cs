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

    [Serializable, DesignerCategory("code"), ToolboxItem(true), XmlSchemaProvider("GetTypedDataSetSchema"), XmlRoot("ReceiptData"), HelpKeyword("vs.data.DataSet")]
    public class ReceiptData : DataSet
    {
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        private DataTable1DataTable tableDataTable1;
        private System.Data.SchemaSerializationMode _schemaSerializationMode;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        public ReceiptData()
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
        protected ReceiptData(SerializationInfo info, StreamingContext context) : base(info, context, false)
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
            ReceiptData data = (ReceiptData) base.Clone();
            data.InitVars();
            data.SchemaSerializationMode = this.SchemaSerializationMode;
            return data;
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
            ReceiptData data = new ReceiptData();
            XmlSchemaComplexType type = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            XmlSchemaAny item = new XmlSchemaAny {
                Namespace = data.Namespace
            };
            sequence.Items.Add(item);
            type.Particle = sequence;
            XmlSchema schemaSerializable = data.GetSchemaSerializable();
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
            base.DataSetName = "ReceiptData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/ReceiptData.xsd";
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
        public class DataTable1DataTable : TypedTableBase<ReceiptData.DataTable1Row>
        {
            private DataColumn columnDescription;
            private DataColumn columnQuantity;
            private DataColumn columnUnitPrice;
            private DataColumn columnGross;

            [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public event ReceiptData.DataTable1RowChangeEventHandler DataTable1RowChanged;

            [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public event ReceiptData.DataTable1RowChangeEventHandler DataTable1RowChanging;

            [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public event ReceiptData.DataTable1RowChangeEventHandler DataTable1RowDeleted;

            [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public event ReceiptData.DataTable1RowChangeEventHandler DataTable1RowDeleting;

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
            public void AddDataTable1Row(ReceiptData.DataTable1Row row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public ReceiptData.DataTable1Row AddDataTable1Row(string Description, string Quantity, string UnitPrice, string Gross)
            {
                ReceiptData.DataTable1Row row = (ReceiptData.DataTable1Row) base.NewRow();
                row.ItemArray = new object[] { Description, Quantity, UnitPrice, Gross };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public override DataTable Clone()
            {
                ReceiptData.DataTable1DataTable table = (ReceiptData.DataTable1DataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override DataTable CreateInstance() => 
                new ReceiptData.DataTable1DataTable();

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override Type GetRowType() => 
                typeof(ReceiptData.DataTable1Row);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                ReceiptData data = new ReceiptData();
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
                    FixedValue = data.Namespace
                };
                type.Attributes.Add(attribute);
                XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                    Name = "tableTypeName",
                    FixedValue = "DataTable1DataTable"
                };
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                XmlSchema schemaSerializable = data.GetSchemaSerializable();
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
                this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDescription);
                this.columnQuantity = new DataColumn("Quantity", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnQuantity);
                this.columnUnitPrice = new DataColumn("UnitPrice", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnUnitPrice);
                this.columnGross = new DataColumn("Gross", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnGross);
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            internal void InitVars()
            {
                this.columnDescription = base.Columns["Description"];
                this.columnQuantity = base.Columns["Quantity"];
                this.columnUnitPrice = base.Columns["UnitPrice"];
                this.columnGross = base.Columns["Gross"];
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public ReceiptData.DataTable1Row NewDataTable1Row() => 
                (ReceiptData.DataTable1Row) base.NewRow();

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => 
                new ReceiptData.DataTable1Row(builder);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.DataTable1RowChanged != null)
                {
                    this.DataTable1RowChanged(this, new ReceiptData.DataTable1RowChangeEvent((ReceiptData.DataTable1Row) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.DataTable1RowChanging != null)
                {
                    this.DataTable1RowChanging(this, new ReceiptData.DataTable1RowChangeEvent((ReceiptData.DataTable1Row) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.DataTable1RowDeleted != null)
                {
                    this.DataTable1RowDeleted(this, new ReceiptData.DataTable1RowChangeEvent((ReceiptData.DataTable1Row) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.DataTable1RowDeleting != null)
                {
                    this.DataTable1RowDeleting(this, new ReceiptData.DataTable1RowChangeEvent((ReceiptData.DataTable1Row) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void RemoveDataTable1Row(ReceiptData.DataTable1Row row)
            {
                base.Rows.Remove(row);
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataColumn DescriptionColumn =>
                this.columnDescription;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataColumn QuantityColumn =>
                this.columnQuantity;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataColumn UnitPriceColumn =>
                this.columnUnitPrice;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataColumn GrossColumn =>
                this.columnGross;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0"), Browsable(false)]
            public int Count =>
                base.Rows.Count;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public ReceiptData.DataTable1Row this[int index] =>
                (ReceiptData.DataTable1Row) base.Rows[index];
        }

        public class DataTable1Row : DataRow
        {
            private ReceiptData.DataTable1DataTable tableDataTable1;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            internal DataTable1Row(DataRowBuilder rb) : base(rb)
            {
                this.tableDataTable1 = (ReceiptData.DataTable1DataTable) base.Table;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public bool IsDescriptionNull() => 
                base.IsNull(this.tableDataTable1.DescriptionColumn);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public bool IsGrossNull() => 
                base.IsNull(this.tableDataTable1.GrossColumn);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public bool IsQuantityNull() => 
                base.IsNull(this.tableDataTable1.QuantityColumn);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public bool IsUnitPriceNull() => 
                base.IsNull(this.tableDataTable1.UnitPriceColumn);

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void SetDescriptionNull()
            {
                base[this.tableDataTable1.DescriptionColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void SetGrossNull()
            {
                base[this.tableDataTable1.GrossColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void SetQuantityNull()
            {
                base[this.tableDataTable1.QuantityColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public void SetUnitPriceNull()
            {
                base[this.tableDataTable1.UnitPriceColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public string Description
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableDataTable1.DescriptionColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("The value for column 'Description' in table 'DataTable1' is DBNull.", exception);
                    }
                    return str;
                }
                set => 
                    base[this.tableDataTable1.DescriptionColumn] = value;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public string Quantity
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableDataTable1.QuantityColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("The value for column 'Quantity' in table 'DataTable1' is DBNull.", exception);
                    }
                    return str;
                }
                set => 
                    base[this.tableDataTable1.QuantityColumn] = value;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public string UnitPrice
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableDataTable1.UnitPriceColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("The value for column 'UnitPrice' in table 'DataTable1' is DBNull.", exception);
                    }
                    return str;
                }
                set => 
                    base[this.tableDataTable1.UnitPriceColumn] = value;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public string Gross
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableDataTable1.GrossColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("The value for column 'Gross' in table 'DataTable1' is DBNull.", exception);
                    }
                    return str;
                }
                set => 
                    base[this.tableDataTable1.GrossColumn] = value;
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        public class DataTable1RowChangeEvent : EventArgs
        {
            private ReceiptData.DataTable1Row eventRow;
            private DataRowAction eventAction;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataTable1RowChangeEvent(ReceiptData.DataTable1Row row, DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public ReceiptData.DataTable1Row Row =>
                this.eventRow;

            [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
            public DataRowAction Action =>
                this.eventAction;
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        public delegate void DataTable1RowChangeEventHandler(object sender, ReceiptData.DataTable1RowChangeEvent e);
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    }
}

