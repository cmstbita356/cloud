namespace Cloud.Models
{
    public class Company
    {
        public string comp_id { get; set; }
        public string comp_name { get; set; }
        public string comp_email { get; set; }
        public string comp_phone { get; set; }
        public string comp_password { get; set; }
        public string status { get; set; }

        public Company(string comp_id, string comp_name, string comp_email, string comp_phone, string comp_password, string status)
        {
            this.comp_id = comp_id;
            this.comp_name = comp_name;
            this.comp_email = comp_email;
            this.comp_phone = comp_phone;
            this.comp_password = comp_password;
            this.status = status;
        }
        public Company() { }
    }
}
