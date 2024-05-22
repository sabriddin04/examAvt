using System.Net;

namespace Domain.Responses;

public class PagedResponse<T>:Response<T>
{


    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecord { get; set; }
    public int TotalPage { get; set; }

    public PagedResponse(T data, int pageNumber, int pageSize, int totalRecord): base(data)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecord= totalRecord;
        TotalPage = (int)Math.Ceiling(totalRecord / (float)PageNumber);
    }
    
    
   

    public PagedResponse(T data, HttpStatusCode statusCode, string error) : base(data, statusCode, error)
    {
    }

    public PagedResponse(HttpStatusCode statusCode, List<string> errors) : base(statusCode, errors)
    {
    }

    public PagedResponse(HttpStatusCode statusCode, string error) : base(statusCode, error)
    {
    }
}