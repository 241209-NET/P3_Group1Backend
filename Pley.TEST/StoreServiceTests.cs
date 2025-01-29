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
        var utility = new Utility(); 

        var store = new Store
        { 
            Id = 1, Username = "store1", Password = "password123"
        };

        storeRepoMock.Setup(repo => repo.GetStoreById(It.IsAny<int>())).Returns(store);

        var storeService = new StoreService(storeRepoMock.Object, utility);

        // Act
        var result = storeService.GetStoreById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("store1", result.Username);
    }

    [Fact]
    public void GetStoreById_ShouldReturnNull_WhenStoreNotExist()
    {
        // Arrange
        var storeRepoMock = new Mock<IStoreRepo>();
        var utility = new Utility(); 

        storeRepoMock.Setup(repo => repo.GetStoreById(It.IsAny<int>())).Returns((Store?)null);

        var storeService = new StoreService(storeRepoMock.Object, utility);

        // Act
        var result = storeService.GetStoreById(99);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteStoreById_ShouldReturnNull_WhenStoreDoesNotExist()
    {
        // Arrange
        var storeRepoMock = new Mock<IStoreRepo>();
        var utility = new Utility();

        storeRepoMock.Setup(repo => repo.GetStoreById(It.IsAny<int>())).Returns((Store?)null);

        var storeService = new StoreService(storeRepoMock.Object, utility);

        // Act
        var result = storeService.DeleteStoreById(99);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteStoreById_ShouldReturnStoreOutDTO_WhenStoreExists()
    {
        // Arrange
        var storeRepoMock = new Mock<IStoreRepo>();
        var utility = new Utility();

        var store = new Store 
        { 
            Id = 1, Username = "store1", Password = "password123" 
        };

        storeRepoMock.Setup(repo => repo.GetStoreById(It.IsAny<int>())).Returns(store);
        storeRepoMock.Setup(repo => repo.DeleteStoreById(It.Is<Store>(s => s.Id == 1))).Returns(store);


        var storeService = new StoreService(storeRepoMock.Object, utility);

        // Act
        var result = storeService.DeleteStoreById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("store1", result.Username);
        Assert.Equal(1, result?.Id);
        storeRepoMock.Verify(repo => repo.DeleteStoreById(It.Is<Store>(s => s.Id == 1)), Times.Once);
    }

    [Fact]
    public void CreateNewStore_ShouldReturnStoreOutDTO()
    {
        // Arrange
        var storeRepoMock = new Mock<IStoreRepo>();
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

        var storeService = new StoreService(storeRepoMock.Object, utility);

        // Act
        var result = storeService.CreateNewStore(newStoreDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("store1", result.Username);
        Assert.Equal(1, result.Id);
    }
}