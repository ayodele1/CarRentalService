using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models.ViewModels
{
    interface IFormProcessing
    {
        FormSubmissionViewModel FormProcessing { get; set; }
    }
}
