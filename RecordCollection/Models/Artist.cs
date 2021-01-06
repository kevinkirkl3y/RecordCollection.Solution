using System.Collections.Generic;

namespace RecordCollection.Models
{
  public class Artist
  {
    public Artist()
    {
      this.Albums = new HashSet<ArtistAlbum>();
    }
    public int ArtistId { get; set; }
    public string ArtistName { get; set; }
    public virtual ICollection<ArtistAlbum> Albums { get; set; }

  }
}