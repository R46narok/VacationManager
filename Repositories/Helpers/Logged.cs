using Models;

namespace Repositories.Helpers;

public static class Logged
{
    public static User User { get; set; }

    public static bool CEOAuth()
    {
        if (User is not null)
            if (User.RoleId == "1")
                return true;
        return false;
    }
}