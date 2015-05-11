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
    public class Repository<TModel> : DbContext,IRepository<TModel> where TModel : class, new()
    {
        #region Propriedades
        public DbSet<TModel> DbObject { get; set; }
        public IQueryable<TModel> dbQuery { get; set; }

        #endregion

        #region Construtores
        public Repository()
            : base(ConfigurationManager.ConnectionStrings[PgmGlobal.DbConnectName].ConnectionString)
        {
            ConfigRepository();
        }

        public Repository(string tConnectName)
            : base(ConfigurationManager.ConnectionStrings[tConnectName].ConnectionString)
        {
            ConfigRepository();
        }
        #endregion

        #region Metodos

        public void ConfigRepository()
        {
            System.Data.Entity.Database.SetInitializer<Repository<TModel>>(null);
            dbQuery = DbObject;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;

            Database.CommandTimeout = 99999;
        }

        protected override void OnModelCreating(DbModelBuilder oModelBuilder)
        {
            TModel model = new TModel();
            if (model is IBase)
            {
                ((IBase)model).OnCreating(oModelBuilder);
                base.OnModelCreating(oModelBuilder);
            }
        }

        private TModel RegisterModel(TModel tRow, char tAcao) // Inclui as informações obrigatorios para inclusão/alteração do modelo na base
        {
            // CONVERTE O PARAMETRO PARA A INTERFACE BASE E ALTERA AS CONFIGURAÇÕES DE INCLUSÃO DO SISTEMA
            IBase oRow = (IBase)tRow;
            if (tAcao=='I')
                oRow.DhInclusao = DateTime.Now; // DATA E HORA DA INCLUSAO DO REGISTRO
            else
                oRow.DhAlteracao = DateTime.Now; // DATA E HORA DA ALTERACAO DO REGISTRO

            oRow.Owner = PgmGlobal.UserCurrentId; // CÓDIGO DO USUARIO QUE ESTÁ LOGADO
            oRow.Inativo = 0;

            return ((TModel)oRow);
        }

        public bool Insert(TModel tRow)
        {
            TModel oRow = RegisterModel(tRow, 'I');

            DbObject.Add(oRow); // CONVERTE PARA A CLASSE ANTERIOR E SALVA
            return SaveChanges() > 0; 
        }

        public bool Insert(IList<TModel> tRows)
        {
            foreach (TModel oItem in tRows)
            {
                TModel oRow = RegisterModel(oItem, 'I');

                DbObject.Add(oRow); // CONVERTE PARA A CLASSE ANTERIOR E SALVA
            }
            return SaveChanges() > 0;
        }

        public bool Update(TModel tRow)
        {
            TModel oRow = RegisterModel(tRow, 'U');
            DbObject.Attach(oRow);
            Entry(oRow).State = EntityState.Modified;
            return SaveChanges() > 0;
        }

        public bool Update(IList<TModel> tRows)
        {
            foreach (TModel oItem in tRows)
            {
                TModel oRow = RegisterModel(oItem, 'U');
                DbObject.Attach(oRow);
                Entry(oRow).State = EntityState.Modified;
            }
            
            return SaveChanges() > 0;
        }

        public bool Delete(TModel tRow)
        {
            DbObject.Remove(tRow);
            return SaveChanges() > 0;
        }

        public bool Delete(IList<TModel> tRows)
        {
            foreach (TModel oItem in tRows)
            {
                DbObject.Remove(oItem);
            }
            return SaveChanges() > 0;
        }

        public IList<TModel> Where(Expression<Func<TModel, bool>> tLambda)
        {
            return dbQuery.Where(tLambda).ToList();
        }

        public IList<TModel> Select(params Expression<Func<TModel, object>>[] tNavProperties)
        {
            IQueryable<TModel> oQuery = dbQuery;
            if (tNavProperties != null)
            {
                foreach (var oNavProp in tNavProperties)
                    oQuery = oQuery.Include<TModel, object>(oNavProp);
            }
            return oQuery.ToList();
        }

        public TModel SelectId(object tId)
        {
            TModel oMod = DbObject.Find(tId);
            return oMod==null?new TModel() : oMod;
        }

        public IList<TModel> SelectAll(params Expression<Func<TModel, object>>[] tNavProperties)
        {
            IList<TModel> oQuery = Select(tNavProperties).ToList();

            return oQuery;
        }

        public IList<TModel> Take(int tTake,params Expression<Func<TModel, object>>[] tNavProperties)
        {
            IQueryable<TModel> oQuery = dbQuery;
            if (tNavProperties != null)
            {
                foreach (var oNavProp in tNavProperties)
                    oQuery = oQuery.Include<TModel, object>(oNavProp);
            }

            return oQuery.Take(tTake).ToList();
        }

        #endregion
    }
}
