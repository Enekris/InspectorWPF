﻿using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Domains.Entities;
using Inspector.Persistence.DbSettings;
using Microsoft.EntityFrameworkCore;

namespace Inspector.Persistence.Repositories
{
    public class SertificatesRepository : BaseRepository<SertificatesDb>, ISertificatesRepository
    {
        public SertificatesRepository(DbContextOptions<RegistrationOIContext> dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}
