using System;

namespace DTO.Entities
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", GetType().Name, Id);
        }

        public void SelfUpdate<T>(T toUpdateWith) where T : Entity
        {
            var ownType = this.GetType();

            foreach (var property in ownType.GetProperties())
            {
                var newValue = ownType.GetProperty(property.Name)?.GetValue(toUpdateWith);
                if (property.GetValue(this) != newValue)
                    property.SetValue(this, newValue);
            }
        }
    }
}