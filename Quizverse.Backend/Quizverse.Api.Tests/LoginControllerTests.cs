using QuizVerse.Api.Controllers;
using QuizVerse.Platform.Application.Requests;
using QuizverseBack.Requests;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;



namespace Quizverse.Api.Tests
{
    [TestFixture]
    public class LoginControllerTests
    {
        [Test]
        public async Task LoginUser()
        {
            var httpClient = new HttpClient {
                BaseAddress = new Uri("https://localhost:7295")
            };

            Random random = new();
            int randomNumber = random.Next();

            // Register User
            var registerRequest = new UserRegisterRequestDto { Username = "validUser" + randomNumber, Password = "validPassword" };
            var jsonRegisterRequest = JsonSerializer.Serialize(registerRequest);
            var registerContent = new StringContent(jsonRegisterRequest, Encoding.UTF8, "application/json");

            var registerResponse = await httpClient.PostAsync("api/register", registerContent, CancellationToken.None);
            Assert.That(registerResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            // Log In
            var loginRequest = new UserLoginRequestDto { Username = "validUser" + randomNumber, Password = "validPassword" };
            var jsonLoginRequest = JsonSerializer.Serialize(loginRequest);
            var loginContent = new StringContent(jsonLoginRequest, Encoding.UTF8, "application/json");

            //TO-DO mudar a logica para essa abaixo usando metodo inves de passar direto na API, e usando Mocks!
            //LoginController loginController = new();
            //loginController.Login(loginRequest);

            var loginResponse = await httpClient.PostAsync("api/login", loginContent, CancellationToken.None);
            Assert.That(loginResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        //await cloudExpertAppServiceMock.Received(1)
        //    .AddAddress(
        //        Arg.Is<AddAddressCommand>(a => a.Street == command.Street && a.City == command.City),
        //        Arg.Any<CancellationToken>());
    }
}
