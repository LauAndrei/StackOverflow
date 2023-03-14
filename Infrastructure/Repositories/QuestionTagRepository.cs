using Core.Entities;

namespace Infrastructure.Repositories;

public class QuestionTagRepository : GenericRepository<QuestionTag>
{
    private readonly DatabaseContext _context;

    public QuestionTagRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}