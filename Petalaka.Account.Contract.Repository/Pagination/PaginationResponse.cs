using Petalaka.Account.Contract.Repository.Base;

namespace Petalaka.Account.Contract.Repository.Pagination;

public class PaginationResponse<T> : BaseResponse<T> where T : class
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public IList<T> Data { get; set; }
    public PaginationResponse(int statusCode, string message, PaginationResponse<T> paginationResponse) : base(statusCode, message)
    {
        PageNumber = paginationResponse.PageNumber;
        PageSize = paginationResponse.PageSize;
        TotalPages = paginationResponse.TotalPages;
        TotalRecords = paginationResponse.TotalRecords;
        Data = paginationResponse.Data;
    }
    public PaginationResponse(IList<T> data, int pageNumber, int pageSize, int totalRecords)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        Data = data;
    }
}