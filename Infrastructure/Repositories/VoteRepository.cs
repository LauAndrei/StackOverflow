using Core.Entities;

namespace Infrastructure.Repositories;

public class VoteRepository : GenericRepository<Vote>
{
    private readonly DatabaseContext _context;

    public VoteRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}