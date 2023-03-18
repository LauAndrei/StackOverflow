using Core.Dtos.AnswerDtos;
using Core.Entities;

namespace Core.EntityExtensions.AnswerExtensions;

public static class AnswerExtensions
{
    private static AnswerDto ToAnswerDto(this Answer answer)
    {
        return new AnswerDto
        {
            Text = answer.Text,
            DatePosted = answer.DatePosted,
            LastModifiedDate = answer.LastModifiedDate,
            PictureUrl = answer.PictureUrl,
            Score = answer.Score
        };
    }

    public static List<AnswerDto> ToAnswerDtoList(this IEnumerable<Answer> answers)
    {
        return answers.Select(ans => ans.ToAnswerDto()).ToList();
    } 
}