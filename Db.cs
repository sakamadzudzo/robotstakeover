using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace robotstakeover.DB;

public record Survivor
{
    public int id { get; set; }
    public string? FirstName { get; set; }
    public string? OtherNames { get; set; }
    public string? LastName { get; set; }
    public DateOnly DoB { get; set; }
    public string? gender { get; set; }
    public string? location { get; set; }
    [SwaggerSchema(ReadOnly = true)]
    public Boolean infected { get; set; } = false;
}

public record Resource
{
    public int id { get; set; }
    public string? type { get; set; }
    public string? unit { get; set; }
    public double amount { get; set; }
    public int survivorId { get; set; }
}

public record Infection
{
    public int id { set; get; }
    public int reporter { get; set; }
    public int reportee { get; set; }
    [SwaggerSchema(ReadOnly = true)]
    public DateOnly date { get; set; } = new DateOnly();
}

public record Robot
{
    public string? model { get; set; }
    public string? serialNumber { get; set; }
    public DateTime manufacturedDate { get; set; }
    public string? category { get; set; }
}

public class RobotstakeoverDB
{
    // Survivor related stuff here
    private static List<Survivor> _survivors = new List<Survivor>() {
        new Survivor{id=1,FirstName="Saka",OtherNames="Shingirai",LastName="Madzudzo",DoB=new DateOnly(1995,03,05),gender="male",location="-17.8251657, 31.03351"},
        new Survivor{id=2,FirstName="James",OtherNames="",LastName="Nollen",DoB=new DateOnly(1978,07,12),gender="male",location="-17.8251657, 31.03351"},
        new Survivor{id=3,FirstName="Cathryn",OtherNames="Jane",LastName="Moyo",DoB=new DateOnly(1960,04,20),gender="female",location="-17.8251657, 31.03351"},
        new Survivor{id=4,FirstName="Jeofrey",OtherNames="Malak",LastName="Callistro",DoB=new DateOnly(1958,07,25),gender="male",location="-17.8251657, 31.03351"}
    };

    public static List<Survivor> GetSurvivors()
    {
        checkInfections();
        return _survivors;
    }

    public static Survivor? GetSurvivor(int id)
    {
        checkInfections();
        Survivor survivor = _survivors.SingleOrDefault(survivor => survivor.id == id)!;
        if (survivor == null)
        {
            throw new KeyNotFoundException();
        }
        return survivor;
    }

    public static Survivor CreateSurvivor(Survivor survivor)
    {
        checkInfections();
        int newid = _survivors.MaxBy(x => x.id)!.id + 1;
        survivor.id = newid;
        _survivors.Add(survivor);
        return survivor;
    }

    public static Survivor UpdateSurvivor(Survivor update)
    {
        checkInfections();
        Boolean found = false;
        _survivors = _survivors.Select(survivor =>
        {
            if (survivor.id == update.id)
            {
                survivor.FirstName = update.FirstName;
                survivor.OtherNames = update.OtherNames;
                survivor.LastName = update.LastName;
                survivor.DoB = update.DoB;
                survivor.gender = update.gender;
                survivor.location = update.location;
                found = true;
            }
            return survivor;
        }).ToList();
        if (found == false)
        {
            throw new KeyNotFoundException();
        }
        return update;
    }

    public static Survivor UpdateSurvivorLocation(int id, string location)
    {
        checkInfections();
        Survivor survivorOld = _survivors.SingleOrDefault(survivor => survivor.id == id)!;
        if (survivorOld == null)
        {
            throw new KeyNotFoundException();
        }
        _survivors = _survivors.Select(survivor =>
        {
            if (survivor.id == survivorOld.id)
            {
                survivor.FirstName = survivorOld.FirstName;
                survivor.OtherNames = survivorOld.OtherNames;
                survivor.LastName = survivorOld.LastName;
                survivor.DoB = survivorOld.DoB;
                survivor.gender = survivorOld.gender;
                survivor.location = location;
                survivorOld = survivor;
            }
            return survivor;
        }).ToList();
        return survivorOld;
    }

    public static void RemoveSurvivor(int id)
    {
        checkInfections();
        Boolean found = false;
        _survivors.ForEach(survivor =>
        {
            if (survivor.id == id)
            {
                found = true;
            }
        });
        if (found == false)
        {
            throw new KeyNotFoundException();
        }
        _survivors = _survivors.FindAll(survivor => survivor.id != id).ToList();
    }

