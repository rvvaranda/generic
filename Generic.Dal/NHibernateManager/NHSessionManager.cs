using NHibernate;
using System;

namespace Generic.Dal.NHibernateManager
{
    public class NHSessionManager
    {
        public static ISession GetSession()
        {
            try
            {
                return NHSessionFactoryManager.GetSessionFactory().OpenSession();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}