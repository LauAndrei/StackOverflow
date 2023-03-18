using API.Dtos.TagDtos;
using Core.Entities;

namespace Core.EntityExtensions.TagExtensions;

public static class TagExtensions
{
    public static Tag ToTag(this TagDto tagDto)
    {
        return new Tag
        {
            Id = tagDto.Id,
            Name = tagDto.Name.ToLower()
        };
    }

    public static TagDto ToTagDto(this string tagName)
    {
        return new TagDto
        {
            Name = tagName.ToLower()
        };
    }

    public static TagDto ToTagDto(this Tag tag)
    {
        return new TagDto
        {
            Id = tag.Id,
            Name = tag.Name
        };
    }

    /// <summary>
    ///     Method used when posting a question
    /// </summary>
    /// <param name="tagDtos">The list of the tags</param>
    /// <returns>A list of question tags with no field initialized other than the tagId</returns>
    public static List<QuestionTag> ToQuestionTagList(this IEnumerable<TagDto> tagDtos)
    {
        return tagDtos.Select(t => t.ToQuestionTag()).ToList();
    }

    /// <summary>
    ///     Method used when getting a question full details and is used to transform
    ///     a list of questionTag (which is the intermediary table between Question and Tags)
    ///     into a list of tagDto.
    /// </summary>
    /// <param name="questionTags">The list of questionTags retrieved from the db</param>
    /// <returns>A List of tagDto containing all the tags a question has</returns>
    public static List<TagReducedDto> ToQuestionTagReducedDtoList(this IEnumerable<QuestionTag> questionTags)
    {
        return questionTags.Select(qt => qt.ToQuestionTagReducedDto()).ToList();
    }
    
    /// <summary>
    ///     Method used when getting a question full details and is used to transform
    ///     a questionTag (which is the intermediary table between Question and Tags)
    ///     into a tagDto.
    /// </summary>
    /// <param name="questionTag">The list of questionTags retrieved from the db</param>
    /// <returns>A List of tagDto containing all the tags a question has</returns>
    private static TagReducedDto ToQuestionTagReducedDto(this QuestionTag questionTag)
    {
        return new TagReducedDto
        {
            Name = questionTag.Tag.Name,
        };
    }
    
    /// <summary>
    ///     Method used for transforming the tags when posting a question
    /// </summary>
    /// <param name="tagDto">The tag object</param>
    /// <returns>The QuestionTag object having ONLY tagId initialized</returns>
    private static QuestionTag ToQuestionTag(this TagDto tagDto)
    {
        return new QuestionTag
        {
            TagId = tagDto.Id
        };
    }
}