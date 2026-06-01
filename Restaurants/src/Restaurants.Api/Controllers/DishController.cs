using Microsoft.AspNetCore.Mvc;
using MediatR;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Dishes.Queries.GetAllDishesForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishForRestaurant;
using Restaurants.Application.Dishes.Commands.DeleteDishForRestaurant;
using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/restaurant/{restaurantid}/dish")]
    public class DishController(IMediator mediator) : Controller
    {

        [HttpPost]
        
        public  async Task<IActionResult> Create([FromRoute]int restaurantid, [FromBody] CreateDishCommand command)
        {
            command.RestaurantId = restaurantid;
            var dishId=await mediator.Send(command);
            return CreatedAtAction(nameof(GetForRestaurant),
                new{ 
                    restaurantid = restaurantid,
                    DishId = dishId
                },
                null);
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant
            ([FromRoute] int restaurantid) {


            var query = new GetAllDishesForRestaurantQuery(restaurantid);

             var result=  await mediator.Send(query);
            return Ok(result);
            
        
        }

        [HttpGet("{DishId}")]

        public async Task<ActionResult<DishDto>> GetForRestaurant
            ([FromRoute] int restaurantid, [FromRoute] int DishId) {


            var query = new GetDishForRestaurantQuery(restaurantid,DishId);

             var result=  await mediator.Send(query);
            return Ok(result);
            
        
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDishForRestaurant([FromRoute] int restaurantid) {


             await mediator.Send(new DeleteDishesForRestaurantCommand(restaurantid));
        
             return NoContent();
        }

    }
}
