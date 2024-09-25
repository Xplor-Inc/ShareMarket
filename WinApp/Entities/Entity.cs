namespace ShareMarket.WinApp.Entities;

public class Entity
{
    public long             Id          { get; set; }
    public long             CreatedById { get; set; }
    public DateTimeOffset   CreatedOn   { get; set; }
}
public class Auditable : Entity
{
    public long?            UpdatedById { get; set; }
    public DateTimeOffset?  UpdatedOn   { get; set; }
    public long?            DeletedById { get; set; }
    public DateTimeOffset?  DeletedOn   { get; set; }

}