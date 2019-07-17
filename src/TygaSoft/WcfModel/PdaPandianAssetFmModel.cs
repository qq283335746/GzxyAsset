using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "PdaPandianAssetFmModel")]
    public class PdaPandianAssetFmModel
    {
        [DataMember]
        public object PandianId { get; set; }

        [DataMember]
        public List<PdaPandianAssetItemModel> ItemList { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string AppKey { get; set; }
    }
}
