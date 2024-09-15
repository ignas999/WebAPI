
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
    public class TransactionsController :ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionsController(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Transactions>))]
        public IActionResult GetTransactions()
        {
            var Transactions = _mapper.Map<List<TransactionsDto>>(_transactionRepository.GetTransactions());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Transactions);
        }

        [HttpGet("{transaction_id}")]
        [ProducesResponseType(200, Type = typeof(Transactions))]
        [ProducesResponseType(400)]
        public IActionResult GetTransaction(int transaction_id)
        {
            if (!_transactionRepository.TransactionExists(transaction_id))
            {
                return NotFound();
            }

            var transaction = _mapper.Map<TransactionsDto>(_transactionRepository.GetTransaction(transaction_id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(transaction);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateTransaction([FromBody] TransactionsDtoCreate transactionCreate)
        {
            if (transactionCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transactionMap = _mapper.Map<Transactions>(transactionCreate);


            if (!_transactionRepository.CreateTransaction(transactionMap))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }

        [HttpPut("{transaction_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult updateTransaction(int transaction_id, [FromBody] TransactionsDtoCreate updatedtransaction)
        {
            if (updatedtransaction == null)
            {
                return BadRequest(ModelState);
            }
            if (transaction_id != updatedtransaction.transaction_id)
            {
                return BadRequest(ModelState);
            }

            if (!_transactionRepository.TransactionExists(transaction_id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transactionMap = _mapper.Map<Transactions>(updatedtransaction);

            if (!_transactionRepository.UpdateTransaction(transactionMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully updated");
        }

        [HttpDelete("{transaction_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteTransaction(int transaction_id)
        {
            if (!_transactionRepository.TransactionExists(transaction_id))
            {
                return NotFound();
            }

            var transactionToDelete = _transactionRepository.GetTransaction(transaction_id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            if (!_transactionRepository.DeleteTransaction(transactionToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the row");
            }
            return Ok("Sucessfully Deleted");
        }
    }
}
