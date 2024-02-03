namespace EcommerceWeb.Utilities
{
    public class ApiResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public List<string> Errors { get; set; }

        public T Data { get; set; }

        public PaginationMetadata? Metadata { get; set; }

    }
}
