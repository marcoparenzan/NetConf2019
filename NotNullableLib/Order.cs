namespace NonNullableLib
{
    #region Order
    public class Order
    {
        public int Id { get; set; }
        public string TrackingNumber { get; set; }
        //public string PartitionKey { get; set; }
        public StreetAddress ShippingAddress { get; set; }
    }
    #endregion
}
