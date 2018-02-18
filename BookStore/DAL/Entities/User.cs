namespace DAL.Entities
{
    public class User : Entity
    {
        public string FullName { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public int Age { get; set; }

        public virtual int? CountryId { get; set; }
        public virtual Country Country { get; set; } 

        public virtual int? LibraryId { get; set; }
        public virtual Library Library { get; set; }
    }
}