namespace Application.Dtos;
// public class MailRequest
// {
//     public string ToEmail { get; set; }
//     public string ToName { get; set; }
//     public string AttachmentName { get; set; }
//     public string HtmlContent { get; set; }
//     public string Subject { get; set; }
// }

    public class EmailSenderDetails
     {
    public string Subject { get; set; }
    public string ReceiverName { get; set; }
    public string ReceiverEmail { get; set; }
    public string HtmlContent { get; set; }

}

