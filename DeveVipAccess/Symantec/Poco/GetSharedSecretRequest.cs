using System.Xml.Serialization;

namespace DeveVipAccess.Symantec.Poco
{
    [XmlRoot(ElementName = "OtpAlgorithm", Namespace = "http://www.verisign.com/2006/08/vipservice")]
    public class OtpAlgorithm
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }

    [XmlRoot(ElementName = "DeviceId", Namespace = "http://www.verisign.com/2006/08/vipservice")]
    public class DeviceId
    {
        [XmlElement(ElementName = "Manufacturer", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string Manufacturer { get; set; }
        [XmlElement(ElementName = "SerialNo", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string SerialNo { get; set; }
        [XmlElement(ElementName = "Model", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string Model { get; set; }
    }

    [XmlRoot(ElementName = "ClientInfo", Namespace = "http://www.verisign.com/2006/08/vipservice")]
    public class ClientInfo
    {
        [XmlElement(ElementName = "os", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string Os { get; set; }
        [XmlElement(ElementName = "platform", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string Platform { get; set; }
    }

    [XmlRoot(ElementName = "Extension", Namespace = "http://www.verisign.com/2006/08/vipservice")]
    public class Extension
    {
        [XmlElement(ElementName = "AppHandle", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string AppHandle { get; set; }
        [XmlElement(ElementName = "ClientIDType", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string ClientIDType { get; set; }
        [XmlElement(ElementName = "ClientID", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string ClientID { get; set; }
        [XmlElement(ElementName = "DistChannel", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string DistChannel { get; set; }
        [XmlElement(ElementName = "ClientInfo", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public ClientInfo ClientInfo { get; set; }
        [XmlElement(ElementName = "ClientTimestamp", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string ClientTimestamp { get; set; }
        [XmlElement(ElementName = "Data", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string Data { get; set; }
        [XmlAttribute(AttributeName = "extVersion")]
        public string ExtVersion { get; set; } = "auth";
        [XmlAttribute(AttributeName = "type", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string Type { get; set; } = "vip:ProvisionInfoType";
        [XmlAttribute(AttributeName = "vip", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Vip { get; set; } = "http://www.verisign.com/2006/08/vipservice";
    }

    [XmlRoot(ElementName = "GetSharedSecret", Namespace = "http://www.verisign.com/2006/08/vipservice")]
    public class GetSharedSecretRequest
    {
        [XmlElement(ElementName = "TokenModel", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string TokenModel { get; set; }
        [XmlElement(ElementName = "ActivationCode", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string ActivationCode { get; set; }
        [XmlElement(ElementName = "OtpAlgorithm", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public OtpAlgorithm OtpAlgorithm { get; set; }
        [XmlElement(ElementName = "SharedSecretDeliveryMethod", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string SharedSecretDeliveryMethod { get; set; }
        [XmlElement(ElementName = "DeviceId", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public DeviceId DeviceId { get; set; }
        [XmlElement(ElementName = "Extension", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public Extension Extension { get; set; }
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "Version")]
        public string Version { get; set; } = "2.0";
    }
}
