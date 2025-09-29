using AutoMapper;
using FastTechKitchen.Application.DataTransferObjects;
using FastTechKitchen.Application.Interfaces;
using FastTechKitchen.Domain.Interfaces;
using EN = FastTechKitchen.Domain.Entities;

namespace FastTechKitchen.Application.Services;

public class UserApplicationService(
    IUserService userService,
    IMapper mapper) : IUserApplicationService
{
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;

    public async Task<User> Add(BasicUser model)
    {
        var user = _mapper.Map<EN.User>(model);

        user = await _userService.Add(user);

        return _mapper.Map<User>(user);
    }
}
