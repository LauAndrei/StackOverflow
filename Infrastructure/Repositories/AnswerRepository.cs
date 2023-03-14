using Core.Entities;

namespace Infrastructure.Repositories;

public class AnswerRepository : GenericRepository<Answer>
{
    private readonly DatabaseContext _context;

    public AnswerRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}