using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Persons.Data.Entities;
using Persons.Data.Interfaces;
using Persons.Api.MiddlewareServices;
using Microsoft.AspNetCore.Http;
using System.Net;
using Persons.Data.Models;
using Persons.Data.Helpers;
using Microsoft.AspNetCore.Localization;
using System.Threading;

namespace Persons.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : Controller
    {

        private readonly IPersonServices _personService;
        private readonly LinkGenerator _linkGanarator;
        private readonly ILoggerService _logger;
        private readonly IFileService _fileService;
        private readonly IRelatedPersonServices _relatedPersonServices;

        private readonly SharedViewLocalizer _localizer;

        public PersonsController(IPersonServices personService, LinkGenerator linkGanarator, ILoggerService logger,
            IFileService fileService, IRelatedPersonServices relatedPersonServices, SharedViewLocalizer localizer)
        {
            _personService = personService;
            _linkGanarator = linkGanarator;
            _logger = logger;
            _fileService = fileService;
            _relatedPersonServices = relatedPersonServices;
            _localizer = localizer;
            var test = _localizer["Email"].Value;
        }

        [HttpGet("test")]
        public string Bla()
        {
            return _localizer["Email"].Value;
        }

        [HttpGet("set-culture")]
        public IActionResult SetCulture(string path, bool ishttps, string lang = "en-US")
        {
            string culture = lang;
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            ViewData["Message"] = "Culture set to " + culture;
            if (path != null)
            {
                var url = ishttps ? @"https://" + path : @"http://" + path;
                return Redirect(url);
            }
            return Json("test");
        }



        #region Persons
        [HttpGet]
        [Route("AllPersons")]
        public async Task<IActionResult> GetAllPersonsAsync([FromQuery]PagedParameters pagedParameters)
        {
            var model = await _personService.GetAllPersonsAsync(pagedParameters);
            //var result = _mapper.Map<CatalogsEntity>(catalogs);
            return Ok(model);
        }

        [HttpGet("{personId}")]
        public async Task<IActionResult> GetPersonByIdAsync(int personId)
        {
            var model = await _relatedPersonServices.GetPersonByIdAsync(personId);
            if (model == null) return NotFound();
            /*var test = nameof(model.PrivateNumber);*//* = _localizer["PrivateNumber"].Value;*/
            return Ok(model);
        }

        [HttpPost]
        [Route("AddPerson")]
        [ModelStateFilter]
        public async Task<IActionResult> CreatePersonAsync(PersonsModel person)
        {
            if (_personService.CheckIfNewPersonExit(person))
            {
                var model = await _personService.CreatePersonAsync(person);
                var location = _linkGanarator.GetPathByAction("GetPersonByIdAsync", "Persons", new { personId = model.PersonId });
                return Created(location, model);
            }
            else
            {
                return BadRequest("person has already been added");
            }
        }


        [HttpPut]
        [Route("EditPerson/{personId}")]
        [ModelStateFilter]
        public async Task<ActionResult<PersonsModel>> EditPersonAsync(int personId, PersonsModel person)
        {
            if (_personService.CheckIfOldPersonExit(personId, person))
            {
                var model = await _personService.EditPersonAsync(personId, person);
                var location = _linkGanarator.GetPathByAction("GetPersonByIdAsync", "Persons", new { personId = model.PersonId });
                return model;
            }
            else
            {
                return BadRequest("person has already been added");
            }
        }



        [HttpPut]
        [Route("{personId}/EditAddress/{addressId}")]
        [ModelStateFilter]
        public async Task<ActionResult<PersonsModel>> EditPersonAddressAsync(int personId, AddressesModel address, int addressId)
        {

                var model = await _personService.EditPersonAddressAsync(personId, address, addressId);
                var location = _linkGanarator.GetPathByAction("GetPersonByIdAsync", "Persons", new { personId = model.PersonId });
                return model;
        }

        [HttpDelete]
        [Route("DeletePerson/{personId}")]
        public async Task<IActionResult> DeletePersonAsync(int personId)
        {
          
            await _personService.DeletePersonAsync(personId);
            return Ok();
        }
        #endregion

        #region Files

        [HttpPost]
        [Route("{personId}/AddFile")]
        [ModelStateFilter]
        public async Task<IActionResult> AddPersonFileAsync(int personId, IFormFile file)
        {
            await _fileService.UploadFileAsync(personId, file);
            var model = await _relatedPersonServices.GetPersonByIdAsync(personId);
            var location = _linkGanarator.GetPathByAction("GetPersonByIdAsync", "Persons", new { personId = model.PersonId });
            return Created(location, model);
        }

        [HttpPost]
        [Route("{personId}/EditFile")]
        [ModelStateFilter]
        public async Task<IActionResult> EditPersonFileAsync(int personId, IFormFile file)
        {
            await _fileService.EditFileAsync(personId, file);
            var model = await _relatedPersonServices.GetPersonByIdAsync(personId);
            var location = _linkGanarator.GetPathByAction("GetPersonByIdAsync", "Persons", new { personId = model.PersonId });
            return Created(location, model);
        }

        [HttpDelete("{personId}/DeleteFiles/{fileId}")]
        public async Task<IActionResult> RemovePersonFileAsync(int personId, int fileId)
        {
            await _fileService.DeleteFileAsync(personId, fileId);
            var model = await _relatedPersonServices.GetPersonByIdAsync(personId);
            var location = _linkGanarator.GetPathByAction("GetPersonByIdAsync", "Persons", new { personId = model.PersonId });
            return Created(location, model);
        }

        #endregion

        #region RelatedPersons

        [HttpPost]
        [Route("{personId}/AddRelativePerson/{RelativePersonId}")]
        [ModelStateFilter]
        public async Task<IActionResult> CreateRelativePersonAsync(int personId, PersonTypesModel personTypeModel, int relativePersonId)
        {
            if (_relatedPersonServices.checkIfRelatedPersonExist(personId, relativePersonId))
            {
                var model = await _relatedPersonServices.AddRelatedPersonAsync(personId, personTypeModel, relativePersonId);
                var location = _linkGanarator.GetPathByAction("GetPersonByIdAsync", "Persons", new { personId = model.PersonId });
                return Created(location, model);
            }
            else
            {
                return BadRequest("Related person already added");
            }
        }

        [HttpDelete("{personId}/DeleteRelated/{relativePersonId}")]
        public async Task<IActionResult> DeleteRelatedPersonFileAsync(int personId, int relativePersonId)
        {
            await _relatedPersonServices.DeleteRelatedPersonAsync(personId, relativePersonId);
            var model = await _relatedPersonServices.GetPersonByIdAsync(personId);
            var location = _linkGanarator.GetPathByAction("GetPersonByIdAsync", "Persons", new { personId = model.PersonId });
            return Created(location, model);
        }


        [HttpGet("{personId}/RelatedPersons")]
        public async Task<IActionResult> GetRelatedPersonsByIdAsync(int personId)
        {
            var model = await _relatedPersonServices.GetRelatedPersonsByIdAsync(personId);
            if (model == null) return NotFound();
            return Ok(model);
        }

        #endregion

    }
}