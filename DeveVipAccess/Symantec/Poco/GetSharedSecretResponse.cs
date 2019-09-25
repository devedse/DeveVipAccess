using System.Xml.Serialization;

namespace DeveVipAccess.Symantec.Poco
{
    [XmlRoot(ElementName = "Status", Namespace = "http://www.verisign.com/2006/08/vipservice")]
    public class Status
    {
        [XmlElement(ElementName = "ReasonCode", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string ReasonCode { get; set; }
        [XmlElement(ElementName = "StatusMessage", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string StatusMessage { get; set; }
    }

    [XmlRoot(ElementName = "EncryptionMethod", Namespace = "http://www.verisign.com/2006/08/vipservice")]
    public class EncryptionMethod
    {
        [XmlElement(ElementName = "PBESalt", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string PBESalt { get; set; }
        [XmlElement(ElementName = "PBEIterationCount", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string PBEIterationCount { get; set; }
        [XmlElement(ElementName = "IV", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string IV { get; set; }
    }

    [XmlRoot(ElementName = "AI", Namespace = "http://www.verisign.com/2006/08/vipservice")]
    public class AI
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }

    [XmlRoot(ElementName = "Usage", Namespace = "http://www.verisign.com/2006/08/vipservice")]
    public class Usage
    {
        [XmlElement(ElementName = "AI", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public AI AI { get; set; }
        [XmlElement(ElementName = "TimeStep", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string TimeStep { get; set; }
        [XmlElement(ElementName = "Time", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string Time { get; set; }
        [XmlElement(ElementName = "ClockDrift", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string ClockDrift { get; set; }
        [XmlAttribute(AttributeName = "otp")]
        public string Otp { get; set; }
    }

    [XmlRoot(ElementName = "Digest", Namespace = "http://www.verisign.com/2006/08/vipservice")]
    public class Digest
    {
        [XmlAttribute(AttributeName = "algorithm")]
        public string Algorithm { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Data", Namespace = "http://www.verisign.com/2006/08/vipservice")]
    public class Data
    {
        [XmlElement(ElementName = "Cipher", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string Cipher { get; set; }
        [XmlElement(ElementName = "Digest", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public Digest Digest { get; set; }
    }

    [XmlRoot(ElementName = "Secret", Namespace = "http://www.verisign.com/2006/08/vipservice")]
    public class Secret
    {
        [XmlElement(ElementName = "Issuer", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string Issuer { get; set; }
        [XmlElement(ElementName = "Usage", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public Usage Usage { get; set; }
        [XmlElement(ElementName = "FriendlyName", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string FriendlyName { get; set; }
        [XmlElement(ElementName = "Data", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public Data Data { get; set; }
        [XmlElement(ElementName = "Expiry", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string Expiry { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "Device", Namespace = "http://www.verisign.com/2006/08/vipservice")]
    public class Device
    {
        [XmlElement(ElementName = "Secret", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public Secret Secret { get; set; }
    }

    [XmlRoot(ElementName = "SecretContainer", Namespace = "http://www.verisign.com/2006/08/vipservice")]
    public class SecretContainer
    {
        [XmlElement(ElementName = "EncryptionMethod", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public EncryptionMethod EncryptionMethod { get; set; }
        [XmlElement(ElementName = "Device", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public Device Device { get; set; }
        [XmlAttribute(AttributeName = "Version")]
        public string Version { get; set; }
    }

    [XmlRoot(ElementName = "GetSharedSecretResponse", Namespace = "http://www.verisign.com/2006/08/vipservice")]
    public class GetSharedSecretResponse
    {
        [XmlElement(ElementName = "Status", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public Status Status { get; set; }
        [XmlElement(ElementName = "SharedSecretDeliveryMethod", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string SharedSecretDeliveryMethod { get; set; }
        [XmlElement(ElementName = "SecretContainer", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public SecretContainer SecretContainer { get; set; }
        [XmlElement(ElementName = "UTCTimestamp", Namespace = "http://www.verisign.com/2006/08/vipservice")]
        public string UTCTimestamp { get; set; }
        [XmlAttribute(AttributeName = "RequestId")]
        public string RequestId { get; set; }
        [XmlAttribute(AttributeName = "Version")]
        public string Version { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }
}
