using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SiteCatch.Models
{
    public class EngineModel
    {

            [Required]
            [Display(Name = "搜索引擎")]
            public int Engine { get; set; }
            [Required]
            [Display(Name = "站点URL")]
            public string Url { get; set; }           
    }
}