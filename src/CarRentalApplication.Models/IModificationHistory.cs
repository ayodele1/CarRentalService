using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models
{
    interface IModificationHistory
    {
        DateTime DateCreated { get; set; }

        DateTime DateModified { get; set; }
    }
}
