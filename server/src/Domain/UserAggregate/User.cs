using System.Net.Mail;

using Domain.Common;
using Domain.Common.Exceptions;
using Domain.UserAggregate.ValueObjects;

namespace Domain.UserAggregate;

/// <summary>
/// Users in this case are not persisted, but instead mapped onto by an authentication
/// service, either internal or external, that has its own inner entities for users.
/// </summary>
public class User : Entity {
    // public int ApplicationUserId { get; private set; }

    Username _username = null!;
    public string Username {
        get => _username.Value;
        private set => _username = new Username(value);
    }

    // public Username Username { get; private set; } = null!;

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

    // public User(Username username, string email) {}

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
