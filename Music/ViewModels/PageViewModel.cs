namespace Music.Common;

public class PageViewModel(int count, int pageNumber = 1, int pageSize = 1)
{
    public int PageNumber { get; set; } = pageNumber;
    public int TotalPage { get; set; } = (int)Math.Ceiling(count / (double)pageSize);

    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPage;
}