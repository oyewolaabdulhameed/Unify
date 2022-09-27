using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UNIFY.Model.Entities;

namespace UNIFY.Interfaces.Repository
{
    public interface ISecurityRepository : IRepository<Security>
    {
        Task<Security> Get(string id);
        Task<IList<Security>> GetAllSecurityWithUser();
        Task<Security> GetSecurityWithUser(string id);
        Task<Security> GetSecurityInfo(string id);
        Task<Security> Get(Expression<Func<Security, bool>> expression);
        Task<IEnumerable<Security>> GetSelected(List<string> id);
        Task<IEnumerable<Security>> GetSelected(Expression<Func<Security, bool>> expression);
        Task<IEnumerable<Security>> GetAll();
    }
}
