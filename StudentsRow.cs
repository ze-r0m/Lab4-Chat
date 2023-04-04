using Postgrest.Attributes;
using Postgrest.Models;

namespace Lab4;

// Класс представляющий запись в таблице Students
[Table("Students")]
public class StudentsRow : BaseModel
{
    [PrimaryKey("id")] public int Id { get; set; }
    [Column("name")] public string Name { get; set; } = string.Empty;
    [Column("surname")] public string Surname { get; set; } = string.Empty;
    [Column("email")] public string Email { get; set; } = string.Empty;

    // Текстовое представление строки таблицы
    public override string ToString()
    {
        return $"{Id} {Name} {Surname} {Email}";
    }
}