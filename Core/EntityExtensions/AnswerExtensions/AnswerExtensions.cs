using Core.Dtos.AnswerDtos;
using Core.Entities;

namespace Core.EntityExtensions.AnswerExtensions;

public static class AnswerExtensions
{
    public static AnswerDto ToAnswerDto(this Answer answer)
    {
        return new AnswerDto
        {
            Id = answer.Id,
            AuthorUsername = answer.Author!.UserName,
            Text = answer.Text,
            DatePosted = answer.DatePosted,
            LastModifiedDate = answer.LastModifiedDate,
            PictureUrl = answer.PictureUrl,
            Score = answer.Score
        };
    }

    /// <summary>
    ///     Method used when posting an answer
    /// </summary>
    /// <param name="postAnswerDto">The dto of the new answer</param>
    /// <param name="authorId">The currently logged in user</param>
    /// <returns>An object of type answer</returns>
    public static Answer ToAnswer(this PostAnswerDto postAnswerDto, int authorId)
    {
        return new Answer
        {
            Id = 0,
            Text = postAnswerDto.Text,
            AuthorId = authorId,
            DatePosted = DateTime.Now,
            PictureUrl = postAnswerDto.PictureUrl,
            QuestionId = postAnswerDto.QuestionId,
            Score = 0
        };
    }

    public static Answer ToAnswer(this PostAnswerDto newAnswer, Answer oldAnswer)
    {
        return new Answer
        {
            Id = oldAnswer.Id,
            Text = newAnswer.Text,
            AuthorId = oldAnswer.AuthorId,
            DatePosted = oldAnswer.DatePosted,
            LastModifiedDate = DateTime.Now,
            PictureUrl = newAnswer.PictureUrl,
            QuestionId = oldAnswer.QuestionId,
            Votes = oldAnswer.Votes,
            Score = oldAnswer.Score
        };
    }

    public static List<AnswerDto> ToAnswerDtoList(this IEnumerable<Answer> answers)
    {
        return answers.Select(ans => ans.ToAnswerDto()).ToList();
    }
}