using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data.Models;
using SalesWebMVC.Models;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync() => await _context.Seller.ToListAsync();

        public async Task<Seller> FindByIdAsync(int id) => await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(seller => seller.Id == id);

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        

        public async Task RemoveAsync(int id) 
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == seller.Id);
            if(!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();

            }catch(DbUpdateConcurrencyException ex) 
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
