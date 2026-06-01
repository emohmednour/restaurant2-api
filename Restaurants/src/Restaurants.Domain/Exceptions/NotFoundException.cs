namespace Restaurants.Domain.Exceptions;

public class NotFoundException(string ResourceType, string IdentifierType) 
    : Exception($"{ResourceType} with id {IdentifierType}Dosen't exist  ")
{

}
