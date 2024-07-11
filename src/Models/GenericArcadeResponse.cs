using HackClub.Arcade.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackClub.Arcade.Models
{
    internal class GenericArcadeResponse
    {
        public bool OK { get; private set; }

        public string? Error { get; private set; }

        public JObject? Data { get; private set; }

        public JArray? ArrayData { get; private set; }

        private GenericArcadeResponse(bool ok, JObject? data)
        {
            this.OK = ok;

            this.Data = data!;
        }

        private GenericArcadeResponse(bool ok, JArray? data)
        {
            this.OK = ok;

            this.ArrayData = data!;
        }

        private GenericArcadeResponse(string error)
        {
            OK = false;
            this.Error = error;
        }

        public static GenericArcadeResponse? FromJSON(string jsonstring, bool dataArray = false)
        {
            try
            {
                JObject json = JObject.Parse(jsonstring);

                bool? ok = json["ok"]?.ToObject<bool>();

                JToken? dataToken = json["data"];



                if (ok == null) return null;
                if(ok == false)
                {
                    string? error = json["error"]?.ToObject<string>();
                    if (error == null) return null;
                    return new GenericArcadeResponse(error);
                }

                if (dataToken == null) return null;

                if (dataArray)
                return new GenericArcadeResponse(ok.Value, dataToken.ToObject<JArray>());
                else
                return new GenericArcadeResponse(ok.Value, dataToken.ToObject<JObject>());
            }
            catch (Exception)
            {
                return null;
            }
        }


        public T GetData<T>(bool asArray = false) where T : new()
        {
            T? data = asArray ? ArrayData!.ToObject<T>() : Data!.ToObject<T>();

            if(data == null) throw new ArcadeHTTPException("Failed to parse response from Arcade API");
            return data;
        }
    }
}
