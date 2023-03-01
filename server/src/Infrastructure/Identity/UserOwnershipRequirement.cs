using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Identity;

public class UserOwnershipRequirement : IAuthorizationRequirement { }
