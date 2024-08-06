using MISA.CokCok.Core.Entities;
using MISA.CokCok.Core.Interfaces.IRepositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MISA.CokCok.Infrastructure.Interfaces;

namespace MISA.CokCok.Infrastructure.Repository
{
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(IMisaDBContext dbcontext) : base(dbcontext)
        {
        }
    }
}
