using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Generic.Dao;
using Generic.Logger;
using NHibernate.Criterion.Lambda;

namespace Generic.Bo
{
     public abstract class BaseBO<T> where T : class, new()
    {
        GenericLogger logger = new GenericLogger();
        protected BaseDao<T> dao = BaseDao<T>.GetInstance;

        public virtual List<T> ListarBO()
        {
            try
            {
                return dao.ListarDao();
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("ListarBO", ex, "Record");
                throw ex;
            }
        }


        public virtual async Task<List<object[]>> ListarPorColunasBO(
            Func<QueryOverProjectionBuilder<T>, QueryOverProjectionBuilder<T>> colunas)
        {
            try
            {
                return await dao.ListarPorColunasDao(colunas);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("ListarBO", ex, "Record");
                throw ex;
            }
        }

        public virtual int TotalDeRegistrosBO(Expression<Func<T, bool>> expression = null)
        {
            try
            {
                return dao.TotalDeRegistrosDao(expression);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("TotalDeRegistrosBO", ex, "Record");
                throw ex;
            }
        }

        public virtual List<T> ListarPorPaginacaoBO(int itens, int pagina)
        {
            try
            {
                return dao.ListarPorPaginaDao(itens, pagina);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("ListarPorPaginacaoBO", ex, "Record");
                throw ex;
            }
        }

        public virtual List<T> ListarAtivoBO()
        {
            try
            {

                var parameter = Expression.Parameter(typeof(T), "t");
                var member = Expression.Property(parameter, "Ativo"); //t.Ativo
                var constant = Expression.Constant(1);
                var body = Expression.Equal(member, constant); //t.Ativo == 1
                var finalExpression = Expression.Lambda<Func<T, bool>>(body, parameter);

                return dao.FiltrarDao(finalExpression);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("ListarAtivoBO", ex, "Record");
                throw ex;
            }
        }

        public virtual List<T> ListarInativoBO()
        {
            try
            {

                var parameter = Expression.Parameter(typeof(T), "t");
                var member = Expression.Property(parameter, "Ativo"); //t.Ativo
                var constant = Expression.Constant(0);
                var body = Expression.Equal(member, constant); //t.Ativo == 0
                var finalExpression = Expression.Lambda<Func<T, bool>>(body, parameter);

                return dao.FiltrarDao(finalExpression);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("ListarInativoBO", ex, "Record");
                throw ex;
            }
        }
           
        public virtual List<T> FiltrarBO(Expression<Func<T, bool>> expression)
        {
            try
            {
                return dao.FiltrarDao(expression);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("FiltrarBO", ex, "Record");
                throw ex;
            }
        }


        public virtual async Task<List<object[]>> FiltrarPorColunaBO(
            Func<QueryOverProjectionBuilder<T>, QueryOverProjectionBuilder<T>> colunas,
            Expression<Func<T, bool>> expression)
        {
            try
            {
                return await dao.FiltrarPorColunaDao(colunas, expression);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("FiltrarBO", ex, "Record");
                throw ex;
            }
        
        }

        public virtual T LocalizarBO(Expression<Func<T, bool>> expression)
        {
            try
            {
                return dao.LocalizarDao(expression);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("LocalizarBO", ex, "Record");
                throw ex;
            }
        }

        public virtual T RetornarPorGuidBO(string Guid)
        {
            try
            {
                return dao.RetornarPorGuidDao(Guid);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("RetornarPorGuidBO", ex, "Record");
                throw ex;
            }
        }

        public virtual string InserirBO(T dados)
        {
            try
            {
                return dao.InserirDao(dados);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("InserirBO", ex, "Record");
                throw ex;
            }
        }

        public virtual Boolean EditarBO(T dados)
        {
            try
            {
                return dao.EditarDao(dados);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("EditarBO", ex, "Record");
                throw ex;
            }
        }

        public virtual Boolean ExcluirBO(T dados)
        {
            try
            {
                return dao.ExcluirDao(dados);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("ExcluirBO", ex, "Record");
                throw ex;
            }
        }

        public virtual object RawSQLBO(String query)
        {
            try
            {
                return dao.RawSqlDao(query);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("RawSQLBO", ex, "Record");
                throw ex;
            }
        }

        public virtual List<T> RawSQLBO(String query, string tableName)
        {
            try
            {
                return dao.RawSqlDao(query, tableName);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("RawSQLBO", ex, "Record");
                throw ex;
            }
        }

        public virtual T RawSqlToModelBO(String query)
        {
            try
            {
                return dao.RawSqlToModelDao(query);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("RawSqlToModelBO", ex, "Record");
                throw ex;
            }
        }

        public virtual List<T> RawSqlToModelListBO(String query)
        {
            try
            {
                return dao.RawSqlToModeListlDao(query);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("RawSqlToModelListBO", ex, "Record");
                throw ex;
            }
        }
        public virtual List<T> RawSqlToModelListBO(String query, Dictionary<string, object> param)
        {
            try
            {
                return dao.RawSqlToModeListlDao(query, param);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("RawSqlToModelListBO", ex, "Record");
                throw ex;
            }
        }        
    }
}