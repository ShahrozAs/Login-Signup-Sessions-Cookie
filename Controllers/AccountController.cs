using Identity_in_WebApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Identity_in_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly EcommerceContext context;

        public AccountController(EcommerceContext context)
        {
            this.context = context;
        }



        [HttpPost("Register")]
        public async Task<ActionResult<PersonalInfo>> Register(PersonalInfo entity)
        {
            try
            {
                var db = context;
                var user = await db.PersonalInfos.Where(x => x.Email == entity.Email).FirstOrDefaultAsync();

                if (user == null)
                {
                    PersonalInfo personalInfo = new PersonalInfo();
                    personalInfo.Email = entity.Email;
                    personalInfo.Password = entity.Password;
                    personalInfo.FirstName = entity.FirstName;
                    personalInfo.LastName = entity.LastName;
                    personalInfo.Address = entity.Address;
                    personalInfo.City = entity.City;
                    personalInfo.ProfileImage = entity.ProfileImage;

                    db.PersonalInfos.Add(personalInfo);
                    db.SaveChanges();

                    return Ok(personalInfo);
                }

                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(string email,string password)
        {
            try
            {
                var db = context;
                var user=await db.PersonalInfos.Where(x=>x.Email==email&&x.Password==password).FirstOrDefaultAsync();

            if (user != null)
                {
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) },
                        CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    HttpContext.Session.SetString("Email", user.Email);
                    return Ok(user);
 
                }
                else
                {
                    return StatusCode(403, new { message = "user Not Found" });
                }

            } catch (Exception ex) { 
            
               return StatusCode(500, ex.Message);
            }
           
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<PersonalInfo>>> GetAllUsers()
        {
            try
            {
                var db = context;
                if (db.PersonalInfos == null)
                {
                    return NotFound();
                }
                else
                {
                    return await db.PersonalInfos.ToListAsync();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("ViewEmail")]
        public string ViewEmail()
        {
            var user= HttpContext.Session.GetString("Email");


            return user;
        }

        [HttpGet("Logout")]
        public string Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storeCookie = Request.Cookies.Keys;
            foreach (var cookies in storeCookie)
            {
                Response.Cookies.Delete(cookies);
            }
            HttpContext.Session.Remove("Email");
            return "Logout Successfully";
        }

        
    }
}
