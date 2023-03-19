using Core.Interfaces.RepositoryInterfaces;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DatabaseContext _context;
    public IAnswerRepository AnswerRepository { get; set; }
    public IQuestionRepository QuestionRepository { get; set; }
    public IQuestionTagRepository QuestionTagRepository { get; set; }
    public ITagRepository TagRepository { get; set; }
    public IVoteRepository VoteRepository { get; set; }

    public UnitOfWork(DatabaseContext context)
    {
        _context = context;

        AnswerRepository = new AnswerRepository(_context);
        QuestionRepository = new QuestionRepository(_context);
        QuestionTagRepository = new QuestionTagRepository(_context);
        TagRepository = new TagRepository(_context);
        VoteRepository = new VoteRepository(_context);
    }
    
    public async Task<bool> SaveChangesAsync()
    {
       return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}