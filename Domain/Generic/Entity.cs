using Flunt.Notifications;

namespace Produtos_api.Domain.Generic;

public abstract class Entity : Notifiable<Notification>
{
    public int Id { get;private set; }

   /* protected Entity(int id, string created_by, DateTime created_on, string edited_by, DateTime edited_on)
    {
        Id = id;
        CreatedBy= created_by;
        CreatedOn= created_on;
        EditedBy= edited_by;
        EditedOn= edited_on;

    }*/

    public string CreatedBy { get; protected set; }
    public DateTime CreatedOn { get; protected set; }
    public string EditedBy { get; protected set; }
    public DateTime EditedOn { get; protected set; }
}
