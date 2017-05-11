using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WindowsFormsApplication3
{
    [Serializable]
    [XmlRoot(ElementName = "ApiKeys")]
    public class ApiKeys
    {
        public string Key { set; get; }
        public string IP { set; get; }
    }
}
