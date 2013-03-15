using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neocean.DataObjects;
using Neocean.Domain.Data;
using Neocean.Domain.Model.Customer;

namespace Neocean.Services.Customer
{
    public class UserService : ApplicationService, IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository, IDbContext dbContent)
            : base(dbContent)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// Insert a customer
        /// </summary>
        /// <param name="customer">Customer</param>
        public void InsertUser(List<UserDataObject> userDataObjects)
        {
            if (userDataObjects == null)
                throw new ArgumentNullException("userDataObjects");

            PerformCreateObjects<List<UserDataObject>, UserDataObject, User>(userDataObjects,
                _userRepository,
                dto =>
                {
                    if (dto.DateRegistered == null)
                        dto.DateRegistered = DateTime.Now;
                },
                ar =>
                {

                });
        }
    }
}
