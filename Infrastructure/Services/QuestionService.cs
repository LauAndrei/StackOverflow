using Core.Dtos.QuestionDtos;
using Core.EntityExtensions.QueryExtensions;
using Core.EntityExtensions.QuestionExtensions;
using Core.Exceptions;
using Core.Interfaces.RepositoryInterfaces;
using Core.Interfaces.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class QuestionService : IQuestionService
{
    private readonly IUnitOfWork _unitOfWork;

    public QuestionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<QuestionDto>> GetAllQuestions()
    {
        return await _unitOfWork.QuestionRepository.GetAll()
            .Include(q => q.Author)
            .OrderByDescending(q => q.DatePosted)
            .Select(q => q.ToQuestionDto())
            .ToListAsync();
    }

    public async Task<bool> CheckUserIsQuestionsAuthor(int authorId, int questionId)
    {
        var questionsAuthorId = await _unitOfWork.QuestionRepository.GetAll()
            .AsNoTracking()
            .Where(q => q.Id == questionId)
            .Select(q => q.AuthorId)
            .FirstOrDefaultAsync();
        
        return questionsAuthorId == authorId;
    }

    public async Task<QuestionExpandedDto> GetQuestionFullInfo(int id)
    {
        return await _unitOfWork.QuestionRepository.GetAll()
            .Include(q => q.Author)
            .Include(q => q.Answers)
                .ThenInclude(a => a.Author)
            .Include(q => q.Tags)!
                .ThenInclude(t => t.Tag)
            .Where(q => q.Id == id)
            .Select(q => q.ToQuestionExpandedDto())
            .FirstAsync();
    }

    //TODO: Fix a bug - tags need to be removed before updating and change approach
    public async Task<bool> UpdateQuestion(PostQuestionDto updatedQuestion)
    {
        var existingQuestion = await _unitOfWork.QuestionRepository.GetAll()
                                    .Include(q => q.Answers)
                                    .Include(q => q.Votes)
                                    .Include(q => q.Tags)
                                    .Where(q => q.Id == updatedQuestion.Id)
                                    .FirstOrDefaultAsync();

        if (existingQuestion is null)
        {
            throw new ItemNotFoundException();
        }

        existingQuestion = updatedQuestion.ToQuestion(existingQuestion);
        
        //_unitOfWork.QuestionTagRepository.RemoveRange(existingQuestion.Tags);
        
        _unitOfWork.QuestionRepository.Update(existingQuestion);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> PostQuestion(PostQuestionDto newQuestion, int authorId)
    {
        var questionToAdd = newQuestion.ToQuestion(authorId);
        
        await _unitOfWork.QuestionRepository.AddAsync(questionToAdd);
        
        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> DeleteQuestion(int questionId)
    {
        await _unitOfWork.QuestionRepository.RemoveByIdAsync(questionId);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<FilteredQuestions> GetPaginatedAndFilteredQuestions(QuestionFilters filters, int pageNumber, int pageSize)
    {
        var baseQuery = _unitOfWork.QuestionRepository.GetAll()
            .Include(q => q.Author)
            .Include(q => q.Tags)!
            .ThenInclude(t => t.Tag)
            .WhereIf(q => q.Author!.UserName == filters.AuthorUsername, !string.IsNullOrWhiteSpace(filters.AuthorUsername))
            .WhereIf(q => q.Title.ToLower().Contains(filters.Title.ToLower()), !string.IsNullOrWhiteSpace(filters.Title))
            .WhereIf(q => q.Tags!.Any(t => t.Tag.Name.ToLower().Contains(filters.Tag.ToLower())), !string.IsNullOrWhiteSpace(filters.Tag));

        var totalCount = await baseQuery.CountAsync();
        
        var paginatedQuery = await baseQuery.OrderByDescending(q => q.DatePosted)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Select(q => q.ToQuestionDto())
            .ToListAsync();

        return new FilteredQuestions
        {
            Questions = paginatedQuery,

            TotalNumberOfQuestions = totalCount
        };
    }
}