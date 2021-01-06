using Microsoft.EntityFrameworkCore;

namespace RecordCollection.Models
{
  public class RecordCollectionContext : DbContext
  {
    public virtual DbSet<Album> Albums { get; set; }
    public virtual DbSet<Artist> Artists { get; set; }

    public RecordCollectionContext(DbContextOptions options) : base(options) { }
    
  }
}