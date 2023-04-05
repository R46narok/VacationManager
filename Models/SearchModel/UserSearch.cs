using System.Collections.Generic;

namespace Models.SearchModel;

public class UserSearch
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public string Team { get; set; }

    public List<User> Results { get; set; }
}