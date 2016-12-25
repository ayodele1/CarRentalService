using CarRentalApplication.Models.ViewModels.Reservation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CarRentalApplication.Services
{
    public class ViewModelSesssionService
    {
        public void SaveToSession(HttpContext context,object viewModel, string sessionKey)
        {
           context.Session.SetString(sessionKey, JsonConvert.SerializeObject(viewModel, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        public T GetFromSession<T>(HttpContext context, string sessionKey)
        {
            var sessionString = context.Session.GetString(sessionKey);
            return JsonConvert.DeserializeObject<T>(sessionString);
        }
    }
}
