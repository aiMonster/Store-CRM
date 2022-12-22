using System;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using StoreCRM.DTOs;
using StoreCRM.Context;
using Microsoft.EntityFrameworkCore;
using StoreCRM.Interfaces;
using StoreCRM.Entities;

namespace StoreCRM.Services
{
	public class UserService : IUserService
	{
        private readonly StoreCrmDbContext _dbContext;

		public UserService(StoreCrmDbContext dbContext)
		{
            _dbContext = dbContext;
		}

        public async Task AddNewUserAsync(RegisterDTO user)
        {
            var email = user.Email;
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(user.Password))
            {
                throw new Exception("Email and password are required");
            }

            if (await _dbContext.Users.AnyAsync(x => x.Email == user.Email))
            {
                throw new Exception($"User with {email} email already registered");
            }

            var userId = Guid.NewGuid();

            var newUser = new User
            {
                Id = userId,
                DisplayName = user.DisplayName,
                Email = email,
                Password = user.Password
            };

            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetTokenAsync(LoginDTO user)
        {
            var identity = await GetIdentity(user);
            if (identity == null)
            {
                throw new Exception("Username or password is incorrect");
            }
            return EncodedJwt(identity);
        }

        private async Task<ClaimsIdentity> GetIdentity(LoginDTO user)
        {
            var person = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);

            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Id.ToString())
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;
        }

        private string EncodedJwt(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
            notBefore: now,
                claims: identity.Claims,
            expires: now.Add(TimeSpan.FromHours(AuthOptions.LIFETIME_HOURS)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}

