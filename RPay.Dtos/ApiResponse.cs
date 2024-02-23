namespace RPay.Dtos
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public ApiResponse(T data) {
            Success = true;
            Data = data;
        }

        public ApiResponse(string errorMessage)
        {
            Success = false;
            ErrorMessage = errorMessage;
        }
    }
}
