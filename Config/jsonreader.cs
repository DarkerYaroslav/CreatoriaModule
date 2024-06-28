using Newtonsoft.Json;
using System.IO;

namespace CreatoriaModule.Config
{
    public static class JsonReader
    {
        public static JsonConfiguration GetCfg()
        {
            string cfgPath = "configuration.json";
            string jsonData;
            if (File.Exists(cfgPath))
            {
                jsonData = File.ReadAllText(cfgPath);
                return JsonConvert.DeserializeObject<JsonConfiguration>(jsonData);
            }

            var newConfig = new JsonConfiguration(
                goldPatch: true, 
                grenadePatch: true, 
                markerPatch: true, 
                nicknamePatch: true, 
                voicePatch: true);

            jsonData = JsonConvert.SerializeObject(newConfig);
            File.WriteAllText(cfgPath, jsonData);
            return newConfig;
        }
    }
}
