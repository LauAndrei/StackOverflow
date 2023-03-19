using Core.Dtos.AnswerDtos;
using Core.EntityExtensions.AnswerExtensions;
using Core.Interfaces.ServiceInterfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AnswerService : IAnswerService
{
    private readonly AnswerRepository _answerRepository;

    public AnswerService(AnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public async Task<List<AnswerDto>> GetAllAnswers()
    {
        return await _answerRepository.GetAll()
            .Include(a => a.Author)
            .Select(a => a.ToAnswerDto())
            .ToListAsync();
    }

    public async Task<int> PostAnswer(PostAnswerDto newAnswer, int authorId)
    {
        var addedAnswer = await _answerRepository.AddAsync(newAnswer.ToAnswer(authorId));

        await _answerRepository.SaveChangesAsync();
        
        return addedAnswer.Entity.Id;
    }

    public async Task<bool> UpdateAnswer(PostAnswerDto updatedAnswer)
    {
        var existingAnswer = await _answerRepository.GetAll()
            .AsNoTracking()
            .Where(a => a.Id == updatedAnswer.Id)
            .FirstAsync();

        var newAnswer = updatedAnswer.ToAnswer(existingAnswer);
        
        _answerRepository.Update(newAnswer);

        return await _answerRepository.SaveChangesAsync();
    }

    public async Task<bool> DeleteAnswer(int answerId)
    {
        await _answerRepository.RemoveByIdAsync(answerId);

        return await _answerRepository.SaveChangesAsync();
    }

    public async Task<bool> CheckIfUserIsAnswersAuthor(int userId, int answerId)
    {
        var answersAuthorId = await _answerRepository.GetAll()
            .Where(a => a.Id == answerId)
            .Select(a => a.AuthorId)
            .FirstOrDefaultAsync();

        return answersAuthorId == userId;
    }
}