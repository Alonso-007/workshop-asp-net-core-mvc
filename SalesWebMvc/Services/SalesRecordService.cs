using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value); //como se fosse and em uma query sql
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value); //como se fosse and em uma query sql
            }
            return await result
                .Include(x => x.Seller) //faz o join
                .Include(x => x.Seller.Department) //faz outro join
                .OrderByDescending(x => x.Date) //order por data
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department,SalesRecord>>> FindByGroupingDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value); //como se fosse and em uma query sql pq o result e um iqueryable
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value); //como se fosse and em uma query sql
            }
            return await result
                .Include(x => x.Seller) //faz o join
                .Include(x => x.Seller.Department) //faz outro join
                .OrderByDescending(x => x.Date) //order por data
                .GroupBy(x => x.Seller.Department) //faz retornar um igrouping
                .ToListAsync();
        }
    }
}
