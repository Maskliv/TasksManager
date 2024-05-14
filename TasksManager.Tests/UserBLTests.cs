using System.ComponentModel.DataAnnotations;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using TasksManager.Application.BL;
using TasksManager.Domain.DTO;
using TasksManager.Domain.Entities;
using TasksManager.Domain.Enum;
using TasksManager.Domain.Exceptions;
using TasksManager.Domain.Interfaces.BL;
using TasksManager.Domain.Interfaces.Persistence;

namespace TasksManager.Tests
{
    public class UserBLTests
    {
        //General Entities
        private IConfiguration _config;
        private IMapper _mapper;
        private UserValidation _userValidation;

        private UserBL _userBL;
        private Mock<IGenericRepository<User>> _userRepoMock;
        private User _testUser;
        private User _testUser2;

        private List<string> _invalidEmails = new List<string> { "", "userTestuserTe.co", "userTest2@", "userTest2@userTest", "userTest2@userTest." };
        private List<string> _invalidPasswords = new List<string> { "", "userTest", "userTest.", "usertest.123", "USER.123" };
        private List<string> _invalidUsernames = new List<string> { "" };
        private List<string> _invalidNames = new List<string> { "" };
        private List<string> _invalidRoles = new List<string> { "", "user", "admin", "raiz", "administrator" };
        private List<string?> _invalidPhones = new List<string?> { "+45 123456789", "1adff2225", "12345678901!" };

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

            _testUser2 = new User
            {
                Id = 2,
                Username = "userTest2",
                Password = "7jNCl4946MiOEoxgLzzVCVAxjswsD23zzlZnTwOhNQE=", // Password enrypted from userTest.123
                Email = "userTest2@userTest.com",
                Role = "User",
                Name = "Test2",
                DateCreated = new DateTime(2024, 01, 01),
            };

            _userRepoMock = new Mock<IGenericRepository<User>>();
            _userValidation = new UserValidation(_config);
            _userBL = new UserBL(_config, _userRepoMock.Object, _userValidation, _mapper);
        }

        [Test]
        public void CreateUser_Success()
        {
            //Arrange
            var newUser = new UserDto
            {
                username = "userTest2",
                password = "userTest.123",
                email = "userTest2@userTest.com",
                name = "Test2",
                role = ERole.User.ToString(),
                phone = "123456789",
                lastName = "lastname lastname"
            };
            _userRepoMock.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(_testUser2);
            _userRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<User>() { _testUser });


            //Act //Assert
            Assert.DoesNotThrowAsync(async () => await _userBL.CreateAsync(newUser));
        }

        [Test]
        public void CreateUser_InvalidUsername()
        {
            //Arrange
            _userRepoMock.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(_testUser2);
            _userRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<User>() { _testUser });

            ValidateInvalidUserField(nameof(UserDto.username), _invalidUsernames);
        }

        [Test]
        public void CreateUser_InvalidPassword()
        {
            //Arrange
            _userRepoMock.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(_testUser2);
            _userRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<User>() { _testUser });

            ValidateInvalidUserField(nameof(UserDto.password), _invalidPasswords);
        }

        [Test]
        public void CreateUser_InvalidEmail()
        {
            //Arrange
            _userRepoMock.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(_testUser2);
            _userRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<User>() { _testUser });

            ValidateInvalidUserField(nameof(UserDto.email), _invalidEmails);
        }

        [Test]
        public void CreateUser_InvalidName()
        {
            //Arrange
            _userRepoMock.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(_testUser2);
            _userRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<User>() { _testUser });

            ValidateInvalidUserField(nameof(UserDto.name), _invalidNames);
        }

        public void CreateUser_InvalidRole()
        {
            //Arrange
            _userRepoMock.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(_testUser2);
            _userRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<User>() { _testUser });

            ValidateInvalidUserField(nameof(UserDto.role), _invalidRoles);
        }

        public void CreateUser_InvalidPhone()
        {
            //Arrange
            _userRepoMock.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(_testUser2);
            _userRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<User>() { _testUser });

            ValidateInvalidUserField(nameof(UserDto.phone), _invalidPhones);
        }

        private void ValidateInvalidUserField<T>(string property, List<T> invalidValues)
        {

            PropertyInfo? propertyInfo = typeof(UserDto).GetProperty(property);
            if (propertyInfo == null || !propertyInfo.CanWrite) throw new Exception($"Property {property} not found or cannot be writed while Validating User");
            var newUser = new UserDto
            {
                username = "userTest2",
                password = "userTest.123",
                email = "userTest2@userTest.com",
                name = "Test2",
                role = ERole.User.ToString()
            };

            for (int i = 0; i < invalidValues.Count; i++)
            {
                propertyInfo.SetValue(newUser, invalidValues[i]);
                Assert.ThrowsAsync<BadRequestException>(async () => await _userBL.CreateAsync(newUser));
            }
        }
    }

}