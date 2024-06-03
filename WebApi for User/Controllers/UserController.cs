using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_for_User.IRepository;
using WebApi_for_User.Models;

namespace WebApi_for_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericRepository<User> _userRepository;
        public UserController(IGenericRepository<User> genericRepository)
        {
            _userRepository = genericRepository;
        }

        [HttpGet]
        public IQueryable<User> GetAll() =>
            _userRepository.GetAll(null);

        [HttpPost]
        public async Task<User> Create(User user)
        {
            var res = await _userRepository.CreateAsync(user);
            _userRepository.SaveChangesAsync();
            return res;
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _userRepository.DeleteAsync(a=>a.Id == id);
            _userRepository.SaveChangesAsync();
        }

        [HttpPut]
        public async Task Put(User user)
        {
            _userRepository.Update(user);
            _userRepository.SaveChangesAsync();
           
        }
    }
}
