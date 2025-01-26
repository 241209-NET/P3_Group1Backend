using Moq;
using Pley.API.Model;
using Pley.API.Repo;
using Pley.API.Service;
using Pley.API.Util;

namespace Pley.TEST;

public class CustomerServiceTests()
{
   [Fact]
    public void GetCustomerById_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        var mockReviewRepo = new Mock<IReviewRepo>();
        var mockUtility = new Mock<Utility>();
        var customerService = new CustomerService(mockCustomerRepo.Object, mockReviewRepo.Object, mockUtility.Object);

        mockCustomerRepo.Setup(repo => repo.GetCustomerById(99)).Returns((Customer?)null);

        // Act
        var result = customerService.GetCustomerById(99);

        // Assert
        Assert.Null(result);  
    } 

    [Fact]
    public void GetCustomerByName_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        var mockReviewRepo = new Mock<IReviewRepo>();
        var mockUtility = new Mock<Utility>();
        var customerService = new CustomerService(mockCustomerRepo.Object, mockReviewRepo.Object, mockUtility.Object);

        mockCustomerRepo.Setup(repo => repo.GetCustomerByName("Eldhose")).Returns((Customer?)null);

        // Act
        var result = customerService.GetCustomerByName("Eldhose");

        // Assert
        Assert.Null(result);  
    }

    [Fact]
    public void GetAllCustomers_ShouldReturnNull_WhenNoCustomers()
    {
        // Arrange
        var customerRepoMock = new Mock<ICustomerRepo>();
        var reviewRepoMock = new Mock<IReviewRepo>();
        var utilityMock = new Mock<Utility>();

        customerRepoMock.Setup(repo => repo.GetAllCustomers()).Returns((List<Customer>)null);

        var customerService = new CustomerService(customerRepoMock.Object, reviewRepoMock.Object, utilityMock.Object);

        // Act
        var result = customerService.GetAllCustomers();

        // Assert
        Assert.Null(result);
    }

}