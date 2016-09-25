﻿using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Net.Http;

namespace PlaySimple.Common
{
    public class DateTimeParameterBinding : HttpParameterBinding
    {
        public DateTimeParameterBinding(HttpParameterDescriptor descriptor)
            : base(descriptor) { }

        public override Task ExecuteBindingAsync(
            ModelMetadataProvider metadataProvider,
            HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            string dateToParse = null;
            string paramName = this.Descriptor.ParameterName;

            //if (ReadFromQueryString)
            //{
            // reading from query string
            var nameVal = actionContext.Request.GetQueryNameValuePairs();
            dateToParse = nameVal.Where(q => q.Key.Equals(paramName))
                                    .FirstOrDefault().Value;
            //}
            //else
            //{
            //    // reading from route
            //    var routeData = actionContext.Request.GetRouteData();
            //    object dateObj = null;
            //    if (routeData.Values.TryGetValue(paramName, out dateObj))
            //    {
            //        dateToParse = Convert.ToString(dateObj);
            //    }
            //}

            DateTime? dateTime = null;
            if (!string.IsNullOrEmpty(dateToParse))
            {
                dateTime = ParseDateTime(dateToParse);
            }

            SetValue(actionContext, dateTime);

            return Task.FromResult<object>(null);
        }

        public DateTime? ParseDateTime(string dateToParse)
        {
            DateTime validDate;

            if (DateTime.TryParseExact(dateToParse, "d/M/yyyy", null, DateTimeStyles.AssumeLocal, out validDate))
            {
                return validDate;
            }

            return null;
        }
    }
}