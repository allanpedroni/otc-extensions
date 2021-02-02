using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration.Json;

namespace Otc.Extensions.Configuration
{
    internal class ObjectConfigurationProvider : JsonConfigurationProvider
    {
        private readonly object configurationObject;

        public ObjectConfigurationProvider(JsonConfigurationSource source, object configurationObject)
            : base(source)
        {
            this.configurationObject = configurationObject ??
                throw new ArgumentNullException(nameof(configurationObject));
        }

        public override void Load()
        {
            var serializedObject = JsonSerializer.Serialize(configurationObject);
            var byteArray = Encoding.ASCII.GetBytes(serializedObject);
            using var memoryStream = new MemoryStream(byteArray);
            Load(memoryStream);
        }
    }
}
