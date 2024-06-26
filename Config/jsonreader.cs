using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatoriaModule.Config
{
    public static class jsonreader
    {
        public static datacfg GetCfg()
        {
            string cfgPath = "creatoriamodule.json";
            string jsonData;
            if (File.Exists(cfgPath))
            {
                jsonData = File.ReadAllText(cfgPath);
                return JsonConvert.DeserializeObject<datacfg>(jsonData);
            }
            else
            {
                var newcfg = new datacfg(true, true, true, true, true);
                jsonData = JsonConvert.SerializeObject(newcfg);
                File.WriteAllText(cfgPath, jsonData);
                return newcfg;
            }
        }
    }
}
