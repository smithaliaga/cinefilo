namespace cinefilo.Models
{
    using Newtonsoft.Json;
    using System;

    public class EntityWSBase
    {
        [JsonProperty(PropertyName = "errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty(PropertyName = "errorMessage")]
        public String ErrorMessage { get; set; }
    }
}
