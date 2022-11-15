using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models
{
    public class Product
    {
        public DateTime ArriveDate { get; set; }

        public DateTime RemovedFromStockDate { get; set; }

        public int InHouseProductNumber { get; set; }

        public string InHouseTitle{ get; set; }

        public string TitleGWS { get; set; }

        public double OrderPrice { get; set; }

        public int InStock { get; set; }

        public int InOrder { get; set; }

        public int Available { get; set; }

        public int Ordered { get; set; }

        public int Barcode { get; set; }

        public int PackSize { get; set; }

        public int Target { get; set; }

        public int MinOrder { get; set; }

        public int OrderQuantity { get; set; }
    }
}
