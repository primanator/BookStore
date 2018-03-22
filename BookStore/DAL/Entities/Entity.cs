namespace DAL.Entities
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", GetType().Name, Id);
        }
    }
}