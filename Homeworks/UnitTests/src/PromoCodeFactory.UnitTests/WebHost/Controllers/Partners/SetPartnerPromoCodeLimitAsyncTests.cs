using Microsoft.AspNetCore.Mvc;
using Moq;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Controllers;
using PromoCodeFactory.WebHost.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace PromoCodeFactory.UnitTests.WebHost.Controllers.Partners
{
    public class SetPartnerPromoCodeLimitAsyncTests: IClassFixture<TestFixture>
    {
        //TODO: Add Unit Tests

        private PartnersController _partnersController;
        private Mock<IRepository<Partner>> _repositoryPartnerMock
           = new Mock<IRepository<Partner>>();
        public SetPartnerPromoCodeLimitAsyncTests(TestFixture testFixture)
        {
            var serviceProvider = testFixture.ServiceProvider;

            _partnersController = new PartnersController(_repositoryPartnerMock.Object);
        }
        [Fact]
        public async Task Test_Parner_NotFound__ReturnsNotFound()
        {
            //Arrange
            _repositoryPartnerMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
               .ReturnsAsync(default(Partner));
            var setPartnerPromoCodeLimitRequest = new SetPartnerPromoCodeLimitRequest()
            {
                EndDate = DateTime.UtcNow,
                Limit = 5
            };
            //Act
            var result = await _partnersController.SetPartnerPromoCodeLimitAsync(Guid.NewGuid(), setPartnerPromoCodeLimitRequest);
            var actionResult = result as NotFoundResult;
            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(actionResult.StatusCode, 404);
        }
        [Fact]
        public async Task Test_Parner_IsNotActive_BadRequest_400()
        {
            //Arrange
            _repositoryPartnerMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
               .ReturnsAsync(new Partner()
               {
                   IsActive = false
               });
            var setPartnerPromoCodeLimitRequest = new SetPartnerPromoCodeLimitRequest()
            {
                EndDate = DateTime.UtcNow,
                Limit = 5
            };
            //Act
            var result = await _partnersController.SetPartnerPromoCodeLimitAsync(Guid.NewGuid(), setPartnerPromoCodeLimitRequest);
            var actionResult = result as ObjectResult;
            //Assert
            Assert.NotNull(actionResult);
            //использоваль FluentAssertions
            actionResult.StatusCode.Should().Be(400);


            //Assert.Equal(actionResult.StatusCode, 400);
        }
        [Fact]
        public async Task Test_Parner_IsLessOrZero_BadRequest()
        {
            //Arrange
            _repositoryPartnerMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
               .ReturnsAsync(new Partner()
               {
                   IsActive = false
               });
            var setPartnerPromoCodeLimitRequest = new SetPartnerPromoCodeLimitRequest()
            {
                EndDate = DateTime.UtcNow,
                //"Лимит должен быть больше 0"
                Limit = 0
            };
            //Act
            var result = await _partnersController.SetPartnerPromoCodeLimitAsync(Guid.NewGuid(), setPartnerPromoCodeLimitRequest);
            var actionResult = result as ObjectResult;
            //Assert
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            


            //Assert.Equal(actionResult.StatusCode, 400);
        }
        [Fact]
        public async Task Test_Parner_SetLimit_CreatedAtAction_201()
        {
            //Arrange

            var setPartnerPromoCodeLimitRequest = new SetPartnerPromoCodeLimitRequest()
            {
                EndDate = DateTime.UtcNow,
                Limit = 3
            };

            var partner = new Partner()
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                PartnerLimits = new List<PartnerPromoCodeLimit>()
                {
                    {
                        new PartnerPromoCodeLimit     {Id=Guid.NewGuid(),Limit=setPartnerPromoCodeLimitRequest.Limit,EndDate=setPartnerPromoCodeLimitRequest.EndDate}
                    }
                }
            }; 

            _repositoryPartnerMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
               .ReturnsAsync(partner);
            _repositoryPartnerMock.Setup(m => m.UpdateAsync(It.IsAny<Partner>()));

            
            //Act
            var result = await _partnersController.SetPartnerPromoCodeLimitAsync(Guid.NewGuid(), setPartnerPromoCodeLimitRequest);
            var actionResult = result as ObjectResult;
            //Assert
            Assert.NotNull(actionResult);
            //использоваль FluentAssertions
            actionResult.StatusCode.Should().Be(201);

            _repositoryPartnerMock.Verify(_ => _.UpdateAsync(It.Is<Partner>(p => p.Id == partner.Id 
                                                                && p.PartnerLimits.FirstOrDefault().Id == partner.PartnerLimits.FirstOrDefault().Id)));

            //Assert.Equal(actionResult.StatusCode, 400);
        }
    }
}