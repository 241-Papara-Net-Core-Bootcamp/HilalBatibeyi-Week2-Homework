using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaSecondWeek.Attributes;
using PaparaSecondWeek.Models;
using PaparaSecondWeek.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace PaparaSecondWeek.Controllers
{
    //[ValidateModelState]  
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        public static List<Models.Owner> AllOwners = new List<Models.Owner>()
        {
                new Models.Owner { Id = 1, Name = "Hilal", Surname="Batibeyi", Date=DateTime.Now, Description="Software Engineer", Type="Type1" },
                new Models.Owner { Id = 2, Name = "Ali", Surname="Beyaz", Date=DateTime.Now, Description="CEO", Type="Type2" },
                new Models.Owner { Id = 3, Name = "Eren", Surname="Demir", Date=DateTime.Now, Description="Human Resources Specialist", Type="Type3" }
        };

        [HttpGet]
        [Route("Owners")]
        public IActionResult Get()
        {
            var ownerList = AllOwners.OrderBy(x => x.Id).ToList();
            return Ok(ownerList);
        }

        [HttpPost]
        [Route("NewOwner")]
        [Consumes("application/json")]
        public IActionResult Post(Models.Owner newOwner)
        {
            var owner = AllOwners.SingleOrDefault(x => x.Id == newOwner.Id);
            if (owner is not null)
                return BadRequest();
            else if (newOwner.Description.ToLower().Contains("hack"))
                return BadRequest("Invalid description");
            else
            {
                AllOwners.Add(newOwner);
                return Ok(newOwner);
            }
        }

        [HttpPut]
        [Route("UpdateOwner")]
        public IActionResult Update(int id, Models.Owner ownerToUpdate)
        {
            var owner = AllOwners.FirstOrDefault(x => x.Id == id);
            if (owner == null) return NotFound();

            owner.Name = ownerToUpdate.Name != default ? ownerToUpdate.Name : owner.Name;
            owner.Surname = ownerToUpdate.Surname != default ? ownerToUpdate.Surname : owner.Surname;
            owner.Description = ownerToUpdate.Description != default ? ownerToUpdate.Description : owner.Description;
            owner.Date = ownerToUpdate.Date != default ? ownerToUpdate.Date : owner.Date;
            owner.Type = ownerToUpdate.Type != default ? ownerToUpdate.Type : owner.Type;

            return Ok(ownerToUpdate);
        }

        [HttpDelete]
        [Route("DeleteOwner")]
        public IActionResult Delete(int id)
        {
            var ownerToDelete = AllOwners.FirstOrDefault(x => x.Id == id);
            if (ownerToDelete == null) return NotFound($"Owner {id} not found! ");
            AllOwners.Remove(ownerToDelete);
            return Ok("Owner is deleted");
        }
    }
}
