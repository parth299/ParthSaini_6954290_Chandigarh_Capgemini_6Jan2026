class User : IUser
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int IncorrectAttempt { get; set; }
    public string Location { get; set; }

    public User(int id, string email, string password, string location)
    {
        Id = id;
        Email = email;
        Password = password;
        Location = location;
        IncorrectAttempt = 0;
    }
}

class ApplicationAuthState : IApplicationAuthState
{
    public List<IUser> RegisteredUsers { get; set; }
    public List<IUser> UsersLoggedIn { get; set; }
    public List<string> AllowedLocations { get; set; }

    public ApplicationAuthState(List<string> allowedLocations)
    {
        RegisteredUsers = new List<IUser>();
        UsersLoggedIn = new List<IUser>();
        AllowedLocations = allowedLocations;
    }

    public string Register(IUser user)
    {
        if (RegisteredUsers.Any(u => u.Email == user.Email))
            return $"{user.Email} is already registered!";

        RegisteredUsers.Add(user);
        return $"{user.Email} registered successfully!";
    }

    public string Login(IUser user)
    {
        var registered = RegisteredUsers.FirstOrDefault(u => u.Email == user.Email);

        if (registered == null)
            return $"{user.Email} is not registered!";

        if (registered.IncorrectAttempt >= 3)
            return $"{user.Email} is blocked!";

        if (!AllowedLocations.Contains(user.Location))
            return $"{user.Email} is not allowed to login from this location!";

        if (registered.Password != user.Password)
        {
            registered.IncorrectAttempt++;
            return $"{user.Email} password is incorrect!";
        }

        if (UsersLoggedIn.Any(u => u.Email == user.Email))
        {
            var logged = UsersLoggedIn.First(u => u.Email == user.Email);

            if (logged.Location != user.Location)
                return $"{user.Email} is already logged in from another location!";

            return $"{user.Email} is already logged in!";
        }

        registered.Location = user.Location;
        registered.IncorrectAttempt = 0;
        UsersLoggedIn.Add(registered);

        return $"{user.Email} logged in successfully!";
    }

    public string Logout(IUser user)
    {
        var logged = UsersLoggedIn.FirstOrDefault(u => u.Email == user.Email);

        if (logged == null)
            return $"{user.Email} is not logged in!";

        UsersLoggedIn.Remove(logged);
        return $"{user.Email} logged out successfully!";
    }
}