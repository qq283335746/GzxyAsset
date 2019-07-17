using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "AssetInStoreModel")]
    public class AssetInStoreModel
    {
        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }
    }
}
