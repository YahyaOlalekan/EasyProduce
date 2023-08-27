using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Abstractions.RepositoryInterfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence.AppDbContext;

namespace Persistence.RepositoryImplementations
{
    public class ChatRepository : BaseRepository<Chat>, IChatRepository
    {
        public ChatRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<Chat>> GetAllChatFromASender(Guid farmerId, Guid managerId)
        {
            return await _context.Chats
            .Include(x => x.Manager)
            .Include(x => x.Farmer)
            .ThenInclude(x => x.User)
            .Where(x => x.ManagerId == managerId && x.FarmerId == farmerId || x.ManagerId == farmerId && x.FarmerId == managerId)
            // .OrderBy(x => x.DateCreated)
            .ToListAsync();
        }
        public async Task<List<Chat>> GetAllUnSeenChatAsync(Guid farmerId)
        {
            return await _context.Chats
            .Include(x => x.Manager)
            .Where(x => x.FarmerId == farmerId && !x.Seen).ToListAsync();
        }
        public async Task<List<Chat>> GetAllUnSeenChatAsync(Guid managerId, Guid farmerId)
        {
            return await _context.Chats
            .Where(x => x.ManagerId == managerId && x.FarmerId == farmerId && !x.Seen)
            .ToListAsync();
        }

    }
}