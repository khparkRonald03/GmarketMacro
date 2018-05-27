using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ExcelControl
{
    [Serializable]
    [DataContract]
    public class ExcelDataModel
{
        [Display(Name = "Identity")]
        [DataMember]
        public long? Identity { get; set; }

        [Display(Name = "구분")]
        [DataMember]
        public string Category { get; set; }

        [Display(Name = "이름")]
        [DataMember]
        public string Name { get; set; }

        [Display(Name = "환경 설정 값")]
        [DataMember]
        public string Value { get; set; }

        [Display(Name = "설명")]
        [DataMember]
        public string Description { get; set; }
    }
}
