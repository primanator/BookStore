namespace DTO_EF.Entities
{
    public class User : Entity
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public int Age { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public int LibraryId { get; set; }
        public virtual Library Library { get; set; }
    }
}
