namespace MultiContainerDockerApp.Services
{
    using MultiContainerDockerApp.Dal;
    using MultiContainerDockerApp.Dal.Models;
    using MultiContainerDockerApp.Models;
    using AutoMapper;
    using BCrypt.Net;
    using MultiContainerDockerApp.Authorization;
    using NodaTime;

    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Register(RegisterRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private MultiContainerDbContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserService(
            MultiContainerDbContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Email);

            // validate
            if (user == null || !BCrypt.Verify(model.Password, user.PasswordHash))
                throw new Exception("Username or password is incorrect");

            // authentication successful
            var response = new AuthenticateResponse()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username
            };
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return GetUser(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.Users.Any(x => x.Username == model.Email))
                throw new Exception("Username '" + model.Email + "' is already taken");

            // map model to new user object
            //var user = _mapper.Map<User>(model);
            var user = new User() {
                Created = LocalDateTime.FromDateTime(DateTime.Now),
                Modified = LocalDateTime.FromDateTime(DateTime.Now),
                FirstName = model.FirstName, 
                LastName = model.LastName, 
                Email = model.Email,
                Username = model.Email,
                Password = model.Password,
            };
            user.PasswordHash = BCrypt.HashPassword(model.Password);

            // save user
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequest model)
        {
            var user = GetUser(id);

            // validate
            if (model.Username != user.Username && _context.Users.Any(x => x.Username == model.Username))
                throw new Exception("Username '" + model.Username + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.HashPassword(model.Password);

            // copy model to user and save
            _mapper.Map(model, user);
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = GetUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        // helper methods

        private User GetUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
