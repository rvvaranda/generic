using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Generic.Dal;
using Generic.Logger;
using NHibernate.Criterion.Lambda;
using Unity;

namespace Generic.Dao
{
     public class BaseDao<T> where T : class, new()
    {
        GenericLogger cLog = new GenericLogger();
        static UnityContainer _container;
        static BaseDao<T> _dao = null;
        static readonly object padLock = new object();

        public BaseDao()
        {
            _container = new UnityContainer();
            _container.RegisterType<RepositoryDAL<T>>();
        }

        public static BaseDao<T> GetInstance
        {
            get
            {
                lock (padLock)
                {
                    if (_dao == null)
                    {
                        _dao = new BaseDao<T>();
                    }
                    return _dao;
                }
            }
        }

        public int TotalDeRegistrosDao(Expression<Func<T, bool>> expression = null)
        {
            try
            {
                return _container.Resolve<RepositoryDAL<T>>().getTotalItens(expression);
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("TotalDeRegistrosDao()", ex, "Record");
                throw ex;
            }
        }

        public List<T> ListarDao()
        {
            try
            {
                return _container.Resolve<RepositoryDAL<T>>().getAll();
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("ListarDao()", ex, "Record");
                throw ex;
            }
        }

        public async Task<List<object[]>> ListarPorColunasDao(Func<QueryOverProjectionBuilder<T>, QueryOverProjectionBuilder<T>> colunas)
        {
            try
            {
                return await _container.Resolve<RepositoryDAL<T>>().getAllCustomColumns(colunas);
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("ListarPorColunasDao()", ex, "Record");
                throw ex;
            }
        }
        
        public List<T> ListarPorPaginaDao(int itens, int page)
        {
            try
            {
                return _container.Resolve<RepositoryDAL<T>>().getByPage(itens, page);
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("ListarPorPaginaDao()", ex, "Record");
                throw ex;
            }
        }

        public T LocalizarDao(Expression<Func<T, bool>> expression)
        {
            try
            {
                return _container.Resolve<RepositoryDAL<T>>().findBy(expression);
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("LocalizarDao()", ex, "Record");
                throw ex;
            }
        }  
        
        public List<T> FiltrarDao(Expression<Func<T, bool>> expression)
        {
            try
            {
                return _container.Resolve<RepositoryDAL<T>>().filterBy(expression);
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("FiltrarDao()", ex, "Record");
                throw ex;
            }
        }
        
        public async Task<List<object[]>> FiltrarPorColunaDao(Func<QueryOverProjectionBuilder<T>, QueryOverProjectionBuilder<T>> colunas, Expression<Func<T, bool>> expression)
        {
            try
            {
                return await _container.Resolve<RepositoryDAL<T>>().filterByCustomColumns(colunas, expression);
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("FiltrarDao()", ex, "Record");
                throw ex;
            }
        }

        public List<T> RawSqlDao(String query, String tableName)
        {
            try
            {
                return _container.Resolve<RepositoryDAL<T>>().rawSql(query, tableName) as List<T>;
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("RawSqlDao()", ex, "Record");
                throw ex;
            }
        }

        public object RawSqlDao(String query)
        {
            try
            {
                return _container.Resolve<RepositoryDAL<T>>().rawSql(query);
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("RawSqlDao()", ex, "Record");
                throw ex;
            }
        }

        public T RawSqlToModelDao(String query)
        {
            try
            {
                return _container.Resolve<RepositoryDAL<T>>().rawSqlModel(query);
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("RawSqlToModelDao()", ex, "Record");
                throw ex;
            }
        }

        public List<T> RawSqlToModeListlDao(String query)
        {
            try
            {
                return _container.Resolve<RepositoryDAL<T>>().rawSqlListModel(query);
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("RawSqlToModeListlDao()", ex, "Record");
                throw ex;
            }
        }
        
        public List<T> RawSqlToModeListlDao(String query, Dictionary<string, object> param)
        {
            try
            {
                return _container.Resolve<RepositoryDAL<T>>().rawSqlListModel(query, param);
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("RawSqlToModeListlDao()", ex, "Record");
                throw ex;
            }
        }

        public T RetornarPorGuidDao(string Guid)
        {
            try
            {
                return _container.Resolve<RepositoryDAL<T>>().getByGuid(Guid);
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("RetornarPorGuidDao()", ex, "Record");
                throw ex;
            }
        }

        public string InserirDao(T dados)
        {
            try
            {
                return _container.Resolve<RepositoryDAL<T>>().addData(dados);
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("InserirDao()", ex, "Record");
                throw ex;
            }
        }

        public bool EditarDao(T dados)
        {
            try
            {
                return _container.Resolve<RepositoryDAL<T>>().updateData(dados);
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("EditarDao()", ex, "Record");
                throw ex;
            }
        }

        public bool ExcluirDao(T dados)
        {
            try
            {
                return _container.Resolve<RepositoryDAL<T>>().deleteData(dados);
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("ExcluirDao()", ex, "Record");
                throw ex;
            }
        }
    }
}