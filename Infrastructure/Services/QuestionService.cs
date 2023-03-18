using Core.Dtos.QuestionDtos;
using Core.EntityExtensions.QuestionExtensions;
using Core.Interfaces.ServiceInterfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class QuestionService : IQuestionService
{
    private readonly QuestionRepository _questionRepository;

    public QuestionService(QuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<List<QuestionDto>> GetAllQuestions()
    {
        return await _questionRepository.GetAll()
            .Include(q => q.Author)
            .Select(q => q.ToQuestionDto())
            .ToListAsync();
    }

    public async Task<bool> CheckUserIsQuestionsAuthor( int authorId, int questionId)
    {
        return (await _questionRepository.FindAsync(questionId)).AuthorId == authorId;
    }

    public async Task<QuestionExpandedDto> GetQuestionFullInfo(int id)
    {
        return await _questionRepository.GetAll()
            .Include(q => q.Author)
            .Include(q => q.Answers)
            .Include(q => q.Tags)!
                .ThenInclude(t => t.Tag)
            .Where(q => q.Id == id)
            .Select(q => q.ToQuestionExpandedDto())
            .FirstAsync();
    }

    public async Task<int> PostQuestion(PostQuestionDto newQuestion, int authorId)
    {
        var questionToAdd = newQuestion.ToQuestion(authorId);
        
        var questionAdded = (await _questionRepository.AddAsync(questionToAdd)).Entity;
        
        await _questionRepository.SaveChangesAsync();

        return questionAdded.Id;
    }

    public async Task<bool> DeleteQuestion(int questionId)
    {
        await _questionRepository.RemoveByIdAsync(questionId);

        return await _questionRepository.SaveChangesAsync();
    }
}