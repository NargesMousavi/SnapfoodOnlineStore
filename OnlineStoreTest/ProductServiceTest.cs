using Moq;
using OnlineStore.Domain;
using OnlineStore.Domain.Infra;

namespace OnlineStoreTest
{
    public class ProductServiceTest
    {
        [Fact]
        public void Add_NameIsNotUnique_ReturnsException()
        {
            //Arrange
            var stubRepository = new Mock<IProductRepository>();
            var stubCache = new Mock<ICache>();
            var stubProductService = new ProductService(stubRepository.Object,stubCache.Object);
            //Act
            //Assert
        }
        [Fact]
        public void Add_NameLessThan40Char_ReturnsException()
        {
            //Arrange
            //Act
            //Assert
        }
        [Fact]
        public void Buy_AddProductToUserOrders_ReturnsUserOrder()
        {
            //Arrange
            //Act
            //Assert
        }
        [Fact]
        public void Buy_DecreseTheInventory_ReturnsOneItem()
        {
            //Arrange
            //Act
            //Assert
        }
    }
}