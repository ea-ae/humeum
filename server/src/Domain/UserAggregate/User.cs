using System.Net.Mail;

using Domain.Common.Exceptions;
using Domain.Common.Models;
using Domain.ProfileAggregate;
using Domain.UserAggregate.ValueObjects;

namespace Domain.UserAggregate;

/// <summary>
/// Users in this case are not persisted, but instead mapped onto by an authentication
/// service, either internal or external, that has its own inner entities for users.
/// </summary>
public class User : Entity {
    Username _username = null!;
    public string Username {
        get => _username.Value;
        private set => _username = new Username(value);
    }

    string? _email;
    public string? Email {
        get => _email;
        private set {
            if (value != null) {
                try {
                    _ = new MailAddress(value); // validation
                } catch (Exception e) {
                    throw new DomainException(e);
                }
            }
            _email = value;
        }
    }

    public IReadOnlyCollection<Profile> Profiles { get; private set; } = null!;

    public User(int id, string username, string email, IReadOnlyCollection<Profile> profiles) : this(id, username, email) {
        Profiles = profiles;
    }

    public User(int id, string username, string email) : this(id, username) {
        Email = email;
    }

    public User(string username, string email) : this(username) {
        Email = email;
    }

    public User(int id, string username) : this(username) {
        Id = id;
    }

    public User(string username) {
        Username = username;
    }

    User() { }
}
