namespace robotstakeover.DB;

public record Survivor
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? OtherNames { get; set; }
    public string? LastName { get; set; }
    public DateOnly DoB { get; set; }
    public string? gender { get; set; }
    public string? location { get; set; }
}

public class RobotstakeoverDB
{
    private static List<Survivor> _survivors = new List<Survivor>() {
        new Survivor{Id=1,FirstName="Saka",OtherNames="Shingirai",LastName="Madzudzo",DoB=new DateOnly(1995,03,05),gender="male",location="-17.8251657, 31.03351"},
        new Survivor{Id=2,FirstName="James",OtherNames="",LastName="Nollen",DoB=new DateOnly(1978,07,12),gender="male",location="-17.8251657, 31.03351"},
        new Survivor{Id=3,FirstName="Cathryn",OtherNames="Jane",LastName="Moyo",DoB=new DateOnly(1960,04,20),gender="female",location="-17.8251657, 31.03351"}
    };

    public static List<Survivor> GetSurvivors()
    {
        return _survivors;
    }

    public static Survivor? GetSurvivor(int id)
    {
        return _survivors.SingleOrDefault(survivor => survivor.Id == id);
    }

    public static Survivor CreateSurvivor(Survivor survivor)
    {
        _survivors.Add(survivor);
        return survivor;
    }

    public static Survivor UpdateSurvivor(Survivor update)
    {
        _survivors = _survivors.Select(survivor =>
        {
            if (survivor.Id == update.Id)
            {
                survivor.FirstName = update.FirstName;
                survivor.OtherNames = update.OtherNames;
                survivor.LastName = update.LastName;
                survivor.DoB = update.DoB;
                survivor.gender = update.gender;
                survivor.location = update.location;
            }
            return survivor;
        }).ToList();
        return update;
    }

    public static void RemoveSurvivor(int id)
    {
        _survivors = _survivors.FindAll(survivor => survivor.Id != id).ToList();
    }
}