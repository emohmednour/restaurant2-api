using Xunit;
using Restaurants.Api.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Restaurants.Domain.Repositories;
using Moq;
using Restaurants.Domain.Entities;
using System.Net.Http.Json;
using Restaurants.Application.Restaurants.DTOs;


namespace Restaurants.Api.Controllers.Tests
{
    public class RestaurantControllerTests  : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _Factory;
        private readonly Mock<IRestaurantRepository> _MockRepository = new();
        public RestaurantControllerTests(WebApplicationFactory<Program> Factory)
        {
            _Factory = Factory.WithWebHostBuilder(Builder=>
            {
                Builder.ConfigureServices(

                    service => {
                        service.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                        service.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantRepository), (_ =>
                        _MockRepository.Object)));

                       
                    });
                    

            });

            

        }

        [Fact()]
        public async Task GetAll_WithValid_ShouldreturnOk200()
        {

            //arrange
            var request=  _Factory.CreateClient();

            //act
            var result =await request.GetAsync("/api/restaurant?PageNumber=1&PageSize=10");
            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            //assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        
        
        }
        [Fact()]
        public async Task GetAll_WithInValid_ShouldreturnBadRequest400()
        {

            //arrange
            var request=  _Factory.CreateClient();

            //act
            var result =await request.GetAsync("api/restaurant");

            //assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);      
        
        
        
        }




        [Fact()]
        public async Task GetBuId_ForNonExisting_ShouldreturnNotFound404()
        {

            //arrange
            int Id = 14;
            var request = _Factory.CreateClient();
            _MockRepository.Setup(x => x.GetByIdAsync(Id)).
                ReturnsAsync((Restaurant?)null);

            //act
            var result = await request.GetAsync($"api/restaurant/{Id}");

            //assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);


        }
        [Fact()]
        public async Task GetBuId_ForExisting_Shouldreturn200Ok()
        {

            //arrange
            int Id = 14;
            var restaurant = new Restaurant
            {

                Id = Id,
                Name="name",
                Description= "description",
            };



            _MockRepository.Setup(x => x.GetByIdAsync(Id)).
                ReturnsAsync(restaurant);



            var client = _Factory.CreateClient();

            //act
            var result = await client.GetAsync($"api/restaurant/{Id}");


            var restaurantdto =await result.Content.ReadFromJsonAsync<RestaurantDTO>();

            //assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            restaurantdto.Should().NotBeNull();
            restaurantdto.Name.Should().Be(restaurant.Name);
            restaurantdto.Description.Should().Be(restaurant.Description);


        }
    }
}