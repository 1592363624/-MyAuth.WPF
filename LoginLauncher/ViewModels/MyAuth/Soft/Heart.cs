namespace LoginLauncher.ViewModels.MyAuth.Soft
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Heart
    {
        [JsonProperty("data", Required = Required.Always)]
        public virtual Data Data { get; set; }

        [JsonProperty("sign", Required = Required.Always)]
        public virtual string Sign { get; set; }

        [JsonProperty("skey", Required = Required.Always)]
        public virtual string Skey { get; set; }

        [JsonProperty("vkey", Required = Required.Always)]
        public virtual string Vkey { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("device_code", Required = Required.Always)]
        public virtual string DeviceCode { get; set; }

        [JsonProperty("device_info", Required = Required.Always)]
        public virtual string DeviceInfo { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public virtual string Timestamp { get; set; }

        [JsonProperty("token", Required = Required.Always)]
        public virtual string Token { get; set; }
    }
}