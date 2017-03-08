using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WindowsFormsApplication1
{
    public class sdnProto
    {
        //[XmlRoot(ElementName = "programList")]
        public class ProgramList
        {
            //[XmlElement(ElementName = "program")]
            public string Program { get; set; }
        }

        //[XmlRoot(ElementName = "id")]
        public class Id
        {
            //[XmlElement(ElementName = "uid")]
            public string Uid { get; set; }
            //[XmlElement(ElementName = "idType")]
            public string IdType { get; set; }
            //[XmlElement(ElementName = "idNumber")]
            public string IdNumber { get; set; }
            //[XmlElement(ElementName = "idCountry")]
            public string IdCountry { get; set; }
        }

        //[XmlRoot(ElementName = "idList")]
        public class IdList
        {
            //[XmlElement(ElementName = "id")]
            public Id Id { get; set; }
        }

        //[XmlRoot(ElementName = "address")]
        public class Address
        {
            //[XmlElement(ElementName = "uid")]
            public string Uid { get; set; }
            //[XmlElement(ElementName = "address1")]
            public string Address1 { get; set; }
            //[XmlElement(ElementName = "address2")]
            public string Address2 { get; set; }
            //[XmlElement(ElementName = "address3")]
            public string Address3 { get; set; }
            //[XmlElement(ElementName = "city")]
            public string City { get; set; }
            //[XmlElement(ElementName = "stateOrProvince")]
            public string StateOrProvince { get; set; }
            //[XmlElement(ElementName = "postalCode")]
            public string PostalCode { get; set; }
            //[XmlElement(ElementName = "country")]
            public string Country { get; set; }
        }

        //[XmlRoot(ElementName = "addressList")]
        public class AddressList
        {
            //[XmlElement(ElementName = "address")]
            public Address Address { get; set; }
        }

        //[XmlRoot(ElementName = "sdnEntry")]
        public class SdnEntry
        {
            //[XmlElement(ElementName = "uid")]
            public int Uid { get; set; }
            //[XmlElement(ElementName = "lastName")]
            public string LastName { get; set; }
            //[XmlElement(ElementName = "sdnType")]
            public string SdnType { get; set; }
           // [XmlElement(ElementName = "programList")]
            public ProgramList ProgramList { get; set; }
           // [XmlElement(ElementName = "idList")]
            public IdList IdList { get; set; }
          //  [XmlElement(ElementName = "addressList")]
            public AddressList AddressList { get; set; }
        }

    }


}


