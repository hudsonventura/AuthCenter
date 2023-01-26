using AuthCenter;

string client_id = "user.YourClientID";       //see https://bitwarden.com/help/public-api/
string client_secret = "YourClientSecret";    //see https://bitwarden.com/help/public-api/
string email = "yourmail@yourdomain.com";
string password = "Sup3rS3cr3tP@ssw0rd";
int otp2FA = 999999;                                //it's not mandatory, but highly recommended
string url = "https://yourownserver.youdomain.com"; //default: https://vault.bitwarden.com


//using AuthCenter; //Don't forget!
BitwardenClient bitwarden;
try
{
    Console.Write("Trying logging into Bitwarden ... ");

    //Choose just one method to login

    //login with email
    //bitwarden = new BitwardenClient(url, email, password);

    //login with email and OTP 2FA
    //bitwarden = new BitwardenClient(url, email, password, otp2FA);

    //login with client_id and client_secret
    bitwarden = new BitwardenClient(url, client_id, client_secret, password);

    Console.WriteLine("Success!");
}
catch (Exception error)
{
    Console.WriteLine($"Fail: {error.Message}");
    return;
}



var vaultItem = bitwarden.GetItem("0b4eb51f-11e6-48dd-80db-bbe41fca9d26");
Console.WriteLine($"Username: {vaultItem.item.login.username}");
Console.WriteLine($"Password: {vaultItem.item.login.password}");

bitwarden.LogOut();