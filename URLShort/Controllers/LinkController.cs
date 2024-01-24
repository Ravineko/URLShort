using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using URLShort.Services;
using URLShort.Models;
using URLShort.Dtos;
using System.Linq; // Додайте цей імпорт для методів розширення LINQ

namespace URLShort.Controllers
{
    public class LinkController : Controller
    {

        private readonly ShortenerDbContext _dbContext; // Додайте об'єкт контексту бази даних

        public LinkController( ShortenerDbContext dbContext) // Змініть "YourDbContext" на ваш реальний клас контексту бази даних
        {

            _dbContext = dbContext; // Ініціалізуйте об'єкт контексту бази даних

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
                    dateAdded = linkModel.DateAdded // Припустимо, що у вашій моделі є властивість DateAdded
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

            // Генеруємо скорочене посилання
            string shortenedLink = ShortenLinkGenerator.ShortenLink(linkDto.OriginalLink);

            // Створюємо новий об'єкт LinkModel
            var newLink = new LinkModel
            {
                OriginalLink = linkDto.OriginalLink,
                ShortenedLink = shortenedLink,
                DateAdded = DateTime.Now
            };

            // Додаємо новий об'єкт до бази даних
            _dbContext.Links.Add(newLink);
            _dbContext.SaveChanges();

            return Ok(newLink); // Повертаємо створений об'єкт, якщо потрібно
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
                // Обробка помилки
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
