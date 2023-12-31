using Moq;
using OnlineStore.Domain;

namespace OnlineStoreTest
{
    public class ProductServiceTest
    {
        [Fact]
        public void Add_NameIsNotUnique_ReturnsException()
        {
            //Arrange
            var product = new ProductViewModel {Title="test" };
            var stubProdRepository = new Mock<IProductRepository>();
            var stubUserRepository = new Mock<IUserRepository>();
            stubProdRepository.Setup(p => p.GetByTitleAsync("test")).Returns(Task.FromResult<ProductViewModel?>(product));
            var stubProductService = new ProductService(stubProdRepository.Object, stubUserRepository.Object);
            //Act - assert
            var ex =
                Assert.ThrowsAsync<ArgumentException>(() => stubProductService.AddAsync(product));
            Assert.Equal("Product title must be unique.", ex.Result.Message);
        }
        [Fact]
        public void Add_NameMoreThan40Char_ReturnsException()
        {
            //Arrange
            var title = "t"; 
            var product = new ProductViewModel { Title = title.PadLeft(41) };
            var stubProdRepository = new Mock<IProductRepository>();
            var stubUserRepository = new Mock<IUserRepository>();
            stubProdRepository.Setup(p => p.GetByIdAsync(0)).Returns(Task.FromResult<ProductViewModel?>(product));
            var stubProductService = new ProductService(stubProdRepository.Object, stubUserRepository.Object);
            //Act - assert
            var ex = 
                Assert.ThrowsAsync<ArgumentException>(() => stubProductService.AddAsync(product));
            Assert.Equal("Product title must be less than 40 characters.", ex.Result.Message);
        }
    }
}