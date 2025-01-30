using Microsoft.Extensions.Configuration;
using Moq;
using Pley.API.DTO;
using Pley.API.Model;
using Pley.API.Repo;
using Pley.API.Service;
using Pley.API.Util;

namespace Pley.TEST;

public class StoreServiceTests
{
    [Fact]
    public void GetStoreById_ShouldReturn_WhenStoreExists()
    {
        // Arrange
        var storeRepoMock = new Mock<IStoreRepo>();
        var configurationMock = new Mock<IConfiguration>();
        var utility = new Utility(); 

        var store = new Store
        { 
            Id = 1, Username = "store1", Password = "password123"
        };

        storeRepoMock.Setup(repo => repo.GetStoreById(It.IsAny<int>())).Returns(store);

        var storeService = new StoreService(storeRepoMock.Object, utility, configurationMock.Object);

        // Act
        var result = storeService.GetStoreById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("store1", result.Username);
    }

    [Fact]
    public void DeleteStoreById_ShouldReturnStoreOutDTO_WhenStoreExists()
    {
        // Arrange
        var storeRepoMock = new Mock<IStoreRepo>();
        var configurationMock = new Mock<IConfiguration>();
        var utility = new Utility();

        var store = new Store 
        { 
            Id = 1, Username = "store1", Password = "password123" 
        };

        storeRepoMock.Setup(repo => repo.GetStoreById(It.IsAny<int>())).Returns(store);
        storeRepoMock.Setup(repo => repo.DeleteStoreById(It.Is<Store>(s => s.Id == 1))).Returns(store);

        var storeService = new StoreService(storeRepoMock.Object, utility, configurationMock.Object);

        // Act
        var result = storeService.DeleteStoreById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("store1", result.Username);
        Assert.Equal(1, result?.Id);
    }

    [Fact]
    public void CreateNewStore_ShouldReturnStoreOutDTO()
    {
        // Arrange
        var storeRepoMock = new Mock<IStoreRepo>();
        var configurationMock = new Mock<IConfiguration>();
        var utility = new Utility();
        
        var newStoreDTO = new SignUpInDTO  
        { 
            Username = "store1", Password = "password123", Name = "Starbucks" 
        };

        var store = new Store 
        { 
            Id = 1, Username = "store1", Password = "password123", Name = "Starbucks" 
        };
        
        storeRepoMock.Setup(repo => repo.CreateNewStore(It.IsAny<Store>())).Returns(store);

        var storeService = new StoreService(storeRepoMock.Object, utility, configurationMock.Object);

        // Act
        var result = storeService.CreateNewStore(newStoreDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("store1", result.Username);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public void Login_ValidCredentials_ReturnsToken()
    {
        // Arrange
        var mockRepo = new Mock<IStoreRepo>();
        var mockUtility = new Mock<Utility>();
        var mockConfig = new Mock<IConfiguration>();
        
        var storeService = new StoreService(mockRepo.Object, mockUtility.Object, mockConfig.Object);

        var username = "validUser";
        var password = "validPassword";
        var user = new Store { Username = username, Password = password };
        
        mockRepo.Setup(repo => repo.Login(username)).Returns(user);
        
        // Set the correct length for the JWT key (256 bits / 32 characters)
        mockConfig.Setup(config => config["Jwt:Key"]).Returns("a_secure_256bit_key_for_testing_1234567890");
        mockConfig.Setup(config => config["Jwt:Issuer"]).Returns("issuer");

        // Act
        var result = storeService.Login(username, password);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("Token", result.ToString());
    }


    [Fact]
    public void Login_InvalidUsername_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var mockRepo = new Mock<IStoreRepo>();
        var mockUtility = new Mock<Utility>();
        var mockConfig = new Mock<IConfiguration>();

        var storeService = new StoreService(mockRepo.Object, mockUtility.Object, mockConfig.Object);

        var username = "Eldhose";
        var password = "validPassword";
        
        mockRepo.Setup(repo => repo.Login(username)).Returns((Store?)null);

        // Act
        var exception = Assert.Throws<UnauthorizedAccessException>(() => storeService.Login(username, password));
        
        // Assert
        Assert.Equal("Invalid Username.", exception.Message);
    }

    [Fact]
    public void Login_InvalidPassword_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var mockRepo = new Mock<IStoreRepo>();
        var mockUtility = new Mock<Utility>();
        var mockConfig = new Mock<IConfiguration>();
        var storeService = new StoreService(mockRepo.Object, mockUtility.Object, mockConfig.Object);

        var username = "Eldhose";
        var password = "wrongPassword";
        var user = new Store { Username = username, Password = "validPassword" };
        
        mockRepo.Setup(repo => repo.Login(username)).Returns(user);

        // Act 
        var exception = Assert.Throws<UnauthorizedAccessException>(() => storeService.Login(username, password));
        
        // Assert
        Assert.Equal("Invalid Password.", exception.Message);
    }

