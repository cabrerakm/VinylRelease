using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VinylRelease.Data;
using VinylRelease.Models;

namespace VinylRelease.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly VinylReleaseContext _context;

        public ArtistsController(VinylReleaseContext context)
        {
            _context = context;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<Models.Response<Artist>>> GetArtists()
        {
            var data = await _context.Artists.ToListAsync();

            var response = new Models.Response<Artist>();
            
            response.StatusCode = 200;
            response.StatusDescription = "Successfully fetched all artists";
            response.Data = data;

            return response;
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Response<Artist>>> GetArtist(long id)
        {
            var response = new Models.Response<Artist>();

            if (!ArtistExists(id))
            {
                response.StatusCode = 404;
                response.StatusDescription = "Artist Id not found";
                return NotFound(response);
            }

            var artist = await _context.Artists
                .Where(artist => artist.ArtistId == id)
                .Select(artist => new Artist
                {
                    ArtistId = artist.ArtistId,
                    ArtistName = artist.ArtistName,
                    ArtistDesc = artist.ArtistDesc,

                    Masters = artist.Masters.Select(master => new Master
                    {
                        MasterId = master.MasterId,
                        MasterName = master.MasterName,
                        MasterYear = master.MasterYear
                    })
                    .ToList()

                }).ToListAsync();
                //}).SingleAsync();

            response.StatusCode = 200;
            response.StatusDescription = "Successful fetch of artist";
            response.Data = artist;

            return response;
        }
        
       // Commenting out the PUT Request since it involves more work
        /*
        
       // PUT: api/Artists/5
       // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       // TODO
       [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(long id, Artist artist)
        {
            if (id != artist.ArtistId)
            {
                return BadRequest();
            }

            _context.Entry(artist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }        
        */


        // POST: api/Artists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Models.Response<Artist>>> PostArtist(Artist artist)
        {
            var response = new Models.Response<Artist>();

            if (artist.ArtistName == null)
            {
                response.StatusCode = 400;
                response.StatusDescription = "Bad request; Need to add in artist's Name";
                return BadRequest(response);
            }
            if (artist.ArtistId != 0)
            {
                response.StatusCode = 400;
                response.StatusDescription = "Bad request; Do not provide artist id";
                return BadRequest(response);
            }
            
            // TODO: validate the masters within the artist
            

            Artist a = _context.Artists.Add(artist).Entity;
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetArtist", new { id = artist.ArtistId }, artist);

            response.StatusCode = 201;
            response.StatusDescription = "Added the artist";
            
            var data = new List<Artist>();
            data.Add(a);
            response.Data = data;

            return Created("GetArtist", response);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Response<Artist>>> DeleteArtist(long id)
        {
            var response = new Models.Response<Artist>();

            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                response.StatusCode = 404;
                response.StatusDescription = "Artist not found";
                return NotFound(response);
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            response.StatusCode = 200;
            response.StatusDescription = "Artist deleted";
            return response;
        }

        private bool ArtistExists(long id)
        {
            return _context.Artists.Any(e => e.ArtistId == id);
        }
    }
}
