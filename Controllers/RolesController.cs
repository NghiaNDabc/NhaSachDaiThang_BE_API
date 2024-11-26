using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;
using NhaSachDaiThang_BE_API.Models.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NhaSachDaiThang_BE_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly IMapper _mapper;

        public RolesController(BookStoreContext bookStoreContext, IMapper mapper)
        {
            _bookStoreContext = bookStoreContext;
            _mapper = mapper;
        }
        // GET: api/<RolesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roles = await _bookStoreContext.Role.ToListAsync();
            var rolesDto = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return Ok(rolesDto);
        }
    }
}
