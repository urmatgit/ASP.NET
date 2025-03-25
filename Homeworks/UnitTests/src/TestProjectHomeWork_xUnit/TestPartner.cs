using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Controllers;
using PromoCodeFactory.WebHost.Models;

namespace TestProjectHomeWork_xUnit
{
    public class TestPartner:IClassFixture<TestFixture>
    {
        private PartnersController _partnersController;
        private Mock<IRepository<Partner>> _repositoryPartnerMock
           = new Mock<IRepository<Partner>>();
        public TestPartner(TestFixture testFixture)
        {
            var serviceProvider = testFixture.ServiceProvider;
            
            _partnersController = new PartnersController(_repositoryPartnerMock.Object);
        }
        [Fact]
        public async Task Test_Parner_NotFound()
        {
            //Arrange
             _repositoryPartnerMock.Setup( m =>  m.GetByIdAsync(It.IsAny<Guid>()))
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
        public async Task Test_Parner_IsNotActive()
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
        public async Task Test_Parner_SetLimit()
        {
            //Arrange
            var partner = new Partner()
            {
                IsActive = true,
                PartnerLimits=new List<PartnerPromoCodeLimit> ()
            };

            _repositoryPartnerMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
               .ReturnsAsync(partner) ;
            _repositoryPartnerMock.Setup(m => m.UpdateAsync(It.IsAny<Partner>()));
                

            var setPartnerPromoCodeLimitRequest = new SetPartnerPromoCodeLimitRequest()
            {
                EndDate = DateTime.UtcNow,
                Limit =3 
            };
            //Act
            var result = await _partnersController.SetPartnerPromoCodeLimitAsync(Guid.NewGuid(), setPartnerPromoCodeLimitRequest);
            var actionResult = result as ObjectResult;
            //Assert
            Assert.NotNull(actionResult);
            //использоваль FluentAssertions
            actionResult.StatusCode.Should().Be(201);


            //Assert.Equal(actionResult.StatusCode, 400);
        }
    }
}