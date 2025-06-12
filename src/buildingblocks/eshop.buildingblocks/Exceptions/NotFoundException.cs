namespace eshop.buildingblocks.Exceptions;

public class NotFoundException:Exception
{
    public string? Details { get; }
    public NotFoundException(string message) : base(message)
    {
    }
    public NotFoundException(string name, object key) 
        : base($" Entity [{name}] ({key.ToString()}) Is Not Found ") 
    { 
    }

}
