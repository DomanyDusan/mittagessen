using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mittagessen.Data.Interfaces;
using Mittagessen.Data;

namespace Mittagessen.Web.Infrastructure
{
    public class DbContextManager : IDbContextManager
    {
        public const string DATA_CONTEXT_KEY = "CurrentDataContext";
        public const string READ_MODELS_CONNECTION_STRING_NAME = "ReadModels";

        public DataContext GetCurrentContext()
        {
            var dataContext = (DataContext)HttpContext.Current.Items[DATA_CONTEXT_KEY];

            if (dataContext == null || dataContext.IsDisposed)
            {
                dataContext = new DataContext(READ_MODELS_CONNECTION_STRING_NAME);
                HttpContext.Current.Items.Add(DATA_CONTEXT_KEY, dataContext);
            }

            return dataContext;
        }
    }
}