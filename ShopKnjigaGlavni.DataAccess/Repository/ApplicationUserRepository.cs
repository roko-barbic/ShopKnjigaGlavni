﻿using ShopKnjigaGlavni.DataAccess.Data;
using ShopKnjigaGlavni.DataAccess.Repository.IRepository;
using ShopKnjigaGlavni.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopKnjigaGlavni.DataAccess.Repository;

public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
{
    private ApplicationDbContext _context;
    public ApplicationUserRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public void Update(ApplicationUser applicationUser)
    {
        _context.Update(applicationUser);
    }
}

