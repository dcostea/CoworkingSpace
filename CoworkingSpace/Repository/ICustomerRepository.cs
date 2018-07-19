using CoworkingSpace.Models;

namespace CoworkingSpace.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        bool CustomerExists(int id);
    }
}
