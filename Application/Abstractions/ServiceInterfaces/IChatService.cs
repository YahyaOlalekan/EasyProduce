using System;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface IChatService
    {
        Task<BaseResponse<ChatDto>> CreateChat(CreateChatRequestModel model, Guid id, Guid farmerId);
        Task<ChatsResponseModel> GetChatFromASenderAsync(Guid managerId, Guid farmerId);
        // Task<BaseResponse<ChatDto>> MarkAllChatsAsReadAsync(Guid managerId, Guid farmerId);
        // Task<ChatsResponseModel> GetAllUnSeenChatAsync(Guid farmerId);
    }
}