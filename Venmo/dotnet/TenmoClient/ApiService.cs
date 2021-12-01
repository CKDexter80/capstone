using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using TenmoClient.Exceptions;
using TenmoClient.Models;

namespace TenmoClient
{
    class ApiService {
        private readonly static string API_URL = "https://localhost:44315/";
        private IRestClient client;
        
        public ApiService()
        {
            client = new RestClient();            
        }


        public Account GetAccount()
        {
            RestRequest request = new RestRequest(API_URL + "account");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            IRestResponse<Account> response = client.Get<Account>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        public decimal GetAccountBalance() {
            RestRequest request = new RestRequest(API_URL + "account/balance");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            IRestResponse<decimal> response = client.Get<decimal>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful) {
                ProcessErrorResponse(response);
            } else {
                return response.Data;
            }
            return 0;

        }

        //
        public IList<UserTEBucks> GetAllUsers()
        {
            RestRequest request = new RestRequest(API_URL + "user");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            IRestResponse<List<UserTEBucks>> response = client.Get<List<UserTEBucks>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        public IList<ViewTransfer> GetAllUserTransfers()
        {
            RestRequest request = new RestRequest(API_URL + "transfer/view");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            IRestResponse<List<ViewTransfer>> response = client.Get<List<ViewTransfer>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        public void CreateTransfer(Transfer transfer)
        {
            RestRequest request = new RestRequest(API_URL + "transfer");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            request.AddJsonBody(transfer);

            IRestResponse response = client.Post<Transfer>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            
            
        }

        public TransferDetails GetTransferDetail(int transferId)
        {
            RestRequest request = new RestRequest(API_URL + "transfer/details/"+ transferId);
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            IRestResponse<TransferDetails> response = client.Get<TransferDetails>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        //todo: see AccountDao
        public void AddToBalance(Transfer transfer)
        {
            RestRequest request = new RestRequest(API_URL + "account/add");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            request.AddJsonBody(transfer);
            IRestResponse response = client.Put(request);

            ProcessErrorResponse(response);
            
        }

        public void SubtractFromBalance(Transfer transfer)
        {
            RestRequest request = new RestRequest(API_URL + "account/subtract");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            request.AddJsonBody(transfer);
            IRestResponse response = client.Put(request);

            ProcessErrorResponse(response);

        }

        private void ProcessErrorResponse(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed) {
                throw new NoResponseException("Error occurred - unable to reach server.", response.ErrorException);
            } else if (!response.IsSuccessful) {
                
                throw new NonSuccessException((int)response.StatusCode);
            }
        }
    }
}
