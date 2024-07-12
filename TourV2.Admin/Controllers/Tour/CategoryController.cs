using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;
using TourV2.MediatR.Queries;

namespace TourV2.Admin.Controllers.Tour
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategoryController : BaseController
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create A Category
        /// </summary>
        /// <param name="addCategoryCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(CategoryDto))]
        public async Task<IActionResult> AddCategory(AddCategoryCommand addCategoryCommand)
        {
            var result = await _mediator.Send(addCategoryCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Update Exist Category By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateCategoryCommand"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(CategoryRecordDto))]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryCommand updateCategoryCommand)
        {
            updateCategoryCommand.Code = id;
            var result = await _mediator.Send(updateCategoryCommand);
            return Ok(result);
        }
        /// <summary>
        /// Get GetRecord By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetCategory/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(CategoryDto))]
        public async Task<IActionResult> GetCategory(int id)
        {
            var getCategoryQuery = new GetCategoryQuery { Id = id };

            var result = await _mediator.Send(getCategoryQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get GetCategoryRecord By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetCategoryRecord/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(CategoryRecordDto))]
        public async Task<IActionResult> GetCategoryRecord(Guid id)
        {
            var getCategoryQuery = new GetCategoryRecordQuery { Id = id };

            var result = await _mediator.Send(getCategoryQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All Categories BY Lang
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllCategoryByLang/{languageCode}")]
        [Produces("application/json", "application/xml", Type = typeof(List<CategoryListDto>))]
        public async Task<IActionResult> GetAllCategoryByLang(string languageCode)
        {
            var getAllQuery = new GetAllCategoriesQuery { LanguageCode = languageCode };
            var result = await _mediator.Send(getAllQuery);
            return Ok(result);
        }
        /// <summary>
        /// Delete Category By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            var deleteCategoryCommand = new DeleteCategoryCommand { Id = Id };
            var result = await _mediator.Send(deleteCategoryCommand);
            return ReturnFormattedResponse(result);
        }


        
    }
}
