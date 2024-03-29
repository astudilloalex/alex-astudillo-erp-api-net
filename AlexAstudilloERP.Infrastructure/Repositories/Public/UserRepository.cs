﻿using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class UserRepository(PostgreSQLContext context, DbContextOptions<PostgreSQLContext> contextOptions) : IUserRepository
{
    private readonly PostgreSQLContext _context = context;
    private readonly DbContextOptions<PostgreSQLContext> _contextOptions = contextOptions;

    public async Task<User> ChangePasswordAsync(User entity, bool multithread = false)
    {
        User? finded = null;
        if (multithread)
        {
            using PostgreSQLContext context = new(_contextOptions);
            finded = await context.Users.FirstOrDefaultAsync(u => u.Email != null && u.Email.Equals(entity.Email));
            if (finded != null)
            {
                finded.Password = entity.Password;
            }
            await context.SaveChangesAsync();
        }
        else
        {
            finded = await _context.Users.FirstOrDefaultAsync(u => u.Email != null && u.Email.Equals(entity.Email));
            if (finded != null)
            {
                finded.Password = entity.Password;
            }
            await _context.SaveChangesAsync();
        }
        return finded ?? entity;
    }

    public Task<bool> ExistsByEmail(string mail)
    {
        return _context.Users.AsNoTracking().AnyAsync(u => u.Email != null && u.Email.Equals(mail));
    }

    public Task<bool> ExistsByIdCard(string idCard)
    {
        return _context.Users.AsNoTracking().AnyAsync(u => u.Person != null && u.Person.IdCard.Equals(idCard));
    }

    public Task<bool> ExistsUsername(string username)
    {
        return _context.Users.AsNoTracking().AnyAsync(u => u.Username != null && u.Username.Equals(username));
    }

    public async Task<User?> FindByCodeAsync(string code, bool multithread = false)
    {
        if (multithread)
        {
            using PostgreSQLContext context = new(_contextOptions);
            return await context.Users.AsNoTracking()
                .Include(u => u.UserMetadatum)
                .Include(u => u.Roles)
                .Include(u => u.AuthProviders)
                .Include(u => u.Organizations)
                .FirstOrDefaultAsync(u => u.Code.Equals(code));
        }
        return await _context.Users.AsNoTracking()
            .Include(u => u.UserMetadatum)
            .Include(u => u.Roles)
            .Include(u => u.AuthProviders)
            .Include(u => u.Organizations)
            .FirstOrDefaultAsync(u => u.Code.Equals(code));
    }

    public Task<User?> FindByIdAsync(int id)
    {
        return _context.Users.AsNoTracking()
            .Include(u => u.Person)
            .Include(u => u.Email)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<User?> FindByIdCard(string idCard)
    {
        return _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Person != null && u.Person.IdCard.Equals(idCard));
    }

    public Task<User?> FindByEmailAsync(string email)
    {
        return _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email != null && u.Email.Equals(email));
    }

    public async Task<User> SaveAsync(User entity, bool multithread = false)
    {
        if (multithread)
        {
            using PostgreSQLContext context = new(_contextOptions);
            foreach (AuthProvider provider in entity.AuthProviders) context.AuthProviders.Attach(provider);
            entity.AuthProviders = context.AuthProviders.Local
                .Where(p => entity.AuthProviders.Select(ap => ap.Id).Contains(p.Id)).ToList();
            await context.Users.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        else
        {
            foreach (AuthProvider provider in entity.AuthProviders) _context.AuthProviders.Attach(provider);
            entity.AuthProviders = _context.AuthProviders.Local
                .Where(p => entity.AuthProviders.Select(ap => ap.Id).Contains(p.Id)).ToList();
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        return entity;
    }

    public async Task<User> VerifyEmailAsync(User entity, bool multithread = false)
    {
        User? finded = null;
        if (multithread)
        {
            using PostgreSQLContext context = new(_contextOptions);
            finded = await context.Users.FirstOrDefaultAsync(u => u.Code.Equals(entity.Code));
            if (finded != null) finded.EmailVerified = true;
            await context.SaveChangesAsync();
        }
        else
        {
            finded = await _context.Users.FirstOrDefaultAsync(u => u.Code.Equals(entity.Code));
            if (finded != null) finded.EmailVerified = true;
            await _context.SaveChangesAsync();
        }
        return finded ?? entity;
    }
}
