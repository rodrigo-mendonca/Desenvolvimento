using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace PGM.Interfaces
{
    public interface IRepository<TModel>
    {
        /// <summary>
        /// Este metodo tem como função incluir um registro na base de dados, com base em um objeto da tabela
        /// Que deve estar preenchido e pronto a inclusão.
        /// </summary>
        /// <param name="tRow">Objeto da Tabela</param>
        /// <returns>Retorna True ou False para informar se conseguiu executar a ação.</returns>
        bool Insert(TModel tRow);

        /// <summary>
        /// Este metodo tem como função incluir varios registros na base de dados, com base em um objeto da tabela
        /// Que deve estar preenchido e pronto a inclusão.
        /// </summary>
        /// <param name="tRows">Lista de Objetos da tabela</param>
        /// <returns>Retorna True ou False para informar se conseguiu executar a ação.</returns>
        bool Insert(IList<TModel> tRows);

        /// <summary>
        /// Este metodo tem como função atualizar um registro na base de dados, com base em um objeto da tabela
        /// Que deve estar preenchido e pronto a inclusão.
        /// </summary>
        /// <param name="tRow">Objeto da Tabela</param>
        /// <returns>Retorna True ou False para informar se conseguiu executar a ação</returns>
        bool Update(TModel tRow);

        /// <summary>
        /// Este metodo tem como função Atualizar varios registros na base de dados, com base em um objeto da tabela
        /// Que deve estar preenchido e pronto a inclusão.
        /// </summary>
        /// <param name="tRows">Lista de Objetos da tabela</param>
        /// <returns>Retorna True ou False para informar se conseguiu executar a ação.</returns>
        bool Update(IList<TModel> tRows);

        /// <summary>
        /// Este metodo tem como função deletar um registro na base de dados, com base em um objeto da tabela
        /// Que deve estar preenchido e pronto a inclusão.
        /// </summary>
        /// <param name="tRow">Objeto da Tabela</param>
        /// <returns>Retorna True ou False para informar se conseguiu executar a ação</returns>
        bool Delete(TModel tRow);

        /// <summary>
        /// Este metodo tem como função Atualizar varios registros na base de dados, com base em um objeto da tabela
        /// Que deve estar preenchido e pronto a inclusão.
        /// </summary>
        /// <param name="tRows">Lista de Objetos da tabela</param>
        /// <returns>Retorna True ou False para informar se conseguiu executar a ação.</returns>
        bool Delete(IList<TModel> tRows);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tNavProperties">Expressões lambda de propriedades para a select</param>
        /// <returns>Retorna um Queryable após fazer a select</returns>
        IList<TModel> Select(params Expression<Func<TModel, object>>[] tNavProperties);

        /// <summary>
        /// Busca um modelo usando a chave primaria
        /// </summary>
        /// <param name="tId">PkId do registro do banco de dados</param>
        /// <returns>Modelo selecionado no repositorio</returns>
        TModel SelectId(object tId);

        /// <summary>
        /// Busca todos os registros do modelo selecionado no repositorio
        /// </summary>
        /// <param name="tNavProperties">Expressões lambda de propriedades para a select</param>
        /// <returns>Lista de modelos</returns>
        IList<TModel> SelectAll(params Expression<Func<TModel, object>>[] tNavProperties);

        /// <summary>
        /// Busca todos os registros do modelo selecionado no repositorio de acordo o filtro passado por lambda
        /// </summary>
        /// <param name="tNavProperties">Expressões lambda de propriedades para a select</param>
        /// <returns>Lista de modelos</returns>
        IList<TModel> Where(Expression<Func<TModel, bool>> tNavProperties);

        /// <summary>
        /// Buscar "tTake" registros
        /// </summary>
        /// <param name="tTake">Numero de registros que deseja buscar</param>
        /// <param name="tNavProperties">Expressões lambda de propriedades para a select</param>
        /// <returns>Lista de modelos</returns>
        IList<TModel> Take(int tTake, params Expression<Func<TModel, object>>[] tNavProperties);
    }
}
