using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;

namespace X_GpsSample
{
    public class GetAddress
    {
        public async Task<string> GetJsonAsync(double lat, double lon)
        {
            Uri uri = new Uri(string.Format(
                "http://maps.google.com/maps/api/geocode/json?address=" + lat + "%2C" + lon + "&sensor=false&language=ja"));

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode(); // StatusCode が 200 以外なら Exception

                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var streamReader = new StreamReader(stream))
                        {
                            var json = await streamReader.ReadToEndAsync();
                            var res = JsonConvert.DeserializeObject<RootObject>(json);

                            // Latitude, Longitude を投げると results の最初のフルアドレスが返ってくるようなので決め打ちですｗ
                            return res.results[0].formatted_address;
                        }
                    }
                }
            }
            catch (Exception e)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine("***Error***: {0}\n{1}\n{2}", e.Source, e.Message, e.InnerException);
#endif
                return null;
                //res = null;
            }
        }
    }
}

