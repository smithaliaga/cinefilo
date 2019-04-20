namespace cinefilo.Models
{
    using Newtonsoft.Json;

    public class UserAuthenticate : EntityWSBase
    {
        #region Properties
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "userImage")]
        public string UserImage { get; set; }
        #endregion
    }
}
