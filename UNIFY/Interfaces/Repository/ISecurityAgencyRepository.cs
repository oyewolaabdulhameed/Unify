using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UNIFY.Model.Entities;

namespace UNIFY.Interfaces.Repository
{
    public interface ISecurityAgencyRepository : IRepository<SecurityAgency>
    {
        Task<SecurityAgency> Get(string id);
        Task<SecurityAgency> Get(Expression<Func<SecurityAgency, bool>> expression);
        Task<IEnumerable<SecurityAgency>> GetSelected(List<string> id);
        Task<IEnumerable<SecurityAgency>> GetSelected(Expression<Func<SecurityAgency, bool>> expression);
        Task<IEnumerable<SecurityAgency>> GetAll();
    

    }
}
