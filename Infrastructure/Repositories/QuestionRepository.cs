using Core.Entities;

namespace Infrastructure.Repositories;

public class QuestionRepository : GenericRepository<Question>
{
    private readonly DatabaseContext _context;

    public QuestionRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}