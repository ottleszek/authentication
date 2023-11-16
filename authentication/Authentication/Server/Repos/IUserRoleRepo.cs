namespace Authentication.Server.Repos
{
    public interface IUserRoleRepo
    {
        string GetEnglishNameBy(Guid userRoleId);
        Guid? GetByEnglishName(string v);
    }
}
