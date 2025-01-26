using Moq;
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
        mockRepo.Setup(repo => repo.GetReviewById(999)).Returns((Review?)null);
        
        var reviewService = new ReviewService(mockRepo.Object, null);
        
        // Act
        var result = reviewService.GetReviewById(99);
        
        // Assert
        Assert.Null(result);
    }

}