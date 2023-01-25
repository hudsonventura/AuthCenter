using hudsonventura;

string client_id = "user.YourClientID";       //see https://bitwarden.com/help/public-api/
string client_secret = "YourClientSecret";    //see https://bitwarden.com/help/public-api/
string email = "yourmail@yourdomain.com";
string password = "Sup3rS3cr3tP@ssw0rd";
int otp2FA = 999999;                                //it's not mandatory, but highly recommended
string url = "https://yourownserver.youdomain.com"; //default: https://vault.bitwarden.com



Bitwarden_AuthCenter bitwarden;
try
{
    Console.Write("Trying logging into Bitwarden ... ");

    //Choose just one method to login

    //login with email
    //bitwarden = new Bitwarden_AuthCenter(url, email, password);

    //login with email and OTP 2FA
    //bitwarden = new Bitwarden_AuthCenter(url, email, password, otp2FA);

    //login with client_id and client_secret
    bitwarden = new Bitwarden_AuthCenter(url, client_id, client_secret, password);

    Console.WriteLine("Success!");
}
catch (Exception error)
{
    Console.WriteLine($"Fail: {error.Message}");
    return;
}



// Get the vault status
var status = bitwarden.Status();
