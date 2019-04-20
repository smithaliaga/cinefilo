using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace cinefilo.Models.ws
{
    public class RegisterIntentTrivia : EntityWSBase
    {
        [JsonProperty(PropertyName = "codigoTriviaUsuario")]
        public long? codigoTriviaUsuario { get; set; }
        [JsonProperty(PropertyName = "estadoRespuesta")]
        public bool estadoRespuesta { get; set; }
        [JsonProperty(PropertyName = "estadoCobro")]
        public bool estadoCobro { get; set; }
    }
}
