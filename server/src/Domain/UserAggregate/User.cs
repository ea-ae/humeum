using System.Net.Mail;

using Domain.Common;
using Domain.UserAggregate.ValueObjects;

namespace Domain.UserAggregate;

public class User : TimestampedEntity {
    Username _username = null!;
    public string Username {
        get => _username.Value;
        set => _username = new Username(value);
    }

    // public Username Username { get; private set; } = null!;

    private MailAddress _email = null!;
    public string Email {
        get => _email.Address;
        private set => new MailAddress(value);
    }

    // public User(Username username, string email)
    public User(string username, string email) {
        Username = username;
        Email = email;
    }

    private User() { }
}
