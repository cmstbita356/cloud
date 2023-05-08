namespace Cloud.Models
{
    public class TruckItemView
    {
        public string item_name { get; set; }
        public int item_quantity { get; set; }

        public TruckItemView(string item_name, int item_quantity)
        {
            this.item_name = item_name;
            this.item_quantity = item_quantity;
        }
        public TruckItemView() { }
    }
}
