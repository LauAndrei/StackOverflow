using Core.Dtos.QuestionDtos;
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

    //TODO: Fix a bug - tags need to be removed before updating 
    public async Task<bool> UpdateQuestion(PostQuestionDto question)
    {
        var existingQuestion = await _unitOfWork.QuestionRepository.GetAll()
                                    .AsNoTracking()
                                    .Include(q => q.Answers)
                                    .Include(q => q.Votes)
                                    .Include(q => q.Tags)
                                    .Where(q => q.Id == question.Id)
                                    .FirstOrDefaultAsync();

        if (existingQuestion is null)
        {
            throw new ItemNotFoundException();
        }

        var updatedQuestion = question.ToQuestion(existingQuestion);
        
        //_unitOfWork.QuestionTagRepository.RemoveRange(existingQuestion.Tags);
        
        _unitOfWork.QuestionRepository.Update(updatedQuestion);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<int> PostQuestion(PostQuestionDto newQuestion, int authorId)
    {
        var questionToAdd = newQuestion.ToQuestion(authorId);
        
        var questionAdded = (await _unitOfWork.QuestionRepository.AddAsync(questionToAdd)).Entity;
        
        await _unitOfWork.SaveChangesAsync();

        return questionAdded.Id;
    }

    public async Task<bool> DeleteQuestion(int questionId)
    {
        await _unitOfWork.QuestionRepository.RemoveByIdAsync(questionId);

        return await _unitOfWork.SaveChangesAsync();
    }
}