using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CurrencyParserConsole.Services
{
    class JsonStateConverter
    {
        public string To<T>(T value)
        {
            if (value != null)
            {
                try
                {
                    return JsonConvert.SerializeObject(value);
                }
                catch (Exception ex)
                {
                    throw new JsonSerializationException(ex.Message);
                }
                
            }
            else
            {
                return string.Empty;
            }
        }

        public T From<T>(string jsonData)
        {
            if (jsonData != null)
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(jsonData);
                }
                catch (Exception ex)
                {
                    throw new JsonSerializationException(ex.Message);
                }

            }
            else
            {
                return default(T); ;
            }
        }

        public List<T> FromRange<T>(string allJsonData)
        {
            List<T> list = new List<T>();
            if (allJsonData != null) 
            {
                try
                {
                    foreach (string item in allJsonData.Split('}'))
                    {
                        if (item.Length>2)
                        {
                            list.Add(JsonConvert.DeserializeObject<T>(item.Remove(0, 1) + "}"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new JsonSerializationException(ex.Message);
                }

            }
                return list;
        }
    }
}
