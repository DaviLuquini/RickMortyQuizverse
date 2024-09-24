using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using QuizVerse.Api.Controllers;
using QuizVerse.Platform.Infrastructure.Database;
using System.Collections.Generic;
using QuizverseBack.Models;

namespace Quizverse.Api.Tests
{
    public class RegisterControllerTests
    {
        private RegisterController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new RegisterController();
        }

        [Test]
        public void Register_Should_Return_NameAlreadyExists_When_Name_Already_Exists()
        {
            RegisterRequest request = new() { Username = "test", Password = "456" };

            var result = _controller.Register(request) as UnauthorizedObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(401));

            //dynamic response = result.Value;
            //Assert.AreEqual("NAME_ALREADY_USED", response.code);
            //Assert.AreEqual("Register failed! Name already used.", response.message);
        }
    }
}
