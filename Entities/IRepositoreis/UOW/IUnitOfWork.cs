namespace Core.IRepositoreis.UOW
{
    public interface IUnitOfWork:IDisposable
    {
        IProjectRepository ProjectRepository { get; }

        ITaskRepository TaskRepository { get; }

        int Complete();
    }
}
