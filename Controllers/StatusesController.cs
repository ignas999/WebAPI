using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.DataTransferObject;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers
{
    //atributai , tai kas leidzia jiems buti controlleriais .
    [Route("/api/[controller]")]
    [ApiController]
    public class StatusesController: ControllerBase
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public StatusesController(IStatusRepository statusRepository, IMapper mapper)
        {
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Statuses>))]
        public IActionResult GetStatuses()
        {
            var statuses = _mapper.Map<List <StatusesDto>>(_statusRepository.GetStatuses());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(statuses);
        }

        [HttpGet("{status_id}")]
        [ProducesResponseType(200, Type = typeof(Statuses))]
        [ProducesResponseType(400)]
        public IActionResult GetStatus(int status_id) 
        {
            if (!_statusRepository.StatusExists(status_id))
            {
                return NotFound();
            }

            var status = _mapper.Map<StatusesDto>(_statusRepository.GetStatus(status_id));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(status);
        }

        [HttpGet("{status_id}/Transports")]
        [ProducesResponseType(200, Type = typeof(ICollection<Transports>))]
        [ProducesResponseType(400)]

        public IActionResult getTransports(int status_id)
        {
            if (!_statusRepository.StatusExists(status_id))
            {
                return NotFound();
            }

            var transports = _mapper.Map < List < TransportsDto >> (_statusRepository.GetTransports(status_id));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(transports);
        }

        [HttpGet("{status_id}/Orders")]
        [ProducesResponseType(200, Type = typeof(ICollection<Orders>))]
        [ProducesResponseType(400)]

        public IActionResult getOrders(int status_id)
        {
            if (!_statusRepository.StatusExists(status_id))
            {
                return NotFound();
            }

            var orders = _mapper.Map<List<OrdersDto>>(_statusRepository.GetOrdersByStatusId(status_id));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(orders);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateStatus([FromBody] StatusesDto statusCreate)
        {
            if (statusCreate == null)
            {
                return BadRequest(ModelState);
            }

            //check if there is an instance not identical to the one

            var status = _statusRepository.GetStatuses()
                .Where(p => p.name.Trim().ToLower() == statusCreate.name.Trim().ToLower())
                .FirstOrDefault();

            if (status != null)
            {
                ModelState.AddModelError("", "Already Exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var statusMap = _mapper.Map<Statuses>(statusCreate);

            if (!_statusRepository.CreateStatus(statusMap))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }

        [HttpPut("{status_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult updateStatus(int status_id, [FromBody] StatusesDto updatedstatus)
        {
            if (updatedstatus == null)
            {
                return BadRequest(ModelState);
            }
            if (status_id != updatedstatus.status_id)
            {
                return BadRequest(ModelState);
            }

            if (!_statusRepository.StatusExists(status_id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var statusMap = _mapper.Map<Statuses>(updatedstatus);

            if (!_statusRepository.UpdateStatus(statusMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully updated");
        }

        [HttpDelete("{status_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteCategory(int status_id)
        {
            if (!_statusRepository.StatusExists(status_id))
            {
                return NotFound();
            }

            var statusToDelete = _statusRepository.GetStatus(status_id);

            if (_statusRepository.GetTransports(status_id).ToList().Count > 0 && _statusRepository.GetOrdersByStatusId(status_id).ToList().Count > 0)
            {
                ModelState.AddModelError("", "Can't delete, there are records with this value.");
                return StatusCode(500, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            if (!_statusRepository.DeleteStatus(statusToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the row");
            }
            return Ok("Sucessfully Deleted");
        }

    }
}
