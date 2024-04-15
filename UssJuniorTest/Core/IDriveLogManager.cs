using UssJuniorTest.Core.Models.Response;

namespace UssJuniorTest.Core
{
    public interface IDriveLogManager
    {
        public List<ResponseDriveLog> GetDriveLogInfo(
                DateTime startTime,
                DateTime endTime,
                int page,
                int pageSize,
                string? nameFilter = null,
                string? carFilter = null,
                string? sortBy = null
            );
    }
}
