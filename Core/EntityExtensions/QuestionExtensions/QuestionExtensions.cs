using Core.Dtos.QuestionDtos;
using Core.Entities;
using Core.EntityExtensions.AnswerExtensions;
using Core.EntityExtensions.TagExtensions;

namespace Core.EntityExtensions.QuestionExtensions;

public static class QuestionExtensions
{
    /// <summary>
    ///     This will be used on the questions page, where the user
    ///     sees all the questions
    /// </summary>
    /// <param name="question">The question retrieved from the db</param>
    /// <returns>A questionDto containing the strictly necessary info a user needs to see</returns>
    public static QuestionDto ToQuestionDto(this Question question)
    {
        return new QuestionDto
        {
            Id = question.Id,
            AuthorUsername = question.Author!.UserName,
            Slug = question.Slug,
            Title = question.Title,
            Text = question.Text.Length > 200 ? question.Text[..200] + ".." : question.Text,
            CreationDate = question.DatePosted,
            LastModifiedDate = question.LastModifiedDate
        };
    }
    
    public static QuestionExpandedDto ToQuestionExpandedDto(this Question question)
    {
        return new QuestionExpandedDto
        {
            Title = question.Title,
            Text = question.Text,
            AuthorUsername = question.Author.UserName,
            PictureUrl = question.PictureUrl,
            DatePosted = question.DatePosted,
            LastModifiedDate = question.LastModifiedDate,
            Answers = question.Answers?.ToAnswerDtoList(),
            Tags = question.Tags?.ToQuestionTagReducedDtoList(),
            Score = question.Score
        };
    }
    
    public static Question ToQuestion(this PostQuestionDto postQuestionDto, int authorId)
    {
        return new Question
        {
            Id = postQuestionDto.Id,
            Title = postQuestionDto.Title,
            Slug = postQuestionDto.Title.Length > 60 ? postQuestionDto.Title.Substring(0, 60).Replace(' ', '-').ToLower() : postQuestionDto.Title.Replace(' ', '-').ToLower(),
            Text = postQuestionDto.Text,
            AuthorId = authorId,
            PictureUrl = postQuestionDto.PictureUrl,
            DatePosted = DateTime.Now,
            Tags = postQuestionDto.Tags.ToQuestionTagList(),
            Score = 0
        };
    }

    public static Question ToQuestion(this PostQuestionDto updatedQuestionDto, Question oldQuestion)
    {
        return new Question
        {
            Id = oldQuestion.Id,
            Title = updatedQuestionDto.Title,
            Text = updatedQuestionDto.Text,
            AuthorId = oldQuestion.AuthorId,
            PictureUrl = updatedQuestionDto.PictureUrl,
            DatePosted = oldQuestion.DatePosted,
            LastModifiedDate = DateTime.Now,
            Answers = oldQuestion.Answers,
            Tags = updatedQuestionDto.Tags.ToQuestionTagList(),
            Votes = oldQuestion.Votes,
            Score = oldQuestion.Score
        };
    }
}