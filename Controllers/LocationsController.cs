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
    public class LocationsController :ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationsController(ILocationRepository locationRepository , IMapper mapper) {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Locations>))]
        public IActionResult GetLocations()
        {
            var locations = _mapper.Map<List<LocationsDto>>(_locationRepository.GetLocations());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(locations);
        }

        [HttpGet("{location_id}")]
        //httget verte ir getlocation methodo paramentras turi ,matchint
        [ProducesResponseType(200, Type = typeof(Locations))]
        [ProducesResponseType(400)]
        public IActionResult GetLocation(int location_id)
        {
            if (!_locationRepository.LocationExists(location_id))
            {
                return NotFound();
            }

            var location = _mapper.Map<LocationsDto>(_locationRepository.GetLocation(location_id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(location);
        }

        [HttpGet("{location_id}/Transports")]
        [ProducesResponseType(200, Type = typeof(ICollection<Transports>))]
        [ProducesResponseType(400)]

        public IActionResult getTransportsByLocation(int location_id)
        {
            if (!_locationRepository.LocationExists(location_id))
            {
                return NotFound();
            }

            var transports = _mapper.Map<List<TransportsDto>>(_locationRepository.GetTransportsByLocation(location_id));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(transports);
        }

        [HttpGet("{location_id}/Workers")]
        [ProducesResponseType(200, Type = typeof(ICollection<Workers>))]
        [ProducesResponseType(400)]

        public IActionResult getWorkersByLocation(int location_id)
        {
            if (!_locationRepository.LocationExists(location_id))
            {
                return NotFound();
            }

            var transports = _mapper.Map<List<WorkersDto>>(_locationRepository.GetWorkersByLocation(location_id));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(transports);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateLocation([FromBody] LocationsDto locationCreate)
        {
            if (locationCreate == null)
            {
                return BadRequest(ModelState);
            }

            //check if there is an instance not identical to the one

            var location = _locationRepository.GetLocations()
                .Where(p => p.street.Trim().ToLower() == locationCreate.street.Trim().ToLower())
                .FirstOrDefault();

            if (location != null)
            {
                ModelState.AddModelError("", "Already Exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationMap = _mapper.Map<Locations>(locationCreate);

            if (!_locationRepository.CreateLocation(locationMap))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }

        [HttpPut("{location_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult updateLocation(int location_id, [FromBody] LocationsDto updatedlocation)
        {
            if (updatedlocation == null)
            {
                return BadRequest(ModelState);
            }
            if (location_id != updatedlocation.location_id)
            {
                return BadRequest(ModelState);
            }

            if (!_locationRepository.LocationExists(location_id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationMap = _mapper.Map<Locations>(updatedlocation);

            if (!_locationRepository.UpdateLocation(locationMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully updated");
        }

        [HttpDelete("{location_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteLocation(int location_id)
        {
            if (!_locationRepository.LocationExists(location_id))
            {
                return NotFound();
            }

            var locationToDelete = _locationRepository.GetLocation(location_id);

            if (_locationRepository.GetTransportsByLocation(location_id).ToList().Count > 0)
            {
                ModelState.AddModelError("", "Can't delete, there are records with this value.");
                return StatusCode(500, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            if (!_locationRepository.DeleteLocation(locationToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the row");
            }
            return Ok("Sucessfully Deleted");
        }
    }
}
