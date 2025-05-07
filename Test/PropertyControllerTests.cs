using Application.Common.Exceptions;
using Application.Common;
using Application.DTOs;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.API.Controllers;
using Application.Catalog.PropertyType.Queries.Get;
using Microsoft.AspNetCore.Mvc;

namespace Test
{
    [TestFixture]
    public class PropertyControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private PropertyController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new PropertyController(_mediatorMock.Object);
        }

        [Test]
        public async Task GetFilterProperties_ReturnsPaginatedResult()
        {
            var paginatedResult = new PaginatedResult<PropertyDto>
            {
                Items = new List<PropertyDto>
                {
                    new PropertyDto { Id = "1", Name = "House A" },
                    new PropertyDto { Id = "2", Name = "House B" }
                },
                TotalCount = 2
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetPropertiesByFilterQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(paginatedResult);

            var result = await _controller.GetFilterProperties(null, null, null, null, 1, 6);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Items.Count);
            Assert.AreEqual(2, result.TotalCount);
        }

        [Test]
        public async Task GetById_ReturnsOk_WhenPropertyExists()
        {
            var propertyDto = new PropertyDto { Id = "abc123", Name = "House ABC" };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetPropertyByIdTypesQuery>(q => q.Id == "abc123"), It.IsAny<CancellationToken>()))
                .ReturnsAsync(propertyDto);

            var result = await _controller.GetById("abc123") as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<PropertyDto>(result.Value);
        }

        [Test]
        public void GetById_ThrowsNoDataFoundException_WhenNotFound()
        {
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetPropertyByIdTypesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((PropertyDto)null);

            var ex = Assert.ThrowsAsync<NoDataFoundException>(async () =>
            {
                await _controller.GetById("notexist");
            });

            Assert.That(ex.Message, Is.EqualTo("Property with ID notexist not found"));
        }
    }
}