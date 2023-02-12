using System.Net.Mail;

using Domain.Common;
using Domain.UserAggregate.ValueObjects;

namespace Domain.UserAggregate;

public class User : Entity {
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
                _ = new MailAddress(value); // validation
            }
            _email = value;
        }
    }

    // public User(Username username, string email) {}

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
