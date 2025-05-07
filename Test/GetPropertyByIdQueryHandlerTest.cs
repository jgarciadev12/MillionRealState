using Application.Catalog.PropertyType.Queries.Get;
using Application.Catalog.PropertyType.Queries.GetById;
using Application.DTOs;
using Application.Repository.PropertyRepository;
using AutoMapper;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class GetPropertyByIdQueryHandlerTest
    {
        [TestFixture]
        public class GetPropertyByIdQueryHandlerTests
        {
            private Mock<IPropertyRepository> _repositoryMock;
            private Mock<IMapper> _mapperMock;
            private GetPropertyByIdQueryHandler _handler;

            [SetUp]
            public void Setup()
            {
                _repositoryMock = new Mock<IPropertyRepository>();
                _mapperMock = new Mock<IMapper>();
                _handler = new GetPropertyByIdQueryHandler(_repositoryMock.Object, _mapperMock.Object);
            }

            [Test]
            public async Task Handle_ReturnsPropertyDto_WhenPropertyExists()
            {
                var property = new Property
                {
                    Id = "123",
                    Name = "House Example",
                    Address = "Street Fake 123",
                    Price = 150
                };

                _repositoryMock
                    .Setup(r => r.GetByIdAsync("123"))
                    .ReturnsAsync(property);

                _mapperMock
                    .Setup(m => m.Map<PropertyDto>(It.IsAny<Property>()))
                    .Returns((Property src) => new PropertyDto
                    {
                        Id = src.Id,
                        Name = src.Name
                    });

                var query = new GetPropertyByIdTypesQuery { Id = "123" };

                var result = await _handler.Handle(query, CancellationToken.None);

                Assert.IsNotNull(result);
                Assert.AreEqual("123", result.Id);
                Assert.AreEqual("House Example", result.Name);
            }

            [Test]
            public async Task Handle_ReturnsNull_WhenPropertyDoesNotExist()
            {
                _repositoryMock
                    .Setup(r => r.GetByIdAsync("999"))
                    .ReturnsAsync((Property)null);

                var query = new GetPropertyByIdTypesQuery { Id = "999" };

                var result = await _handler.Handle(query, CancellationToken.None);

                Assert.IsNull(result);
            }
        }
    }
}