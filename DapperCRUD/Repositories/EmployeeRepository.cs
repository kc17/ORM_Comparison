using Dapper;
using DapperCRUD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DapperCRUD.Repositories
{
    internal class EmployeeRepository : RepositoryBase, IEmployeeRepository
    {
        public EmployeeRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public IEnumerable<Employee> All()
        {
            return Connection.Query<Employee>(
                "SELECT * FROM Employee",
                transaction: Transaction
            );
        }

        public Employee Find(int id)
        {
            return Connection.Query<Employee>(
                "SELECT * FROM Employee WHERE id = @id",
                param: new { id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Add(Employee entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            entity.id = Connection.ExecuteScalar<int>(
                "INSERT INTO Employee (firstName, lastName, age, email) VALUES (@firstName, @lastName, @age, @email); SELECT SCOPE_IDENTITY()",
                param: new { entity.firstName, entity.lastName, entity.age, entity.email },
                transaction: Transaction
            );
        }

        public void Update(Employee entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Connection.Execute(
                "UPDATE Employee SET firstName = @firstName, lastName = @lastName, age = @age, email = @email WHERE id = @id",
                param: new { entity.id, entity.firstName, entity.lastName, entity.age, entity.email },
                transaction: Transaction
            );
        }

        public void Remove(int id)
        {
            Connection.Execute(
                "DELETE FROM Employee WHERE id = @id",
                param: new { id },
                transaction: Transaction
            );
        }
    }
}