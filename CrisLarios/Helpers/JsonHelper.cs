using System;
using System.IO;
using Newtonsoft.Json;

namespace CrisLarios.Helpers
{
    public static class JsonHelper
    {
        public static rootClass desserializar(string path)
        {
            try
            {
                rootClass root = null;
            
                using (StreamReader jsonStream = File.OpenText(path))
                {
                    var json = jsonStream.ReadToEnd();
                    root = JsonConvert.DeserializeObject<rootClass>(json);
                }

                return root;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Error al momento de Deserializar");
                return null;
            }
        }

        public static void serializar(string path, rootClass root)
        {
            try
            {
                string json = JsonConvert.SerializeObject(root);
                File.WriteAllText(path, json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Error al momento de serializar");
            }
        }
    }
}