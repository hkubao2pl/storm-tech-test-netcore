using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Todo.Models.TodoItems;
using Todo.Services;

namespace Todo.EntityModelMappers.TodoItems
{
    public class UserSummaryViewmodelFactory
    {
        public static async Task<UserSummaryViewmodel> CreateAsync(IdentityUser identityUser)
        {
            string userName = await Gravatar.GetUserName(identityUser.Email) + " " + identityUser.Email;
            return new UserSummaryViewmodel(userName, identityUser.Email);
        }
    }
}