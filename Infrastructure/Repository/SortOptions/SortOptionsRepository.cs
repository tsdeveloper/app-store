using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.SortOptions {
    public class SortOptionsRepository : ISortOptionRepository {
        private readonly AppStoreContext _context;
        protected DbSet<SortOption> DbSet;

        public SortOptionsRepository (AppStoreContext context) {
            _context = context;
            DbSet = _context.Set<SortOption> ();

        }
       
    }
}