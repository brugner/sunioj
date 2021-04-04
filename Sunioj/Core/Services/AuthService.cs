using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Contracts.UnitsOfWork;
using Sunioj.Core.Exceptions;
using Sunioj.Core.Resources.Auth;
using Sunioj.Data.Entities;
using Sunioj.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sunioj.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashService _hashService;
        private readonly JwtOptions _jwtOptions;

        public AuthService(IUnitOfWork unitOfWork, IHashService hashService,
            IOptionsMonitor<JwtOptions> jwtOptions)
        {
            _unitOfWork = unitOfWork;
            _hashService = hashService;
            _jwtOptions = jwtOptions.CurrentValue;
        }

        public async Task<AuthResultDTO> LoginAsync(UserForAuthDTO userForAuth)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(userForAuth.Email);

            if (!IsValidAttempt(user, userForAuth.Password))
            {
                throw new BadRequestException("Email or password invalid");
            }

            return new AuthResultDTO()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccessToken = CreateTokenForUser(user)
            };
        }

        public async Task ChangePasswordAsync(ChangePasswordDTO changePassword, int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            var currentPasswordIsValid = _hashService.ValidatePassword(changePassword.CurrentPassword, user.PasswordHash);

            if (!currentPasswordIsValid || !changePassword.NewPassword.Equals(changePassword.RepeatNewPassword))
            {
                throw new BadRequestException("Invalid password");
            }

            var newPasswordHash = _hashService.HashPassword(changePassword.NewPassword);
            user.PasswordHash = newPasswordHash;

            await _unitOfWork.CompleteAsync();
        }

        #region Helpers
        /// <summary>
        /// Returns true if its a valid login attempt.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="plainPassword">Plain password.</param>
        /// <returns></returns>
        private bool IsValidAttempt(User user, string plainPassword)
        {
            return user != null && _hashService.ValidatePassword(plainPassword, user.PasswordHash);
        }

        /// <summary>
        /// Creates a JSON Web Token for the specified user.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns></returns>
        private string CreateTokenForUser(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
