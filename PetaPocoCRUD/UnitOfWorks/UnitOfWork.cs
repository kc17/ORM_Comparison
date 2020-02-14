using PetaPoco;
using System.Data;
using System.Data.SqlClient;

namespace PetaPocoCRUD.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(string connectionStringName)
        {
            Context = new Database(new SqlConnection(connectionStringName));
            Context.Connection.Open();
            _useDispose = true;
            _transaction = new Transaction(Context);
        }

        public UnitOfWork(Database context)
        {
            Context = context;
            _useDispose = false;
        }

        public Database Context { get; private set; }
        private readonly bool _useDispose;
        private Transaction _transaction;

        public void Commit()
        {
            if (_transaction == null) return;

            _transaction.Complete();
            _transaction.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                Context.AbortTransaction();
                _transaction.Dispose();
            }

            if (_useDispose && Context != null)
            {
                if (Context.Connection.State != ConnectionState.Closed)
                {
                    Context.Connection.Close();
                }

                Context.Dispose();
                Context = null;
            }
        }
    }
}