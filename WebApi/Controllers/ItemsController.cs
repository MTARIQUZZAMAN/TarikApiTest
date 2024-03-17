using Application.Entities.Requests;
using Application.Extensions;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text;


namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        private readonly IItemService _dataService;

        public ItemsController(IItemService dataService)
        {
            _dataService = dataService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //throw new Exception("some error");
            var modelVms = await _dataService.GetAll();
            if (modelVms == null || modelVms.Count <= 0)
                return NotFound(ApiResponseBuilder.GenerateNotFound("Get failed", "Record mot found"));

            return Ok(ApiResponseBuilder.GenerateOk(modelVms, "OK", $"{modelVms.Count} record(s) fetched"));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Get Failed", "Invalid Input"));

            var modelVm = await _dataService.GetById(id);

            if (modelVm == null)
                return NotFound(ApiResponseBuilder.GenerateNotFound("Get failed", $"Record with id {id} not found"));

            return Ok(ApiResponseBuilder.GenerateOk(modelVm, "OK", $"record with id {modelVm.Id} fetched"));

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemRequest modelDto)
        {
            if (modelDto == null)
            {
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Create Failed", "Input not valid or null"));
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetModelStateErrors();
                if (errors != null && errors.Count > 0)
                {
                    var msgBuilder = new StringBuilder();
                    foreach (var error in errors)
                    {
                        msgBuilder.AppendLine(error.ToString());
                    }
                    return BadRequest(ApiResponseBuilder.GenerateBadRequest("Create Failed", msgBuilder.ToString()));
                }
            }

            var createdDto = await _dataService.Create(modelDto);
            if (createdDto == null)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Create Failed", "Some error occured"));

            return Ok(ApiResponseBuilder.GenerateOk(createdDto, "Ok", $"Record created at api/Items/Create/{createdDto.Id}"));
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemRequest modelDto)
        {
            if (id <= 0 || modelDto == null || modelDto.Id <= 0)
            {
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Update Failed", "Input not valid or null"));
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetModelStateErrors();
                if (errors != null && errors.Count > 0)
                {
                    var msgBuilder = new StringBuilder();
                    foreach (var error in errors)
                        msgBuilder.AppendLine(error.ToString());

                    return BadRequest(ApiResponseBuilder.GenerateBadRequest("Create Failed", msgBuilder.ToString()));
                }
            }

            var updatedDto = await _dataService.Update(modelDto);

            if (updatedDto == null)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Update Failed", "Some error occured"));

            return Ok(ApiResponseBuilder.GenerateOk(updatedDto, "Ok", $"Record updated at api/Items/Update/{updatedDto.Id}"));

        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Delete Failed", "Input not valid or null"));

            var rowsDeleted = await _dataService.Delete(id);

            if (rowsDeleted <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Delete Failed", "There might be a chold record"));

            return Ok(ApiResponseBuilder.GenerateOk(rowsDeleted, "Ok", $"Record deleted at api/Items/Delete/{id}"));

        }


        [HttpGet("{cid?}")]
        public async Task<IActionResult> GetByCategoryIdAlter([FromRoute] int? cid)
        {
            if (cid <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Get Failed", "Invalid Input"));

            var modelVms = await _dataService.GetByCategegoryId(cid);

            if (modelVms == null)
                return NotFound(ApiResponseBuilder.GenerateNotFound("Get failed", $"Record with id {cid} not found"));

            return Ok(ApiResponseBuilder.GenerateOk(modelVms, "OK", $"{modelVms.Count()} record(s) with fetched"));

        }

        [HttpGet]
        [Route("{cid:int?}")]
        public async Task<IActionResult> GetByCategoryId(int? cid)
        {
            if (cid <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Get Failed", "Invalid Input"));

            var modelVms = await _dataService.GetByCategegoryId(cid);

            if (modelVms == null)
                return NotFound(ApiResponseBuilder.GenerateNotFound("Get failed", $"Record with id {cid} not found"));

            return Ok(ApiResponseBuilder.GenerateOk(modelVms, "OK", $"{modelVms.Count()} record(s) with fetched"));

        }


    }
}
