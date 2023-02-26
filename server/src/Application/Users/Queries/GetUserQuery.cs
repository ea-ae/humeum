using System.ComponentModel.DataAnnotations;

using Application.Common.Interfaces;

using AutoMapper;

namespace Application.Users.Queries.GetUserQuery;

public record GetUserQuery : IQuery<UserDto> {
    [Required] public required int User { get; init; }
}

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto> {
    private readonly IApplicationUserService _userService;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IApplicationUserService userService, IMapper mapper) {
        _userService = userService;
        _mapper = mapper;
    }

    public Task<UserDto> Handle(GetUserQuery request, CancellationToken token = default) {
        var user = _userService.GetUserById(request.User);

        var userDto = _mapper.Map<UserDto>(user);
        return Task.Run(() => userDto);
    }
}
