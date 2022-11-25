using Microsoft.AspNetCore.Mvc;
using Moq;
using ShelterBuddy.CodePuzzle.Api.Controllers;
using ShelterBuddy.CodePuzzle.Api.Models;
using ShelterBuddy.CodePuzzle.Core.DataAccess;
using ShelterBuddy.CodePuzzle.Core.Entities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShelterBuddy.CodePuzzle.Core.Tests.Controller
{
    public class AnimalControllerTests
    {

        [Theory]
        [InlineData("Iggy", "Dog", "1 Jan 2020", 2, 10, 3, 200)]
        public async void Create_Animal_Test_AllData(string name, string species, 
            string dob, int? ageYears, int? ageMonths, int? ageWeeks, 
            int expectedResult)
        {
            // Setup mock repo and controller
            var mockRepo = new Mock<IRepository<Animal, Guid>>();
            var controller = new AnimalController(mockRepo.Object);

            // Create Test animal
            var testAnimal = new AnimalModel
            {
                Name = name,
                Species = species,
                Colour = "Black and white",
                DateOfBirth = string.IsNullOrEmpty(dob) ? null : DateTime.Parse(dob),
                DateFound = DateTime.Now,
                AgeYears = ageYears,
                AgeMonths = ageMonths,
                AgeWeeks = ageWeeks
            };

            IActionResult result = await controller.Post(testAnimal);

            ObjectResult objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(expectedResult, objectResult.StatusCode);
        }

        // I originally had this a one test but could not figure out how to get the ObjectResult to be generic or passed through
        // This works :) but does have duplicated code which I would normally try to avoid.
        [Theory]
        [InlineData("", "Dog", "1 Jan 2020", 2, 10, 3, 400)]
        [InlineData("Iggy", "", "1 Jan 2020", 2, 10, 3, 400)]
        [InlineData("Iggy", "Dog", "", null, null, null, 400)]
        public async void Create_Animal_Test_MissingData(string name, string species,
            string dob, int? ageYears, int? ageMonths, int? ageWeeks,
            int expectedResult)
        {
            // Setup mock repo and controller
            var mockRepo = new Mock<IRepository<Animal, Guid>>();
            var controller = new AnimalController(mockRepo.Object);

            // Create Test animal
            var testAnimal = new AnimalModel
            {
                Name = name,
                Species = species,
                Colour = "Black and white",
                DateOfBirth = string.IsNullOrEmpty(dob) ? null : DateTime.Parse(dob),
                DateFound = DateTime.Now,
                AgeYears = ageYears,
                AgeMonths = ageMonths,
                AgeWeeks = ageWeeks
            };

            IActionResult result = await controller.Post(testAnimal);

            ObjectResult objectResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(expectedResult, objectResult.StatusCode);
        }
    }
}
