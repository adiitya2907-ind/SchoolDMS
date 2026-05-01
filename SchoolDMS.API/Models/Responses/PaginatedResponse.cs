namespace SchoolDMS.API.Models.Responses
{
    public class PaginatedResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public PaginatedData<T>? Data { get; set; }
        public int StatusCode { get; set; }

        public static PaginatedResponse<T> SuccessResponse(IEnumerable<T> items, int pageNumber, int pageSize, int totalRecords, string message = "Fetch successful")
        {
            return new PaginatedResponse<T>
            {
                Success = true,
                Message = message,
                StatusCode = 200,
                Data = new PaginatedData<T>
                {
                    Items = items,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalRecords = totalRecords,
                    TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
                }
            };
        }
    }

    public class PaginatedData<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
    }
}
