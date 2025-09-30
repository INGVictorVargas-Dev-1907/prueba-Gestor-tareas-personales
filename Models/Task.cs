using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public partial class TaskItem : IValidatableObject
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio")]
    [StringLength(100, ErrorMessage = "El título no puede superar los 100 caracteres")]
    public string Title { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errores = new List<ValidationResult>();

        // Verifica que no sea solo espacios o símbolos
        if (!Regex.IsMatch(Title ?? "", @"[a-zA-Z0-9]"))
        {
            errores.Add(new ValidationResult("El título debe contener al menos una letra o número", new[] { nameof(Title) }));
        }

        return errores;
    }
}
