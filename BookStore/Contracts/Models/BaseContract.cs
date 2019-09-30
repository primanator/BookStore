namespace Contracts.Models
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;
    using System.Text;

    public abstract class BaseContract
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private static readonly Dictionary<Type, PropertyInfo[]> TypeProperties;

        static BaseContract()
        {
            TypeProperties = new Dictionary<Type, PropertyInfo[]>();

            var entityTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(BaseContract).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .ToList();

            foreach (var entityType in entityTypes)
            {
                TypeProperties.Add(entityType, entityType.GetProperties());
            }
        }

        public void SelfUpdate<T>(T toUpdateWith) where T : BaseContract
        {
            if (!TypeProperties.TryGetValue(GetType(), out var cachedProperties))
                return;

            foreach (var property in cachedProperties)
            {
                var newValue = property.GetValue(toUpdateWith);
                if (property.GetValue(this) != newValue)
                    property.SetValue(this, newValue);
            }
        }

        public static T GetFromUser<T>() where T : BaseContract, new()
        {
            if (!TypeProperties.TryGetValue(typeof(T), out var cachedProperties))
                throw new ArgumentException("Type argument can't be used to create Dto from user input. Please use Dto inherited types.");

            var newDto = new T();
            foreach (var prop in cachedProperties)
            {
                if (!prop.Name.Contains("Id"))
                {
                    Console.WriteLine("Please enter {0}", prop.Name);
                    var userValue = Console.ReadLine();
                    if (!string.IsNullOrEmpty(userValue))
                        prop.SetValue(newDto, Convert.ChangeType(userValue, prop.PropertyType));
                }
            }

            return newDto;
        }

        public void EditByUser()
        {
            if (!TypeProperties.TryGetValue(GetType(), out var cachedProperties))
                return;

            foreach (var prop in cachedProperties)
            {
                if (prop.Name != "Id" && prop.Name != "Name" && prop.Name != "LibraryId")
                {
                    Console.WriteLine("Please enter book's {0}", prop.Name);
                    var userValue = Console.ReadLine();
                    if (!string.IsNullOrEmpty(userValue))
                        prop.SetValue(this, Convert.ChangeType(userValue, prop.PropertyType));
                }
            }
        }

        public override string ToString()
        {
            var stateBuilder = new StringBuilder();

            foreach (var property in TypeProperties[GetType()])
            {
                stateBuilder.AppendLine($"{property.Name} - {property.GetValue(this)}\n");
            }

            return stateBuilder.ToString();
        }
    }
}