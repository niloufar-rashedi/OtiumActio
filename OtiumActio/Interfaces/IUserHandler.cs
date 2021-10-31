using OtiumActio.Dto;
using OtiumActio.Models;

namespace OtiumActio
{
    public interface IUserHandler
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        Participant Login(ParticipantLoginDto dto);
        Participant Register(ParticipantDto dto);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
        Participant FindByEmail(string userName);
        string GenerateToken(Participant user);
        Participant UpdatePassword(ResetPasswordDto dto);
    }
}