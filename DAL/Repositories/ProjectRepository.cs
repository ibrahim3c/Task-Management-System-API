using Core.IRepositoreis;
using DAL.Data;
using Entities.Models;

namespace DAL.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
