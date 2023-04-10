using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS_Project_mvc.DAL;
using LMS_Project_mvc.Models;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Collections;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace LMS_Project_mvc.Controllers
{
    public class MediaController : Controller
    {
        private readonly MediaDB _context;

        public MediaController(MediaDB context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Search([Bind("CategoryId, MTypeId, Title, FileName, IsArchive")] FilterMedia filterMedia)
        {
            ViewData["MediaTypeList"] = new SelectList(_context.MediaTypes, "MTypeId", "TypeName");
            ViewData["CategoryList"] = new SelectList(_context.MediaCategories, "CategoryId", "CategoryName");
            List<Media> mediaList = await _context.Medias.ToListAsync();
            List<Media> mediaListFiltered = new List<Media>();


            foreach (Media media in mediaList)
            {
                if (
                    (filterMedia.Title == null || media.Title == filterMedia.Title) &&
                    (filterMedia.FileName == null || media.FileName == filterMedia.FileName) &&
                    (filterMedia.CategoryId == null || media.CategoryId == filterMedia.CategoryId) &&
                    (filterMedia.MTypeId == null || media.MTypeId == filterMedia.MTypeId) &&
                    (filterMedia.IsArchive = false)
                    )
                {
                    mediaListFiltered.Add(media);
                }
            }

            TempData["Medias"] = JsonConvert.SerializeObject(mediaListFiltered);

            return RedirectToAction(nameof(Index));
        }

        // GET: Media
        public async Task<IActionResult> Index()
        {
            FilterMedia Filter = new FilterMedia();
            ViewData["MediaTypeList"] = new SelectList(_context.MediaTypes, "MTypeId", "TypeName");
            ViewData["CategoryList"] = new SelectList(_context.MediaCategories, "CategoryId", "CategoryName");



            if (TempData["Medias"] != null)
            {
                ViewData["Medias"] = JsonConvert.DeserializeObject<List<Media>>(TempData["Medias"].ToString());
                TempData["Medias"] = null;
            }
            else
            {
                ViewData["Medias"] = await _context.Medias.ToListAsync();
            }
                

              return _context.Medias != null ? 
                          View(Filter) :
                          Problem("Entity set 'MediaDB.medias'  is null.");
        }

        // GET: Media/Details/5
        public async Task<IActionResult> Details(int? id)
        {


            if (id == null || _context.Medias == null)
            {
                return NotFound();
            }

            var media = await _context.Medias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (media == null)
            {
                return NotFound();
            }

            media.Category = await _context.MediaCategories.FirstOrDefaultAsync(m => m.CategoryId == media.CategoryId);
            
            media.MType = await _context.MediaTypes.FirstOrDefaultAsync(m => m.MTypeId == media.MTypeId);

            return View(media);
        }

        // GET: Media/Create
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.MediaCategories, "CategoryId", "CategoryName");

            ViewData["MTypes"] = new SelectList(_context.MediaTypes, "MTypeId", "TypeName");

            return View();
        }

        // POST: Media/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,MTypeId,FileName,FileSize,FileSizeHuman,IsArchive,Time,CategoryId,Description,FilePath")] Media media, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                media.FilePath = "uploads/" + file.FileName;

                FileStream fs = new FileStream("wwwroot/uploads/" + file.FileName, FileMode.Create);
                file.CopyTo(fs);
                fs.Close();
                media.FileSize = file.Length;
                media.FileName = file.FileName;
                media.FileSizeHuman = BytesToString(file.Length);
                media.IsArchive = false;

                _context.Add(media);

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(media);
        }

        static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        // GET: Media/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Categories"] = new SelectList(_context.MediaCategories, "CategoryId", "CategoryName");

            ViewData["MTypes"] = new SelectList(_context.MediaTypes, "MTypeId", "TypeName");

            if (id == null || _context.Medias == null)
            {
                return NotFound();
            }

            var media = await _context.Medias.FindAsync(id);
            if (media == null)
            {
                return NotFound();
            }
            return View(media);
        }

        // POST: Media/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,FileName,FileSize,Time,Description,FilePath")] Media media)
        {
            if (id != media.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(media);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaExists(media.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(media);
        }

        // GET: Media/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medias == null)
            {
                return NotFound();
            }

            var media = await _context.Medias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // POST: Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medias == null)
            {
                return Problem("Entity set 'MediaDB.medias'  is null.");
            }
            var media = await _context.Medias.FindAsync(id);
            if (media != null)
            {
                _context.Medias.Remove(media);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaExists(int id)
        {
          return (_context.Medias?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        // GET: Media/Archive/5
        public async Task<IActionResult> Archive(int? id)
        {


            if (id == null || _context.Medias == null)
            {
                return NotFound();
            }

            var media = await _context.Medias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // POST: Media/Archive/5
        [HttpPost, ActionName("Archive")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchiveConfirmed(int id)
        {
            if (_context.Medias == null)
            {
                return Problem("Entity set 'MediaDB.medias'  is null.");
            }
            var media = await _context.Medias.FindAsync(id);
            if (media != null)
            {
                media.IsArchive = true;
                _context.Update(media);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
