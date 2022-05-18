namespace LaxPos.Inventory
{
    using System;
    using System.Runtime.CompilerServices;

    public class SttItem
    {
        public string SttNumber { get; set; }

        public string Itemcode { get; set; }

        public string ProductName { get; set; }

        public int Expected { get; set; }

        public int Counted { get; set; }

        public string CountStatus { get; set; }

        public DateTime DateCounted { get; set; }
    }
}

