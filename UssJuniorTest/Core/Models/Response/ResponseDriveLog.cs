namespace UssJuniorTest.Core.Models.Response
{
    public class ResponseDriveLog 
    {
        public ResponseDriveLog(Car car, Person person, TimeDto drivingTime)
        {
            Car = car;
            Person = person;
            DriveTime = drivingTime;
        }

        /// <summary>
        /// Информация о машине.
        /// </summary>
        public Car Car { get; set; }

        /// <summary>
        /// Информация о человеке.
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Время вождения.
        /// </summary>
        public TimeDto DriveTime { get; set; }
    }
}