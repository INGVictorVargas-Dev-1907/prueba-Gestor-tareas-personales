namespace GestorPersonalTareas.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public String Message { get; set; }
        public T? Data { get; set; }
    }
}
