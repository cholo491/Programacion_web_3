using Apis.Data;
using Apis.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Apis.Models;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;



namespace Apis.Services

{
    public class UserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterUserAsync (RegisterUserDto dto)
        {
            throw new NotImplementedException();
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                throw new Exception("Email ya registrado.");
                
            }
            var user = new Models.User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = HashPassword(dto.Password)
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public string HashPassword(string password)
        {
            var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
