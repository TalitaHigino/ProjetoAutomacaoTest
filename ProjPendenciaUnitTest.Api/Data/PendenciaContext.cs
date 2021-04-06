using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjPendenciaUnitTest.Api.Models;

    public class PendenciaContext : DbContext
    {
        public PendenciaContext (DbContextOptions<PendenciaContext> options)
            : base(options)
        {
        }

        public DbSet<ProjPendenciaUnitTest.Api.Models.Pendencia> Pendencia { get; set; }
    }
