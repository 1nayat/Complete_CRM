using AskKhadim.HRMS.Infrastructure.Data;
using AskKhadim.HRMS.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;



public interface IRefreshTokenRepository
{
    Task AddAsync(refresh_token token);

    Task<refresh_token?> GetActiveByHashAsync(byte[] tokenHash);

    Task RevokeAsync(refresh_token token, string reason);

    Task RevokeAllForUserAsync(long userId, string reason);
    Task UpdateAsync(refresh_token token);

}


public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AskKhadimDbContext _db;

    public RefreshTokenRepository(AskKhadimDbContext db)
    {
        _db = db;
    }


    public async Task AddAsync(refresh_token token)
    {
        _db.refresh_tokens.Add(token);
        await _db.SaveChangesAsync();
    }


    public async Task<refresh_token?> GetActiveByHashAsync(byte[] tokenHash)
    {
        return await _db.refresh_tokens
            .FirstOrDefaultAsync(t =>
                t.token_hash == tokenHash &&
                !t.revoked &&
                t.expires_at > DateTime.UtcNow
            );
    }


    public async Task RevokeAsync(refresh_token token, string reason)
    {
        token.revoked = true;
        token.revoked_at = DateTime.UtcNow;
        token.revoked_reason = reason;

        _db.refresh_tokens.Update(token);
        await _db.SaveChangesAsync();
    }


    public async Task RevokeAllForUserAsync(long userId, string reason)
    {
        var tokens = await _db.refresh_tokens
            .Where(t => t.user_id == userId && !t.revoked)
            .ToListAsync();

        foreach (var token in tokens)
        {
            token.revoked = true;
            token.revoked_at = DateTime.UtcNow;
            token.revoked_reason = reason;
        }

        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(refresh_token token)
    {
        _db.refresh_tokens.Update(token);
        await _db.SaveChangesAsync();
    }

}