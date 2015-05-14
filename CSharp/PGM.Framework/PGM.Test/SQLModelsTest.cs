using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PGM.SQL.Models;
using PGM.SQL.Repositories;
using PGM.Interfaces;
using PGM.Modules;
using PGM.Sys;
using System.Reflection;

namespace PGM.Test.SQL
{
    /// <summary>
    /// A Classe tem como objetivo testar todos os modelos mapeados da base de dados.
    /// </summary>
    public static class SQLModelsTest
    {
        public static void StartTest()
        {
            Console.WriteLine("Validando Modelos");
            // Lista todos os tipos do name space e para cada modelo verifica se está executando
            foreach (Type T in GetTypesInNamespace(typeof(Ativo).Assembly, "PGM.SQL.Models"))
            {
                var obj = Activator.CreateInstance(T);
                MethodInfo method = typeof(SQLModelsTest).GetMethod("TestModel");
                MethodInfo generic = method.MakeGenericMethod(T);
                generic.Invoke(method,new object[] {obj});
            }
            Console.ReadKey();
        }

        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }

        public static void TestModel<T>(T Model)
        {
            try
            {
                ISysRepository<T> Repo = (ISysRepository<T>)PgmInjector.GetInstance<ISysRepository<T>>();
                IList<T> Reg = Repo.Take(1);

                Console.WriteLine(Model.GetType().Name + "-> Ok");
            }
            catch (Exception)
            {
                Console.WriteLine(Model.GetType().Name + "-> Erro");
            }
        }
    }

    
}
