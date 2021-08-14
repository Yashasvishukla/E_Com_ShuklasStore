using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if( !userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Yash",
                    Email = "yash@test.com",
                    UserName = "yash@test.com",
                    Address = new Address
                    {
                        FirstName = "Yash",
                        LastName = "Shukla",
                        Street = "901 Street",
                        City = "BSW",
                        State = "Raj.",
                        Zipcode = "064654",
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        } 
    }
}