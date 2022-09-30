using JWT;
using JWT.Serializers;
using Newtonsoft.Json;

namespace SuperPastel.Nucleo.Ajudantes
{
    public class TokenHelper
    {
        public static dynamic Deserialize(string token, string key)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

            var json = decoder.Decode(token.Trim(), key, verify: true);
            return JsonConvert.DeserializeObject(json);
        }
    }
}
