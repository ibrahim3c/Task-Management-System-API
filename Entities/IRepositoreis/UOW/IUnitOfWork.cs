namespace Core.IRepositoreis.UOW
{
    public interface IUnitOfWork:IDisposable
    {
        IProjectRepository ProjectRepository { get; }

        ITaskRepository TaskRepository { get; }
        ITaskAttachmentRepository TaskAttachmentRepository { get; }

        int Complete();
    }
}
