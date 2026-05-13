using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, IRoleRepository roleRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto> LoginAsync(UserLoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Invalid email or password"
            };
        }

        var role = user.Role;
        var token = _tokenService.GenerateToken(user.UserId, user.Email, user.RoleId, role!.RoleName);

        return new AuthResponseDto
        {
            Success = true,
            Message = "Login successful",
            Token = token,
            User = new UserResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                RoleId = user.RoleId,
                RoleName = role.RoleName
            }
        };
    }

    public async Task<AuthResponseDto> RegisterAsync(UserRegisterDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Email already registered"
            };
        }

        var customerRole = await _roleRepository.GetByNameAsync("Customer");
        if (customerRole == null)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Customer role not found"
            };
        }

        var newUser = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            RoleId = customerRole.RoleId,
            Role = customerRole
        };

        await _userRepository.AddAsync(newUser);
        await _userRepository.SaveAsync();

        var token = _tokenService.GenerateToken(newUser.UserId, newUser.Email, newUser.RoleId, customerRole.RoleName);

        return new AuthResponseDto
        {
            Success = true,
            Message = "Registration successful",
            Token = token,
            User = new UserResponseDto
            {
                UserId = newUser.UserId,
                FullName = newUser.FullName,
                Email = newUser.Email,
                Phone = newUser.Phone,
                RoleId = newUser.RoleId,
                RoleName = customerRole.RoleName
            }
        };
    }
}
