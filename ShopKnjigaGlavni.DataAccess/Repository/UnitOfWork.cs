﻿using ShopKnjigaGlavni.DataAccess.Data;
using ShopKnjigaGlavni.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopKnjigaGlavni.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _context;
    public ICategoryRepository Category { get; private set; }
    public IProductRepository Product { get; set; }
    public ICompanyRepository Company { get; set; }
    public IShoppingCartRepository ShoppingCart { get; set; }
    public IApplicationUserRepository ApplicationUser { get; set; }

    public UnitOfWork(ApplicationDbContext context)
    {
         _context = context;
        Category = new CategoryRepository(_context);
        Product = new ProductRepository(_context);
        Company = new CompanyRepository(_context);
        ShoppingCart = new ShoppingCartRepository(_context);
        ApplicationUser = new ApplicationUserRepository(_context);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
