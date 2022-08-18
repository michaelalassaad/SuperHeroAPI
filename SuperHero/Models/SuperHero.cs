using System.ComponentModel.DataAnnotations;

namespace SuperHero.Models;

public class SuperHero
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Place { get; set; }
}