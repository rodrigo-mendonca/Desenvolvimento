using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using SimpleInjector;
using SimpleInjector.Extensions;
using System.Reflection;
using PGM.SQL.Repositories;
using PGM.Interfaces;
using PGM.Controllers;

namespace PGM.Modules
{
    public class PgmModulo
    {
        public void SetBinding(Container tCon)
        {
            // registro de tipos genericos
            tCon.RegisterOpenGeneric(typeof(IRepository<>), typeof(SysRepository<>), Lifestyle.Singleton);
            tCon.RegisterOpenGeneric(typeof(ISysRepository<>), typeof(SysRepository<>), Lifestyle.Singleton);
            tCon.RegisterOpenGeneric(typeof(IControllerDigitar<>), typeof(ControllerDigitar<>), Lifestyle.Singleton);
            tCon.RegisterOpenGeneric(typeof(IField<>), typeof(Field<>), Lifestyle.Singleton);

            // registro de tipo
            tCon.Register(typeof(IControllerLogin), typeof(ControllerLogin), Lifestyle.Singleton);

            tCon.Verify();
        }
    }
}