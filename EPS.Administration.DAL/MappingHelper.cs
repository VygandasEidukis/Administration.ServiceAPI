using System.Linq;
using System.Reflection;

namespace EPS.Administration.DAL
{
    public static class MappingHelper<TEntity> where TEntity : class, new()
    {
        public static TEntity Convert(object model)
        {
            if(model == null)
            {
                return null;
            }

            TEntity entity = new TEntity();
            var entityType = entity.GetType();
            var objectProperties = model.GetType().GetProperties();
            var entityProperties = entityType.GetProperties();
            
            if(!objectProperties.Any() || !entityProperties.Any())
            {
                return null;
            }

            foreach (PropertyInfo property in objectProperties)
            {
                if (entityProperties.Any(x => x.Name == property.Name))
                {
                    var value = property.GetValue(model);
                    entityType.GetProperty(property.Name).SetValue(entity, value);
                }
            }

            return entity;
        }
    }
}
