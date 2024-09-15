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

    public class WorkersController : ControllerBase
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;

        public WorkersController(IWorkerRepository workerRepository, IMapper mapper , ITransactionRepository transactionRepository)
        {
            _workerRepository = workerRepository;
            _mapper = mapper;
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Workers>))]
        public IActionResult GetWorkers() {

            var workers= _mapper.Map<List<WorkersDto>>(_workerRepository.GetWorkers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(workers);
        }

        [HttpGet("{worker_id}")]
        [ProducesResponseType(200, Type = typeof(Workers))]
        [ProducesResponseType(400)]

        public IActionResult GetWorker(int worker_id)
        {
            if (!_workerRepository.WorkerExists(worker_id))
            {
                return NotFound();
            }

            var worker = _mapper.Map<WorkersDto>(_workerRepository.GetWorker(worker_id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(worker);
        }

        [HttpGet("{worker_id}/Maintenances")]
        [ProducesResponseType(200, Type = typeof(ICollection<Maintenances>))]
        [ProducesResponseType(400)]

        public IActionResult getWorkerMaintenances(int worker_id)
        {
            if (!_workerRepository.WorkerExists(worker_id))
            {
                return NotFound();
            }

            var maintenances = _mapper.Map<List<MaintenancesDto>>(_workerRepository.GetMaintenancesByWorkerId(worker_id));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(maintenances);
        }

        [HttpGet("{worker_id}/Transactions")]
        [ProducesResponseType(200, Type = typeof(ICollection<Transactions>))]
        [ProducesResponseType(400)]

        public IActionResult getWorkerTransactions(int worker_id)
        {
            if (!_workerRepository.WorkerExists(worker_id))
            {
                return NotFound();
            }

            var Transactions = _mapper.Map<List<TransactionsDto>>(_workerRepository.GetTransactionsByWorkerId(worker_id));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(Transactions);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult Createworker([FromBody] WorkersDtoCreate workerCreate)
        {
            if (workerCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workerMap = _mapper.Map<Workers>(workerCreate);
            

            Console.WriteLine(workerMap);

            if (!_workerRepository.CreateWorker(workerMap))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }

        [HttpPut("{worker_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult updateWorker(int worker_id, [FromBody] WorkersDtoCreate updatedworker)
        {
            if (updatedworker == null)
            {
                return BadRequest(ModelState);
            }
            if (worker_id != updatedworker.worker_id)
            {
                return BadRequest(ModelState);
            }

            if (!_workerRepository.WorkerExists(worker_id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workerMap = _mapper.Map<Workers>(updatedworker);

            if (!_workerRepository.UpdateWorker(workerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully updated");
        }

        [HttpDelete("{worker_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteWorker(int worker_id)
        {
            if (!_workerRepository.WorkerExists(worker_id))
            {
                return NotFound();
            }

            var workerToDelete = _workerRepository.GetWorker(worker_id);

            if (_workerRepository.GetMaintenancesByWorkerId(worker_id).ToList().Count > 0 && _transactionRepository.GetTransactions().Where(o => o.worker_id == worker_id).ToList().Count > 0 )
            {
                ModelState.AddModelError("", "Can't delete, there are records with this value.");
                return StatusCode(500, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            if (!_workerRepository.DeleteWorker(workerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the row");
            }
            return Ok("Sucessfully Deleted");
        }

        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(Workers))]
        [ProducesResponseType(400)]

        public IActionResult LoginWorker([FromBody] LoginWorkerDto workerLogin)
        {
            if (workerLogin == null)
            {
                return BadRequest(ModelState);
            }
            if (workerLogin.username == null || workerLogin.password == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

				var workerauth = _workerRepository.GetWorkerFromUsername(workerLogin.username);
            if(workerauth == null)
            {
				return BadRequest(ModelState);
			}
            
            var workerHash = workerauth.password;
			
			if (!BCrypt.Net.BCrypt.Verify(workerLogin.password, workerHash)){
				return BadRequest(ModelState);
			}
            else
            {
				var workerinfo = _workerRepository.GetWorkerfromlogin(workerLogin.username, workerHash);

				if (workerinfo == null)
				{
					return BadRequest();
				}
				else
				{
					var workerMap = _mapper.Map<WorkersDto>(workerinfo);
					workerMap.password = "Hidden";
					return Ok(workerMap);
				}
			}
            
        }
    }
}
