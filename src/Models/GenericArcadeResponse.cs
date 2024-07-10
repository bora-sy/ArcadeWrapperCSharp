using HackclubArcadeAPIWrapper.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackclubArcadeAPIWrapper.Models
{
    internal class GenericArcadeResponse
    {
        public JObject Json { get; private set; }

        public bool OK { get; private set; }

        public JObject Data { get; private set; }

        private GenericArcadeResponse(JObject json, bool ok, JObject data)
        {
            this.Json = json;
            this.OK = ok;
            this.Data = data;
        }

        public static GenericArcadeResponse? FromJSON(string jsonstring)
        {
            try
            {
                JObject json = JObject.Parse(jsonstring);

                bool? ok = json["ok"]?.ToObject<bool>();
                JObject? data = json["data"]?.ToObject<JObject>();

                if (ok == null) return null;

                return new GenericArcadeResponse(json, ok.Value, data);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public T GetData<T>() where T : new()
        {
            T? data = Data.ToObject<T>();
            return data;
        }
    }
}
