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
    public class MaintenancesController :ControllerBase
    {
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly IMapper _mapper;

        public MaintenancesController(IMaintenanceRepository maintenanceRepository , IMapper mapper)
        {
            _maintenanceRepository = maintenanceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Maintenances>))]
        public IActionResult GetMaintenances()
        {
            var maintenances = _mapper.Map<List<MaintenancesDto>>(_maintenanceRepository.GetMaintenances());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(maintenances);
        }

        [HttpGet("{maintenance_id}")]
        [ProducesResponseType(200, Type = typeof(Maintenances))]
        [ProducesResponseType(400)]
        public IActionResult GetMaintenance(int maintenance_id)
        {
            if (!_maintenanceRepository.MaintenanceExists(maintenance_id))
            {
                return NotFound();
            }

            var maintenance = _mapper.Map<MaintenancesDto>(_maintenanceRepository.GetMaintenance(maintenance_id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(maintenance);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateMaintenance([FromBody] MaintenancesDtoCreate maintenanceCreate)
        {


            if (maintenanceCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maintenanceMap = _mapper.Map<Maintenances>(maintenanceCreate);


            if (!_maintenanceRepository.CreateMaintenance(maintenanceMap))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }

        [HttpPut("{maintenance_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult updateMaintenance(int maintenance_id, [FromBody] MaintenancesDtoCreate updatedmaintenance)
        {
            if (updatedmaintenance == null)
            {
                return BadRequest(ModelState);
            }
            if (maintenance_id != updatedmaintenance.maintenance_id)
            {
                return BadRequest(ModelState);
            }

            if (!_maintenanceRepository.MaintenanceExists(maintenance_id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maintenanceMap = _mapper.Map<Maintenances>(updatedmaintenance);

            if (!_maintenanceRepository.UpdateMaintenance(maintenanceMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully updated");
        }

        [HttpDelete("{maintenance_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteMaintenance(int maintenance_id)
        {
            if (!_maintenanceRepository.MaintenanceExists(maintenance_id))
            {
                return NotFound();
            }

            var maintenanceToDelete = _maintenanceRepository.GetMaintenance(maintenance_id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            if (!_maintenanceRepository.DeleteMaintenance(maintenanceToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the row");
            }
            return Ok("Sucessfully Deleted");
        }
    }
}
