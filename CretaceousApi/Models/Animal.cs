using System.ComponentModel.DataAnnotations;

namespace CretaceousApi.Models;
public class Animal
{
    public int AnimalId { get; set; }

    [Required]
    [StringLength(20)]
    public string Name { get; set; }

    [Required]
    public string Species { get; set; }

    [Required]
    [Range(0, 200, ErrorMessage = "Age must be between 0 and 200")]
    public int Age { get; set; }

    // Our Animal class will represent the creatures of the Cretaceous period. Add other animal class properties if you want to.
    
}