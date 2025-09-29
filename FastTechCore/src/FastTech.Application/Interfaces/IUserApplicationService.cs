using FastTech.Application.DataTransferObjects;

namespace FastTech.Application.Interfaces;

public interface IUserApplicationService
{
    Task<User> Add(BasicUser model);
}