using Core.IRepositoreis;
using DAL.Data;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TaskAttachmentRepository:BaseRepository<TaskAttachment>,ITaskAttachmentRepository
    {
        public TaskAttachmentRepository( AppDbContext context):base(context) { }

    }
}
