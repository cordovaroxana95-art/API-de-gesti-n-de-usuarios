// Controllers/UsersController.cs
using Microsoft.AspNetCore.Mvc;
using UserManagementApi.DTOs;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // La ruta será /api/users
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET /api/users
        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetAll()
        {
            return Ok(_userService.GetAllUsers());
        }

        // GET /api/users/{id}
        [HttpGet("{id}")]
        public ActionResult<UserReadDto> GetById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound("Usuario no encontrado.");
            return Ok(user);
        }

        // POST /api/users
        // ASP.NET Core realiza automáticamente la validación usando el atributo [ApiController] y las anotaciones en UserCreateDto
        [HttpPost]
        public ActionResult<UserReadDto> Create([FromBody] UserCreateDto dto)
        {
            // Verifica las reglas de validación básicas del DTO (Name, Email format, Age range)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                var newUser = _userService.CreateUser(dto);
                // Retorna 201 Created y la ubicación del nuevo recurso
                return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
            }
            catch (InvalidOperationException ex)
            {
                // Maneja la excepción de unicidad del email desde el servicio
                return Conflict(ex.Message); // 409 Conflict
            }
        }

        // PUT /api/users/{id}
        [HttpPut("{id}")]
        public ActionResult<UserReadDto> Update(int id, [FromBody] UserCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedUser = _userService.UpdateUser(id, dto);
                if (updatedUser == null) return NotFound("Usuario no encontrado.");
                return Ok(updatedUser);
            }
            catch (InvalidOperationException ex)
            {
                // Maneja la excepción de unicidad del email desde el servicio
                return Conflict(ex.Message); // 409 Conflict
            }
        }

        // DELETE /api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _userService.DeleteUser(id);
            if (!deleted) return NotFound("Usuario no encontrado.");
            
            return NoContent(); // 204 No Content
        }
    }
}