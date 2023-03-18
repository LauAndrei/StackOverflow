using Core.Entities;

namespace Infrastructure.Repositories;

public class VoteRepository : GenericRepository<Vote>
{
    public VoteRepository(DatabaseContext context) : base(context)
    {
    }
}