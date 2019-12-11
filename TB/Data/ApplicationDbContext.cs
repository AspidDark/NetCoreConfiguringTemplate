using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TB.Data
{
    public class DataConrext : IdentityDbContext
    {
        public DataConrext(DbContextOptions<DataConrext> options)
            : base(options)
        {
        }
    }
}
