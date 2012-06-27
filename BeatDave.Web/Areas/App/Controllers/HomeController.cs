using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BeatDave.Domain;

namespace BeatDave.Web.Areas.App.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            var logbook = new LogBook
            {
                Description = "Any Old Description",
                OwnerId = "66",
                Tags = new List<string> { "any", "old", "logbook" },
                Title = "Any Old Logbook",
                Units = new Units { Symbol = "£", Precision = 2, SymbolPosition = SymbolPosition.Before },
                Visibility = Visibility.PublicAnonymous
            };

            logbook.LogEntry(new Entry { LogBook = logbook, OccurredOn = DateTime.Now.AddDays(-10), Value = 10 });
            logbook.LogEntry(new Entry { LogBook = logbook, OccurredOn = DateTime.Now.AddDays(-5), Value = 5 });
            logbook.LogEntry(new Entry { LogBook = logbook, OccurredOn = DateTime.Now.AddDays(0), Value = 0 });

            return View();
        }
    }
}
