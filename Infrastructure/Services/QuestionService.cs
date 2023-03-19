using Core.Dtos.QuestionDtos;
using Core.EntityExtensions.QuestionExtensions;
using Core.Exceptions;
using Core.Interfaces.ServiceInterfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class QuestionService : IQuestionService
{
    private readonly QuestionRepository _questionRepository;
    private readonly QuestionTagRepository _questionTagRepository;
    
    public QuestionService(QuestionRepository questionRepository, QuestionTagRepository questionTagRepository)
    {
        _questionRepository = questionRepository;
        _questionTagRepository = questionTagRepository;
    }

    public async Task<List<QuestionDto>> GetAllQuestions()
    {
        return await _questionRepository.GetAll()
            .Include(q => q.Author)
            .Select(q => q.ToQuestionDto())
            .ToListAsync();
    }

    public async Task<bool> CheckUserIsQuestionsAuthor(int authorId, int questionId)
    {
        var questionsAuthorId = await _questionRepository.GetAll()
            .AsNoTracking()
            .Where(q => q.Id == questionId)
            .Select(q => q.AuthorId)
            .FirstOrDefaultAsync();
        
        return questionsAuthorId == authorId;
    }

    public async Task<QuestionExpandedDto> GetQuestionFullInfo(int id)
    {
        return await _questionRepository.GetAll()
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
        var existingQuestion = await _questionRepository.GetAll()
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
        
        //_questionTagRepository.RemoveRange(existingQuestion.Tags);
        
        _questionRepository.Update(updatedQuestion);

        return await _questionRepository.SaveChangesAsync();
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