using Microsoft.AspNetCore.Mvc;
using OrderApi.Services;

namespace OrderApi.Controllers
{
    /// <summary>
    /// Manage delivery type related operations
    /// </summary>
    /// <remarks>
    /// This controller provides CRUD operations for Delivery types
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryTypesController : ControllerBase
    {
        private readonly IDeliveryTypeService _deliveryTypeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryTypesController"/> class.
        /// </summary>
        /// <param name="deliveryTypeService">Service for order operations.</param>
        public DeliveryTypesController(IDeliveryTypeService deliveryTypeService)
        {
            _deliveryTypeService = deliveryTypeService;
        }

        /// <summary>
        /// Retrieves list of delivery type
        /// </summary>
        /// <returns>List of delivery types</returns>
        /// <response code="200">Retrieval successful, returns the list</response>
        /// <response code="404">Could not find the delivery types</response>
        [HttpGet]
        [Route("GetDeliveryTypes")]
        public async Task<ActionResult<IEnumerable<DeliveryTypeDto>>> GetDeliveryTypes([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var deliveryTypes = await _deliveryTypeService.GetDeliveryTypesAsync(pageNumber, pageSize);

            if (deliveryTypes == null || !deliveryTypes.Items.Any())
            {
                return NotFound("No delivery types found");
            }

            return Ok(deliveryTypes);
        }

        /// <summary>
        /// Retrieves Delivery type by id
        /// </summary>
        /// <param name="id">Delivery type id</param>
        /// <returns>Delivery type which id matches with given one</returns>
        /// <response code="200">Retrieval successful, return the delivery type</response>
        /// <response code="404">Could not find the delivery type</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryTypeDto>> GetDeliveryTypeById(Guid id)
        {
            var deliveryType = await _deliveryTypeService.GetDeliveryTypeByIdAsync(id);

            if (deliveryType == null)
            {
                return NotFound($"Delivery type with Id:{id} not found.");
            }
            return Ok(deliveryType);
        }

        /// <summary>
        /// Creates a new delivery type
        /// </summary>
        /// <param name="deliveryTypeDto">Delivery type data</param>
        /// <returns>Created delivery type</returns>
        /// <response code="201">Delivery type created successfully</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="500">Object with the given Id already exists</response>
        [HttpPost]
        public async Task<ActionResult<DeliveryTypeDto>> CreateDeliveryType([FromBody] DeliveryTypeDto deliveryTypeDto)
        {
            if (deliveryTypeDto == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var newDeliveryType = await _deliveryTypeService.CreateDeliveryTypeAsync(deliveryTypeDto);

            return CreatedAtAction(nameof(GetDeliveryTypeById), new { id = newDeliveryType.Id }, newDeliveryType);
        }

        /// <summary>
        /// Updates existing deliveryType
        /// </summary>
        /// <param name="deliveryTypeDto">Updated delivery type data</param>
        /// <returns>The updated delivery type</returns>
        /// <response code="200">Delivery type updated successfully</response>
        /// <response code="400">Invalid input data</response>
        [HttpPut]
        public async Task<ActionResult<DeliveryTypeDto>> UpdateDeliveryType([FromBody] DeliveryTypeDto deliveryTypeDto)
        {
            if (deliveryTypeDto == null || !ModelState.IsValid)
            {
                return BadRequest("InvalidData.");
            }

            var updatedDeliveryTypes = await _deliveryTypeService.UpdateDeliveryTypeAsync(deliveryTypeDto);

            return Ok(updatedDeliveryTypes);
        }

        /// <summary>
        /// Deletes a delivery type by id
        /// </summary>
        /// <param name="id">Delivery type id</param>
        /// <returns>NoContent on success</returns>
        /// <response code="204">Delivery type deleted successfully</response>
        /// <response code="404">Could not find the delivery type</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDeliveryType(Guid id)
        {
            var isDeleted = await _deliveryTypeService.DeleteDeliveryTypeAsync(id);

            if (!isDeleted)
            {
                return NotFound("Order not found.");
            }

            return NoContent();
        }
    }
}
