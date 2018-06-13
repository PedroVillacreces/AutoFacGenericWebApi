namespace AutofacGenericAPI.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Model;
    using Repositories;
    using System.Data.Entity.Infrastructure;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http.Description;
   
    public class UsersController : ApiController
    {
        private readonly IGenericRepository<User> _userRepository;

        public UsersController(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //// PUT: api/Users/?
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            try
            {
                await _userRepository.PutTaskAsync(user).ConfigureAwait(false);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (await UserExists(id).ConfigureAwait(false))
                {
                    throw;
                }

                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        //// POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _userRepository.PostTaskAsync(user).ConfigureAwait(false);
            }
            catch (DbUpdateException)
            {
                if (await UserExists(user.UserId).ConfigureAwait(false))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        //// DELETE: api/Users/?
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            try
            {
                await _userRepository.DeleteTaskAsync(id).ConfigureAwait(false);

                if (await UserExists(id).ConfigureAwait(false))
                {
                    return Ok();
                }

                return Conflict();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }
        }

        private async Task<bool> UserExists(int id)
        {
            var user = await _userRepository.GetByIdAsync(id).ConfigureAwait(false);
            return user != null;
        }
    }
}