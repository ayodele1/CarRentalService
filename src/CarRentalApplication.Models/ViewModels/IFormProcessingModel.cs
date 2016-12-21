using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models.ViewModels
{
    interface IFormProcessingModel
    {
        FormSubmissionViewModel FormProcessing { get; set; }
    }
}
