using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineInfo.DAL.Infrastructure;
using WineInfo.DAL.Repositories;
using WineInfo.Entities;
using WineInfo.Services;

namespace Tests
{
    [TestFixture]
    public class MesurementServiceTests
    {
        Mock<IMesurementRepository> repositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        MesurementService service;

        [SetUp]
        public void Init()
        {
            // Arrange
            repositoryMock = new Mock<IMesurementRepository>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            service = new MesurementService(repositoryMock.Object, unitOfWorkMock.Object);
        }

        [Test]
        public async Task AddMesurementAsyncShouldReturnSuccess()
        {
            // Arrange
            var mesurementToAdd = new Mesurement
            {
                Year = 2024,
                Variety = Variety.Red,
                WineType = WineType.Tinto,
                Color = "Red",
                Temperature = 15,
                Gradation = 20,
                PH = 5,
                Observations = "Observations"
            };

            repositoryMock.Setup(r => r.AddAsync(It.IsAny<Mesurement>()))
                .ReturnsAsync(mesurementToAdd);

            // Act
            var result = await service.AddMesurementAsync(mesurementToAdd);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);
            Assert.AreEqual(mesurementToAdd, result.Mesurement);
            repositoryMock.Verify(r => r.AddAsync(mesurementToAdd), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task GetMesurementByIdAsyncShouldReturnMesurement()
        {
            // Arrange
            var expectedMesurement = new Mesurement { Id = 1 };

            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedMesurement);

            // Act
            var result = await service.GetMesurementByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedMesurement.Id, result.Id);
            repositoryMock.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [Test]
        public async Task GetMesurementsAsyncShouldReturnMesurementsList()
        {
            // Arrange
            var expectedMesurements = new List<Mesurement> {
                new Mesurement(1, 2021, Variety.Red, WineType.Merlot, "Red", 18, 14, 3, "Good condition"),
                new Mesurement(2, 2022, Variety.White, WineType.Chardonnay, "White", 12, 12, 3, "Excellent"),
                new Mesurement(3, 2023, Variety.Rose, WineType.Tinto, "Pink", 16, 13, 3, "Smooth")
            };

            repositoryMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(expectedMesurements);

            // Act
            var result = await service.GetMesurementsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedMesurements.Count, result.ToList().Count);
            repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}