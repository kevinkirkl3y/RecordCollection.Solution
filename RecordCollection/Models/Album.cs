using System.Collections.Generic;

namespace RecordCollection.Models
{
  public class Album
  {
    // public Album()
    // {
    //   this.Artist = new HashSet<ArtistAlbum>();
    // }
    public int AlbumId { get; set; }
    public string AlbumName { get; set; }
    public string PressNumber {get; set; }
  }
}