using UssJuniorTest.Core;
using UssJuniorTest.Core.Models;

namespace UssJuniorTest.Infrastructure.Repositories
{
    public class AllRepositories : IAllRepositories
    {
        public IRepository<DriveLog> RepositoryDriveLog { get; }
        public IRepository<Car> RepositoryCar { get; }
        public IRepository<Person> RepositoryPerson { get; }

        public AllRepositories(IRepository<DriveLog> repositoryDriveLog, IRepository<Car> repositoryCar, IRepository<Person> repositoryPerson)
        {
            RepositoryDriveLog = repositoryDriveLog;
            RepositoryCar = repositoryCar;
            RepositoryPerson = repositoryPerson;
        }
    }
}
