using System.ComponentModel.DataAnnotations;

namespace CommandsService.Models;

public class Platform
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ExternalID { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    public ICollection<Command> Commands { get; set; } = new List<Command>();
}