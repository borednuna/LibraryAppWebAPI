namespace LibraryAppWebAPI.Models;

using LibraryAppWebAPI.Enums;

public class Rack
{
    public int Id { get; set; }
    public int Floor { get; set; }
    public Section Section { get; set; }
    public int Capacity { get; set; }
    public DateTime? DeletedAt { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
