BalancedSharp
=============

This is a .Net library for integrating with [Balanced Payments](https://www.balancedpayments.com) (Payments for Marketplaces).

## Getting Started

The first thing you need to do to use this library is get your api key. They can be found on the balanced dashboard under "settings".

### Signing up for Balanced

Test driving their api is as easy as going to [https://www.balancedpayments.com](https://www.balancedpayments.com) (Payments for Marketplaces) and clicking 'Try It Now'

### Getting an Api Key

* Go to the Balanced Payments Dashboard
* Click "settings"

To use the library you will 'api key secret' underneath the marketplace section.

## Hello World

We tried to architect the library to follow the specs in the [Balanced Payments Rest Api](https://www.balancedpayments.com/docs/api). Everything stems from the BalancedService:

    BalancedService service = new BalancedService("myapikey");

### Getting Data

Every section of the api has a direct uri path to get the data you need. If you were looking to get the details of a bank account:

    BalancedService service = new BalancedService("myapikey");
    var bankAccount = service.Account.Get("https://api.balancedpayments.com/v1/bank_accounts/BA6ThbEt9vlVXtNB1K4C9VUs");

### Creating Data

Creating information can be done at multiple levels. For example you can create a bank account:

    BalancedService service = new BalancedService("myapikey");
    var account = service.CreateBankAccount(new BankAccount()
    {
        Name = "myName",
        AccountNumber = "120923409",
        RoutingNumber = "029034080",
        Type = BankAccountType.Checking
    });

Or you can create a card for the default marketplace:

    BalancedService service = new BalancedService("myapikey");
    var card = service.CurrentMarketplace.CreateCard(new Card()
    {
        CardNumber = "0293840298340",
        ExpirationYear = 2020,
        ExpirationMonth = 10,
        SecurityCode = 234
    });

## Common Examples


## Api

* [Accounts](https://github.com/rentler/BalancedSharp/wiki/Accounts-Api)
* [Bank Accounts](https://github.com/rentler/BalancedSharp/wiki/BankAccounts-Api)
* [Cards](https://github.com/rentler/BalancedSharp/wiki/Cards-Api)
* [Credits](https://github.com/rentler/BalancedSharp/wiki/Credits-Api)
* [Debits](https://github.com/rentler/BalancedSharp/wiki/Debits-Api)
* [Events](https://github.com/rentler/BalancedSharp/wiki/Events-Api)
* [Holds](https://github.com/rentler/BalancedSharp/wiki/Holds-Api)
* [Refunds](https://github.com/rentler/BalancedSharp/wiki/Refunds-Api)
* [Verifications](https://github.com/rentler/BalancedSharp/wiki/Accounts-Api)

For more information on the Balanced Api itself go to the [Balanced Payments Rest Api](https://www.balancedpayments.com/docs/api)

## Running the Tests

If unit testing is your thing just open Config.cs in the BalancedSharp.Test project and change the Api key. All tests are done in NUnit.

## License

This software is licensed under the MIT License. View the LICENSE file for more information.
