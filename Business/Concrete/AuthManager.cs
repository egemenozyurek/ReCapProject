using Business.Abstract;
using Core.Utilities.Results;
using Core.Utilities.Security;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            var claims = await _userService.GetClaims(user);
            if (!claims.Success)
            {
                return new ErrorDataResult<AccessToken>(claims.Message);
            }

            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken);
        }

        public async Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto)
        {
            var checkUser = await _userService.GetByEmail(userForLoginDto.Email);
            if (!checkUser.Success)
            {
                return new ErrorDataResult<User>(Constants.Messages.AuthMessages.AuthenticationFailed);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, checkUser.Data.PasswordHash, checkUser.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Constants.Messages.AuthMessages.AuthenticationFailed);
            }

            return new SuccessDataResult<User>(checkUser.Data, Constants.Messages.AuthMessages.AuthenticationSuccessful);
        }

        public async Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);

            User user = new User
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            IResult userRegisterResult = await _userService.Create(user);

            return new DataResult<User>(user, userRegisterResult.Success, userRegisterResult.Message);
        }

        public async Task<IResult> UserExists(string email)
        {
            var checkUser = await _userService.GetByEmail(email);
            if (!checkUser.Success)
            {
                return new ErrorResult(Constants.Messages.UserMessages.UserIsNull);
            }
            return new SuccessResult();
        }
    }
}
