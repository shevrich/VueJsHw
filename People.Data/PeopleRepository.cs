using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Data
{
    public class PeopleRepository
    {
        private readonly string _connectionString;

        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Person> GetAll()
        {
            using (var context = new PeopleDbDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Person>(p => p.Cars);
                context.LoadOptions = loadOptions;
                return context.Persons.ToList();
            }
        }

        public void Add(Person person)
        {
            using (var context = new PeopleDbDataContext(_connectionString))
            {
                context.Persons.InsertOnSubmit(person);
                context.SubmitChanges();
            }
        }

        public IEnumerable<Car> GetCars(int personId)
        {
            using (var context = new PeopleDbDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Car>(c => c.Person);
                context.LoadOptions = loadOptions;
                return context.Cars.Where(c => c.PersonId == personId).ToList();
            }
        }

        public void AddCar(Car car)
        {
            using (var context = new PeopleDbDataContext(_connectionString))
            {
                context.Cars.InsertOnSubmit(car);
                context.SubmitChanges();
            }
        }

    }
}
