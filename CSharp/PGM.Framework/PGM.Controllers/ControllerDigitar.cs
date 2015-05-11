using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PGM.Interfaces;
using PGM.SQL;
using System.Data.Entity;
using PGM.SQL.Repositories;
using System.Linq.Expressions;
using System.Windows.Forms;
using PGM.Sys;
using SimpleInjector;

namespace PGM.Controllers
{
    public enum Action
    {
        Insert,
        Update
    }

    public class ControllerDigitar<T> : IControllerDigitar, IControllerDigitar<T> where T : IBase
    {
        public string cLabel { get; set; }
        public Action oAction { get; set; }

        ISysRepository<T> oRepoDig { get; set; }
        public IField<T> oField { get; set; }

        public ControllerDigitar()
        {
            oRepoDig = PgmInjector.GetInstance<ISysRepository<T>>();
            oField = PgmInjector.GetInstance<IField<T>>();
        }

        public void SetConnection(string tNameOrConnectionString)
        {
            oRepoDig.SetConnection(tNameOrConnectionString);
        }

        public void SetId(object tId)
        {
            T oM = oRepoDig.SelectId(tId);
            this.SetEE(oM);

            // verifica se é uma ação de inclusão ou alteração
            string cT = tId.ToString();
            oAction = Action.Update;
            // busca a propriedade PkId do modelo
            var oP = oM.GetType().GetProperty("PkId").GetValue(oM);

            if (Convert.ToInt16(oP) == 0)
            {
                oAction = Action.Insert;
                cT = "(Novo)";
            }
            if (oP.ToString() == "")
            {
                oAction = Action.Insert;
                cT = "(Novo)";
            }
            cLabel = "ID: " + cT;
        }

        public IControllerDigitar<T> Register(Expression<Func<T, object>> tExpression, Control tControl)
        {
            oField.Register(tExpression, tControl);
            return this;
        }

        public T GetEE()
        {
            return oField.getModel();
        }

        public IField<T> SetEE(T tModel)
        {
            oField.setModel(tModel);

            return oField;
        }

        public bool SaveChances()
        {
            if (oAction == Action.Insert)
                oRepoDig.Insert(this.GetEE());
            if (oAction == Action.Update)
                oRepoDig.Update(this.GetEE());

            return (true);
        }
    }
}