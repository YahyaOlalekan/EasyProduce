using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Abstractions.RepositoryInterfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence.AppDbContext;

namespace Persistence.RepositoryImplementations
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(Context context)
        {
            _context = context;
        }

        public async Task<Customer> GetAsync(Guid id)
        {
            return await _context.Customers
            .Include(a => a.Orders)
             .Include(a => a.User)
             .SingleOrDefaultAsync(a => a.UserId == id && !a.IsDeleted);
        }

        public async Task<Customer> GetAsync(Expression<Func<Customer, bool>> expression)
        {
            return await _context.Customers
            .Where(a => !a.IsDeleted)
            .Include(a => a.Orders)
            .Include(a => a.User)
            .SingleOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(Expression<Func<Customer, bool>> expression)
        {
            return await _context.Customers
            .Where(expression)
            .Include(a => a.User)
            .Include(a => a.Orders)
            .ToListAsync();

        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.AsNoTracking()
            .Where(a => !a.IsDeleted)
            .Include(a => a.User)
            .Include(a => a.Orders)
            .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.Customers
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
            .Include(a => a.User)
            .Include(a => a.Orders)
            .ToListAsync();
        }



        public async Task<IEnumerable<Customer>> GetSelectedAsync(Expression<Func<Customer, bool>> expression)
        {
            return await _context.Customers
            .Where(expression)
            //.Include(a => a.Orders)
            .Include(a => a.User)
            .ToListAsync();
        }

    }
}
