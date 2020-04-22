# ip-api
A .NET Core 2.1 client wrapper for accessing the ip-api API.

# Installation
To use ip-api in your C# project, you can either download the ip-api C# .NET libraries directly from the Github repository or, if you have the NuGet package manager installed, you can grab them automatically.

```
PM> Install-Package ip-api.Core
```
Once you have the ip-api libraries properly referenced in your project, you can include calls to them in your code.

Add the following namespaces to use the library:

```C#
using ip_api.Core;
using ip_api.Core.Models;
```

# Usage
The below code can be used in a .NET project.

```C#
    public class Program
    {
        public static void Main(string[] args)
        {
            IpApiClient client = new IpApiClient();

            // Get single IP address with defaults
            IpAddressDetails singleDetails = client.GetIpAddressDetails("134.201.250.155");
						
            // Get single IP address in Chinese
            IpAddressDetails singleDetails = client.GetIpAddressDetails("134.201.250.155", LanguageOpition.Chinese);

            // Get multiple IP addresses with defaults
            List<IpAddressDetails> bulkDetails = client.GetIpAddressDetails(new List<string>() { "134.201.250.155", "72.229.28.185", "110.174.165.78" });

            // Upgrade client to https
            client = new IpApiClient(true);

            // Get requestors address details
            IpAddressDetails requestorDetails = client.GetRequesterIpAddressDetails();
        }
    }
```
