using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NHibernate.Criterion.Lambda;
using Generic.Dal.NHibernateManager;
using Generic.Infrastruct;
using Generic.Logger;


namespace Generic.Dal
{
    public class RepositoryDAL<T> : IRespository<T> where T : class
    {
        GenericLogger cLog = new GenericLogger();

        public string addData(T entity)
        {
            using (var session = NHSessionManager.GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var obj = session.Save(entity);
                        transaction.Commit();
                        return obj as string;
                    }
                    catch (Exception ex)
                    {
                        cLog.ArquivoLog("addData()", ex, "Record");

                        var objEntity = entity.GetType();
                        var data = entity;
                        var propInfo = objEntity.GetProperty("Guid");
                        propInfo.SetValue(entity, "");
                        
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public int getTotalItens(Expression<Func<T, bool>> expression = null)
        {
            using (var session = NHSessionManager.GetSession())
            {
                using (var transaction = session.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    try
                    {
                        var rslt = 0;

                        if (expression != null)
                        {
                            rslt = session.Query<T>().Where(expression).Count();
                        }
                        else
                        {
                            rslt = session.Query<T>().Count();
                        }
                        
                        transaction.Commit();
                        return rslt;
                    }
                    catch (Exception ex)
                    {
                        cLog.ArquivoLog("getTotalItens()", ex, "Record");
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public List<T> getAll()
        {
            using (var session = NHSessionManager.GetSession())
            {
                using (var transaction = session.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    try
                    {
                        var rslt = session.Query<T>().ToList();
                        transaction.Commit();
                        return rslt;
                    }
                    catch (Exception ex)
                    {
                        cLog.ArquivoLog("getAll()", ex, "Record");
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<List<object[]>> getAllCustomColumns(Func<QueryOverProjectionBuilder<T>, QueryOverProjectionBuilder<T>> columns)
        {
            using (var session = NHSessionManager.GetSession())
            {
                using (var transaction = session.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    try
                    {

                        var rslt = await session.QueryOver<T>()
                            .SelectList(columns).ListAsync<object[]>();
                        
                        transaction.Commit();
                        return rslt.ToList();
                    }
                    catch (Exception ex)
                    {
                        cLog.ArquivoLog("getAllCustomColumns()", ex, "Record");
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
        
        public List<T> getByPage(int itens, int page)
        {
            using (var session = NHSessionManager.GetSession())
            {
                using (var transaction = session.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    try
                    {
                        var rslt = session.Query<T>().Skip(itens * (page - 1)).Take(itens).ToList();
                        transaction.Commit();
                        return rslt;
                    }
                    catch (Exception ex)
                    {
                        cLog.ArquivoLog("getByPage()", ex, "Record");
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public T getByGuid(string Guid)
        {

            var session = NHSessionManager.GetSession();
            var transaction = session.BeginTransaction(IsolationLevel.ReadUncommitted);

            try
            {
                var rslt = session.Get<T>(Guid);
                transaction.Commit();
                return rslt;
            }
            catch (Exception ex)
            {
                cLog.ArquivoLog("getByGuid()", ex, "Record");
                transaction.Rollback();
                throw ex;
            }
        }

        public bool updateData(T entity)
        {
            using (var session = NHSessionManager.GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(entity);
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        cLog.ArquivoLog("updateData()", ex, "Record");
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public bool deleteData(T entity)
        {
            using (var session = NHSessionManager.GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(entity);
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        cLog.ArquivoLog("deleteData()", ex, "Record");
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
        
        public T findBy(Expression<Func<T, bool>> expression)
        {
            using (var session = NHSessionManager.GetSession())
            {
                using (var transaction = session.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    try
                    {
                        var rslt = session.Query<T>().FirstOrDefault(expression);
                        transaction.Commit();
                        return rslt;
                    }
                    catch (Exception ex)
                    {
                        cLog.ArquivoLog("findBy()", ex, "Record");
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public List<T> filterBy(Expression<Func<T, bool>> expression)
        {
            using (var session = NHSessionManager.GetSession())
            {
                using (var transaction = session.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    try
                    {
                        var rslt = session.Query<T>().Where(expression);
                        transaction.Commit();
                        return rslt.ToList();
                    }

                    catch (Exception ex)
                    {
                        cLog.ArquivoLog("filterBy()", ex, "Record");
                        throw ex;
                    }
                }
            }
        }

        public async Task<List<object[]>> filterByCustomColumns(Func<QueryOverProjectionBuilder<T>, QueryOverProjectionBuilder<T>> columns, Expression<Func<T, bool>> expression)
        {
            using (var session = NHSessionManager.GetSession())
            {
                using (var transaction = session.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    try
                    {
                        var rslt = await session.QueryOver<T>()
                            .SelectList(columns)
                            .Where(expression)
                            .ListAsync<object[]>();

                        transaction.Commit();
                        return rslt.ToList();
                    }

                    catch (Exception ex)
                    {
                        cLog.ArquivoLog("filterBy()", ex, "Record");
                        throw ex;
                    }
                }
            }
        }

        public IList<T> rawSql(String sqlQuery, String tableName)
        {
            var session = NHSessionManager.GetSession();

            using (var transaction = session.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var rslt = session.CreateSQLQuery(sqlQuery)
                        .AddEntity(tableName, typeof(T)).List<T>();
                    transaction.Commit();

                    return rslt;
                }
                catch (Exception ex)
                {
                    cLog.ArquivoLog("rawSql()", ex, "Record");
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public object rawSql(String sqlQuery)
        {
            var session = NHSessionManager.GetSession();

            using (var transaction = session.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var rslt = session.CreateSQLQuery(sqlQuery).ExecuteUpdate();
                    transaction.Commit();

                    return rslt;
                }
                catch (Exception ex)
                {
                    cLog.ArquivoLog("rawSql()", ex, "Record");
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public T rawSqlModel(String sqlQuery)
        {
            var session = NHSessionManager.GetSession();

            using (var transaction = session.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {

                    var rslt = session.CreateSQLQuery(sqlQuery)
                       .AddEntity(typeof(T))
                       .UniqueResult<T>();

                    return rslt;
                }
                catch (Exception ex)
                {
                    cLog.ArquivoLog("rawSqlModel()", ex, "Record");
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        [Obsolete("Método será substituído pelo método 'List<T> rawSqlListModel(String sqlQuery, Dictionary<string, object> param)'")]
        public List<T> rawSqlListModel(String sqlQuery)
        {
            var session = NHSessionManager.GetSession();

            using (var transaction = session.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {

                    var rslt = session.CreateSQLQuery(sqlQuery)
                        .AddEntity(typeof(T)).List<T>();

                    return rslt.ToList();
                }
                catch (Exception ex)
                {
                    cLog.ArquivoLog("rawSqlListModel()", ex, "Record");
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        
        public List<T> rawSqlListModel(String sqlQuery, Dictionary<string, object> param)
        {
            var session = NHSessionManager.GetSession();

            using (var transaction = session.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var query = session.CreateSQLQuery(sqlQuery);
                    foreach (var dic in param)
                    {
                        query.SetParameter(dic.Key, dic.Value);
                    }
                    
                    var rslt = query.AddEntity(typeof(T)).List<T>();

                    return (List<T>) rslt;
                }
                catch (Exception ex)
                {
                    cLog.ArquivoLog("rawSqlListModel()", ex, "Record");
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        
        public object rawBulkSql(String sqlQuery)
        {
            var session = NHSessionManager.GetSession();

            using (var transaction = session.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var rslt = session.CreateSQLQuery(sqlQuery);
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    cLog.ArquivoLog("rawBulkSql()", ex, "Record");
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}