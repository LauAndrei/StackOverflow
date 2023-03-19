using Core.Entities;
using Core.Interfaces.RepositoryInterfaces;

namespace Infrastructure.Repositories;

public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
{
    private readonly DatabaseContext _context;

    public QuestionRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}