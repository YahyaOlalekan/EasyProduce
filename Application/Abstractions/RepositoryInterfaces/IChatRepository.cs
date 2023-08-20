using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface IChatRepository : IBaseRepository<Chat>
    {
        Task<List<Chat>> GetAllUnSeenChatAsync(Guid farmerId);
        Task<List<Chat>> GetAllUnSeenChatAsync(Guid managerId,Guid farmerId);
        Task<List<Chat>> GetAllChatFromASender(Guid farmerId, Guid managerId);
    }
}