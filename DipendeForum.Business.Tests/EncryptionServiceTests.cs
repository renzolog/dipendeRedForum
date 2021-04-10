using System;
using DipendeForum.Domain;
using DipendeForum.Interfaces.Services;
using DipendeForum.Services;
using Xunit;

namespace DipendeForum.Business.Tests
{
    public class EncryptionServiceTests
    {
        private readonly IEncryptionService _service;
        private readonly string _password;
        private readonly Role _role;

        public EncryptionServiceTests()
        {
            _service = new EncryptionService();
            _password = Guid.NewGuid().ToString();
            _role = Role.Subscriber;
        }

        [Fact]
        public void EncryptPassword_InputStringPassword_ReturnsStringEncryptedPassword()
        {
            var encryptedPassword = _service.EncryptPassword(_password);
            Assert.NotEqual(_password, encryptedPassword);
        }

        [Fact]
        public void EncryptRole_InputEnumTypeRole_ReturnsStringEncryptedRole()
        {
            var encryptedRole = _service.EncryptRole((int)_role);
            Assert.True(encryptedRole.Length > 1);
            Assert.IsType<string>(encryptedRole);
        }

        [Fact]
        public void IsPasswordMatching_InputTwoDifferentString_ReturnsBoolComparisonTrue()
        {
            var password = "hello";
            var encryptedPassword = _service.EncryptPassword(password);
            var result = _service.IsPasswordMatching(encryptedPassword, password);
            Assert.True(result);
        }

        [Fact]
        public void IsPasswordMatching_InputTwoDifferentString_ReturnsBoolComparisonFalse()
        {
            var encryptedPassword = _service.EncryptPassword(Guid.NewGuid().ToString());
            var result = _service.IsPasswordMatching(encryptedPassword, Guid.NewGuid().ToString());
            Assert.False(result);
        }

        [Fact]
        public void DecryptRole_InputStringEncryptedRole_ReturnsEnumTypeRole()
        {
            var encryptedRole = _service.EncryptRole((int)_role);
            var decryptedRole = _service.DecryptRole(encryptedRole);
            Assert.IsType<Role>(decryptedRole);
            Assert.Equal(_role, decryptedRole);
        }
    }
}