using AuthCenter;

Authentication_OAuth2 auth = new Authentication_OAuth2(
    "https://yourdomain.com"
    , Authentication_OAuth2.GrantType.client_credentials
    , "client_secret"
    , "client_id"
    , "youScope"
    , Authentication_OAuth2.Client_Authentication.SendClientCredentialsInBody
    );
var token = auth.GetToken("/oauth2/token"); //the endpoint
Console.WriteLine(token);