using Microsoft.AspNetCore.Mvc;
using QuizVerse.Api.Controllers;
using QuizVerse.Platform.Application;
using QuizverseBack.Requests;

namespace Quizverse.Api.Tests
{
    [TestFixture]
    public class RegisterControllerTests
    {
        private RegisterController _controller;

        private IUserAppService _userAppService;

        [SetUp]
        public void Setup()
        {
            _controller = new RegisterController(_userAppService);
        }

        [Test]
        public void Register_Should_Return_NameAlreadyExists_When_Name_Already_Exists()
        {
            UserRegisterRequestDto request = new() { Username = "test", Password = "456" };

            var result = _controller.Register(request) as UnauthorizedObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(401));

            //dynamic response = result.Value;
            //Assert.AreEqual("NAME_ALREADY_USED", response.code);
            //Assert.AreEqual("Register failed! Name already used.", response.message);
        }
    }
}
