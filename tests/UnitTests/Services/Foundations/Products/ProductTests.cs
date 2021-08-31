using Moq;
using Application.Storages;
using Domain.Models.Products;
using Application.Services.Fondations.Products;
using System;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;
using System.Data.SqlClient;

namespace UnitTests.Services.Foundations.Products
{
    public partial class ProductTests
    {
        private readonly Mock<IStorage> storageBrokerMock;
        private readonly IProductService productService;
        public ProductTests()
        {
            this.storageBrokerMock = new Mock<IStorage>();

            this.productService = new ProductService(
                Storage: this.storageBrokerMock.Object);

        }
        private static SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static Product CreateRandomProduct() =>
            CreateProductFiller().Create();
        private static double GetRandomNumber() => new DoubleRange().GetValue();
        private static string GetRandomName(NameStyle nameStyle) =>
            new RealNames(nameStyle).GetValue();

        private static string GetRandomMessage() => new MnemonicString().GetValue();
        private static Filler<Product> CreateProductFiller()
        {
            var filler = new Filler<Product>();
            filler.Setup().
                OnProperty(product => product.ProductId).Use(Guid.NewGuid()).
                OnProperty(product => product.Name).Use(GetRandomName(NameStyle.FirstName)).
                OnProperty(product => product.Price).Use(GetRandomNumber()).
                OnProperty(product => product.Picture).Use(GetRandomMessage()).
                OnProperty(product => product.ShortDescription).Use(GetRandomMessage()).
                OnProperty(product => product.FullDescription).Use(GetRandomMessage()).
                OnProperty(product => product.Sku).Use(GetRandomMessage());
            return filler;
        }


    }
}
