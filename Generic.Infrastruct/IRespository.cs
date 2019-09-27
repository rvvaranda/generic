using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Generic.Infrastruct
{
    public interface IRespository<T> where T : class
    {
        string addData(T entity);

        int getTotalItens(Expression<Func<T, bool>> expression = null);

        List<T> getAll();

        List<T> getByPage(int itens, int page);

        T getByGuid(string Guid);

        bool updateData(T entity);

        bool deleteData(T entity);

        T findBy(Expression<Func<T, bool>> expression);

        List<T> filterBy(Expression<Func<T, bool>> expression);

        IList<T> rawSql(String sqlQuery, String tableName);

        object rawSql(String sqlQuery);

        T rawSqlModel(String sqlQuery);

        List<T> rawSqlListModel(String sqlQuery);

        object rawBulkSql(String sqlQuery);

    }
}