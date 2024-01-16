using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RestSharp;

namespace Application.Dtos.PaymentGatewayDTOs
{
    public class PayoutModel
    {
        //  [JsonPropertyName("account_bank")]
        public string account_bank { get; set; }

        // [JsonPropertyName("account_number")]
        public string account_number { get; set; }

        // [JsonPropertyName("amount")]
        public decimal amount { get; set; }

        // [JsonPropertyName("narration")]
        public string narration { get; set; }

        // [JsonPropertyName("currency")]
        public string currency { get; set; }

        // [JsonPropertyName("reference")]
        public string reference { get; set; }

        // [JsonPropertyName("callback_url")]
        public string callback_url { get; set; }

        // [JsonPropertyName("debit_currency")]
        public string debit_currency { get; set; }
    }

    public class PayoutResponse
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public IRestResponse OriginalResponse { get; set; }
    }

    public class GenerateOTPRequestModel
    {
        public int length { get; set; }
        public Customer customer { get; set; }
        public string sender { get; set; }
        public bool send { get; set; }
        public string[] medium { get; set; }
        public int expiry { get; set; }
    }

    public class Customer
    {
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }

    public class GeneratedOtpResponseModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<OtpData> data { get; set; }
    }

    public class OtpData
    {
        public string medium { get; set; }
        public string reference { get; set; }
        public string otp { get; set; }
        public DateTime expiry { get; set; }
    }

    public class PayoutInitiationResponse
    {
        public bool OtpRequired { get; set; }
        public IRestResponse FlutterwaveResponse { get; set; }
    }

    public class PayoutRequestModel
    {
        public string account_bank { get; set; }
        public string account_number { get; set; }
        public decimal amount { get; set; }
        public string narration { get; set; }
        public string currency { get; set; }
        public string reference { get; set; }
        public string callback_url { get; set; }
        public string debit_currency { get; set; }
    }

    public class PayoutResponseModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public PayoutData data { get; set; }
    }

    public class PayoutData
    {
        public int id { get; set; }
        public string account_number { get; set; }
        public string bank_code { get; set; }
        public string full_name { get; set; }
        public DateTime created_at { get; set; }
        public string currency { get; set; }
        public string debit_currency { get; set; }
        public decimal amount { get; set; }
        public decimal fee { get; set; }
        public string status { get; set; }
        public string reference { get; set; }
        public object meta { get; set; }
        public string narration { get; set; }
        public string complete_message { get; set; }
        public int requires_approval { get; set; }
        public int is_approved { get; set; }
        public string bank_name { get; set; }
    }

    // {"status":"success","message":"Transfer Queued Successfully","data":{"id":539910,"account_number":"0690000040",
    // "bank_code":"044","full_name":"Alexis Sanchez","created_at":"2023-12-18T12:18:06.000Z","currency":"NGN","debit_currency":"NGN","amount":500,
    // "fee":10.75,"status":"NEW","reference":"akhlm-pstmnpyt-aax007_PMCKDU_1","meta":null,"narration":"Akhlm Pstmn Trnsfr xx007",
    // "complete_message":"","requires_approval":0,"is_approved":1,"bank_name":"ACCESS BANK NIGERIA"}}




    // public class TransferRequest
    // {
    //     public string account_bank { get; set; }
    //     public string account_number { get; set; }
    //     public decimal amount { get; set; }
    //     public string narration { get; set; }
    //     public string currency { get; set; }
    //     public string reference { get; set; }
    //     public string callback_url { get; set; }
    //     public string debit_currency { get; set; }
    // }

    //   public class PayoutResponseModel
    // {
    //     public string Status { get; set; }
    //     public string Message { get; set; }
    //     public PayoutData Data { get; set; }
    // }

    // public class PayoutData
    // {
    //     public int Id { get; set; }
    //     public string AccountNumber { get; set; }
    //     public string BankCode { get; set; }
    //     public string FullName { get; set; }
    //     public DateTime CreatedAt { get; set; }
    //     public string Currency { get; set; }
    //     public string DebitCurrency { get; set; }
    //     public decimal Amount { get; set; }
    //     public decimal Fee { get; set; }
    //     public string Status { get; set; }
    //     public string Reference { get; set; }
    //     public object Meta { get; set; }
    //     public string Narration { get; set; }
    //     public string CompleteMessage { get; set; }
    //     public int RequiresApproval { get; set; }
    //     public int IsApproved { get; set; }
    //     public string BankName { get; set; }
    // }
}
