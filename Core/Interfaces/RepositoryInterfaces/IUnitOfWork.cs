namespace Core.Interfaces.RepositoryInterfaces;

public interface IUnitOfWork
{
    IAnswerRepository AnswerRepository { get; set; }
    IQuestionRepository QuestionRepository { get; set; }
    IQuestionTagRepository QuestionTagRepository { get; set; }
    ITagRepository TagRepository { get; set; }
    IVoteRepository VoteRepository { get; set; }
    
    Task<bool> SaveChangesAsync();
}