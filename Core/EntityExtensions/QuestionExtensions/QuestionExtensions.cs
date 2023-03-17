using Core.Dtos.QuestionDtos;
using Core.Entities;

namespace Core.EntityExtensions.QuestionExtensions;

public static class QuestionExtensions
{
    public static QuestionDto ToQuestionDto(this Question question)
    {
        return new QuestionDto
        {
            Id = question.Id,
            AuthorUsername = question.Author!.UserName,
            Text = question.Text,
            PictureUrl = question.PictureUrl,
            CreationDate = question.DatePosted,
            LastModifiedDate = question.LastModifiedDate
        };
    }

    public static QuestionExpandedDto ToQuestionExpandedDto(this Question question)
    {
        return new QuestionExpandedDto
        {
            //Answers = question.Answers?
            AuthorUsername = question.Author!.UserName,
            DatePosted = question.DatePosted,
            LastModifiedDate = question.LastModifiedDate,
            Score = question.Score,
            Text = question.Text,
            //Tags = 
        };
    }
}