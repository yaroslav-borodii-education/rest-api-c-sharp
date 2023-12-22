using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillaApi")]
    [ApiController]

    public class VillaAPIController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public VillaAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PatientDTO>> GetVillas()
        {

            return Ok(_context.Patients.ToList());
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PatientDTO> GetVillas(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _context.Patients.FirstOrDefault(u => u.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PatientDTO> CreateVilla([FromBody] PatientDTO villaDTO)
        {
      
            if (_context.Patients.FirstOrDefault(u => u.FirstName.ToLower() == villaDTO.FirstName.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already exist");
                return BadRequest(ModelState);
            }

            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Patient model = new()
            {
                PhoneNumber = villaDTO.PhoneNumber,
                LastName = villaDTO.LastName,
                Id = villaDTO.Id,
                FirstName = villaDTO.FirstName,
                Address = villaDTO.Address,
                Email = villaDTO.Email
            };

            _context.Patients.Add(model);
            _context.SaveChanges();

            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _context.Patients.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(villa);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] PatientDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }

            Patient model = new()
            {
                PhoneNumber = villaDTO.PhoneNumber,
                LastName = villaDTO.LastName,
                Id = villaDTO.Id,
                FirstName = villaDTO.FirstName,
                Address = villaDTO.Address,
                Email = villaDTO.Email
            };

            _context.Patients.Update(model);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<PatientDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var villa = _context.Patients.AsNoTracking().FirstOrDefault(u => u.Id == id);


            PatientDTO villaDTO = new()
            {
                PhoneNumber = villa.PhoneNumber,
                LastName = villa.LastName,
                Id = villa.Id,
                Address = villa.Address,
                FirstName = villa.FirstName,

            };

            if (villa == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(villaDTO, ModelState);

            Patient model = new Patient()
            {
                PhoneNumber = villaDTO.PhoneNumber,
                LastName = villaDTO.LastName,
                Id = villaDTO.Id,
                Address = villaDTO.Address,
                FirstName = villaDTO.FirstName,
                Email = villaDTO.Email
            };

            _context.Patients.Update(model);
            _context.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

    }
}