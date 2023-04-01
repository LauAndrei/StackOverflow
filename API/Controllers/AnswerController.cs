using API.Extensions.ClaimsExtensions;
using Core.Constants;
using Core.Dtos.AnswerDtos;
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
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        private readonly UserManager<User> _userManager;

        public AnswerController(IAnswerService answerService, UserManager<User> userManager)
        {
            _answerService = answerService;
            _userManager = userManager;
        }

        /// <summary>
        ///     Method tested;
        ///     Async calls the method from the answer service which gets all answers from the db.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllAnswers")]
        public async Task<List<AnswerDto>> GetAllAnswers()
        {
            return await _answerService.GetAllAnswers();
        }

        /// <summary>
        ///     Method Tested;
        ///     Checks is the newAnswer is not null and then gets the current logged in user id.
        ///     At the end, asynchronously calls the method PostAnswer from AnswerService.
        /// </summary>
        /// <param name="newAnswer">
        ///     The DTO object containing the required details about the answer
        /// </param>
        /// <returns>An integer representing the ID of the added answer</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        [Route("PostAnswer")]
        public async Task<AnswerDto> PostAnswer(PostAnswerDto newAnswer)
        {
            if (newAnswer is null)
            {
                throw new ArgumentNullException(nameof(newAnswer));
            }

            var authorId = User.GetUserId();
            
            var authorUsername = User.Identity.Name;
            
            return await _answerService.PostAnswer(newAnswer, authorId, authorUsername);
        }
    
        /// <summary>
        ///     Method Tested;
        ///     Checks if the updatedAnswer is not null, Then checks if the currently logged in user is the author
        ///     of the answer to be updated, of it's a moderator. If yes, asynchronously calls the method
        ///     UpdateAnswer implemented in AnswerService, otherwise throws an Unauthorized.
        /// </summary>
        /// <param name="updatedAnswer">
        ///     The Updated Answer DTO
        /// </param>
        /// <returns>
        ///     A boolean indicating if the operation was successfully or not, or Unauthorized in case that
        ///     the user who made the request isn't the author or a moderator.
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPut]
        [Route("UpdateAnswer")]
        public async Task<ActionResult<bool>> UpdateAnswer(PostAnswerDto updatedAnswer)
        {
            if (updatedAnswer is null)
            {
                throw new ArgumentException(nameof(updatedAnswer));
            }

            var userId = User.GetUserId();

            if (await _answerService.CheckIfUserIsAnswersAuthor(userId, updatedAnswer.Id))
            {
                return await _answerService.UpdateAnswer(updatedAnswer);
            }
    
            var user = await _userManager.Users.AsNoTracking().Where(u => u.Id == userId).FirstAsync();
                
            var isModerator = await _userManager.IsInRoleAsync(user,ROLES_CONSTANTS.ROLES.MODERATOR);

            if (isModerator)
            {
                return await _answerService.UpdateAnswer(updatedAnswer);
            }

            return Unauthorized(RESPONSE_CONSTANTS.ERROR.UNAUTHORIZED);
        }

        /// <summary>
        ///     Method Tested;
        ///     Checks if the user is the author or if he's a moderator and then asynchronously calls the method
        ///     for deletion implemented in AnswerService.
        /// </summary>
        /// <param name="id">The answer-to-be-deleted id</param>
        /// <returns>
        ///     A boolean flag indicating if the delete operation was successfully or not, or Unauthorized
        /// </returns>
        [HttpDelete]
        [Route("DeleteAnswer/{id:int}")]
        public async Task<ActionResult<bool>> DeleteAnswer(int id)
        {
            var userId = User.GetUserId();

            if (await _answerService.CheckIfUserIsAnswersAuthor(userId, id))
            {
                return await _answerService.DeleteAnswer(id);
            }

            var user = await _userManager.Users.Where(u => u.Id == userId).FirstAsync();
                
            var isModerator = await _userManager.IsInRoleAsync(user,ROLES_CONSTANTS.ROLES.MODERATOR);

            if (isModerator)
            {
                return await _answerService.DeleteAnswer(id);
            }

            return Unauthorized(RESPONSE_CONSTANTS.ERROR.UNAUTHORIZED);
        }
    }
}
