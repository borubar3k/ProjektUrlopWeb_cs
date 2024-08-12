using Microsoft.AspNetCore.Mvc;
using ProjektUrlopWeb.Data;
using ProjektUrlopWeb.Models.Entities;
using ProjektUrlopWeb.Models;
using NLog;
using Microsoft.AspNetCore.Authorization;
using ProjektUrlopWeb.TokenJwt;

namespace ProjektUrlopWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]

    public class userController : ControllerBase
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly AppDbContext context;
        public userController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("twojahistoria/{id:guid}")]
        public IActionResult GetHistory([FromRoute] Guid id)
        {
            try
            {
                var urlopy = context.Urlopy.Where(p => p.Pracownik.Id == id).ToList();
                if (urlopy == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(urlopy);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return BadRequest("Błąd przy pobieraniu historii.");
            }
        }
        [HttpGet]
        [Route("wezurlop/{id:guid}")]
        public IActionResult GetDni([FromRoute]Guid id)
        {
            try
            {
                var dniUrl = context.Pracownicy.Where(p => p.Id == id).Select(p => p.DniUrlopu).FirstOrDefault();
                return Ok(dniUrl);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return BadRequest("Błąd przy pobieraniu liczby wolnych dni.");
            }
        }

        [HttpPost]
        [Route("wezurlop/{id:guid}")]
        public IActionResult AddUrlop(AddUrlopDto addUrlopDto,[FromRoute] Guid id)
        {
            try
            {
                var pracownik = context.Pracownicy.Where(p => p.Id == id).FirstOrDefault();
                if (pracownik != null)
                {
                    int roznica = (addUrlopDto.DataZak - addUrlopDto.DataRozp).Days + 1;
                    if (roznica > 0 && roznica < pracownik.DniUrlopu && addUrlopDto.DataRozp <= addUrlopDto.DataZak)
                    {
                        var urlop = new Urlop()
                        {
                            Id = Guid.NewGuid(),
                            Pracownik = pracownik,
                            DataRozp = addUrlopDto.DataRozp,
                            DataZak = addUrlopDto.DataZak,
                            IloscDni = roznica,
                            Rodzaj = addUrlopDto.Rodzaj
                        };
                        pracownik.DniUrlopu -= roznica;
                        context.Urlopy.Add(urlop);
                        context.SaveChanges();
                        return Ok(urlop);
                    }
                    else
                    {
                        return BadRequest("Nieprawidłowa liczba dni.");
                    }
                }
                else
                {
                    return BadRequest("Nie znaleziono pracownika.");
                }
            }
            catch (Exception ex)
            { 
                logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("twojedane/{id:guid}")]
        public IActionResult GetPracownikById([FromRoute] Guid id)
        {
            try
            {
                var dane = context.Pracownicy.Where(p => p.Id == id).FirstOrDefault();
                if (dane == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(dane);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return BadRequest("Błąd przy pobieraniu danych.");
            }

        }
    }
}
