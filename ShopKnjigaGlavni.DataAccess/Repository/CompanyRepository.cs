using ShopKnjigaGlavni.DataAccess.Data;
using ShopKnjigaGlavni.DataAccess.Repository.IRepository;
using ShopKnjigaGlavni.DataAccess.Repository;
using ShopKnjigaGlavni.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ShopKnjigaGlavni.DataAccess.Data;
using ShopKnjigaGlavni.DataAccess.Repository.IRepository;
using ShopKnjigaGlavni.Models.Models;

namespace ShopKnjigaGlavni.DataAccess.Repository;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    private ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Update(Company company)
    {
        var companyInDb = _context.Companies.FirstOrDefault(p => p.Id == company.Id);

        if (companyInDb != null)
        {
            companyInDb.Name = company.Name;
            companyInDb.City = company.City;
            companyInDb.State = company.State;
            companyInDb.PhoneNumber = company.PhoneNumber;
            companyInDb.PostalCode = company.PostalCode;
            companyInDb.StreetAddress = company.StreetAddress;

            // _context.Products.Update(productInDb);
        }
    }
}