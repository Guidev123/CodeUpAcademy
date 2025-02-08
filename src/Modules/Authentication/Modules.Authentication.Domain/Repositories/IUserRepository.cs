﻿using Modules.Authentication.Domain.Entities;

namespace Modules.Authentication.Domain.Repositories;

public interface IUserRepository : IDisposable
{
    Task CreateAsync(User user);    
    Task CreateUserRoleAsync(UserRole role);
    Task CreateUserTokenAsync(UserToken userToken);
    void Delete(User user);
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<UserToken?> GetTokenByUserIdAsync(Guid userId);
    void Update(User user);
    Task<ICollection<string>> GetUserRolesAsync(Guid userId);
}
