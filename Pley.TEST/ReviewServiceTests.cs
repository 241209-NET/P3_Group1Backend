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
        var reviews = new List<Review>
        {
            new Review { Id = 1, Comment = "Great!", Rating = 5 },
            new Review { Id = 2, Comment = "Not bad", Rating = 4 }
        };

        mockRepo.Setup(repo => repo.GetAllReviews()).Returns(reviews);

        var reviewService = new ReviewService(mockRepo.Object, null);

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
        var review = new Review 
        { 
            Id = 1, Comment = "Great!", Rating = 5 
        };

        mockRepo.Setup(repo => repo.GetReviewById(1)).Returns(review);
        
        var reviewService = new ReviewService(mockRepo.Object, null);
        
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
        mockRepo.Setup(repo => repo.GetReviewById(99)).Returns((Review?)null);
        
        var reviewService = new ReviewService(mockRepo.Object, null);
        
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
        var review = new Review 
        { 
            Id = 1, Comment = "Great!", Rating = 5 
        };

        mockRepo.Setup(repo => repo.DeleteReviewById(1)).Returns(review);
        
        var reviewService = new ReviewService(mockRepo.Object, null);
        
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
        mockRepo.Setup(repo => repo.DeleteReviewById(999)).Returns((Review?)null);
        
        var reviewService = new ReviewService(mockRepo.Object, null);
        
        // Act
        var result = reviewService.DeleteReviewById(99);
        
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void EditReviewById_UpdatesRating_Test()
    {
        // Arrange
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
        
        var reviewService = new ReviewService(mockRepo.Object, null);
        
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
        
        var reviewService = new ReviewService(mockRepo.Object, null);
        
        // Act
        var result = reviewService.EditReviewById(existingReview, reviewInDTO);
        
        // Assert
        Assert.Equal("Updated Comment", result.Comment);  
        Assert.Equal(3, result.Rating);  
    }
}