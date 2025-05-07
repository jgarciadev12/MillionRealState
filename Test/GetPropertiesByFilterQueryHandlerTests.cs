using Application.Catalog.PropertyType.Queries.Get;
using Application.DTOs;
using Application.Repository.PropertyRepository;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace Test
{
    [TestFixture]
    public class GetPropertiesByFilterQueryHandlerTests
    {
        private Mock<IPropertyRepository> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private GetPropertiesByFilterQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IPropertyRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetPropertiesByFilterQueryHandler(_repositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Handle_ReturnsEmptyResult_WhenNoPropertiesMatch()
        {
            _repositoryMock
                .Setup(r => r.GetByFilterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal?>(), It.IsAny<decimal?>()))
                .ReturnsAsync(new List<Property>());

            var query = new GetPropertiesByFilterQuery
            {
                name = "notexists",
                address = "any",
                minPrice = 1000,
                maxPrice = 2000,
                Page = 1,
                PageSize = 10
            };

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Items.Count);
            Assert.AreEqual(0, result.TotalCount);
        }

        [Test]
        public async Task Handle_ReturnsEmptyPage_WhenPageIsOutOfRange()
        {
            var properties = new List<Property>
            {
                new Property { Id = "1", Name = "House 1", Address = "Street 1", Price = 100 },
                new Property { Id = "2", Name = "House 2", Address = "Street 2", Price = 200 },
                new Property { Id = "3", Name = "House 3", Address = "Street 3", Price = 300 }
            };

            _repositoryMock
                .Setup(r => r.GetByFilterAsync(null, null, null, null))
                .ReturnsAsync(properties);

            _mapperMock
                .Setup(m => m.Map<PropertyDto>(It.IsAny<Property>()))
                .Returns((Property src) => new PropertyDto { Id = src.Id, Name = src.Name });

            var query = new GetPropertiesByFilterQuery
            {
                Page = 2,
                PageSize = 5
            };

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Items.Count);      
            Assert.AreEqual(3, result.TotalCount);       
        }



        [Test]
        public async Task Handle_ReturnsPaginatedResult_WhenDataExists()
        {
            var properties = new List<Property>
            {
                new Property { Id = "1", Name = "House 1", Address = "Street 1", Price = 100 },
                new Property { Id = "2", Name = "House 2", Address = "Street 2", Price = 200 },
                new Property { Id = "3", Name = "House 3", Address = "Street 3", Price = 300 }
            };

            _repositoryMock
                .Setup(r => r.GetByFilterAsync(null, null, null, null))
                .ReturnsAsync(properties);

            _mapperMock
                .Setup(m => m.Map<PropertyDto>(It.IsAny<Property>()))
                .Returns((Property src) => new PropertyDto { Id = src.Id, Name = src.Name });

            var query = new GetPropertiesByFilterQuery
            {
                Page = 1,
                PageSize = 2
            };

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Items.Count);
            Assert.AreEqual(3, result.TotalCount);
        }
    }
}