namespace FBChecklist.Services
{
    interface IDomainControllerService<TEntity>
    {

        string GetDirectoryEntry();

        string GetUsername();

        string GetPassword();

        string GetDomain();
    }
}
