using System.Collections.Generic;
public class StatusController
{
    private HashSet<StatusType> _statuses = new HashSet<StatusType>();

    public bool Has(StatusType status) => _statuses.Contains(status);

    public void Add(StatusType status) => _statuses.Add(status);

    public void Remove(StatusType status) => _statuses.Remove(status);
}
