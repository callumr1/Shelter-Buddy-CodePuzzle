using Microsoft.AspNetCore.Mvc;
using ShelterBuddy.CodePuzzle.Api.Models;
using ShelterBuddy.CodePuzzle.Core.DataAccess;
using ShelterBuddy.CodePuzzle.Core.Entities;

namespace ShelterBuddy.CodePuzzle.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalController : ControllerBase
{
    private readonly IRepository<Animal, Guid> repository;

    public AnimalController(IRepository<Animal, Guid> animalRepository)
    {
        repository = animalRepository;
    }

    [HttpGet]
    public AnimalModel[] Get() => repository.GetAll().Select(animal => new AnimalModel
    {
        Id = $"{animal.Id}",
        Species = animal.Species,
        Name = animal.Name,
        Colour = animal.Colour,
        DateFound = animal.DateFound,
        DateLost = animal.DateLost,
        MicrochipNumber = animal.MicrochipNumber,
        DateInShelter = animal.DateInShelter,
        DateOfBirth = animal.DateOfBirth,
        AgeText = animal.AgeText,
        AgeMonths = animal.AgeMonths,
        AgeWeeks = animal.AgeWeeks,
        AgeYears = animal.AgeYears
    }).ToArray();

    [HttpPost]
    public async Task<IActionResult> Post(AnimalModel newAnimal)
    {
        var animal = new Animal
        {
            Name = newAnimal.Name,
            Species = newAnimal.Species,
            Colour = newAnimal.Colour,
            MicrochipNumber = newAnimal.MicrochipNumber,
            DateOfBirth = newAnimal.DateOfBirth,
            DateInShelter = newAnimal.DateInShelter,
            DateFound = newAnimal.DateFound,
            DateLost = newAnimal.DateLost,
            AgeYears = newAnimal.AgeYears,
            AgeMonths = newAnimal.AgeMonths,
            AgeWeeks = newAnimal.AgeWeeks
        };

        try
        {
            // I decided to enforce these rules here rather than on the data mdoel. Normally I would do so on the model but they were already nullable and would not
            // want that causing issues with loading old data that had null values for those fields./
            if(string.IsNullOrEmpty(animal.Species))
            {
                return BadRequest("Species cannot be null");
            }
            else if (string.IsNullOrEmpty(animal.Name))
            {
                return BadRequest("Name cannot be null");
            }
            else if (animal.DateOfBirth == null || string.IsNullOrEmpty(animal.AgeText))
            {
                return BadRequest("DateOfBirth or Age must be provided");
            }
            else
            {
                repository.Add(animal);
                return Ok(animal);
            }
        }
        catch(Exception ex)
        {
            return BadRequest(ex);
        }
        
    }
}