    [Fact]
    public void Logout_ValidToken_BlacklistsToken()
    {
        // Arrange
        var mockRepo = new Mock<IStoreRepo>();
        var mockUtility = new Mock<Utility>();
        var mockConfig = new Mock<IConfiguration>();

        var storeService = new StoreService(mockRepo.Object, mockUtility.Object, mockConfig.Object);

        var token = "validToken";

        // Act
        storeService.Logout(token);
    }

    [Fact]
    public void GetAllStores_ReturnsListOfStores()
    {
        // Arrange
        var mockRepo = new Mock<IStoreRepo>();
        var mockUtility = new Mock<Utility>();
        var mockConfig = new Mock<IConfiguration>();

        var storeService = new StoreService(mockRepo.Object, mockUtility.Object, mockConfig.Object);

        var stores = new List<Store>
        {
            new Store { Id = 1, Name = "Store1" },
            new Store { Id = 2, Name = "Store2" }
        };

        mockRepo.Setup(repo => repo.GetAllStores()).Returns(stores);

        // Act
        var result = storeService.GetAllStores();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void UpdateStore_ValidData_UpdatesStore()
    {
        // Arrange
        var mockRepo = new Mock<IStoreRepo>();
        var mockUtility = new Mock<Utility>();
        var mockConfig = new Mock<IConfiguration>();

        var storeService = new StoreService(mockRepo.Object, mockUtility.Object, mockConfig.Object);

        var store = new Store { Id = 1, Name = "Old Store", Description = "Old Description", URL = "old.com" };
        var editDTO = new EditStoreDTO { Name = "New Store", Description = "New Description", URL = "new.com" };

        mockRepo.Setup(repo => repo.GetStoreById(1)).Returns(store);
        mockRepo.Setup(repo => repo.UpdateStore(store)).Returns(store);

        // Act
        var result = storeService.UpdateStore(1, editDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New Store", result.Name);
        Assert.Equal("New Description", result.Description);
        Assert.Equal("new.com", result.URL);
    }

    [Fact]
    public void UpdateStore_StoreNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        var mockRepo = new Mock<IStoreRepo>();
        var mockUtility = new Mock<Utility>();
        var mockConfig = new Mock<IConfiguration>();

        var storeService = new StoreService(mockRepo.Object, mockUtility.Object, mockConfig.Object);

        mockRepo.Setup(repo => repo.GetStoreById(1)).Returns((Store?)null);

        // Act
        var exception = Assert.Throws<KeyNotFoundException>(() => storeService.UpdateStore(1, new EditStoreDTO()));
        
        // Assert
        Assert.Equal("Store with Id 1 not found", exception.Message);
    }

    [Fact]
    public void UpdateLogin_ValidData_UpdatesStore()
    {
        // Arrange
        var mockRepo = new Mock<IStoreRepo>();
        var mockUtility = new Mock<Utility>();
        var mockConfig = new Mock<IConfiguration>();

        var storeService = new StoreService(mockRepo.Object, mockUtility.Object, mockConfig.Object);

        var store = new Store { Id = 1, Username = "oldUsername", Password = "oldPassword" };
        var editDTO = new EditLoginDTO { Username = "newUsername", Password = "newPassword" };

        mockRepo.Setup(repo => repo.GetStoreById(1)).Returns(store);
        mockRepo.Setup(repo => repo.UpdateStore(store)).Returns(store);

        // Act
        var result = storeService.UpdateLogin(store, editDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("newUsername", result.Username);
        Assert.Equal("newPassword", result.Password);
    }

    [Fact]
    public void UpdateLogin_EmptyUsername_DoesNotChangeUsername()
    {
        // Arrange
        var mockRepo = new Mock<IStoreRepo>();
        var mockUtility = new Mock<Utility>();
        var mockConfig = new Mock<IConfiguration>();
        var storeService = new StoreService(mockRepo.Object, mockUtility.Object, mockConfig.Object);

        var store = new Store { Id = 1, Username = "oldUsername", Password = "oldPassword" };
        var editDTO = new EditLoginDTO { Username = "", Password = "newPassword" };

        mockRepo.Setup(repo => repo.GetStoreById(1)).Returns(store);
        mockRepo.Setup(repo => repo.UpdateStore(store)).Returns(store);

        // Act
        var result = storeService.UpdateLogin(store, editDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("oldUsername", result.Username);
        Assert.Equal("newPassword", result.Password);
    }
}