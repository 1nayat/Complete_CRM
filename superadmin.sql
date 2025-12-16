use askkhadim_hrms
select *from core_users
SELECT o.organization_id, o.name, o.created_by, u.email, u.employee_id
FROM organizations o
JOIN core_users u ON o.created_by = u.id
select *from refresh_tokens
select *from organizations

--------------------
SELECT 
    u.id AS UserId,
    u.email AS UserEmail,
    rt.id AS RefreshTokenId,
    rt.token AS RefreshToken,
    rt.created_at AS TokenCreatedAt,
    rt.expires_at AS TokenExpiresAt,
    rt.revoked AS IsRevoked,
    rt.revoked_at AS RevokedAt,
    rt.created_by_ip AS CreatedByIp,
    rt.replaced_by_token AS ReplacedByToken
FROM refresh_tokens rt
INNER JOIN core_users u
    ON rt.user_id = u.id
ORDER BY rt.created_at DESC;

