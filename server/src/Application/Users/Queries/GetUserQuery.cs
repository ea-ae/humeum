using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using AutoMapper;

using Shared.Interfaces;
using Shared.Models;

namespace Application.Users.Queries;

public record GetUserQuery : IQuery<IResult<UserDto, IBaseException>> {
    [Required] public required int User { get; init; }
}

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, IResult<UserDto, IBaseException>> {
    private readonly IApplicationUserService _userService;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IApplicationUserService userService, IMapper mapper) {
        _userService = userService;
        _mapper = mapper;
    }

    public Task<IResult<UserDto, IBaseException>> Handle(GetUserQuery request, CancellationToken token = default) {
        var userResult = _userService.GetUserById(request.User);
        var userDto = userResult.Then(_mapper.MapToResult<UserDto>);

        return Task.FromResult(userDto);
    }
}
