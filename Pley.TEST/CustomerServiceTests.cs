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

    [Fact]
    public void AvgRatingHelper_ShouldCalculateAvgRatingForCustomer()
    {
        // Arrange
        var customerRepoMock = new Mock<ICustomerRepo>();
        var reviewRepoMock = new Mock<IReviewRepo>();

        var utility = new Utility(); // Use the real utility
        var customer = new Customer 
        { 
            Id = 1, Name = "Customer 1" 
        };

        var reviews = new List<Review>
        {
            new Review { CustomerId = 1, Rating = 4, Comment = "Good service" },
            new Review { CustomerId = 1, Rating = 5, Comment = "Good experience" }
        };

        reviewRepoMock.Setup(repo => repo.GetAllReviews()).Returns(reviews);

        var customerService = new CustomerService(customerRepoMock.Object, reviewRepoMock.Object, utility);

        // Act
        customerService.AvgRatingHelper(customer);

        // Assert
        Assert.Equal(4.5, customer.AvgRating);
    }

    [Fact]
    public void GetAllCustomers_ShouldReturnCustomersWithAvgRating()
    {
        // Arrange
        var customerRepoMock = new Mock<ICustomerRepo>();
        var reviewRepoMock = new Mock<IReviewRepo>();
        var utility = new Utility();

        var customers = new List<Customer>
        {
            new() { Id = 1, Name = "Customer 1"},
            new() { Id = 2, Name = "Customer 2" }
        };

        var reviews = new List<Review>
        {
            new() { CustomerId = 1, Rating = 4,  Comment = "Good service" },
            new() { CustomerId = 1, Rating = 5,  Comment = "Good experience" },
            new() { CustomerId = 2, Rating = 3,  Comment = "Great service" }
        };

        customerRepoMock.Setup(repo => repo.GetAllCustomers()).Returns(customers);
        reviewRepoMock.Setup(repo => repo.GetAllReviews()).Returns(reviews);

        var customerService = new CustomerService(customerRepoMock.Object, reviewRepoMock.Object, utility);

        // Act
        var result = customerService.GetAllCustomers();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(4.5, result.First(c => c.Id == 1).AvgRating);
        Assert.Equal(3, result.First(c => c.Id == 2).AvgRating);
    }

}