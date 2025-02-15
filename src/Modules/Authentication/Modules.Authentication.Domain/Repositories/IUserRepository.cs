﻿using Modules.Authentication.Domain.Models;

namespace Modules.Authentication.Domain.Repositories;

public interface IUserRepository : IDisposable
{
    Task CreateAsync(User user);
    Task CreateUserRoleAsync(UserRole role);
    Task CreateUserTokenAsync(UserToken userToken);
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<UserToken?> GetTokenByUserIdAsync(Guid userId);
    Task<ICollection<string>> GetUserRoleNamesByUserIdAsync(Guid userId);
    Task<List<UserRole>> GetUserRolesAsync(Guid userId);
    Task<Role?> GetRoleByNameAsync(string roleName);
    void Update(User user);
    Task DeleteAsync(User user);
    void DeleteUserRoles(List<UserRole> roles);
    void DeleteUserToken(UserToken userToken);
}
