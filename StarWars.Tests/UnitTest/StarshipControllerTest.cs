using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StarWars.Application.Interfaces;
using StarWars.Application.Models;
using StarWars.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StarWars.Tests.UnitTest
{
    public class StarshipControllerTest
    {
        #region Fields 

        private readonly Mock<IStarWarsService> _mockStarWarService;
        private readonly Mock<ILogger<StarshipController>> _mockLogger;
        private readonly StarshipController _starshipController;

        #endregion End Fields 

        #region Constructor

        public StarshipControllerTest()
        {
            _mockStarWarService = new Mock<IStarWarsService>();
            _mockLogger = new Mock<ILogger<StarshipController>>();

            _starshipController = new StarshipController(_mockLogger.Object, _mockStarWarService.Object);
        }

        #endregion End Constructor

        #region Tests

        [Fact]
        public void GetAll_Should_Bring_All_List()
        {
            // arrange
            var filter = new StarWarsFilterModel();

            _mockStarWarService
                .Setup(x => x.GetStarShips(filter))
                .Returns(MockStarshipModel);

            // act
            var result = _starshipController.GetAll(filter);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<StarshipModel>>(viewResult.Value);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public void GetAll_Should_Bring_Two()
        {
            // arrange
            var filter = new StarWarsFilterModel {
                Manufacturer = "Kuat Drive Yards"
            };

            _mockStarWarService
                .Setup(x => x.GetStarShips(filter))
                .Returns(MockStarshipModel.Where(w => w.manufacturer.Contains(filter.Manufacturer)));

            // act
            var result = _starshipController.GetAll(filter);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<StarshipModel>>(viewResult.Value);
            Assert.Equal(2, model.Count());
        }

        #endregion End Tests

        #region Mocks

        private IEnumerable<StarshipModel> MockStarshipModel
            => new List<StarshipModel>
            {
                new StarshipModel
                {
                    name = "CR90 corvette",
                    manufacturer = "Corellian Engineering Corporation"

                },
                new StarshipModel
                {
                    name = "Star Destroyer",
                    manufacturer = "Kuat Drive Yards"
                },
                new StarshipModel
                {
                    name = "Executor",
                    manufacturer = "Kuat Drive Yards, Fondor Shipyards"
                },
            };

        #endregion End Mocks
    }
}
