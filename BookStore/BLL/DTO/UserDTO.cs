namespace BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public int Age { get; set; }

        public int CountryId { get; set; }
        public CountryDTO Country { get; set; }

        public int LibraryId { get; set; }
        public LibraryDTO Library { get; set; }
    }
}