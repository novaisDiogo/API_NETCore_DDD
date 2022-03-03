using Api.Data.Context;
using Api.Data.Repository;
using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class CepImplementation : BaseRepository<CepEntity>, ICepRepository
    {
        private DbSet<CepEntity> _dataSet;

        public CepImplementation(MyContext context) : base(context)
        {
            _dataSet = context.Set<CepEntity>();
        }

        public async Task<CepEntity> SelectAsync(string cep)
        {
            return await _dataSet.Include(c => c.Municipio)
                .ThenInclude(m => m.Uf)
                .FirstOrDefaultAsync(c => c.Cep.Equals(cep));
        }
    }
}
