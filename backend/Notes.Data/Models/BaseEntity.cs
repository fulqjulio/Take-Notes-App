using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notes.Data.Models;

public class BaseEntity<TId> 
where TId : struct
{
    [Key]
    public TId Id {get; set;}
}