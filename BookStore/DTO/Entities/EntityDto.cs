namespace DTO.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;

    public abstract class EntityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private static readonly Dictionary<Type, PropertyInfo[]> TypeProperties;

        static EntityDto()
        {
            TypeProperties = new Dictionary<Type, PropertyInfo[]>();

            var entityTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(EntityDto).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .ToList();

            foreach (var entityType in entityTypes)
            {
                TypeProperties.Add(entityType, entityType.GetProperties());
            }
        }

        public void SelfUpdate<T>(T toUpdateWith) where T : EntityDto
        {
            if (!TypeProperties.TryGetValue(this.GetType(), out var cachedProperties))
                return;

            foreach (var property in cachedProperties)
            {
                var newValue = property.GetValue(toUpdateWith);
                if (property.GetValue(this) != newValue)
                    property.SetValue(this, newValue);
            }
        }

        public override string ToString()
        {
            return $"{GetType().Name} - {Id}";
        }
    }
}