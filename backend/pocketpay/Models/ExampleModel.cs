using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Example")]
public class ExampleModel
{
    [Key]
    public int Id {get; set;}
    public String? Name {get; set;}
}