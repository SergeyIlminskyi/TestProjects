using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SWAG.Data
{
    public static class EntityExtensions
    {
        public static Byte[] ToByteArray<TEntity>(this TEntity entry)
            where TEntity : class, IEntity
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, entry);
                ms.Seek(0, SeekOrigin.Begin);
                return ms.ToArray();
            }
        }

        public static Byte[] ToByteArray<TEntity>(this TEntity[] entries)
            where TEntity : class, IEntity
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, entries);
                ms.Seek(0, SeekOrigin.Begin);
                return ms.ToArray();
            }
        }

        public static TEntity GetEntities<TEntity>(Byte[] data)
            where TEntity : class, IEnumerable<IEntity>
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(data, 0, data.Length);
                ms.Seek(0, SeekOrigin.Begin);
                return (TEntity)new BinaryFormatter().Deserialize(ms);
            }
        }

        public static TEntity GetEntity<TEntity>(Byte[] data)
            where TEntity : class, IEntity
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(data, 0, data.Length);
                ms.Seek(0, SeekOrigin.Begin);
                return (TEntity)new BinaryFormatter().Deserialize(ms);
            }
        }

        public static Boolean Exists(this DatabaseFacade source)
        {
            if (source == null)
            {
                return false;
            }

            return source.GetService<IRelationalDatabaseCreator>()?.Exists() ?? false;
        }
    }
}
