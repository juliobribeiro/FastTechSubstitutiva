using FastTechKitchen.Application.DataTransferObjects;

namespace FastTechKitchen.Application.Interfaces;

public interface IUserApplicationService
{
    Task<User> Add(BasicUser model);
}