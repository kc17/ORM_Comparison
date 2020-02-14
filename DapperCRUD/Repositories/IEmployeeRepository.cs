using DapperCRUD.Models;
using System.Collections.Generic;

namespace DapperCRUD.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> All();

        Employee Find(int id);

        void Add(Employee entity);

        void Update(Employee entity);

        void Remove(int id);
    }
}