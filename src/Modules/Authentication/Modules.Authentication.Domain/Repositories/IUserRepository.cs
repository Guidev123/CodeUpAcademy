﻿using Modules.Authentication.Domain.Entities;

namespace Modules.Authentication.Domain.Repositories;

public interface IUserRepository : IDisposable
{
    Task CreateAsync(User user);    
    Task<List<string>> GetRolesAsync(Guid userId);
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<List<UserClaim>> GetUserClaimsAsync(Guid userId);
    void Update(User user);
    Task AddClaimsAsync(List<UserClaim> claims);
    Task AddClaimAsync(UserClaim claims);
    Task RemoveClaimsAsync(Guid userId);
}
