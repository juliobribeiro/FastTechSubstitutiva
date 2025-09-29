using FastTech.Domain.Entities;

namespace FastTech.Domain.Interfaces.Infrastructure;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetById(Guid id, bool include = false, bool tracking = false);
    Task<User> GetByEmail(string email, bool include = false, bool tracking = false);
}