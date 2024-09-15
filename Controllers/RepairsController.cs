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
    public class RepairsController :ControllerBase
    {
        private readonly IRepairRepository _repairRepository;
        private readonly IMapper _mapper;

        public RepairsController(IRepairRepository repairRepository , IMapper mapper)
        {
            _repairRepository = repairRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Repairs>))]
        public IActionResult GetRepairs()
        {
            var repairs = _mapper.Map<List<RepairsDto>>(_repairRepository.GetRepairs());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(repairs);
        }

        [HttpGet("{repair_id}")]
        [ProducesResponseType(200, Type = typeof(Repairs))]
        [ProducesResponseType(400)]
        public IActionResult GetRepair(int repair_id)
        {
            if (!_repairRepository.RepairExists(repair_id))
            {
                return NotFound();
            }

            var repair = _mapper.Map<RepairsDto>(_repairRepository.GetRepair(repair_id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(repair);
        }

        [HttpGet("{repair_id}/Maintenances")]
        [ProducesResponseType(200, Type = typeof(ICollection<Maintenances>))]
        [ProducesResponseType(400)]

        public IActionResult getMaintenances(int repair_id)
        {
            if (!_repairRepository.RepairExists(repair_id))
            {
                return NotFound();
            }

            var maintenance = _mapper.Map<List<MaintenancesDto>>(_repairRepository.GetMaintenancesByRepairId(repair_id));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(maintenance);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult Createrepair([FromBody] RepairsDto repairCreate)
        {
            if (repairCreate == null)
            {
                return BadRequest(ModelState);
            }

            //check if there is an instance not identical to the one

            var repair = _repairRepository.GetRepairs()
                .Where(p => p.name.Trim().ToLower() == repairCreate.name.Trim().ToLower())
                .FirstOrDefault();

            if (repair != null)
            {
                ModelState.AddModelError("", "Already Exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var repairMap = _mapper.Map<Repairs>(repairCreate);

            if (!_repairRepository.CreateRepair(repairMap))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }

        [HttpPut("{repair_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult updateRepair(int repair_id, [FromBody] RepairsDto updatedrepair)
        {
            if (updatedrepair == null)
            {
                return BadRequest(ModelState);
            }
            if (repair_id != updatedrepair.repair_id)
            {
                return BadRequest(ModelState);
            }

            if (!_repairRepository.RepairExists(repair_id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var repairMap = _mapper.Map<Repairs>(updatedrepair);

            if (!_repairRepository.UpdateRepair(repairMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully updated");
        }

        [HttpDelete("{repair_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteRepair(int repair_id)
        {
            if (!_repairRepository.RepairExists(repair_id))
            {
                return NotFound();
            }

            var repairToDelete = _repairRepository.GetRepair(repair_id);

            if (_repairRepository.GetMaintenancesByRepairId(repair_id).ToList().Count > 0)
            {
                ModelState.AddModelError("", "Can't delete, there are records with this value.");
                return StatusCode(500, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            if (!_repairRepository.DeleteRepair(repairToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the row");
            }
            return Ok("Sucessfully Deleted");
        }
    }
}
