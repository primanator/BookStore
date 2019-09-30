namespace Contracts.Models
{
    public class User : BaseContract
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public int Age { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; } 
        public int LibraryId { get; set; }
        public Library Library { get; set; }
    }
}