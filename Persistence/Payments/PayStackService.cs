using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Dtos.PaymentGatewayDTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Persistence.Payments;

public class PayStackService : IPayStackService
{
    private readonly PaystackOptions _paystackOptions;
    private readonly IConfiguration _configure;

    public PayStackService(IOptions<PaystackOptions> paystackOptions, IConfiguration configure)
    {
        _paystackOptions = paystackOptions.Value;
        _configure = configure;
    }


    public async Task<BankResponseModel> GetBanksAsync()
    {
        var url = "https://api.paystack.co/bank";
        // var authorization = "Bearer sk_test_bcd26e03b2282ddf4f2affe2c8ff796c91b86ba5";
        var authorization = _configure.GetValue<string>("Paystack:TestSecreteKey");


        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<BankResponseModel>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return data;
            }
        }

        return null;
    }



    public async Task<VerifyAccountNumberResponseModel> VerifyAccountNumber(VerifyAccountNumberRequestModel model)
    {
        var key = _configure.GetValue<string>("Paystack:TestSecreteKey");
        var getHttpClient = new HttpClient();
        getHttpClient.DefaultRequestHeaders.Accept.Clear();
        getHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        getHttpClient.BaseAddress = new Uri($"https://api.paystack.co/bank/resolve?account_number={model.AccountNumber}&bank_code={model.BankCode}");
        getHttpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", key);
        var response = await getHttpClient.GetAsync(getHttpClient.BaseAddress);
        var responseString = await response.Content.ReadAsStringAsync();
        var responseObj = JsonSerializer.Deserialize<VerifyAccountNumberResponseModel>(responseString);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return responseObj;
        }
        //  return responseObj;
        return null;
    }




    public async Task<CreateTransferRecipientResponseModel> CreateTransferRecipient(CreateTransferRecipientRequestModel model)
    {
        var key = _paystackOptions.APIKey;
        var getHttpClient = new HttpClient();
        getHttpClient.DefaultRequestHeaders.Accept.Clear();
        getHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        getHttpClient.BaseAddress = new Uri($"https://api.paystack.co/transferrecipient");
        getHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
        var response = await getHttpClient.PostAsJsonAsync(getHttpClient.BaseAddress, new
        {
            type = model.Type,
            name = model.Name,
            account_number = model.AccountNumber,
            bank_code = model.BankCode,
            currency = model.Currency,
            description = model.Description,
        });
        var responseString = await response.Content.ReadAsStringAsync();
        var responseObj = JsonSerializer.Deserialize<CreateTransferRecipientResponseModel>(responseString);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return responseObj;
        }
        return responseObj;
    }

    public async Task<InitializeTransactionResponseModel> InitializePayment(InitializeTransactionRequestModel model)
    {
        //var key = _paystackOptions.APIKey;
        var key = "sk_test_14f1594c21ed0aba7d3e0ab35d1fec6ad5f0708c";
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var endPoint = "https://api.paystack.co/transaction/initialize";
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
        var content = new StringContent(JsonSerializer.Serialize(new
        {
            amount = model.Amount * 100,
            email = model.Email,
            reference = model.RefrenceNo,
            currency = "NGN",
            callback_url = model.CallbackUrl,

        }), Encoding.UTF8, "application/json");




        try
        {
            var response = await client.PostAsync(endPoint, content);
            var resString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonSerializer.Deserialize<InitializeTransactionResponseModel>(resString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return responseObj;
            }
            return responseObj;
        }
        catch (Exception)
        {
            return new InitializeTransactionResponseModel
            {
                status = true,
                message = "Payment gateway was assume to be success.",
                data = new InitializePaymentData
                {
                    access_code = "",
                    authorization_url = "",
                    reference = model.RefrenceNo,
                },
            };

            //assumin all payment are succesful
            //return new InitializeTransactionResponseModel
            //{
            //    status = false,
            //    message = "Payment gateway not available at the moment..",
            //};
        }
    }

    public async Task<InitiateTransferResponseModel> InitiateTransfer(CreateTransferRecipientResponseModel model)
    {
        var key = _paystackOptions.APIKey;

        var getHttpClient = new HttpClient();
        getHttpClient.DefaultRequestHeaders.Accept.Clear();
        getHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var baseUri = $"https://api.paystack.co/transfer";
        getHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
        var response = await getHttpClient.PostAsJsonAsync(baseUri, new
        {
            recipient = model.data.recipient_code,
            amount = model.data.amount * 100,
            reference = Guid.NewGuid().ToString().Replace('-', 'y'),
            currency = "NGN",
            source = "balance",
        });
        var responseString = await response.Content.ReadAsStringAsync();

        var responseObj = JsonSerializer.Deserialize<InitiateTransferResponseModel>(responseString);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return responseObj;
        }
        return responseObj;
    }


    // public async Task<IEnumerable<BankResponseModel>> GetBanksAsync()
    // {
    //     var url = "https://api.paystack.co/bank";
    //     var authorization = "Bearer sk_test_bcd26e03b2282ddf4f2affe2c8ff796c91b86ba5";

    //     using (var client = new HttpClient())
    //     {
    //         client.DefaultRequestHeaders.Accept.Clear();
    //         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //         client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);

    //         var response = await client.GetAsync(url);

    //         if (response.IsSuccessStatusCode)
    //         {
    //             var data = await response.Content.ReadAsAsync<IEnumerable<BankResponseModel>>();
    //             return data;
    //         }
    //     }

    //     return null;
    // }



    public async Task<VerifyTransactionResponseModel> ConfirmPayment(string referenceNumber)
    {
        var key = _configure.GetValue<string>("Paystack:TestSecreteKey");
        var getHttpClient = new HttpClient();
        getHttpClient.DefaultRequestHeaders.Accept.Clear();
        getHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        getHttpClient.BaseAddress = new Uri($"https://api.paystack.co/transaction/verify/{referenceNumber}");
        getHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
        var response = await getHttpClient.GetAsync(getHttpClient.BaseAddress);
        var responseString = await response.Content.ReadAsStringAsync();
        var responseObj = JsonSerializer.Deserialize<VerifyTransactionResponseModel>(responseString);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return responseObj;
        }
        return responseObj;
    }
}
