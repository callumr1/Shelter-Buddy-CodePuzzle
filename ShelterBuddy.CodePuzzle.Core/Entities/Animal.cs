namespace ShelterBuddy.CodePuzzle.Core.Entities;

public class Animal : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Species { get; set; }
    public string? Colour { get; set; }
    public string? MicrochipNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateInShelter { get; set; }
    public DateTime? DateLost { get; set; }
    public DateTime? DateFound { get; set; }
    public int? AgeYears { get; set; }
    public int? AgeMonths { get; set; }
    public int? AgeWeeks { get; set; }

    public string AgeText
    {
        get
        {
            string age = "";

            if (AgeYears > 0)
            {
                age += AgeYears + " years ";
            }
            if (AgeMonths > 0)
            {
                age += AgeMonths + " months ";
            }
            if (AgeWeeks > 0)
            {
                age += AgeWeeks + " weeks";
            }
            return age.TrimEnd();
        }
    }
}