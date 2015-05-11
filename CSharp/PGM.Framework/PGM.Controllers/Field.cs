using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Reflection;
using PGM.Interfaces;

namespace PGM.Controllers
{
    public class Field<T> : IField<T> where T : class, IBase, new()
    {
        private T oModel;

        private IList<Tuple<Expression<Func<T, object>>, Control>> oControls;

        public Field()
        {
            oModel = new T();
            oControls = new List<Tuple<Expression<Func<T, object>>, Control>>();
        }

        public IField<T> Register(Expression<Func<T, object>> tFieldModel, Control tControl)
        {
            getInfo(tFieldModel); // valida lambda

            var valuePair = new Tuple<Expression<Func<T, object>>, Control>(tFieldModel, tControl);
            oControls.Add(valuePair);

            return this;
        }

        private void fillModel()
        {
            foreach (var oItem in oControls)
            {
                var info = getInfo(oItem.Item1);
                object oValue = null;

                var oCon = oItem.Item2;

                oValue = oCon.GetType().GetMethod("GetValue").Invoke(oCon, null);

                oModel.GetType().GetProperty(info.Name).SetValue(oModel, oValue, new object[] { });
            }
        }

        private void fillControls()
        {
            foreach (var oItem in oControls)
            {
                var oInfo = getInfo(oItem.Item1);

                object oValue =
                    oModel.GetType().GetProperty(oInfo.Name).GetValue(oModel, new object[] { });

                var oCon = oItem.Item2;

                oCon.GetType().GetMethod("SetValue").Invoke(oCon, new object[]{oValue});
            }
        }

        private PropertyInfo getInfo(Expression<Func<T, object>> fieldModel)
        {
            Type type = typeof(T);

            MemberExpression member = fieldModel.Body as MemberExpression;

            if (fieldModel.Body is UnaryExpression)
                member = (fieldModel.Body as UnaryExpression).Operand as MemberExpression;
            else
                member = fieldModel.Body as MemberExpression;

            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    fieldModel.ToString()));

                      PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    fieldModel.ToString()));


            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expresion '{0}' refers to a property that is not from type {1}.",
                    fieldModel.ToString(),
                    type));

            return propInfo;
        }

        public T getModel()
        {
            fillModel();
            return oModel;
        }

        public IField<T> setModel(T tModel)
        {
            oModel = tModel;

            fillControls();

            return this;
        }

    }
}
