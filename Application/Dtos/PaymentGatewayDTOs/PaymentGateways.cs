using System;
using System.Collections.Generic;

namespace Application.Dtos.PaymentGatewayDTOs;



public class VerifyAccountNumberRequestModel
{
    public string AccountNumber { get; set; }
    public string BankCode { get; set; }

}

public class VerifyAccountNumberData
{
    public string account_name { get; set; }
    public string account_number { get; set; }

}

public class VerifyAccountNumberResponseModel
{
    public bool status { get; set; }
    public string message { get; set; }
    public VerifyAccountNumberData data { get; set; }
}

public class BankResponseModel
{
    public bool status { get; set; }
    public string message { get; set; }
    public IEnumerable<BankModelData> data { get; set; }
    // public Meta meta { get; set; }

}

public class Meta
{
    public string next { get; set; }
    public object previous { get; set; }
    public int perPage { get; set; }
}


public class BankModelData
{
    public string name { get; set; }
    public string slug { get; set; }
    public string code { get; set; }
    public string longcode { get; set; }
    public object gateway { get; set; } // Since "gateway" can be null
    public bool pay_with_bank { get; set; }
    public bool active { get; set; }
    public bool is_deleted { get; set; }
    public string country { get; set; }
    public string currency { get; set; }
    public string type { get; set; }
    public int id { get; set; }
    public DateTime createdAt { get; set; } // Change to DateTime
    public DateTime updatedAt { get; set; } // Change to DateTime
}

public class CreateTransferRecipientRequestModel
{
    // public string Type { get; set; } = "nuban";
    // public string Type { get; set; } 
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public string BankCode { get; set; }
    // public string Description { get; set; }
    // public string Currency { get; set; } = "NGN";
}

public class CreateTransferRecipientResponseModel
{
    public bool status { get; set; }
    public string message { get; set; }
    public CreateTransferRecipientData data { get; set; }
}


public class InitiateTransferRequestModel
{
    public bool status { get; set; }
    public string message { get; set; }
    public CreateTransferRecipientData data { get; set; }
}

public class CreateTransferRecipientData
{
    public bool active { get; set; }
    public DateTime createdAt { get; set; }
    public string domain { get; set; }
    public uint id { get; set; }
    public int integration { get; set; }
    public string name { get; set; }
    public decimal amount { get; set; }
    public string type { get; set; }
    public string recipient_code { get; set; }
    public DateTime updatedAt { get; set; }
    public bool is_deleted { get; set; }
    public CreateTransferRecipientDataDetails details { get; set; }

}

public class CreateTransferRecipientDataDetails
{
    public string authorization_code { get; set; }
    public string account_number { get; set; }
    public string account_name { get; set; }
    public string bank_code { get; set; }
    public string bank_name { get; set; }
}
public class InitiateTransferResponseModel
{
    public bool status { get; set; }
    public string message { get; set; }
    public TransferMoneyToUserData data { get; set; }
}

public class InitiateTransferRequesteModel
{
    public string RecipientCode { get; set; }
    public decimal Amount { get; set; }
}

public class TransferMoneyToUserData
{
    public int integration { get; set; }
    public decimal amount { get; set; }
    public int recipient { get; set; }
    public int id { get; set; }
    public string domain { get; set; }
    public string currency { get; set; }
    public string source { get; set; }
    public string reason { get; set; }
    public string status { get; set; } //note
    public string transfer_code { get; set; } //note
    public string createdAt { get; set; }
    public string updatedAt { get; set; }
}

public class FinalizeTransferResponseModel
{
    public bool status { get; set; }
    public string message { get; set; }
    public FinalTransferMoneyToUserData data { get; set; }
}

public class FinalTransferMoneyToUserData
{
    public string domain { get; set; }
    public decimal amount { get; set; }
    public string currency { get; set; }// = "NGN";
    public string reference { get; set;  }// = Guid.NewGuid().ToString().Replace('-', 'y');
    public string source { get; set; }
    public object source_details { get; set; } 
    public string reason { get; set; }
    public string status { get; set; }
    public object failures { get; set; } 
    public string transfer_code { get; set; }
    public object titan_code { get; set; } 
    public object transferred_at { get; set; } 
    public int id { get; set; }
    public int integration { get; set; }
    public int recipient { get; set; }
    public string createdAt { get; set; }
    public string updatedAt { get; set; }
}






public class TransferMoneyToUserRequestModel
{
    public CreateTransferRecipientResponseModel model { get; set; }
    public string TransferSourse { get; set; } = "balance";
    public string TransferReason { get; set; }
    public string TransferReference { get; set; } = Guid.NewGuid().ToString().Replace('-', 'y');
    public string Currency { get; set; } = "NGN";
    public string Recipient { get; set; }
    public int Amount { get; set; }

}


public class PaymentGateways
{
}

public class PaysatckInitializePayment
{

}

public class InitializeTransactionRequestModel
{
    public decimal Amount { get; set; }
    public string RefrenceNo { get; set; }
    public string Email { get; set; }
    public string CallbackUrl { get; set; }

}

public class InitializePaymentData
{
    public string authorization_url { get; set; }
    public string access_code { get; set; }
    public string reference { get; set; }

}

public class InitializeTransactionResponseModel
{
    public bool status { get; set; }
    public string message { get; set; }
    public InitializePaymentData data { get; set; }
    //  "status": true,
    //"message": "Authorization URL created",
    //"data":
}





//public class VerifyTransactionRequestModel
//{
//    public string ReferenceNumber { get; set; }

//}

public class VerifyTransactionData
{
    public uint id { get; set; }
    public string domain { get; set; }
    public string status { get; set; }
    public string gateway_response { get; set; }
    public string reference { get; set; }
    public int amount { get; set; }
    public DateTime paid_at { get; set; }
    public DateTime created_at { get; set; }
    public string currency { get; set; }
    public string channel { get; set; }
    public string ip_address { get; set; }
    //public string bank_id { get; set; }
    //"data\":{\"account_number\":\"0159192507\",\"account_name\":\"ABDULSALAM AHMAD AYOOLA\",\"bank_id\":9}
}
public class VerifyTransactionResponseModel
{
    public bool status { get; set; }
    public string message { get; set; }
    public VerifyTransactionData data { get; set; }
    //"{\"status\":true,\"message\":\"Account number resolved\",\"data\":{\"account_number\":\"0159192507\",\"account_name\":\"ABDULSALAM AHMAD AYOOLA\",\"bank_id\":9}}"
}






// #!/bin/sh
// url="https://api.paystack.co/transfer"
// authorization="Authorization: Bearer YOUR_SECRET_KEY"
// content_type="Content-Type: application/json"
// data='{ 
//   "source": "balance", 
//   "reason": "Calm down", 
//   "amount":3794800, "recipient": "RCP_gx2wn530m0i3w3m"
// }'

// curl "$url" -H "$authorization" -H "$content_type" -d "$data" -X POST

// {
//   "status": true,
//   "message": "Transfer requires OTP to continue",
//   "data": {
//     "integration": 100073,
//     "domain": "test",
//     "amount": 3794800,
//     "currency": "NGN",
//     "source": "balance",
//     "reason": "Calm down",
//     "recipient": 28,
//     "status": "otp",
//     "transfer_code": "TRF_1ptvuv321ahaa7q",
//     "id": 14,
//     "createdAt": "2017-02-03T17:21:54.508Z",
//     "updatedAt": "2017-02-03T17:21:54.508Z"
//   }
// }










