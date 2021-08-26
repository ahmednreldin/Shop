﻿using Moq;
using Shop.Brokers.Storages;
using Shop.Models.Products;
using Shop.Web.Services.Fondations.Products;
using System;
using Tynamix.ObjectFiller;
using Xunit;
namespace Shop.Tests.Unit.Services.Foundations.Products
{
    public partial class ProductTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IProductService productService;
        public ProductTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.productService = new ProductService(
                storageBroker: this.storageBrokerMock.Object);
            
        }

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
                OnProperty(product => product.Id).Use(Guid.NewGuid()).
                OnProperty(product => product.Name).Use(GetRandomName(NameStyle.FirstName)).
                OnProperty(product => product.Salery).Use(GetRandomNumber()).
                OnProperty(product => product.ImageUrl).Use(GetRandomMessage());
            return filler;
        }


    }   
}
