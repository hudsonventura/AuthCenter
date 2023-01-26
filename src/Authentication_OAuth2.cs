using AuthCenter.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthCenter;

public class Authentication_OAuth2
{
    public Authentication_OAuth2(string baseURL, GrantType grant_type, string client_secret, string client_id, string scope, Client_Authentication client_authentication)
    {
        this.baseURL = baseURL;
        this.grant_type = grant_type;
        this.client_secret = client_secret;
        this.client_id = client_id;
        this.scope = scope;
        this.client_authentication = client_authentication;
    }

    public string baseURL { get; set; }
    public GrantType grant_type { get; set; }
    public string client_secret { get; set; }
    public string client_id { get; set; }
    public string scope { get; set; }
    public Client_Authentication client_authentication { get; set; }

    public enum GrantType { 
        client_credentials,
        /// it needs to be implemented
    }

    public enum Client_Authentication { 
        SendAsBasicAuthHeader, 
        SendClientCredentialsInBody
    }


    public string GetToken(string path)
    {
        if (this.baseURL == null || this.grant_type == null || this.client_secret == null || this.client_id == null || this.scope == null || this.client_authentication == null)
        {
            throw new Exception("Please, fill in all fields before trying to get the token (grant_type, client_secret, client_id, scope, )");
        }

        if (client_authentication == Client_Authentication.SendAsBasicAuthHeader)
        {
            throw new NotImplementedException();
        }

        RestClient client = new RestSharp.RestClient(baseURL);
        var host = baseURL.Split("://")[1].Split(":")[0];
        client.AddDefaultHeader("Host", host);





        if (client_authentication == Client_Authentication.SendClientCredentialsInBody)
        {
            var request = new RestRequest(path, Method.Post);
            request.AddParameter("client_id", client_id, ParameterType.GetOrPost);
            request.AddParameter("grant_type", grant_type.ToString(), ParameterType.GetOrPost);
            request.AddParameter("client_secret", client_secret, ParameterType.GetOrPost);
            request.AddParameter("scope", scope, ParameterType.GetOrPost);

            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.Content);
            }

            return response.Content;
        }


        throw new NotImplementedException("Some was not implemented");

    }
            

        
}