    // Resource related stuff here
    private static List<Resource> _resources = new List<Resource>() {
        new Resource{id=1,type="Water",unit="Liter",amount=5,survivorId=1},
        new Resource{id=2,type="Salt",unit="Kilogram",amount=0.2,survivorId=1},
        new Resource{id=3,type="Apple",unit="Count",amount=50,survivorId=3},
        new Resource{id=4,type="Beef",unit="Kilogram",amount=65,survivorId=4},
        new Resource{id=4,type="Chicken",unit="Kilogram",amount=29,survivorId=4},
        new Resource{id=4,type="Wheat",unit="Kilogram",amount=30,survivorId=4},
        new Resource{id=4,type="Rice",unit="Kilogram",amount=20,survivorId=4},
        new Resource{id=4,type="Water",unit="Liter",amount=131,survivorId=4},
    };

    public static List<Resource> GetResources()
    {
        checkInfections();
        return _resources;
    }

    public static List<Resource> GetResourcesBySurvivor(int survivorId)
    {
        checkInfections();
        return _resources.FindAll(resource => resource.survivorId == survivorId).ToList();
    }

    public static Resource? GetResource(int id)
    {
        checkInfections();
        Resource resource = _resources.SingleOrDefault(resource => resource.id == id)!;
        if (resource == null)
        {
            throw new KeyNotFoundException();
        }
        return resource;
    }

    public static Resource CreateResource(Resource resource)
    {
        checkInfections();
        int newid = _resources.MaxBy(x => x.id)!.id + 1;
        resource.id = newid;
        _resources.Add(resource);
        return resource;
    }

    public static Resource UpdateResource(Resource update)
    {
        checkInfections();
        Boolean found = false;
        _resources = _resources.Select(resource =>
        {
            if (resource.id == update.id)
            {
                resource.type = update.type;
                resource.unit = update.unit;
                resource.amount = update.amount;
                resource.survivorId = update.survivorId;
                found = true;
            }
            return resource;
        }).ToList();
        if (found == false)
        {
            throw new KeyNotFoundException();
        }
        return update;
    }

    public static void RemoveResource(int id)
    {
        checkInfections();
        Boolean found = false;
        _resources.ForEach(resource =>
        {
            if (resource.id == id)
            {
                found = true;
            }
        });
        if (found == false)
        {
            throw new KeyNotFoundException();
        }
        _resources = _resources.FindAll(resource => resource.id != id).ToList();
    }

    // Infection related stuff here
    public static List<Infection> _infections = new List<Infection>() {
        new Infection{id=1,reporter=3,reportee=4,date=new DateOnly()},
        new Infection{id=2,reporter=1,reportee=4,date=new DateOnly()},
        new Infection{id=3,reporter=2,reportee=4,date=new DateOnly()},
    };

    public static void checkInfections()
    {
        _survivors = _survivors.Select(survivor =>
            {
                survivor.infected = isSurvivorInfected(survivor.id);
                return survivor;
            }).ToList();
    }

    public static Boolean isSurvivorInfected(int survivorId)
    {
        if (GetInfectionsByReportee(survivorId).Count >= 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static List<Infection> GetInfectionsByReportee(int survivorId)
    {
        return _infections.FindAll(infection => infection.reportee == survivorId).ToList();
    }

    // Robots related stuff here
    private static List<Robot> _robots = new List<Robot>();

    public static async void GetRobotsFromCPU()
    {
        string path = "https://robotstakeover20210903110417.azurewebsites.net/robotcpu";
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(path);
        if (response.IsSuccessStatusCode)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            _robots = JsonConvert.DeserializeObject<List<Robot>>(responseContent)!;
        }
        _robots.OrderBy(robot => robot.manufacturedDate).ToList();
    }

    public static List<Robot> GetRobots()
    {
        checkInfections();
        return _robots;
    }

    public static List<Robot> GetLandRobots()
    {
        checkInfections();
        return _robots.FindAll(robot => robot.category!.ToLower() == "Land".ToLower()).ToList();
    }

    public static List<Robot> GetFlyingRobots()
    {
        checkInfections();
        return _robots.FindAll(robot => robot.category!.ToLower() == "Flying".ToLower()).ToList();
    }

    public static Robot? GetRobot(string serialNumber)
    {
        checkInfections();
        Robot robot = _robots.SingleOrDefault(robot => robot.serialNumber!.ToUpper() == serialNumber.ToUpper())!;
        if (robot == null)
        {
            throw new KeyNotFoundException();
        }
        return robot;
    }
}