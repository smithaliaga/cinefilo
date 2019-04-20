namespace cinefilo.Models.ws
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class GetListTrivia : EntityWSBase
    {
        [JsonProperty(PropertyName = "codigoTrivia")]
        public long? codigoTrivia { get; set; }
        [JsonProperty(PropertyName = "preguntas")]
        public List<TriviaPregunta> preguntas;
    }

    public class TriviaPregunta
    {
        [JsonProperty(PropertyName = "codigoTriviaPregunta")]
        public long codigoTriviaPregunta { get; set; }
        [JsonProperty(PropertyName = "pregunta")]
        public string pregunta { get; set; }
        [JsonProperty(PropertyName = "respuestas")]
        public List<TriviaRespuesta> respuestas;
    }

    public class TriviaRespuesta
    {
        [JsonProperty(PropertyName = "codigoTriviaRespuesta")]
        public long codigoTriviaRespuesta { get; set; }
        [JsonProperty(PropertyName = "respuesta")]
        public string respuesta { get; set; }
        [JsonProperty(PropertyName = "estadoRespuesta")]
        public bool estadoRespuesta { get; set; }
        public bool estadoSeleccion { get; set; }
    }
}
