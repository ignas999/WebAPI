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

    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Orders>))]

        public IActionResult GetOrders()
        {
            var orders = _mapper.Map<List<OrdersDto>>(_orderRepository.GetOrders());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(orders);
        }

        [HttpGet("{order_id}")]
        //httget verte ir  methodo paramentras turi ,matchint
        [ProducesResponseType(200, Type = typeof(Orders))]
        [ProducesResponseType(400)]
        public IActionResult GetOrder(int order_id)
        {
            if (!_orderRepository.OrderExists(order_id))
            {
                return NotFound();
            }

            var location = _mapper.Map<OrdersDto>(_orderRepository.GetOrder(order_id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(location);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateOrder([FromBody] OrdersDtoCreate orderCreate)
        {
            if (orderCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderMap = _mapper.Map<Orders>(orderCreate);


            if (!_orderRepository.CreateOrder(orderMap))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }
        [HttpPut("{order_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult updateOrder(int order_id, [FromBody] OrdersDtoCreate updatedorder)
        {
            if (updatedorder == null)
            {
                return BadRequest(ModelState);
            }
            if (order_id != updatedorder.order_id)
            {
                return BadRequest(ModelState);
            }

            if (!_orderRepository.OrderExists(order_id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderMap = _mapper.Map<Orders>(updatedorder);

            if (!_orderRepository.UpdateOrder(orderMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully updated");
        }
    }
}
