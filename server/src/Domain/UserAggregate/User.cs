using System.Net.Mail;

using Domain.Common;
using Domain.UserAggregate.ValueObjects;

namespace Domain.UserAggregate;

public class User : TimestampedEntity {
    Username _username = null!;
    public Username Username {
        get => _username;
        private set => _username = value;
    }

    // public Username Username { get; private set; } = null!;

    string _email = null!;
    public string Email {
        get => _email;
        private set {
            _ = new MailAddress(value); // validation
            _email = value;
        }
    }

    // public User(Username username, string email)
    public User(string username, string email) {
        Username = new Username(username);
        Email = email;
    }

    User() { }
}
