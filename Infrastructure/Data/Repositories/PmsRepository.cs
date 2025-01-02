using Application.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PmsRepository<TEntity> : BaseRepository<TEntity, PmsDbContext>, IPmsRepository<TEntity>
        where TEntity : BaseEntity
    {
        public PmsRepository(PmsDbContext pmsDbContext, ILogger<BaseRepository<TEntity, PmsDbContext>> logger)
            : base(pmsDbContext, logger)
        {

        }
    }

}
