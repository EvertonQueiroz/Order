using Order.Domain.Interfaces.Data;
using Order.Infra.Data.Context;

namespace Order.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderDBContext _context;

        public UnitOfWork(OrderDBContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            var rowsAffected = _context.SaveChanges();
            return rowsAffected > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
