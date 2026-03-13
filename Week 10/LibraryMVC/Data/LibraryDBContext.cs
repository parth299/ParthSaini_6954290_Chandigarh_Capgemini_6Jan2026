using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

    public class LibraryDBContext : DbContext
    {
        public LibraryDBContext (DbContextOptions<LibraryDBContext> options)
            : base(options)
        {
        }

        public DbSet<Borrower> Borrower { get; set; } = default!;
    }
