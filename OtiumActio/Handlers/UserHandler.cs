using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OtiumActio.Domain.Activities;
using OtiumActio.Domain.Interfaces;
using OtiumActio.Dto;
using OtiumActio.Helpers;
using OtiumActio.Infrastructure;
using OtiumActio.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OtiumActio.Interfaces
{
    public class UserHandler : IUserHandler
    {
        //private readonly IRepository<Activity> _repository;
        private readonly OtiumActioContext _context;
        private readonly AppSettings _appSettings;

        public UserHandler(OtiumActioContext context, IOptions<AppSettings> appSettings)
        {
            //_repository = repository;
            _context = context;
            _appSettings = appSettings.Value;

        }

        //TODO: Use both GitHub and Login tutorial to create session after login.
        //https://github.com/niloufar-rashedi/SuetiaeBloggV2/blob/master/SuetiaeBlogg.Services/Services/AuthorService.cs

        public Participant Register(ParticipantDto dto)
        {
            // validation
            //if (string.IsNullOrWhiteSpace(dto.PrtcPassword))
            //    throw new Exception("Ange ditt lsenord");


            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(dto.PrtcPassword, out passwordHash, out passwordSalt);

            var obj = new Participant
            {
                PrtcFirstName = dto.PrtcFirstName,
                PrtcLastName = dto.PrtcLastName,
                PrtcUserName = dto.PrtcUserName,
                PrtcPasswordHash = passwordHash,
                PrtcPasswordSalt = passwordSalt,
                PrtcFavouritCategory = dto.PrtcFavouritCategory,
                PrtcCreated = DateTime.Now,
                PrtcModified = DateTime.Now
            };
            //dto.PasswordHash = passwordHash;
            //author.PasswordSalt = passwordSalt;

            _context.Participants.Add(obj);
            _context.SaveChanges();

            return obj;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
        public Participant Login(ParticipantLoginDto dto)
        {
            if (string.IsNullOrEmpty(dto.PrtcUserName) || string.IsNullOrEmpty(dto.PrtcPassword))
                return null;
            //Fix it!
            var user = FindByEmail(dto.PrtcUserName);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(dto.PrtcPassword, user.PrtcPasswordHash, user.PrtcPasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public Participant FindByEmail(string userName)
        {
           var user = _context.Participants.FirstOrDefault(p => p.PrtcUserName == userName);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception($"User not found!");
            }
        }
        public string GenerateToken(Participant user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.PrtcId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        public Participant UpdatePassword(ResetPasswordDto dto)
        {
            // validation
            //if (string.IsNullOrWhiteSpace(dto.Password))
            //    throw new Exception("Ange ditt lsenord");
            var user = FindByEmail(dto.Email);
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);

            string connectionString = GetSrting();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UpdateParticipant", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@id", activity.Id);
                    cmd.Parameters.AddWithValue("@prtcId", user.PrtcId);
                    cmd.Parameters.AddWithValue("@prtcPasswordSalt", passwordSalt);
                    cmd.Parameters.AddWithValue("@prtcPasswordHash", passwordHash);
                    //AddActivityCategory(activity);
                    cmd.ExecuteNonQuery();

                    return null; // success   
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message); // return error message  
                }
                finally
                {
                    con.Close();
                }

            }

            //if (_context.Participants.Any(x => x.PrtcUserName == dto.PrtcUserName))
            //    throw new Exception("Användarnamn '" + dto.PrtcUserName + "' är redan registrerat");


            //Use SP>


            //var obj = new Participant
            //{
            //PrtcFirstName = user.PrtcFirstName,
            //PrtcLastName = user.PrtcLastName,
            ////PrtcUserName = user.PrtcUserName,
            //user.PrtcId = user.PrtcId;
            //user.PrtcPasswordHash = passwordHash;
            //user.PrtcPasswordSalt = passwordSalt;
            //user.PrtcModified = DateTime.Now;
            ////};


            //_context.Participants.Add(user);
            //_context.SaveChanges();

            return user;
        }
        public static string GetSrting()
        {
            return ConnectionStringSetting.GetConnectionString();
        }


    }
}
