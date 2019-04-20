namespace cinefilo.Models.ws
{
    using Newtonsoft.Json;

    public class ProcessQR : EntityWSBase
    {
        #region Properties
        [JsonProperty(PropertyName = "Token")]
        public double? AmountDiscount { get; set; }

        [JsonProperty(PropertyName = "CurrencyCode")]
        public string CurrencyCode { get; set; }
        #endregion
    }
}
