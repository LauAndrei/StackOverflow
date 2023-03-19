using Core.Entities;
using Core.Interfaces.RepositoryInterfaces;

namespace Infrastructure.Repositories;

public class QuestionTagRepository : GenericRepository<QuestionTag>, IQuestionTagRepository
{
    private readonly DatabaseContext _context;

    public QuestionTagRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}