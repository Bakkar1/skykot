namespace SkyKotApp.Test.HelperTest;

public class ModalHelperTest
{
    [Fact]
    public void GetCarouselId_Test()
    {
        // Arrange
        int id = 2;
        string expected = $"carousel-{id}";
        //Act
        string result = ModalHelper.GetCarouselId(id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetModalCarouselId_Test()
    {
        // Arrange
        int id = 2;
        string expected = $"modal-carousel-{id}";
        //Act
        string result = ModalHelper.GetModalCarouselId(id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expected, result);
    }
}