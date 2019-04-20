namespace cinefilo.Models.ws
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class GetListMovie : EntityWSBase
    {
        [JsonProperty(PropertyName = "lista")]
        public List<Movie> ListMovie;

        public class Movie
        {
            [JsonProperty(PropertyName = "codigoPelicula")]
            public long Code { get; set; }
            [JsonProperty(PropertyName = "nombre")]
            public string Name { get; set; }
            [JsonProperty(PropertyName = "descripcion")]
            public string Description { get; set; }
            [JsonProperty(PropertyName = "image")]
            public string Image { get; set; }
            [JsonProperty(PropertyName = "video")]
            public string URL { get; set; }

            public string DescriptionResume
            {
                get
                {
                    if (Description.Length > 250)
                    {
                        return Description.Substring(0, 250) + " ...";
                    }
                    else
                    {
                        return Description;
                    }
                } 
            }
        }
    }
}
