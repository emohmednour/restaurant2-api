namespace Restaurants.Application.Common;

public class PageResult<T>
{

    public PageResult(IEnumerable<T> items , int totalcount,int paganize, int pagenumber)
    {
        Items = items;
        TotalItemsCount = totalcount;
        TotalPages =(int)Math.Ceiling(totalcount/(double)paganize);
        ItemsFrom = paganize * (pagenumber-1) + 1;
        ItemsTo = ItemsFrom +paganize-1;

        
    }



    public IEnumerable<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int TotalItemsCount { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
}
