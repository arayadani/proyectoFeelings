namespace proyectoFeelings.Models
{
    public class Store
    {
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<User> Users { get; set; }


    }
}
