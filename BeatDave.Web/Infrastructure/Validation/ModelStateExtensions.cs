using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace BeatDave.Web.Infrastructure
{
    public static class ModelStateExtensions
    {
        public static string FirstErrorMessage(this ModelStateDictionary modelState)
        {
            var state = modelState.Values.FirstOrDefault(x => x.Errors.Count > 0);

            if (state == null)
                return null;

            var message = state.Errors
                               .Where(error => string.IsNullOrEmpty(error.ErrorMessage) == false)
                               .Select(error => error.ErrorMessage)
                               .FirstOrDefault();

            return message;
        }
    }
}