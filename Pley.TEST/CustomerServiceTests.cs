using Moq;
using Pley.API.Model;
using Pley.API.Repo;
using Pley.API.Service;
using Pley.API.Util;

namespace Pley.TEST;

public class CustomerServiceTests
{
    [Fact]
    public void GetAllCustomers_ShouldReturnNull_WhenNoCustomers()
    {
        // Arrange
        var customerRepoMock = new Mock<ICustomerRepo>();
        var reviewRepoMock = new Mock<IReviewRepo>();
        var utilityMock = new Mock<Utility>();

        customerRepoMock.Setup(repo => repo.GetAllCustomers()).Returns(new List<Customer>());

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

        var utility = new Utility(); 
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