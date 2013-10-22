using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data;
using Mittagessen.Data.Interfaces;

namespace Mittagessen.Tests.Infrastructure
{
    public class DbContextManager : IDbContextManager
    {
        private static DataContext CurrentContext { get; set; }

        public const string READ_MODELS_CONNECTION_STRING_NAME = "ReadModels";

        public DataContext GetCurrentContext()
        {
            if (CurrentContext == null || CurrentContext.IsDisposed)
            {
                CurrentContext = new DataContext(READ_MODELS_CONNECTION_STRING_NAME);
            }

            return CurrentContext;
        }
    }
}
