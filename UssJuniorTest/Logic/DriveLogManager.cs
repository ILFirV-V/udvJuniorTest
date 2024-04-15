using UssJuniorTest.Core.Models;
using UssJuniorTest.Core;
using UssJuniorTest.Core.Models.Response;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UssJuniorTest.Logic
{
    public class DriveLogManager : IDriveLogManager
    {
        private readonly IAllRepositories context;

        public DriveLogManager(IAllRepositories repositories) 
        {
            context = repositories;
        }

        public List<ResponseDriveLog> GetDriveLogInfo(
                DateTime startTime, 
                DateTime endTime,
                int page, 
                int pageSize,
                string? nameFilter = null, 
                string? carFilter = null, 
                string? sortBy = null
            )
        {
            if (startTime > endTime)
                throw new ArgumentException("Start time cannot be greater than end time.");

            if (page <= 0 || pageSize <= 0)
                throw new ArgumentException("Page and page size must be greater than zero.");

            var driveLogs = GetAllDriversInfoInInterval(startTime, endTime);

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                driveLogs = GetAllDriversInfoWithNamePerson(driveLogs, nameFilter);
            }

            if (!string.IsNullOrEmpty(carFilter))
            {
                driveLogs = GetAllDriversInfoWithModelCar(driveLogs, carFilter);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                driveLogs = GetAllDriversInfoSort(driveLogs, sortBy);
            }

            var driversInfo = driveLogs.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return driversInfo;
        }

        private List<ResponseDriveLog> GetAllDriversInfoSort(List<ResponseDriveLog> logs, string sortBy)
        {
            return sortBy.ToLower() switch
            {
                "name" => logs.OrderBy(drivLog => drivLog.Person.Name).ToList(),
                "model" => logs.OrderBy(drivLog => drivLog.Car.Model).ToList(),
                _ => logs.OrderBy(drivLog => drivLog.Person.Name).ToList(),
            };
        }

        private List<ResponseDriveLog> GetAllDriversInfoWithFilter(
            List<ResponseDriveLog> logs, 
            Func<ResponseDriveLog, string, bool> filterFunc, 
            string filter)
        {
            if(string.IsNullOrWhiteSpace(filter))
                throw new ArgumentNullException(nameof(filter));
            return logs.Where(drivLog => filterFunc(drivLog, filter)).ToList();
        }

        private List<ResponseDriveLog> GetAllDriversInfoWithModelCar(List<ResponseDriveLog> logs, string carFilter)
        {
            return GetAllDriversInfoWithFilter(logs, (drivLog, filter) => drivLog.Car.Model.Contains(filter), carFilter);
        }

        private List<ResponseDriveLog> GetAllDriversInfoWithNamePerson(List<ResponseDriveLog> logs, string nameFilter)
        {
            return GetAllDriversInfoWithFilter(logs, (drivLog, filter) => drivLog.Person.Name.Contains(filter), nameFilter);
        }

        private List<ResponseDriveLog> GetAllDriversInfoInInterval(DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime)
            {
                throw new ArgumentException("Start time cannot be greater than end time.");
            }
            var driveLogs = context.RepositoryDriveLog.GetAll();
            if (driveLogs == null)
            {
                throw new Exception("No drive logs found.");
            }
            var items = new List<ResponseDriveLog>();
            foreach (var driveLog in driveLogs)
            {
                if (driveLog.EndDateTime < startTime || driveLog.StartDateTime > endTime)
                {
                    continue;
                }
                var car = context.RepositoryCar.Get(driveLog.CarId);
                if (car == null)
                {
                    throw new Exception("Car information not found for drive log.");
                }
                var person = context.RepositoryPerson.Get(driveLog.PersonId);
                if (person == null)
                {
                    throw new Exception("Person information not found for drive log.");
                }
                var currentEndTime = driveLog.EndDateTime < endTime ? driveLog.EndDateTime : endTime;
                var time = currentEndTime - driveLog.StartDateTime;
                var item = new ResponseDriveLog(car, person, time.ConvertTimeSpanInTimeDto());
                items.Add(item);
            }
            return items;
        }
    }
}
