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
    public class PrivillegesController: ControllerBase
    {
        private readonly IPrivillegeRepository _privillegeRepository;
        private readonly IMapper _mapper;

        public PrivillegesController(IPrivillegeRepository privillegeRepository , IMapper mapper)
        {
            _privillegeRepository = privillegeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Privilleges>))]
        public IActionResult GetPrivilleges() {
            var privilleges = _mapper.Map<List<PrivillegesDto>>(_privillegeRepository.GetPrivilleges());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(privilleges);
        }

        [HttpGet("{privillege_id}")]
        [ProducesResponseType(200, Type = typeof(Privilleges))]
        [ProducesResponseType(400)]
        public IActionResult GetPrivillege(int privillege_id)
        {
            if (!_privillegeRepository.PrivillegeExists(privillege_id))
            {
                return NotFound();
            }

            var privillege = _mapper.Map<PrivillegesDto>(_privillegeRepository.GetPrivillegeById(privillege_id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(privillege);
        }

        //[HttpGet("{privillege_name}")]
        //[ProducesResponseType(200, Type = typeof(Privilleges))]
        //[ProducesResponseType(400)]
        //public IActionResult GetPrivillegeByName(string privillege_name)
        //{
        //    if (!_privillegeRepository.PrivillegeExists(privillege_name))
        //    {
        //        return NotFound();
        //    }

        //    var privillege = _mapper.Map<PrivillegesDto>(_privillegeRepository.GetPrivillegeByName(privillege_name));

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return Ok(privillege);
        //}

        [HttpGet("{privillege_id}/Workers")]
        [ProducesResponseType(200, Type = typeof(ICollection<Workers>))]
        [ProducesResponseType(400)]

        public IActionResult getWorkersByPrivillegeID(int privillege_id)
        {
            if (!_privillegeRepository.PrivillegeExists(privillege_id))
            {
                return NotFound();
            }

            var workers = _mapper.Map<List<WorkersDto>>(_privillegeRepository.GetWorkersByPrivillegeId(privillege_id));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(workers);
        }

    }
}
