using Moq;
using Pley.API.DTO;
using Pley.API.Model;
using Pley.API.Repo;
using Pley.API.Service;
using Pley.API.Util;

namespace Pley.TEST;

public class ReviewServiceTests
{
    [Fact]
    public void GetAllReviewsTest()
    {
       // Arrange
        var mockRepo = new Mock<IReviewRepo>();
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        var mockStoreRepo = new Mock<IStoreRepo>();
        var utility = new Utility();

        var customer1 = new Customer { Id = 1, Name = "Eldhose" };
        var customer2 = new Customer { Id = 2, Name = "Salby" };

        var reviews = new List<Review>
        {
            new Review { Id = 1, Comment = "Great!", Rating = 5, CustomerId = 1, Customer = customer1 },
            new Review { Id = 2, Comment = "Not bad", Rating = 4, CustomerId = 2, Customer = customer2 }
        };

        mockRepo.Setup(repo => repo.GetAllReviews()).Returns(reviews);

        var reviewService = new ReviewService(mockRepo.Object, utility, mockCustomerRepo.Object, mockStoreRepo.Object);

        // Act
        var result = reviewService.GetAllReviews();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, r => r.Comment == "Great!");
        Assert.Contains(result, r => r.Comment == "Not bad");
    }

    [Fact]
    public void GetReviewByIdTest()
    {
        //Arrange
        var mockRepo = new Mock<IReviewRepo>();
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        var mockStoreRepo = new Mock<IStoreRepo>();
        var utility = new Utility();

        var review = new Review 
        { 
            Id = 1, Comment = "Great!", Rating = 5 
        };

        mockRepo.Setup(repo => repo.GetReviewById(1)).Returns(review);
        
        var reviewService = new ReviewService(mockRepo.Object, utility, mockCustomerRepo.Object, mockStoreRepo.Object);
        
        // Act
        var result = reviewService.GetReviewById(1);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Great!", result.Comment);

    }

    [Fact]
    public void GetReviewById_ReturnsNull_WhenReviewDoesNotExist()
    {
        // Arrange
        var mockRepo = new Mock<IReviewRepo>();
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        var mockStoreRepo = new Mock<IStoreRepo>();
        var utility = new Utility();

        mockRepo.Setup(repo => repo.GetReviewById(99)).Returns((Review?)null);
        
        var reviewService = new ReviewService(mockRepo.Object, utility, mockCustomerRepo.Object, mockStoreRepo.Object);
        
        // Act
        var result = reviewService.GetReviewById(99);
        
        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void DeleteReviewByIdTest()
    {
        // Arrange
        var mockRepo = new Mock<IReviewRepo>();
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        var mockStoreRepo = new Mock<IStoreRepo>();
        var utility = new Utility();

        var review = new Review 
        { 
            Id = 1, Comment = "Great!", Rating = 5 
        };

        mockRepo.Setup(repo => repo.DeleteReviewById(1)).Returns(review);
        
        var reviewService = new ReviewService(mockRepo.Object, utility, mockCustomerRepo.Object, mockStoreRepo.Object);
        
        // Act
        var result = reviewService.DeleteReviewById(1);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Great!", result.Comment);
    }

    [Fact]
    public void DeleteReviewById_ReturnsNull_WhenReviewDoesNotExist()
    {
        // Arrange
        var mockRepo = new Mock<IReviewRepo>();
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        var mockStoreRepo = new Mock<IStoreRepo>();
        var utility = new Utility();

        mockRepo.Setup(repo => repo.DeleteReviewById(999)).Returns((Review?)null);
        
        var reviewService = new ReviewService(mockRepo.Object, utility, mockCustomerRepo.Object, mockStoreRepo.Object);
        
        // Act
        var result = reviewService.DeleteReviewById(99);
        
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void EditReviewById_UpdatesRating_Test()
    {
        // Arrange
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        var mockStoreRepo = new Mock<IStoreRepo>();
        var utility = new Utility();

        var existingReview = new Review 
        { 
            Id = 1, Comment = "Great", Rating = 3 
        };

        var reviewInDTO = new ReviewInDTO 
        { 
            Rating = 5 
        };  
        
        var mockRepo = new Mock<IReviewRepo>();
        mockRepo.Setup(repo => repo.UpdateReview(It.IsAny<Review>())).Returns((Review r) => r);
        
        var reviewService = new ReviewService(mockRepo.Object, utility, mockCustomerRepo.Object, mockStoreRepo.Object);
        
        // Act
        var result = reviewService.EditReviewById(existingReview, reviewInDTO);
        
        // Assert
        Assert.Equal(5, result.Rating);  
        Assert.Equal("Great", result.Comment); 
    }

    [Fact]
    public void EditReviewById_UpdatesComment_Test()
    {
        // Arrange
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        var mockStoreRepo = new Mock<IStoreRepo>();
        var utility = new Utility();

        var existingReview = new Review 
        { 
            Id = 1, Comment = "Old Comment", Rating = 3 
        };
        var reviewInDTO = new ReviewInDTO 
        { 
            Comment = "Updated Comment" 
        };  
        
        var mockRepo = new Mock<IReviewRepo>();
        mockRepo.Setup(repo => repo.UpdateReview(It.IsAny<Review>())).Returns((Review r) => r);
        
        var reviewService = new ReviewService(mockRepo.Object, utility, mockCustomerRepo.Object, mockStoreRepo.Object);
        
        // Act
        var result = reviewService.EditReviewById(existingReview, reviewInDTO);
        
        // Assert
        Assert.Equal("Updated Comment", result.Comment);  
        Assert.Equal(3, result.Rating);  
    }

    [Fact]
    public void CreateNewReview_Test()
    {
        // Arrange
        var mockRepo = new Mock<IReviewRepo>();
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        var mockStoreRepo = new Mock<IStoreRepo>();
        var utility = new Utility();

        var customer = new Customer { Id = 1, Name = "Eldhose" };
        var store = new Store { Id = 1, Name = "Store1" };
        
        var reviewInDTO = new ReviewInDTO { Rating = 5, Comment = "Excellent!" };

        // Set up mock repositories
        mockStoreRepo.Setup(repo => repo.GetStoreById(1)).Returns(store);
        mockCustomerRepo.Setup(repo => repo.GetCustomerById(1)).Returns(customer);

        var review = new Review
        {
            Id = 1,
            Comment = "Excellent!",
            Rating = 5,
            CustomerId = 1,
            StoreId = 1,
            Customer = customer,
            Store = store
        };

        mockRepo.Setup(repo => repo.CreateNewReview(It.IsAny<Review>())).Returns(review);
        mockRepo.Setup(repo => repo.GetAllReviews()).Returns(new List<Review> { review });

        var reviewService = new ReviewService(mockRepo.Object, utility, mockCustomerRepo.Object, mockStoreRepo.Object);

        // Act
        var result = reviewService.CreateNewReview(1, 1, reviewInDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Rating);
        Assert.Equal("Excellent!", result.Comment);
        Assert.Equal(1, result.CustomerId);
        Assert.Equal(1, result.StoreId);

        // Verify that GetAllReviews is called to calculate the average rating
        mockRepo.Verify(repo => repo.GetAllReviews(), Times.Once);
    }

    [Fact]
    public void CreateNewReview_StoreDoesNotExist_Test()
    {
        // Arrange
        var mockRepo = new Mock<IReviewRepo>();
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        var mockStoreRepo = new Mock<IStoreRepo>();
        var utility = new Utility();

        var customer = new Customer { Id = 1, Name = "Eldhose" };
        Store? store = null;  // Store is null
        var reviewInDTO = new ReviewInDTO { Rating = 5, Comment = "Excellent!" };

        mockStoreRepo.Setup(repo => repo.GetStoreById(1)).Returns(store);
        mockCustomerRepo.Setup(repo => repo.GetCustomerById(1)).Returns(customer);

        var reviewService = new ReviewService(mockRepo.Object, utility, mockCustomerRepo.Object, mockStoreRepo.Object);

        // Act
        var exception = Assert.Throws<ArgumentException>(() => reviewService.CreateNewReview(1, 1, reviewInDTO));
        
        //Assert
        Assert.Equal("Invalid Store.", exception.Message);
    }

}