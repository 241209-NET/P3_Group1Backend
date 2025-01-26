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

    
}