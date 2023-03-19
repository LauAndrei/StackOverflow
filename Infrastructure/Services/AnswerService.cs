using Core.Dtos.AnswerDtos;
using Core.EntityExtensions.AnswerExtensions;
using Core.Interfaces.RepositoryInterfaces;
using Core.Interfaces.ServiceInterfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AnswerService : IAnswerService
{
    private readonly IUnitOfWork _unitOfWork;

    public AnswerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<AnswerDto>> GetAllAnswers()
    {
        return await _unitOfWork.AnswerRepository.GetAll()
            .Include(a => a.Author)
            .Select(a => a.ToAnswerDto())
            .ToListAsync();
    }

    public async Task<int> PostAnswer(PostAnswerDto newAnswer, int authorId)
    {
        var addedAnswer = await _unitOfWork.AnswerRepository.AddAsync(newAnswer.ToAnswer(authorId));

        await _unitOfWork.SaveChangesAsync();
        
        return addedAnswer.Entity.Id;
    }

    public async Task<bool> UpdateAnswer(PostAnswerDto updatedAnswer)
    {
        var existingAnswer = await _unitOfWork.AnswerRepository.GetAll()
            .AsNoTracking()
            .Where(a => a.Id == updatedAnswer.Id)
            .FirstAsync();

        var newAnswer = updatedAnswer.ToAnswer(existingAnswer);
        
        _unitOfWork.AnswerRepository.Update(newAnswer);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> DeleteAnswer(int answerId)
    {
        await _unitOfWork.AnswerRepository.RemoveByIdAsync(answerId);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> CheckIfUserIsAnswersAuthor(int userId, int answerId)
    {
        var answersAuthorId = await _unitOfWork.AnswerRepository.GetAll()
            .Where(a => a.Id == answerId)
            .Select(a => a.AuthorId)
            .FirstOrDefaultAsync();

        return answersAuthorId == userId;
    }
}