# Bitwarden_AuthCenter


The offical Bitwarden API is only for organisational management.  If you want to manage your Bitwarden Vault there is the BitWarden CLI.

This project is a simple C# wrapper that integrates with the Bitwarden CLI (Command Line Interface) and makes it available as an API (App Programming Interface) accessible via C# code.

This proof of concept API has only been tested on Windows and GNU/Linux. Please, contribute by testing on MacOS.

The Documentation on the Bitwarden CLI is available here: https://bitwarden.com/help/cli/

All right reserved for OceanAirdrop. This project was based on https://github.com/OceanAirdrop/Bitwarden-Vault-CLI-API

Source code: https://github.com/hudsonventura/Bitwarden_AuthCenter
Nuget package: https://www.nuget.org/packages/Bitwarden_AuthCenter/

<br>

The Bitwarden CLI binary (`bw.exe` for Windows or `bw` for GNU/Linux) CLI needs to be in the same diretcory as your application. This lib already contains the binary bw. But if youwant to user another version, you can update the binary of the BitWarden CLI from the Bitwarden website, available here: https://bitwarden.com/help/cli/

After install Nuget package, you have to configure to copy the binary to your app directory. Set the parameter 'Copy to Output Directory' for 'Copy if newer', as your OS version (`bw.exe` for Windows or `bw` for Gnu/Linux) like this:

<img src="https://github.com/hudsonventura/images/raw/main/bitwarden_binary_copy_if_newest.png" />

<br>

Example Bitwarden Code:

``` C#

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
    bitwarden = new BitwardenClient(url, email, password);

    //login with email and OTP 2FA
    bitwarden = new BitwardenClient(url, email, password, otp2FA);

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

{
    // Get the vault status
    var status = bitwarden.Status();
    
    // Get a list of the oragnisations
    var orgs = bitwarden.ListOrganisations();
    
    // Get a list of all the available collections
    var colls = bitwarden.ListCollections();

    // Get a list of all the items in the vault
    var vaultItems = bitwarden.ListItems();

    // Create a new secure note
    var newNote = bitwarden.CreateSecureNote("org-guid", "collection-guid", "my test secure note", "some text here");

    // Create a new login for a website
    var newLogin = bitwarden.CreateLogin("org-guid", "collection-guid", "My test Login", "user", "pass", "https://127.0.0.1");

    // Get a list of items that contain the word "test"
    var testItems = bitwarden.ListItems(searchPattern: "test");

    // Get a list of items that contain the word "test" in a specific collection
    var testItemsInColl = bitwarden.ListItems(searchPattern: "test", collectionId: "collection-guid");
    
    // Get Item by Name
    var vaultItem1 = bitwarden.GetItem("My test Login");

    // Get Item by Id
    var vaultItem2 = bitwarden.GetItem("f6184129-6cf5-4a61-8904-318e821a7615");

    // Edit existing item in the vault
    vaultItem2.item.notes = "some extra notes";
    bitwarden.EditItem(vaultItem2.item);
    
    // Create an attachement
    bitwarden.CreateAttachment("f6184129-6cf5-4a61-8904-318e821a7615", @"C:\Files\SomeFile.txt");
    
    // Download an attachement
    bitwarden.DownloadAttachment("f6184129-6cf5-4a61-8904-318e821a7615", "SomeFile.txt" );
    
    // Delete all test items from the vault
    foreach (var item in testItemsInColl)
    {
        bitwarden.DeleteItem(item.id);
    }

} // LogOut()  is called automatically when you exit this using statement.

```
<br><br>

Example Authentication OAuth2 Code:

``` C#
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
Console.WriteLine(token); //It's response string. Maybe you need to convert it to JSON to obtain the token.
```