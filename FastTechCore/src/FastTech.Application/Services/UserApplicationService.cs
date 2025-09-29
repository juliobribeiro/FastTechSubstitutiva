using AutoMapper;
using FastTech.Application.DataTransferObjects;
using FastTech.Application.Interfaces;
using FastTech.Domain.Interfaces;
using EN = FastTech.Domain.Entities;

namespace FastTech.Application.Services;

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
