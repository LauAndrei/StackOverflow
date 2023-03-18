using API.Extensions.ClaimsExtensions;
using Core.Constants;
using Core.Dtos.QuestionDtos;
using Core.Entities;
using Core.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly UserManager<User> _userManager;

        public QuestionController(IQuestionService questionService, UserManager<User> userManager)
        {
            _questionService = questionService;
            _userManager = userManager;
        }

        /// <summary>
        ///     Method Tested;
        ///     Asynchronously calls the getAllQuestion method from IQuestionService
        /// </summary>
        /// <returns>
        ///     A list of questions
        /// </returns>
        [HttpGet]
        [Route("posts")]
        public async Task<List<QuestionDto>> GetAllQuestions()
        {
            return await _questionService.GetAllQuestions();
        }

        /// <summary>
        ///     Method Tested;
        ///     Asynchronously calls the getQuestionFullInfo method from IQuestionService
        /// </summary>
        /// <param name="id">
        ///     The id of the question to retrieve
        /// </param>
        /// <returns>An object containing all the necessary details of the question, including answers</returns>
        [HttpGet]
        [Route("post-details/{id:int}")]
        public async Task<QuestionExpandedDto> GetQuestionFullInfo(int id)
        {
            return await _questionService.GetQuestionFullInfo(id);
        }

        /// <summary>
        ///     Method Tested;
        ///     Asynchronously calls the postQuestion method from IQuestionService after
        ///     checking if the question-to-be-added is not null and after getting the logged in user's id
        /// </summary>
        /// <param name="newQuestion">
        ///     The question dto object containing the details of the question-to-be-added
        /// </param>
        /// <returns>
        ///     The id of the newly created question
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        [Route("post")]
        public async Task<int> PostQuestion(PostQuestionDto newQuestion)
        {
            if (newQuestion is null)
            {
                throw new ArgumentNullException(nameof(newQuestion));
            }
            
            var authorId = User.GetUserId();

            return await _questionService.PostQuestion(newQuestion, authorId);
        }

        /// <summary>
        ///     Method Tested;
        ///     Gets the logged in user's id and checks if he's the author of the question. If he isn't,
        ///     checks if he's a moderator. If not, unauthorized is return, otherwise will call
        ///     in an asynchronously manner the deleteQuestion method from the IQuestionService
        /// </summary>
        /// <param name="id">
        ///     The id of the question to be removed.
        /// </param>
        /// <returns>
        ///     A boolean indicating if the operation was successfully or not, or Unauthorized
        /// </returns>
        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<ActionResult<bool>> DeleteQuestion(int id)
        {
            var userId = User.GetUserId();

            if (await _questionService.CheckUserIsQuestionsAuthor(userId, id))
            {
                return await _questionService.DeleteQuestion(id);
            }

            var user = await _userManager.Users.Where(u => u.Id == userId).FirstAsync();
                
            var isModerator = await _userManager.IsInRoleAsync(user,ROLES_CONSTANTS.ROLES.MODERATOR);

            if (isModerator)
            {
                return await _questionService.DeleteQuestion(id);
            }

            return Unauthorized();
        }
    }
}
