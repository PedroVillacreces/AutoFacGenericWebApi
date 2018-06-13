namespace AutofacGenericAPI.Services
{
    using Model;
    public abstract class DbService : IDbService
    {
        private ProyectoGloboDBEntities _dbContext;

        public ProyectoGloboDBEntities Init()
        {
            return _dbContext ?? (_dbContext = new ProyectoGloboDBEntities());
        }
    }
}
