using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFUserDal : EFEntityRepositoryBase<User, CarContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (CarContext context = new CarContext())
            {
                return GetClaimsQuery(context, user.Id).ToList();
            }
        }

        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            using (CarContext context = new CarContext())
            {
                return await GetClaimsQuery(context, user.Id).ToListAsync();
            }
        }

        private IQueryable<OperationClaim> GetClaimsQuery(CarContext context, int userId)
        {
            return from operationClaim in context.OperationClaims
                   join userOperationClaim in context.UserOperationClaims on operationClaim.Id equals userOperationClaim.OperationClaimId
                   where userOperationClaim.UserId == userId
                   select new OperationClaim
                   {
                       Id = operationClaim.Id,
                       Name = operationClaim.Name
                   };
        }
    }
}