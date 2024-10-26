using Core.IRepositoreis;
using DAL.Data;
using Entities.Models;

namespace DAL.Repositories
{
    public class TaskRepository:BaseRepository<ProjectTask>,ITaskRepository
    {
        public TaskRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
