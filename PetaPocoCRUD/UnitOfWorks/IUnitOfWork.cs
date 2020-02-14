using PetaPoco;
using System;

namespace PetaPocoCRUD.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        Database Context { get; }

        void Commit();
    }
}