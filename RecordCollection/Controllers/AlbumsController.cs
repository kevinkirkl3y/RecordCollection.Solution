using Microsoft.AspNetCore.Mvc;
using RecordCollection.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecordCollection.Controllers
{
  public class AlbumsController : Controller
  {
    private readonly RecordCollectionContext _db;
    public AlbumsController(RecordCollectionContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      return View(_db.Albums.ToList());
    }

    public ActionResult Details(int id)
    {
      var thisAlbum = _db.Albums
      .Include(album => album.Artists) 
      .ThenInclude(join => join.Artist)
      .FirstOrDefault(album => album.AlbumId == id);
      return View(thisAlbum);
    }
    public ActionResult Create()
    {
      ViewBag.ArtistId = new SelectList(_db.Artists, "ArtistId", "ArtistName");
      return View();
    }
    [HttpPost]
    public ActionResult Create(Album album, int ArtistId)
    {
      _db.Albums.Add(album);
      if (ArtistId != 0)
      {
        _db.ArtistAlbum.Add(new ArtistAlbum() { ArtistId = ArtistId, AlbumId = album.AlbumId});
      }
      _db.Entry(album).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Edit(int id)
    {
      var thisAlbum = _db.Albums.FirstOrDefault(albums => albums.AlbumId == id);
      ViewBag.ArtistId = new SelectList(_db.Artists, "ArtistId", "Artists");
      return View(thisAlbum);
    }
    [HttpPost]
    public ActionResult Edit(Album album, int ArtistId)
    {
      if (ArtistId != 0)
      {
        _db.ArtistAlbum.Add(new ArtistAlbum() { ArtistId = ArtistId, AlbumId = album.AlbumId});
      }
      _db.Entry(album).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult AddArtist(int id)
    {
      var thisAlbum = _db.Albums.FirstOrDefault(albums => albums.AlbumId == id);
      ViewBag.ArtistId = new SelectList(_db.Artists, "ArtistId", "ArtistName");
      return View(thisAlbum);
    }
    [HttpPost]
    public ActionResult AddArtist(Album album, int ArtistId)
    {
      if (ArtistId != 0)
      {
        _db.ArtistAlbum.Add(new ArtistAlbum() { ArtistId = ArtistId, AlbumId = album.AlbumId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      var thisAlbum = _db.Albums.FirstOrDefault(albums => albums.AlbumId == id);
      return View(thisAlbum);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisAlbum = _db.Albums.FirstOrDefault(albums => albums.AlbumId == id);
      _db.Albums.Remove(thisAlbum);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteArtist(int joinId)
    {
      var joinEntry = _db.ArtistAlbum.FirstOrDefault(entry => entry.ArtistAlbumId == joinId);
      _db.ArtistAlbum.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}