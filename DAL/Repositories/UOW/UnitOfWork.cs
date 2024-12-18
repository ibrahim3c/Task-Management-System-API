﻿using Core.IRepositoreis;
using Core.IRepositoreis.UOW;
using Core.Models;
using DAL.Data;

namespace DAL.Repositories.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public IProjectRepository ProjectRepository {  get; private set; }

        public ITaskRepository TaskRepository { get;private set; }
        public ITaskAttachmentRepository TaskAttachmentRepository { get; private set; }
        public IBaseRepository<RefreshToken> RefreshTokenRepository { get; private set; }
        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
            ProjectRepository =new ProjectRepository(context);
            TaskRepository =new TaskRepository(context);
            TaskAttachmentRepository =new TaskAttachmentRepository(context);
            RefreshTokenRepository= new BaseRepository<RefreshToken>(context);
        }


        public int Complete()
        {
           return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
