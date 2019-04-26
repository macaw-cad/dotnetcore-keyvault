# 1.    Implementation of Azure Key Vault with dotnet core

This projects shows how you can easily implement [Azure Key Vault](https://azure.microsoft.com/en-us/services/key-vault/) within your donet core Web app. For more information about what Key Vault is and why use should use it. Please read below.

## 1.1. Prerequisites
- Visual Studio 2019
- Dotnet core 2.2 MVC app

This projects uses the following NuGet packages for Azure Key Vault:
- Microsoft.Azure.KeyVault
- Microsoft.Extensions.Configuration.AzureKeyVault

## 1.2. The solution
First let's start by opening the solution file in Visual Studio. You can either clone or download the repo from Github. Once the solution has loaded navigate to the `Program.cs`. In this class you will find the code for starting up the dotnet core app as well as the Azure Key Vault registration.

The following lines are used to read the values from the appsetings configuration file named `appsettings.json`. In the configuration you will need to put in your Key Vault settings. If you forget to do this, the application will not be able to start up.
```c#
private const string KeyVaultUri = "KeyVault:Uri";
private const string KeyVaultClientId = "KeyVault:ClientId";
private const string KeyVaultClientSecrect = "KeyVault:ClientSecret";
```

For now, let's have a closer look at the `Program.cs` file. In the following lines of code, is where the "magic" happens. In the extension method `AddAzureKeyVault`, the code will be executed to load all Azure Key Vault secrets into the `IConfiguration`. Therefor it will be very easy to access the Key Vault secrets further in the application.
```c#
var config = configurationBuilder.Build();
configurationBuilder.AddAzureKeyVault(
     config[KeyVaultUri],
     config[KeyVaultClientId],
     config[KeyVaultClientSecrect]
 );
```

After the project has started, when can now take a look at the `ValuesController`. When you open the controller you will immediately see that dotnet core uses dependency injection (DI). Therefor you can request an instance of the `IConfiguration`. The DI will try to fetch the instance, so that you can access it. With the following line of code, you can read a secret from the Key Vault. The `key` for the configuration is the same as the key used for the secret in Key Vault.
```c#
_keyVaultValue = configuration["name-of-secret-in-key-vault"];
```

## 1.3. Run the web app
When you start the web app you'll see that the browser executes the `Get` method of the `ValuesController`. When all works well you will see your secret value in the output, along side with `value1` and `value2`.

## 1.4. Questions
Thank you for reading example on how to implement Azure Key Vault. If you have any questions please contact us on [Twitter](https://twitter.com/MacawNL).

# What is Azure Key Vault
*This information is copied from Microsoft, see the [website](https://docs.microsoft.com/en-us/azure/key-vault/key-vault-overview) for more information.*
Azure Key Vault helps solve the following problems:
- **Secrets Management** - Azure Key Vault can be used to Securely store and tightly control access to tokens, passwords, certificates, API keys, and other secrets
- **Key Management** - Azure Key Vault can also be used as a Key Management solution. Azure Key Vault makes it easy to create and control the encryption keys used to encrypt your data.
- **Certificate Management** - Azure Key Vault is also a service that lets you easily provision, manage, and deploy public and private Secure Sockets Layer/Transport Layer Security (SSL/TLS) certificates for use with Azure and your internal connected resources.
- **Store secrets backed by Hardware Security Modules** - The secrets and keys can be protected either by software or FIPS 140-2 Level 2 validates HSMs

## Why use Azure Key Vault?
### Centralize application secrets
Centralizing storage of application secrets in Azure Key Vault allows you to control their distribution. Key Vault greatly reduces the chances that secrets may be accidentally leaked. When using Key Vault, application developers no longer need to store security information in their application. Not having to store security information in applications eliminates the need to make this information part of the code. For example, an application may need to connect to a database. Instead of storing the connection string in the app's code, you can store it securely in Key Vault.

Your applications can securely access the information they need by using URIs. These URIs allow the applications to retrieve specific versions of a secret. There is no need to write custom code to protect any of the secret information stored in Key Vault.

### Securely store secrets and keys
Secrets and keys are safeguarded by Azure, using industry-standard algorithms, key lengths, and hardware security modules (HSMs). The HSMs used are Federal Information Processing Standards (FIPS) 140-2 Level 2 validated.

Access to a key vault requires proper authentication and authorization before a caller (user or application) can get access. Authentication establishes the identity of the caller, while authorization determines the operations that they are allowed to perform.

Authentication is done via Azure Active Directory. Authorization may be done via role-based access control (RBAC) or Key Vault access policy. RBAC is used when dealing with the management of the vaults and key vault access policy is used when attempting to access data stored in a vault.

Azure Key Vaults may be either software- or hardware-HSM protected. For situations where you require added assurance you can import or generate keys in hardware security modules (HSMs) that never leave the HSM boundary. Microsoft uses Thales hardware security modules. You can use Thales tools to move a key from your HSM to Azure Key Vault.

Finally, Azure Key Vault is designed so that Microsoft does not see or extract your data.

### Monitor access and use
Once you have created a couple of Key Vaults, you will want to monitor how and when your keys and secrets are being accessed. You can monitor activity by enabling logging for your vaults. You can configure Azure Key Vault to:

### Archive to a storage account.
Stream to an event hub.
Send the logs to Azure Monitor logs.
You have control over your logs and you may secure them by restricting access and you may also delete logs that you no longer need.

### Simplified administration of application secrets
When storing valuable data, you must take several steps. Security information must be secured, it must follow a life cycle, it must be highly available. Azure Key Vault simplifies the process of meeting these requirements by:

- Removing the need for in-house knowledge of Hardware Security Modules
- Scaling up on short notice to meet your organization’s usage spikes.
- Replicating the contents of your Key Vault within a region and to a secondary region. Data replication ensures high availability and takes away the need of any action from the administrator to trigger the failover.
- Providing standard Azure administration options via the portal, Azure CLI and PowerShell.
- Automating certain tasks on certificates that you purchase from Public CAs, such as enrollment and renewal.

In addition, Azure Key Vaults allow you to segregate application secrets. Applications may access only the vault that they are allowed to access, and they can be limited to only perform specific operations. You can create an Azure Key Vault per application and restrict the secrets stored in a Key Vault to a specific application and team of developers.
