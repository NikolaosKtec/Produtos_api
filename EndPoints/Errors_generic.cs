using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;


namespace Produtos_api.EndPoints;

public static class Errors_generic
{
    public static Dictionary<string, string[]> convertToProblemsDetails(this IReadOnlyCollection<Notification> notifications)
    {
        return notifications
       .GroupBy(g => g.Key)
       .ToDictionary(g => g.Key, g => g.Select(x => x.Message)
           .ToArray());
    }


    public static Dictionary<string, string[]> convertToProblemsDetails(this IEnumerable<IdentityError> errors)
    {
        return errors.GroupBy(g => g.Code)
            .ToDictionary(g => g.Key, g
                => g.Select(x => x.Description).ToArray());
    }
}
