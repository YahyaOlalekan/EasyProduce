using System;
using System.Collections.Generic;
using Domain.Entity;

namespace Application.Dtos
{
    public class ChatDto
    {
        public Guid ManagerId { get; set; }
        public Guid FarmerId { get; set; }
        public string Message { get; set; }
        public bool Seen { get; set; }
        public string PostedTime { get; set; }
        public string profileImage { get; set; }
    }


    public class CreateChatRequestModel
    {
        public string Message { get; set; }
    }
    public class ChatResponseModel : BaseResponse<Chat>
    {
        public ChatDto Data { get; set; }
    }
    public class ChatsResponseModel : BaseResponse<Chat>
    {
        public List<ChatDto> Data { get; set; }
    }
}