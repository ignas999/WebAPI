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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        //we inject repository into our controller
        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Categories>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoriesDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }

        [HttpGet("{category_id}")]
        [ProducesResponseType(200, Type = typeof(Categories))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int category_id)
        {
            if (!_categoryRepository.CategoryExists(category_id))
            {
                return NotFound();
            }

            var category = _mapper.Map<CategoriesDto>(_categoryRepository.GetCategory(category_id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }

        [HttpGet("{category_id}/Transports")]
        [ProducesResponseType(200, Type = typeof(ICollection<Transports>))]
        [ProducesResponseType(400)]

        public IActionResult getTransportsByCategoryId(int category_id)
        {
            if (!_categoryRepository.CategoryExists(category_id))
            {
                return NotFound();
            }

            var transports = _mapper.Map<List<TransportsDto>>(_categoryRepository.GetTransportsByCategory(category_id));
            //var transports = _categoryRepository.GetTransportsByCategory(category_id);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(transports);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateCategory([FromBody] CategoriesDto categoryCreate)
        {
            if(categoryCreate == null)
            {
                return BadRequest(ModelState);
            }

            //check if there is an instance not identical to the one

            var category = _categoryRepository.GetCategories()
                .Where(p=> p.name.Trim().ToLower() == categoryCreate.name.Trim().ToLower())
                .FirstOrDefault();

            if(category != null)
            {
                ModelState.AddModelError("", "Already Exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryMap = _mapper.Map<Categories>(categoryCreate);

            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500 , ModelState);
            }
            return Ok("Succesfully created");
        }

        [HttpPut("{category_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult updateCategory(int category_id , [FromBody]CategoriesDto updatedcategory)
        {
            if(updatedcategory == null)
            {
                return BadRequest(ModelState);
            }
            if(category_id != updatedcategory.category_id)
            {
                return BadRequest(ModelState);
            }

            if (!_categoryRepository.CategoryExists(category_id))
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryMap = _mapper.Map<Categories>(updatedcategory);

            if (!_categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500 , ModelState);
            }

            return Ok("Succesfully updated");
        }

        [HttpDelete("{category_id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteCategory(int category_id)
        {
            if (!_categoryRepository.CategoryExists(category_id))
            {
                return NotFound();
            }

            var categoryToDelete = _categoryRepository.GetCategory(category_id);

            if(_categoryRepository.GetTransportsByCategory(category_id).ToList().Count > 0)
            {
                ModelState.AddModelError("", "Can't delete, there are records with this value.");
                return StatusCode(500, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            if (!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the row");
            }
            return Ok("Sucessfully Deleted");
        }
    }
}
