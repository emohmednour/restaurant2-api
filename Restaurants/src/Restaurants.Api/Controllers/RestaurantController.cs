using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurant;
using Restaurants.Application.Restaurants.Queries.GetRestaurant;
using Restaurants.Infrastructure.Authorization;


namespace Restaurants.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/restaurant")]
    public class RestaurantController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
     
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetAll
            ([FromQuery]GetAllRestaurantQuery query )
        {
            

            var restaurants =  await mediator.Send(query);

            return Ok(restaurants);


        }


        [HttpGet("{Id}")]
        //[Authorize(Roles = UserRoles.Owner)]
        //[Authorize(Policy = PolicyNames.HasNationality)]
        //[Authorize(Policy = PolicyNames.AtLeast20)]

        //[Authorize(Policy = PolicyNames.AtLeast2Restaurant)]
        public async Task<ActionResult<RestaurantDTO>> GetRestaurantById([FromRoute] int Id)
        {
            var query = new GetRestaurantQuery(Id);

            var restaurant =  await mediator.Send(query);
            


            return Ok(restaurant);


        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand  command)
        {

            var id = await mediator.Send(command);

            return CreatedAtAction(nameof(GetRestaurantById), new {id},null);
        

        
        
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int Id) {


            

            await mediator.Send(new DeleteRestaurantCommand(Id));



            return NoContent();


        }



        [HttpPut("{Id}")]
        //[produc
        public async Task<IActionResult> UpdateRestaurant
            ([FromRoute] int Id, [FromBody] UpdateRestaurantCommand command) {

            command.Id = Id;
            await mediator.Send(command);


            return NoContent();


        }


    }
}
