namespace Utilities.MostVisitedPages;

public class MostVisitedPageModel
{
    public string Id { get; set; }

    public int UserId { get; set; }

    public int MenuId { get; set; }

    public string UserName { get; set; }

    public DateTime LastVisitDay { get; set; }

    public string MenuName { get; set; }

    public string MenuURL { get; set; }

    public string Title { get; set; }

    public ulong Count { get; set; } = 1;

    public bool IsPinned { get; set; }

    public int Position { get; set; }
}