namespace DTO.Entities
{
    using System.Reflection;

    public abstract class EntityDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", GetType().Name, Id);
        }

        private PropertyInfo[] _cashedProperties;

        public void SelfUpdate<T>(T toUpdateWith) where T : EntityDto
        {
            if (_cashedProperties == null)
            {
                _cashedProperties = GetType().GetProperties();
            }

            foreach (var property in _cashedProperties)
            {
                var newValue = property.GetValue(toUpdateWith);
                if (property.GetValue(this) != newValue)
                    property.SetValue(this, newValue);
            }
        }
    }
}