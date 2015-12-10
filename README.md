# Lipisha Payments .NET SDK


This package provides bindings for the Lipisha Payments API (http://developer.lipisha.com/)

* Free software: Apache 2.0 Licence

Features
--------

- Send money
- Get float balanace
- Get account balance
- Search Transactions
- Complete transactions
- Reverse transactions
- Authorize card transactions
- Complete card transactions
- Reverse card transactions
- Search Customers
- Create sub-users with specific roles
- Send airtime
- Send SMS

Documentation
--------------

- Lipisha API: http://developer.lipisha.com/index.php/app/launch/api
- Package: https://www.nuget.org/packages/Lipisha.Sdk

Installation and Download
-------------------------

Download the DLL from here:

https://github.com/lipisha/lipisha-dotnet-sdk/releases

Install using Nuget:

```shell
nuget install Lipisha.Sdk
```

Or via the Package Manager console

```shell
Install-Package Lipisha.Sdk
```


IPN and SDK Examples
--------------------

The Lipisha tests offer a complete overview of integrating with all APIs.

IPN Examples using APN.NET MVC can be seen here.

https://github.com/lipisha/lipisha-dotnet-sdk/tree/master/LipishaIPNMVC

See examples here (https://github.com/lipisha/lipisha-dotnet-sdk/tree/master/LipishaTests)

IPN Examples
-------------

Coming soon.

Quick start
-----------

The class below showcases integration with the API.

*Note that all responses from the API implement these methods. These can be used to determine if the request was successful.*

```csharp
response.isSuccessful()
response.getStatusMessage()
response.getStatusDescription()
response.getStatusCode()
```

Example Integration
-------------------

```csharp
using System;
using Lipisha;
using Lipisha.Response;

namespace LipishaExample
{
    class Program
    {

        private const string API_KEY = "<YOUR_API_KEY>";
        private const string API_SIGNATURE = "<YOUR_API_SIGNATURE>";
        private LipishaClient client;

        public Program() {
            this.client = new LipishaClient(API_KEY, API_SIGNATURE, "live");
        }

        private void print(string label, object content)
        {
            Console.WriteLine(label + "::" + content);
        }

        private void sendMoney ()
        {
            Payout payout = client.sendMoney("0XXXX", "0722123456", 10.00);
            print("PayoutStatus", payout.isSuccessful());
            print("PayoutAmount", payout.getAmount());
            print("PayoutStatus", payout.getStatus());
            print("PayoutStatusDescription", payout.getStatusDescription());
        }

        private void getBalance ()
        {
            AccountBalance balance = client.getBalance();
            print("Balance", balance.getBalance());
            print("BalanceCurrency", balance.getCurrency());
        }

        private void confirmTransaction()
        {
            TransactionResponse confirmation = client.acknowledgeTransaction("TC90000424");
            print("ConfirmationStatus", confirmation.getStatus());
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.sendMoney();
            program.getBalance();
            program.confirmTransaction();
        }
    }
}
```

*See documentation for detailed API. Refer to Lipisha API for parameters required for each method.*
