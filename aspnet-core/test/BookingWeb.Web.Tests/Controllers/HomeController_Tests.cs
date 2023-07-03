using System.Threading.Tasks;
using BookingWeb.Models.TokenAuth;
using BookingWeb.Web.Controllers;
using Shouldly;
using Xunit;

namespace BookingWeb.Web.Tests.Controllers
{
    public class HomeController_Tests: BookingWebWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}