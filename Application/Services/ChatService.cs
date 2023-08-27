using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.RepositoryInterfaces;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Domain.Entity;

namespace Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IFarmerRepository _farmerRepository;
        public ChatService(IChatRepository chatRepository, IManagerRepository managerRepository, IFarmerRepository farmerRepository)
        {
            _chatRepository = chatRepository;
            _managerRepository = managerRepository;
            _farmerRepository = farmerRepository;
        }
        public async Task<BaseResponse<ChatDto>> CreateChat(CreateChatRequestModel model, Guid id, Guid farmerId)
        {
            var manager = await _managerRepository.GetManagerByManagerIdAsync(id);
            var farmer = await _farmerRepository.GetAsync(farmerId);
            if (manager == null || farmer == null)
            {
                return new BaseResponse<ChatDto>
                {
                    Message = "Opps Something Bad went wrong",
                    Status = false
                };
            }
            var chat = new Chat
            {
                Message = model.Message,
                PostedTime = DateTime.Now.ToLongDateString(),
                ManagerId = manager.Id,
                FarmerId = farmer.Id
            };
            await _chatRepository.CreateAsync(chat);
            await _chatRepository.SaveAsync();
            return new BaseResponse<ChatDto>
            {
                Message = "Message successfully sent",
                Status = true
            };
        }


        public async Task<ChatsResponseModel> GetChatFromASenderAsync(Guid managerId, Guid farmerId)
        {
            var manager = await _managerRepository.GetManagerByManagerIdAsync(managerId);
            var farmer = await _farmerRepository.GetAsync(farmerId);
            if (manager == null || farmer == null)
            {
                return new ChatsResponseModel
                {
                    Message = "Oops! Something Bad went wrong",
                    Status = false
                };
            }
            var chats = await _chatRepository.GetAllChatFromASender(farmer.Id, manager.Id);
            foreach (var chat in chats)
            {
                if ((DateTime.Now - chat.DateCreated).TotalSeconds < 60)
                {
                    chat.PostedTime = (int)(DateTime.Now - chat.DateCreated).TotalSeconds + " " + "Sec ago";
                }
                if ((DateTime.Now - chat.DateCreated).TotalSeconds > 60 && (DateTime.Now - chat.DateCreated).TotalHours < 1)
                {
                    chat.PostedTime = (int)(DateTime.Now - chat.DateCreated).TotalMinutes + " " + "Mins ago";
                }
                if ((DateTime.Now - chat.DateCreated).TotalMinutes > 60 && (DateTime.Now - chat.DateCreated).TotalDays < 1)
                {
                    chat.PostedTime = (int)(DateTime.Now - chat.DateCreated).TotalHours + " " + "Hours ago";
                }
                if ((DateTime.Now - chat.DateCreated).TotalHours > 24 && (DateTime.Now - chat.DateCreated).TotalDays < 30)
                {
                    chat.PostedTime = (int)(DateTime.Now - chat.DateCreated).TotalDays + " " + "Days ago";
                }
                if ((DateTime.Now - chat.DateCreated).TotalDays > 30 && (DateTime.Now - chat.DateCreated).TotalDays <= 365)
                {
                    chat.PostedTime = ((int)(DateTime.Now - chat.DateCreated).TotalDays / 30) + " " + "Months ago";
                }

            }
            return new ChatsResponseModel
            {
                Message = "Chats restored successfully",
                Status = true,
                Data = chats.Select(x => new ChatDto
                {
                    ManagerId = x.Manager.UserId,
                    Message = x.Message,
                    PostedTime = x.DateCreated.ToShortTimeString(),
                    Seen = x.Seen,
                    profileImage = x.Manager.User.ProfilePicture
                }).ToList()
            };
        }






        // public async Task<ChatsResponseModel> GetAllUnSeenChatAsync(Guid farmerId)
        // {
        //     var farmer = await _farmerRepository.GetAsync(farmerId);
        //     if (farmer == null)
        //     {
        //         return new ChatsResponseModel
        //         {
        //             Message = "Opps Something Bad went wrong",
        //             Status = false
        //         };
        //     }
        //     var unseen = await _chatRepository.GetAllUnSeenChatAsync(farmer.Id);

        //     return new ChatsResponseModel
        //     {
        //         Message = "Successful",
        //         Status = true,
        //         Data = unseen.Select(x => new ChatDto
        //         {
        //             ManagerId = x.Manager.UserId,

        //         }).ToList()
        //     };
        // }




        // public async Task<BaseResponse<ChatDto>> MarkAllChatsAsReadAsync(Guid managerId, Guid farmerId)
        // {
        //     var manager = await _managerRepository.GetAsync(managerId);
        //     var farmer = await _farmerRepository.GetAsync(farmerId);
        //     if (manager == null || farmer == null)
        //     {
        //         return new BaseResponse<ChatDto>
        //         {
        //             Message = "Opps Something Bad went wrong",
        //             Status = false
        //         };
        //     }

        //     var chats = await _chatRepository.GetAllUnSeenChatAsync(manager.Id, farmer.Id);
        //     foreach (var chat in chats)
        //     {
        //         chat.Seen = true;
        //         _chatRepository.Update(chat);
        //     }
        //     return new BaseResponse<ChatDto>
        //     {
        //         Message = "Messages marked as seen",
        //         Status = true
        //     };
        // }


    }
}