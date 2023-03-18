using Core.Entities;

namespace Infrastructure.Repositories;

public class AnswerRepository : GenericRepository<Answer>
{
    public AnswerRepository(DatabaseContext context) : base(context)
    {
    }
}