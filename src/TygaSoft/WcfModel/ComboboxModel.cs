using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "ComboboxModel")]
    public class ComboboxModel
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}
