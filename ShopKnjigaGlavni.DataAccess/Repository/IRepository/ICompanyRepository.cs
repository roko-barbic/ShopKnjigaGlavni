using ShopKnjigaGlavni.DataAccess.Repository.IRepository;
using ShopKnjigaGlavni.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopKnjigaGlavni.Models.Models;

namespace ShopKnjigaGlavni.DataAccess.Repository.IRepository;

public interface ICompanyRepository : IRepository<Company>
{
    void Update(Company company);
}