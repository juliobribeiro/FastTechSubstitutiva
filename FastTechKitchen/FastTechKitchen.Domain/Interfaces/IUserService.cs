using FastTechKitchen.Domain.Entities;

namespace FastTechKitchen.Domain.Interfaces;

public interface IUserService : IBaseService<User>
{
    Task<User> GetById(Guid id);
    Task<User> GetByEmail(string email);
}