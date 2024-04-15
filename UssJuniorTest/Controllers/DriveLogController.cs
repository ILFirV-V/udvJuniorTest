using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UssJuniorTest.Core;
using UssJuniorTest.Core.Models.Response;
using UssJuniorTest.Logic;

namespace UssJuniorTest.Controllers
{
    [ApiController]
    [Route("api/driveLog")]
    public class DriveLogController : ControllerBase
    {
        private readonly IDriveLogManager driveLogManager;
        public DriveLogController(IDriveLogManager driveLogManager)
        {
            this.driveLogManager = driveLogManager;
        }

        [HttpGet]
        [Route("aggregation")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ResponseDriveLog>> GetDriveLogsAggregation(
            [Required(ErrorMessage = "Время начала обязательно для заполнения")][DataType(DataType.DateTime)] DateTime startTime,
            [Required(ErrorMessage = "Время завершения обязательно для заполнения")][DataType(DataType.DateTime)] DateTime endTime,
            int page = 1,
            int pageSize = 10,
            string nameFilter = "",
            string carFilter = "",
            string sortBy = "")
        {
            var driversInfo = driveLogManager.GetDriveLogInfo(startTime, endTime, page, pageSize, nameFilter, carFilter, sortBy);
            if (driversInfo == null)
            {
                return NotFound();
            }

            return Ok(driversInfo);
        }
    }
}
