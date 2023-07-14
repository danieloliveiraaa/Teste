using Microsoft.EntityFrameworkCore;
using Sistema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Data
{
    public class BDContext : DbContext
    {
        public BDContext(DbContextOptions<BDContext> options) : base (options)
        {

        }

        public DbSet<Pessoa> Pessoa { get; set; }
    }
}
