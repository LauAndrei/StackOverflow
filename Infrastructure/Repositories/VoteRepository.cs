using Core.Entities;
using Core.Interfaces.RepositoryInterfaces;

namespace Infrastructure.Repositories;

public class VoteRepository : GenericRepository<Vote>, IVoteRepository
{
    public VoteRepository(DatabaseContext context) : base(context)
    {
    }
}