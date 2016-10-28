#region "Directives"

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

#endregion

namespace CSDropdownListMVC4.Models
{
    /// <summary>
    /// This model is used to bind the CascadingDropDownSample Controller's Index action
    /// </summary>
    public class CascadingDropDownSampleModel
    {
        /// <summary>
        /// Dictionary holding the sample Makes values
        /// </summary>
        ///   
        [Required]
        public IDictionary<string, string> Makes { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Please Upload CV/Resume")]
        public HttpPostedFileBase upload { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "txt,doc,docx,pdf", ErrorMessage = "Please upload valid format")]
        public HttpPostedFileBase idDoc { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Please Upload Credentials")]
        public HttpPostedFileBase AccResults { get; set; }

    }
}