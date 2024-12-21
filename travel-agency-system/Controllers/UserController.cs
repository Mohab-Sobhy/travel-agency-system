using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using travel_agency_system.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using travel_agency_system.DTO;
using travel_agency_system.Services;

namespace travel_agency_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly HotelManagementContext _context;
        private readonly UserManager<User> _userManager;

        public UserController(TokenService tokenService, HotelManagementContext context, UserManager<User> userManager)
        {
            _tokenService = tokenService;
            _context = context;
            _userManager = userManager;
        }
        
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult displayUserNo(string id)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> UserRegister(UserRegistrationDto userRegistrationDto)
        {
            if (userRegistrationDto == null)
            {
                return BadRequest("Invalid user data.");
            }

            if (userRegistrationDto.Role == null)
            {
                userRegistrationDto.Role = "CUSTOMER";
            }

            userRegistrationDto.Role = userRegistrationDto.Role.ToUpper();

            if (userRegistrationDto.Role == "ADMIN")
            {
                if (userRegistrationDto.AdminVerificationCode != "admin777")
                {
                    return BadRequest("Not allowed to register as an admin");
                }
            }

            // إنشاء كائن المستخدم الجديد
            var newUser = new User
            {
                UserName = userRegistrationDto.Email, // اسم المستخدم غالبًا يكون البريد الإلكتروني
                Email = userRegistrationDto.Email,
                PhoneNumber = userRegistrationDto.Phone,
                Name = userRegistrationDto.Name, // إذا كانت خاصية `Name` مضافة في `ApplicationUser`
                Role = userRegistrationDto.Role
                
            };

            // استخدام UserManager لإنشاء المستخدم مع كلمة المرور
            var result = await _userManager.CreateAsync(newUser, userRegistrationDto.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "User registered successfully.", UserLoginDto = userRegistrationDto });
            }

            // إذا حدثت أخطاء أثناء عملية التسجيل
            return BadRequest(new { message = "Registration failed.", errors = result.Errors });
        }


        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin(UserLoginDto userLoginInfo)
        {
            if (userLoginInfo == null)
            {
                return BadRequest("Invalid login data.");
            }

            // البحث عن المستخدم باستخدام UserManager
            User user = await _userManager.FindByNameAsync(userLoginInfo.email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userLoginInfo.password))
            {
                return Unauthorized("Invalid username or password.");
            }
            
            string token = _tokenService.GenerateToken(user.Id, user.Role);
            return Ok(new { Token = token });

        }
        
        

    }
}
