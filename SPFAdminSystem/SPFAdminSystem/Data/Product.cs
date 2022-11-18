namespace SPFAdminSystem.Data
{
    public class Product
    {
        public ulong productID { get; set; }
        public ulong barcode { get; set; }
        public string inHouseTitle { get; set; }
        public string GWStitle { get; set; }
        public int group { get; set; }
        public int inStock { get; set; }
        public int inOrder { get; set; }
        public int available { get; set; }
        public int ordered { get; set; }
        public int target { get; set; }
        public int packSize { get; set; }
        public int minOrder { get; set; }
       
    }
    
}
