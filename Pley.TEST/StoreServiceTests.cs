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

        var store = new Store { Id = 1, Username = "store1", Password = "password123" };
        storeRepoMock.Setup(repo => repo.GetStoreById(It.IsAny<int>())).Returns(store);
        storeRepoMock.Setup(repo => repo.DeleteStoreById(It.IsAny<int>())).Verifiable();

        var storeService = new StoreService(storeRepoMock.Object, utility);

        // Act
        var result = storeService.DeleteStoreById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("store1", result.Username);
        storeRepoMock.Verify(repo => repo.DeleteStoreById(It.IsAny<int>()), Times.Once);
    }
}