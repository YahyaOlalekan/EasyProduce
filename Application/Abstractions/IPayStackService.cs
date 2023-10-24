using System.Threading.Tasks;
using Application.Dtos.PaymentGatewayDTOs;

namespace Application.Abstractions;

public interface IPayStackService
{
    Task<InitializeTransactionResponseModel> InitializePayment(InitializeTransactionRequestModel model);
    Task<VerifyTransactionResponseModel> ConfirmPayment(string referenceNumber);
    Task<VerifyAccountNumberResponseModel> VerifyAccountNumber(VerifyAccountNumberRequestModel model);
    Task<CreateTransferRecipientResponseModel> CreateTransferRecipient(CreateTransferRecipientRequestModel model);
    Task<InitiateTransferResponseModel> InitiateTransfer(CreateTransferRecipientResponseModel model);

}
