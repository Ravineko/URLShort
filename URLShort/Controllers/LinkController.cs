using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using URLShort.Services;
using URLShort.Models;
using URLShort.Dtos;
using System.Linq; 

namespace URLShort.Controllers
{
    public class LinkController : Controller
    {

        private readonly ShortenerDbContext _dbContext; 

        public LinkController( ShortenerDbContext dbContext) 
        {

            _dbContext = dbContext; 

        }

        [HttpGet("List")]
        public IEnumerable<LinkModel> GetLinks()
        {
            return _dbContext.Links.ToList();
        }
        [HttpGet("GetLinkInfo/{id}")]
        public IActionResult GetLinkInfo(int id)
        {
            var linkModel = _dbContext.Links.FirstOrDefault(l => l.Id == id);

            if (linkModel != null)
            {
                return Ok(new
                {
                    originalLink = linkModel.OriginalLink,
                    shortenedLink = linkModel.ShortenedLink,
                    dateAdded = linkModel.DateAdded 
                });
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("Add")]
        public IActionResult AddLink([FromBody] LinkDto linkDto)
        {
            if (linkDto == null || string.IsNullOrEmpty(linkDto.OriginalLink))
            {
                return BadRequest("Original link cannot be empty");
            }

           
            string shortenedLink = ShortenLinkGenerator.ShortenLink(linkDto.OriginalLink);

            
            var newLink = new LinkModel
            {
                OriginalLink = linkDto.OriginalLink,
                ShortenedLink = shortenedLink,
                DateAdded = DateTime.Now
            };

           
            _dbContext.Links.Add(newLink);
            _dbContext.SaveChanges();

            return Ok(newLink); 
        }
        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteLink(int id)
        {
            try
            {
                var linkToDelete = _dbContext.Links.FirstOrDefault(l => l.Id == id);

                if (linkToDelete == null)
                {
                    return NotFound($"Link with ID {id} not found");
                }

                _dbContext.Links.Remove(linkToDelete);
                _dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                
                return BadRequest($"Failed to delete link: {ex.Message}");
            }
        }
        [HttpPost("Redirect")]
        public IActionResult Redirect([FromBody] RedirectDto redirectDto)
        {
            var linkModel = _dbContext.Links.FirstOrDefault(l => l.Id == redirectDto.Id);

            if (linkModel != null)
            {
                return Ok(new { redirectUrl = linkModel.OriginalLink });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
