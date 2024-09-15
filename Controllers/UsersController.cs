using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DataTransferObject;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UsersController :ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Users>))]
        public IActionResult GetUsers()
        {

            var users = _mapper.Map<List<UsersDto>>(_userRepository.GetUsers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(users);
        }

        [HttpGet("{user_id}")]
        [ProducesResponseType(200, Type = typeof(Users))]
        [ProducesResponseType(400)]

        public IActionResult GetUser(int user_id)
        {
            if (!_userRepository.UserExists(user_id))
            {
                return NotFound();
            }

            var worker = _mapper.Map<UsersDto>(_userRepository.GetUser(user_id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(worker);
        }

        [HttpGet("{user_id}/Orders")]
        [ProducesResponseType(200, Type = typeof(ICollection<Orders>))]
        [ProducesResponseType(400)]

        public IActionResult getOrdersOfAUser(int user_id)
        {
            if (!_userRepository.UserExists(user_id))
            {
                return NotFound();
            }

            var users = _mapper.Map<List<OrdersDto>>(_userRepository.GetOrdersByUserId(user_id));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(users);
        }

        [HttpGet("{user_id}/Transactions")]
        [ProducesResponseType(200, Type = typeof(ICollection<Transactions>))]
        [ProducesResponseType(400)]

        public IActionResult getTransactionsOfAUser(int user_id)
        {
            if (!_userRepository.UserExists(user_id))
            {
                return NotFound();
            }

            var users = _mapper.Map<List<TransactionsDto>>(_userRepository.GetTransactionsByUserId(user_id));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(users);
        }

        [HttpGet("Email/{email}")]
        [ProducesResponseType(200, Type = typeof(Users))]
        [ProducesResponseType(400)]

        public IActionResult GetUserByEmail(string email)
        {
            if (!_userRepository.UserExists(email))
            {
                return NotFound();
            }

            var worker = _mapper.Map<UsersDto>(_userRepository.GetUser(email));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(worker);
        }

		[HttpPut("{user_id}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]

		public IActionResult updateTransport(int user_id, [FromBody] UsersDto updateduser)
		{
			if (updateduser == null)
			{
				return BadRequest(ModelState);
			}
			if (user_id != updateduser.user_id)
			{
				return BadRequest(ModelState);
			}

			if (!_userRepository.UserExists(user_id))
			{
				return NotFound();
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var userMap = _mapper.Map<Users>(updateduser);

			if (!_userRepository.UpdateUser(userMap))
			{
				ModelState.AddModelError("", "Something went wrong updating");
				return StatusCode(500, ModelState);
			}

			return Ok("Succesfully updated");
		}


	}
}
