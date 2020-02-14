using DapperCRUD.Repositories;
using System;

namespace DapperCRUD.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }

        void Complete();
    }
}