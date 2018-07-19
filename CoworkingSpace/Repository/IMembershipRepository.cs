using CoworkingSpace.Models;

namespace CoworkingSpace.Repository
{
    public interface IMembershipRepository : IRepository<Membership>
    {
        bool MembershipExists(int id);
    }
}
