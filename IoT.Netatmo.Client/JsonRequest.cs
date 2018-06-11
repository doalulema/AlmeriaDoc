using System.Runtime.Serialization;

namespace IoT.Netatmo.Client
{
    [DataContract]
    public class JsonRequest
    {
        [DataMember(Name = "value")]
        public float Value { get; set; }
        [DataMember(Name = "sourceId")]
        public int SourceId { get; set; }
        [DataMember(Name = "sourceDescription")]
        public string SourceDescription { get; set; }
        [DataMember(Name = "dataType")]
        public string DataType { get; set; }
        [DataMember(Name = "direction")]
        public string Direction { get; set; }
    }
}