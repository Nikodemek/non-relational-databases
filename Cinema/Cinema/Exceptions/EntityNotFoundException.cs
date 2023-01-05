namespace Cinema.Services;

public class EntityNotFoundException : Exception
{
    private const string MessageTemplate = "Entity of type '{0}' with '{1}' = '{2}' was not found";

    public EntityNotFoundException(Type type, string filedName, object fieldValue)
        : base(String.Format(MessageTemplate, type, filedName, fieldValue))
    { }
}