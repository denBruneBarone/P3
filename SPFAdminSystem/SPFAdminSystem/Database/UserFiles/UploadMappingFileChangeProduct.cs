using System.ComponentModel.DataAnnotations;

namespace SPFAdminSystem.Database.UserFiles
{
    public class UploadMappingFileChangeProduct
    {
        [Required]
        [Key]
        public int FileChangeProductId { get; set; }
        public Product Product { get; set; }
        public UserAction UserAction { get; set; }
        //public UploadFileChangeProduct(int fileChangeProductId, Product product, UserAction userAction)
        //{
        //    FileChangeProductId = fileChangeProductId;
        //    Product = product;
        //    UserAction = userAction;
        //}
    }
}
