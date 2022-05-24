using MultiContainerDockerApp.Dal;
using MultiContainerDockerApp.Helpers;
using MultiContainerDockerApp.Models;
using MultiContainerDockerApp.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MultiContainerDockerApp.Controllers
{
    public class UserController : Controller
    {
        private MultiContainerDbContext _dbContext;
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserController(
            MultiContainerDbContext dbContext,
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _dbContext = dbContext;
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public IActionResult GetUsers()
        {
            var users = _dbContext.Users
                .Select(u => u)
                .ToList();

            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] RegisterRequest model)
        {
            _userService.Register(model);
            return Ok(new { message = "Registration successful" });
        }
    }
}
