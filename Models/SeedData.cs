using proekt.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using proekt.Areas.Identity.Data;

namespace proekt.Models
{
    public class SeedData
    {

        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<proektUser>>();
            IdentityResult roleResult;
            //Add Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); }
            proektUser user = await UserManager.FindByEmailAsync("admin@project.com");
            if (user == null)
            {
                var User = new proektUser();
                User.Email = "admin@project.com";
                User.UserName = "admin@project.com";
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Admin"); }
            }
            //Add Teacher Role
            roleCheck = await RoleManager.RoleExistsAsync("User");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("User")); }
            user = await UserManager.FindByEmailAsync("user@project.com");
            if (user == null)
            {
                var User = new proektUser();
                User.Email = "user@project.com";
                User.UserName = "user@project.com";
                string userPWD = "User123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Teacher
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "User"); }
            }
        }
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new proektContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<proektContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();

                if (context.Hotel.Any())
                {
                    return;
                }

                context.Hotel.AddRange(
                    new Hotel()
                    {
                        Rating = 5,
                        Name = "DoubleTree By Hilton",
                        City = "Skopje",
                        Address = "17 ASNOM blvd.",
                        Phone = "022400800",
                        Description = "Located along the river Vardar, DoubleTree by Hilton Skopje offers 5-star accommodations within a 10 minute drive of the city center, featuring luxuriously furnished rooms and suites, large indoor swimming pool with fitness and spa and an elegant roof bar with a relaxing view.",
                        Picture= "https://www.hilton.com/im/en/SKPABDI/10434349/outside-1.jpg?impolicy=crop&cw=5000&ch=2669&gravity=NorthWest&xposition=0&yposition=71&rw=800&rh=427"
                    },

                    new Hotel()
                    {
                        Rating = 4,
                        Name = "Hotel & Spa Tino Sveti Stefan",
                        City = "Ohrid",
                        Address = "B.B. Sveti Stefan, 6000 Ohrid",
                        Phone = "046209340",
                        Description = "Located on the shores of Lake Ohrid, Hotel Tino Sveti Stefan offers free WiFi, free air conditioning, a 24-reception, a wellness area and a restaurant. Private parking spaces are available on site free of charge.\r\n\r\nEach comfortably-furnished unit comes with a balcony, a flat-screen TV and a mini-bar. In addition, they feature private bathroom facilities and the suites provide a seating area, as well.",
                        Picture = "https://cf.bstatic.com/images/hotel/840x460/267/267854018.jpg"
                    },

                    new Hotel()
                    {
                        Rating = 3,
                        Name = "City Palace",
                        City = "Ohrid",
                        Address = "Kej Makedonija br.31, 6100 Ohrid,",
                        Phone = "046200520",
                        Description = "Located directly on the promenade along Lake Ohrid and only 300 m from Ohrid’s Old Town, City Palace Hotel offers a restaurant and free WiFi access. The spa area can be used at an additional cost.\r\n\r\nThe spacious rooms offer lake and mountain views. They are air-conditioned and come with a seating area, cable TV, wooden floors, and a bathroom. Some have a balcony, and some have a hot tub.",
                        Picture = "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/07/36/e2/4c/city-palace-hotel.jpg?w=700&h=-1&s=1"
                    },

                    new Hotel()
                    {
                        Rating = 3,
                        Name = "Romantique",
                        City = "Dojran",
                        Address = "19 Marshal Tito str., 1487 ",
                        Phone = "034225050",
                        Description = "Located directly at the shore of the Dojran Lake, Romantique Dojran Hotel does not only await guests with a private beach area, but also with a restaurant offering Macedonian cuisine. Guests can relax in the modern and stylish rooms which feature air conditioning and free WiFi.",
                    Picture = "https://img.directhotels.com/mk/gevgelija/romantique-dojran-hotel/1.jpg"
                    },
                    new Hotel()
                    {
                        Rating = 3,
                        Name = "Ambasador",
                        City = "Skopje",
                        Address = "Pirinska 38, 1000 Skopje",
                        Phone = "022728691",
                        Description = "Located in the center, between the Russian"
                    ,
                        Picture = "https://cdn.mopano.com/images/50651.jpeg"
                    },
                    new Hotel()
                    {
                        Rating = 5,
                        Name = "Marriott",
                        City = "Skopje",
                        Address = "Macedonia Square No.7,",
                        Phone = "022792601",
                        Description = "Skopje Marriot Hotel is set in Skopje, 100 m from Macedonia Squ"
                   ,
                        Picture = "https://www.kayak.com/rimg/himg/07/2b/81/leonardo-1960753-skpmc-exterior-0008-hor-clsc_O-333013.jpg?width=1366&height=768&crop=true"
                    }
                 );

                context.SaveChanges();



            }

        }
    }
}
