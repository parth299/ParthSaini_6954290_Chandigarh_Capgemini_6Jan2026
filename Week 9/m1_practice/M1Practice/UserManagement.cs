static class UserManager
{
    public static (List<User> updated, List<User> inserted) CompareUsers(
        List<User> usersListInDB,
        List<User> newUsersList)
    {
        List<User> updated = new List<User>();
        List<User> inserted = new List<User>();

        foreach (var newUser in newUsersList)
        {
            var existing = usersListInDB.FirstOrDefault(u => u.Id == newUser.Id);

            if (existing != null)
            {
                existing.IdentityNumber = newUser.IdentityNumber;
                existing.FirstName = newUser.FirstName;
                existing.LastName = newUser.LastName;
                existing.Age = newUser.Age;
                existing.BirthDate = newUser.BirthDate;
                existing.Email = newUser.Email;
                existing.Gender = newUser.Gender;
                existing.Country = newUser.Country;
                existing.City = newUser.City;
                existing.Address = newUser.Address;
                existing.ZipCode = newUser.ZipCode;
                existing.PhoneNumber = newUser.PhoneNumber;
                existing.Department = newUser.Department;
                existing.Roles = newUser.Roles;
                existing.JoinDate = newUser.JoinDate;
                existing.Credit = newUser.Credit;
                existing.Status = newUser.Status;

                updated.Add(existing);
            }
            else
            {
                inserted.Add(newUser);
            }
        }

        return (updated, inserted);
    }
}