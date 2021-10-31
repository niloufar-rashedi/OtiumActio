using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OtiumActio.Dto;
using OtiumActio.Helpers;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using WebMatrix.WebData;
using OtiumActio.EmailService;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OtiumActio.Models;

namespace OtiumActio.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<Participant> _repository;
        private readonly OtiumActioContext _context;
        private readonly IUserHandler _handler;
        private readonly AppSettings _appSettings;
        private readonly IEmailSender _emailSender;
        //private readonly UserManager<Participant> _userManager;
        //private readonly SignInManager<Participant> _signInManager;

        public UserController(OtiumActioContext context, IUserHandler handler, 
            IOptions<AppSettings> appSettings, IEmailSender emailSender
            /*UserManager<Participant> userManager, SignInManager<Participant> signInManager*/
            , IRepository<Participant> repository)
        {
            _repository = repository;
            _context = context;
            _handler = handler;
            _appSettings = appSettings.Value;
            _emailSender = emailSender;
            //_userManager = userManager;
            //_signInManager = signInManager;
        }
        [HttpGet("User")]
        public IActionResult Index()
        {
            return View();
        }
        //TODO: Use both GitHub and Login tutorial to create session after login.
        //https://github.com/niloufar-rashedi/SuetiaeBloggV2/blob/master/SuetiaeBlogg.Services/Services/AuthorService.cs
        [HttpGet("User/Login")]
        public IActionResult LoginForm()
        {
            return View("LoginForm");
        }
        [HttpGet("User/ForgottenPassword")]
        public IActionResult EnterEmail()
        {
            return View("ForgetPassword");
        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordDto { Token = token, Email = email };
            return View(model);
        }
        //[AllowAnonymous]
        [HttpPost]
        public IActionResult Register(ParticipantDto dto)
        {
            // map dto to entity
            //var user = _mapper.Map<Author>(authorDto);
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            else if(_context.Participants.Any(x => x.PrtcUserName == dto.PrtcUserName))
            {
                ModelState.AddModelError("", "Användarnamn '" + dto.PrtcUserName + "' är redan registrerat");
                return View("Index");

               // throw new Exception();

            }
            else
            {
                try
                {
                    // save 
                    var user = _handler.Register(dto);
                    ViewBag.Msg = "Toppen! Du är nu en medlem av OtiumActio!";
                    //return Ok();
                }
                catch (Exception ex)
                {
                    // return error message if there was an exception
                    return BadRequest(ex.Message);
                }
            }
            ModelState.Clear();
            return View("Index");
        }
        //[AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate(ParticipantLoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            else
            {
                try
                {
                    var user = _handler.Login(dto);

                    if (user == null)
                    {
                        ModelState.AddModelError("", "Användernamn el. lösenord är fel");
                    return View("LoginForm");

                    }
                        //return BadRequest("Användernamn el. lösenord är fel");
                    //var tokenHandler = new JwtSecurityTokenHandler();
                    //var key = Encoding.UTF8.GetBytes(_appSettings.Secret);

                    //var tokenDescriptor = new SecurityTokenDescriptor
                    //{
                    //    Subject = new ClaimsIdentity(new Claim[]
                    //    {
                    //        new Claim(ClaimTypes.Name, user.PrtcId.ToString())
                    //    }),
                    //    Expires = DateTime.UtcNow.AddDays(7),
                    //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    //};
                    //var token = tokenHandler.CreateToken(tokenDescriptor);
                    //var tokenString = tokenHandler.WriteToken(token);
                    //// return basic user info (without password) and token to be stored in the client side

                    //var principal = new ClaimsPrincipal(new ClaimsIdentity(tokenDescriptor.Subject.Claims, "local"));

                    //await AuthenticationHttpContextExtensions.SignInAsync(HttpContext, principal);

                    //var cookieOptions = new CookieOptions
                    //{
                    //    Expires = DateTime.Now.AddDays(1)
                    //};                        
                    //Response.Cookies.Append("CurrentUser", dto.PrtcUserName, cookieOptions);

                    //var cookieValue = Request.Cookies["CurrentUser"];

                    //return RedirectToAction("Index", "Home", new
                    //{
                    //    Id = user.PrtcId,
                    //    user.PrtcUserName,
                    //    //dto.PrtcFirstName,
                    //    //dto.PrtcLastName,
                    //    Token = tokenString
                    //});


                    //var scheme = CookieAuthenticationDefaults.AuthenticationScheme;

                    //var loggedInUser = new ClaimsPrincipal(
                    //    new ClaimsIdentity(
                    //    new[] { new Claim(ClaimTypes.Name, user.PrtcFirstName) }, scheme));

                    //return SignIn(loggedInUser, scheme);

                    var claims = new List<Claim>() {
                        new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.PrtcId)),
                        new Claim(ClaimTypes.Name, user.PrtcFirstName)

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties());
                    return LocalRedirect("/");
                }
                catch (Exception ex)
                {

                    throw new Exception($"Login Failed", ex);
                }
            }
        }
        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut    
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page    
            return LocalRedirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(ForgottenPasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "E-posten är ogiltig");
                return View("EnterEmail");
            }
            //replace it with a linq query/use _handler
            //var user = await _userManager.FindByEmailAsync(dto.PrtcUserName);
            var user = _handler.FindByEmail(dto.PrtcUserName);

            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            //Generate the token yourself/get token of that user?
            //var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var token = _handler.GenerateToken(user);

            var callback = Url.Action(nameof(ResetPassword), "User", new { token, email = user.PrtcUserName }, Request.Scheme);
            var message = new Message(new string[] { user.PrtcUserName }, "Reset password token", callback, null);
            _emailSender.SendEmail(message);
            return RedirectToAction(nameof(ForgotPasswordConfirmation)); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            //if (user == null)
            //    RedirectToAction(nameof(ResetPasswordConfirmation));

            //var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            _handler.UpdatePassword(dto);
            //if (!resetPassResult.Succeeded)
            //{
            //    foreach (var error in resetPassResult.Errors)
            //    {
            //        ModelState.TryAddModelError(error.Code, error.Description);
            //    }

            //}
            //return View();
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }
    }
}
