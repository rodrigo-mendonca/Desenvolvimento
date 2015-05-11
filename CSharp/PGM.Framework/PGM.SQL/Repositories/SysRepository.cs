using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Configuration;
using PGM.Interfaces;
using System.Linq.Expressions;
using PGM.Sys;

namespace PGM.SQL.Repositories
{
    public class SysRepository<TModel> : ISysRepository<TModel> where TModel : class, new()
    {
        private string nameOrConnectionString = PgmGlobal.DbConnectName;

        public SysRepository()
        {
            
        }

        public void SetConnection(string tnameOrConnectionString)
        {
            nameOrConnectionString = tnameOrConnectionString;

            if (string.IsNullOrEmpty(nameOrConnectionString))
            {
                nameOrConnectionString =
                (ConfigurationManager.ConnectionStrings[PgmGlobal.DbConnectName]).ConnectionString;
            }
        }

        public bool Insert(TModel tRow)
        {
            using (var oConRepo = new Repository<TModel>(this.nameOrConnectionString))
            {
                return oConRepo.Insert(tRow);
            }
        }
        public bool Insert(IList<TModel> tRows)
        {
            using (var oConRepo = new Repository<TModel>(this.nameOrConnectionString))
            {
                return oConRepo.Insert(tRows);
            }
        }
        public bool Update(TModel tRow)
        {
            using (var oConRepo = new Repository<TModel>(this.nameOrConnectionString))
            {
                return oConRepo.Update(tRow);
            }
        }
        public bool Update(IList<TModel> tRows)
        {
            using (var oConRepo = new Repository<TModel>(this.nameOrConnectionString))
            {
                return oConRepo.Update(tRows);
            }
        }
        public bool Delete(TModel tRow)
        {
            using (var oConRepo = new Repository<TModel>(this.nameOrConnectionString))
            {
                return oConRepo.Delete(tRow);
            }
        }
        public bool Delete(IList<TModel> tRows)
        {
            using (var oConRepo = new Repository<TModel>(this.nameOrConnectionString))
            {
                return oConRepo.Delete(tRows);
            }
        }
        public IList<TModel> Select(params Expression<Func<TModel, object>>[] tNavProperties)
        {
            IList<TModel> oRetr;
            using (var oConRepo = new Repository<TModel>(this.nameOrConnectionString))
            {
                oRetr = oConRepo.Select(tNavProperties);
            }
            return oRetr;
        }
        public TModel SelectId(object tId)
        {
            TModel oM;
            using (var oConRepo = new Repository<TModel>(this.nameOrConnectionString))
            {
                oM = oConRepo.SelectId(tId);
            }
            return oM;
        }
        public IList<TModel> SelectAll(params Expression<Func<TModel, object>>[] tNavProperties)
        {
            IList<TModel> oL;
            using (var oConRepo = new Repository<TModel>(this.nameOrConnectionString))
            {
                oL = oConRepo.SelectAll(tNavProperties);
            }
            return oL;
        }
        public IList<TModel> Where(Expression<Func<TModel, bool>> tNavProperties)
        {
            IList<TModel> oL;
            using (var oConRepo = new Repository<TModel>(this.nameOrConnectionString))
            {
                oL = oConRepo.Where(tNavProperties);
            }
            return oL;
        }

        public IList<TModel> Take(int tTake, params Expression<Func<TModel, object>>[] tNavProperties)
        {
            IList<TModel> oL;
            using (var oConRepo = new Repository<TModel>(this.nameOrConnectionString))
            {
                oL = oConRepo.Take(tTake,tNavProperties);
            }
            return oL;
        }
    }
}