using Application.Loggins;
using Application.Services.Fondations.Products;
using Application.Storages;
using Domain.Models.Products;
using Moq;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;

namespace UnitTests.Services.Foundations.Products
{
    public partial class ProductTests
    {
        private readonly Mock<IStorage> storageMock;
        private readonly Mock<ILogging> loggingMock;
        private readonly IProductService productService;
        public ProductTests()
        {
            this.storageMock = new Mock<IStorage>();
            this.loggingMock = new Mock<ILogging>();

            this.productService = new ProductService(
                Storage: this.storageMock.Object,
                logging: this.loggingMock.Object
                );

        }
        private static SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static Product CreateRandomProduct() =>
            CreateProductFiller().Create();

        private static IQueryable<Product> CreateRandomProducts() =>
            CreateProductFiller().Create(GetRandomNumberInt()).AsQueryable();

        private static int GetRandomNumberInt() => new IntRange().GetValue();
        private static double GetRandomNumberDouble() => new DoubleRange().GetValue();
        private static string GetRandomName(NameStyle nameStyle) =>
            new RealNames(nameStyle).GetValue();

        private static string GetRandomMessage() => new MnemonicString().GetValue();
        private static Filler<Product> CreateProductFiller()
        {
            var filler = new Filler<Product>();
            filler.Setup().
                OnProperty(product => product.ProductId).Use(Guid.NewGuid()).
                OnProperty(product => product.Name).Use(GetRandomName(NameStyle.FirstName)).
                OnProperty(product => product.Price).Use(GetRandomNumberDouble()).
                OnProperty(product => product.Picture).Use(GetRandomMessage()).
                OnProperty(product => product.ShortDescription).Use(GetRandomMessage()).
                OnProperty(product => product.FullDescription).Use(GetRandomMessage()).
                OnProperty(product => product.Sku).Use(GetRandomMessage());
            return filler;
        }


    }
}
