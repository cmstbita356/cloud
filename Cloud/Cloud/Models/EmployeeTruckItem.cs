namespace Cloud.Models
{
    public class EmployeeTruckItem
    {
        public TblEmployee Employee { get; set; }
        public TblTruck Truck { get; set; }
        public List<TruckItemView> ListItem { get; set; }

        public EmployeeTruckItem(TblEmployee Employee, TblTruck Truck,  List<TruckItemView> ListItem)
        {
            this.Employee = Employee;
            this.Truck = Truck;
            this.ListItem = ListItem;
        }
        public EmployeeTruckItem() { }
    }
}
