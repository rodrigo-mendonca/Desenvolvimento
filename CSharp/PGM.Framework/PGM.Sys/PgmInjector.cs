using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

using SimpleInjector;
using SimpleInjector.Extensions;

using System.Windows.Forms;
using System.Reflection;

namespace PGM.Sys
{
    public static class PgmInjector
    {
        public static Container SjContainer = new Container();

        public static T GetInstance<T>() where T : class
        {
            return SjContainer.GetInstance<T>();
        }

        public static T GetInstance<T>(Type tType)
        {
            return (T)SjContainer.GetInstance(tType);
        }
    }
}
