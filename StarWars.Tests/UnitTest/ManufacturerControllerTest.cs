using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StarWars.Application.Interfaces;
using StarWars.Application.Models;
using StarWars.WebApi.Controllers;
using Xunit;

namespace StarWars.Tests.UnitTest
{
    public class ManufacturerControllerTest
    {
        #region Fields 

        private readonly Mock<IStarWarsService> _mockStarWarService;
        private readonly Mock<ILogger<ManufacturerController>> _mockLogger;
        private readonly ManufacturerController _manufacturerController;

        #endregion End Fields 

        #region Constructor

        public ManufacturerControllerTest()
        {
            _mockStarWarService = new Mock<IStarWarsService>();
            _mockLogger = new Mock<ILogger<ManufacturerController>>();

            _manufacturerController = new ManufacturerController(_mockLogger.Object, _mockStarWarService.Object);
        }

        #endregion End Constructor

        #region Tests

        [Fact]
        public void GetAll_Should_Bring_All_List()
        {
            // arrange
            var filter = new StarWarsFilterModel();

            _mockStarWarService
                .Setup(x => x.GetManufacturer())
                .Returns(MockManufacturer);

            // act
            var result = _manufacturerController.GetAll();

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<string>>(viewResult.Value);
            Assert.Equal(2, model.Count());
        }


        #endregion End Tests

        #region Mocks

        private IEnumerable<string> MockManufacturer => new List<string>{
                "Corellian Engineering Corporation",
                "Kuat Drive Yards"
            };
            
        #endregion End Mocks
    }
}
