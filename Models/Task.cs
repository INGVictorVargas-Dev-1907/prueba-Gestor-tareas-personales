using System;
using System.Collections.Generic;

namespace GestorPersonalTareas.Models;

public partial class TaskItem
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; }
}
