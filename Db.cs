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
}

public record Resource
{
    public int id { get; set; }
    public string? type { get; set; }
    public string? unit { get; set; }
    public double amount { get; set; }
    public int survivorId { get; set; }
}

public class RobotstakeoverDB
{
    private static List<Survivor> _survivors = new List<Survivor>() {
        new Survivor{id=1,FirstName="Saka",OtherNames="Shingirai",LastName="Madzudzo",DoB=new DateOnly(1995,03,05),gender="male",location="-17.8251657, 31.03351"},
        new Survivor{id=2,FirstName="James",OtherNames="",LastName="Nollen",DoB=new DateOnly(1978,07,12),gender="male",location="-17.8251657, 31.03351"},
        new Survivor{id=3,FirstName="Cathryn",OtherNames="Jane",LastName="Moyo",DoB=new DateOnly(1960,04,20),gender="female",location="-17.8251657, 31.03351"}
    };

    public static List<Survivor> GetSurvivors()
    {
        return _survivors;
    }

    public static Survivor? GetSurvivor(int id)
    {
        Survivor survivor = _survivors.SingleOrDefault(survivor => survivor.id == id)!;
        if (survivor == null)
        {
            throw new KeyNotFoundException();
        }
        return survivor;
    }

    public static Survivor CreateSurvivor(Survivor survivor)
    {
        int newid = _survivors.MaxBy(x => x.id)!.id + 1;
        survivor.id = newid;
        _survivors.Add(survivor);
        return survivor;
    }

    public static Survivor UpdateSurvivor(Survivor update)
    {
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

    private static List<Resource> _resources = new List<Resource>() {
        new Resource{id=1,type="Water",unit="Liter",amount=5,survivorId=1},
        new Resource{id=2,type="Salt",unit="Kilogram",amount=0.2,survivorId=1},
        new Resource{id=3,type="Apple",unit="Count",amount=50,survivorId=3}
    };

    public static List<Resource> GetResources()
    {
        return _resources;
    }

    public static List<Resource> GetResourcesBySurvivor(int survivorId)
    {
        return _resources.FindAll(resource => resource.survivorId == survivorId).ToList();
    }

    public static Resource? GetResource(int id)
    {
        Resource resource = _resources.SingleOrDefault(resource => resource.id == id)!;
        if (resource == null)
        {
            throw new KeyNotFoundException();
        }
        return resource;
    }

    public static Resource CreateResource(Resource resource)
    {
        int newid = _resources.MaxBy(x => x.id)!.id + 1;
        resource.id = newid;
        _resources.Add(resource);
        return resource;
    }

    public static Resource UpdateResource(Resource update)
    {
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
}