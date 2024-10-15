using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contactly.Controllers.Models;
using Contactly.Controllers.Models.Domain;
using Contactly.DAL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contactly.ControllersBase
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : Controller
    {
        private readonly ContactlyDbContext _dbContext;
        public ContactsController(ContactlyDbContext contactlyDbContext)
        {
            _dbContext = contactlyDbContext;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAllContacts()
        {
            var contacts =  _dbContext.Contacts.ToList() ;
            return Ok(contacts); 
        }

        // POST api/values
        [HttpPost]
        public IActionResult AddContact([FromBody] AddContactRequestDto requestDto)
        {
            var domainModelContact = new Contact
            {
                Id = Guid.NewGuid(),
                Name = requestDto.Name,
                Email = requestDto.Email,
                Phone = requestDto.Phone,
                Favourite = requestDto.Favourite
            };

            _dbContext.Contacts.Add(domainModelContact);
            _dbContext.SaveChanges();

            return Ok(domainModelContact);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteContact(Guid id)
        {
            var contact = _dbContext.Contacts.Find(id);
            if(contact is not null)
            {
                _dbContext.Contacts.Remove(contact);
                _dbContext.SaveChanges();
            }
            return Ok();
        }


 
    }
}

