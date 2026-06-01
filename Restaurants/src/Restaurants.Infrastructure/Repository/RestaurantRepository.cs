using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System.Linq;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repository;

internal class RestaurantRepository(RestaurantsDbContext db) : IRestaurantRepository
{
   

    public async Task<int> Create(Restaurant entity)
    {
       db.Restaurants.Add(entity);
       await SaveChanges();
        return entity.Id;
    }

    public async Task Delete(Restaurant entity)
    {
       
        db.Restaurants.Remove(entity);
        await SaveChanges();
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await db.Restaurants.ToListAsync();      
    }

  

    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync
        (string? search, int PageSize, int pageNumber, string? Sortby, SortDirection sortDirection)
    {

        var query = db.Restaurants.AsQueryable();
        var Totalcount = await query.CountAsync();
        if (!String.IsNullOrEmpty(search))
        {
            search = search.ToLower();

            query.Where(x => x.Name.ToLower().Contains(search)
            || x.Description.ToLower().Contains(search));

        }
        // المفروض يحل امتي بعد ال pagenation

        if (!String.IsNullOrEmpty(Sortby))
        {
            var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>> {


                {nameof(Restaurant.Name),x=>x.Name },
                {nameof(Restaurant.Category),x=>x.Category },
                {nameof(Restaurant.Description),x=>x.Description }
            };




            var SelectedColum =  (columnSelector[Sortby]);

            query = sortDirection == SortDirection.Ascending ?
                query.OrderBy(SelectedColum):query.OrderByDescending(SelectedColum);

        
        }



            var items =await query.Skip((pageNumber - 1) * PageSize).
                Take(PageSize).ToListAsync();




        return  (items,Totalcount);
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await db.Restaurants.Include(dish=>dish.Dishes).FirstOrDefaultAsync(rest => rest.Id == id);
        if (restaurant is null)
            return null;

        return restaurant;

    }

    public async Task SaveChanges() =>  await db.SaveChangesAsync();
   
}
