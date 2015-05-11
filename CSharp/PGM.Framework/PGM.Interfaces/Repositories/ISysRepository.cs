using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace PGM.Interfaces
{
    public interface ISysRepository<TModel> : IRepository<TModel>
    {
        /// <summary>
        /// Método destinado a modificar a string de conexão usada pelo Entity Framework
        /// </summary>
        /// <param name="nameOrConnectionString">String de conexão</param>
        void SetConnection(string tnameOrConnectionString);
    }
}
