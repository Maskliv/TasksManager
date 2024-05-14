using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using TasksManager.Application.BL;
using TasksManager.Domain.DTO;
using TasksManager.Domain.Entities;
using TasksManager.Domain.Enum;
using TasksManager.Domain.Interfaces.BL;

namespace TasksManager.Tests
{
    public class AuthBLTests
    {
        //General Entities
        private IConfiguration _config;
        private IMapper _mapper;

        private AuthBL _authBL;
        private UserValidation _userValidation;

        private Mock<IUserBL> _userBLMock;

        private User _testUser;

        [SetUp]
        public void Setup()
        {
            var TestStartup = new TestStartup();
            _config = TestStartup.Config;
            _mapper = TestStartup.Mapper;

            _testUser = new User
            {
                Id = 1,
                Username = "userTest",
                Password = "7jNCl4946MiOEoxgLzzVCVAxjswsD23zzlZnTwOhNQE=", // Password enrypted from userTest.123
                Email = "userTest@userTest.com",
                Role = "User",
                Name = "Test",
                DateCreated = new DateTime(2024, 01, 01),

            };

            _userBLMock = new Mock<IUserBL>();
            _userValidation = new UserValidation(_config);
            _authBL = new AuthBL(_userBLMock.Object, _userValidation, _config, _mapper);
        }

        [Test]
        public async Task Login_ValidSuccess()
        {
            //Arrange
            var loginData = new LoginDto
            {
                username = "userTest",
                password = "userTest.123"
            };
            _userBLMock.Setup(x => x.GetByUsernameAsync("userTest")).ReturnsAsync(_testUser);

            //Act
            var result = await _authBL.Login(loginData);

            //Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task Login_InvalidPasswordOrUsername()
        {
            //Arrange
            var loginData = new LoginDto
            {
                username = "userTest",
                password = "otherPassword"
            };
            _userBLMock.Setup(x => x.GetByUsernameAsync("userTest")).ReturnsAsync(_testUser);

            //Act
            var result = await _authBL.Login(loginData);

            //Assert
            Assert.That(result, Is.Null);

            //Arrange
            var loginData2 = new LoginDto
            {
                username = "otherUsername",
                password = "userTest.123" 
            };
            _userBLMock.Setup(x => x.GetByUsernameAsync("userTest")).ReturnsAsync(_testUser);

            //Act
            var result2 = await _authBL.Login(loginData);

            //Assert
            Assert.That(result2, Is.Null);
        }

        [Test]
        public async Task SingUp_ValidSuccess()
        {
            //Arrange
            var newUser = new UserDto
            {
                username = "userTest",
                password = "userTest.123",
                email = "userTest@userTest.com",
                name = "Test",
                role = ERole.User.ToString()
            };
            _userBLMock.Setup(x => x.CreateAsync(It.IsAny<UserDto>())).ReturnsAsync(_testUser);

            //Act                
            var result = await _authBL.SignUp(newUser);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.password, Is.Null);
        }

    }
}