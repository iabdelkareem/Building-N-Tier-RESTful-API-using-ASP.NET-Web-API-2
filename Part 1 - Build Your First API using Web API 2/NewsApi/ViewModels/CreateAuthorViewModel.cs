using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsApi.ViewModels
{
    public class CreateAuthorViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}