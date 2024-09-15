using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using WebApi.DataTransferObject;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly ITransportRepository _transportRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public TransportController(ITransportRepository transportsRepository,IMapper mapper, IOrderRepository orderRepository)
        {

               _transportRepository = transportsRepository;
               _mapper = mapper;
               _orderRepository = orderRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Transports>))]
        public IActionResult GetTransports()
        {
            var transports2 = _mapper.Map<List<TransportsDto>>(_transportRepository.GetTransports());

            //var transports = _transportRepository.GetTransports();



            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(transports2);
        }

        [HttpGet("{transport_id}")]
        [ProducesResponseType(200, Type = typeof(Transports))]
        [ProducesResponseType(400)]
        public IActionResult GetTransport(int transport_id)
        {
            if (!_transportRepository.TransportExists(transport_id))
            {
                return NotFound();
            }

            var transports = _mapper.Map<TransportsDto>(_transportRepository.GetTransport(transport_id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(transports);
        }

        [HttpGet("{transport_id}/Maintenances")]
        [ProducesResponseType(200, Type = typeof(ICollection<Maintenances>))]
        [ProducesResponseType(400)]

        public IActionResult getMaintenancesOfATransport(int transport_id)
        {
            if (!_transportRepository.TransportExists(transport_id))
            {
                return NotFound();
            }

            var transports = _mapper.Map<List<MaintenancesDto>>(_transportRepository.GetMaintenancesByTransportId(transport_id));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(transports);
        }

        [HttpGet("{transport_id}/Orders")]
        [ProducesResponseType(200, Type = typeof(ICollection<Orders>))]
        [ProducesResponseType(400)]

        public IActionResult getOrdersOfATransport(int transport_id)
        {
            if (!_transportRepository.TransportExists(transport_id))
            {
                return NotFound();
            }

            var transports = _mapper.Map<List<OrdersDto>>(_transportRepository.GetOrdersByTransportId(transport_id));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(transports);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateTransport([FromBody] TransportsDtoCreate transportCreate)
        {
            if (transportCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transportMap = _mapper.Map<Transports>(transportCreate);


            if (!_transportRepository.CreateTransport(transportMap))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }

        [HttpPut("{transport_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult updateTransport(int transport_id, [FromBody] TransportsDtoCreate updatedtransport)
        {
            if (updatedtransport == null)
            {
                return BadRequest(ModelState);
            }
            if (transport_id != updatedtransport.transport_id)
            {
                return BadRequest(ModelState);
            }

            if (!_transportRepository.TransportExists(transport_id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transportMap = _mapper.Map<Transports>(updatedtransport);

            if (!_transportRepository.UpdateTransport(transportMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully updated");
        }

        [HttpDelete("{transport_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteTransport(int transport_id)
        {
            if (!_transportRepository.TransportExists(transport_id))
            {
                return NotFound();
            }

            var transportToDelete = _transportRepository.GetTransport(transport_id);

            if (_orderRepository.GetOrders().Where(o => o.transport_id == transport_id).ToList().Count > 0)
            {
                ModelState.AddModelError("", "Can't delete, there are records with this value.");
                return StatusCode(500, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            if (!_transportRepository.DeleteTransport(transportToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the row");
            }
            return Ok("Sucessfully Deleted");
        }
    }

}
