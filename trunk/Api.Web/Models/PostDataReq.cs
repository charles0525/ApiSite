using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Api.Web.Models
{
    [MetadataType(typeof(PostDataReqMeta))]
    public class PostDataReq : BaseReq
    {
        public string param1 { get; set; }
        public string param2 { get; set; }
        public string param3 { get; set; }
        public string sign { get; set; }
    }

    public class PostDataReqMeta
    {
        [Display(Name = "参数1")]
        [Required(ErrorMessage = "{0}不能为空")]
        [RegularExpression(@"\w+", ErrorMessage = "仅限字母,数字,下划线组合")]
        public string param1 { get; set; }
        [Display(Name = "参数2")]
        [Required(ErrorMessage = "{0}不能为空")]
        [RegularExpression(@"\w+", ErrorMessage = "仅限字母,数字,下划线组合")]
        public string param2 { get; set; }
        [Display(Name = "参数3")]
        [Required(ErrorMessage = "{0}不能为空")]
        [RegularExpression(@"\w+", ErrorMessage = "仅限字母,数字,下划线组合")]
        public string param3 { get; set; }
        [Display(Name = "sign")]
        [Required(ErrorMessage = "{0}不能为空")]
        [RegularExpression(@"\w+", ErrorMessage = "仅限字母,数字,下划线组合")]
        public string sign { get; set; }
    }
}