using UssJuniorTest.Core.Models;

namespace UssJuniorTest.Core
{
    public interface IAllRepositories
    {
        public IRepository<DriveLog> RepositoryDriveLog { get; }
        public IRepository<Car> RepositoryCar { get; }
        public IRepository<Person> RepositoryPerson { get; }
    }
}
