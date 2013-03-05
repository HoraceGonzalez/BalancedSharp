BalancedSharp
=============

This is a .Net library for integrating with [Balanced Payments](https://www.balancedpayments.com) (Payments for Marketplaces).

## Getting Started

The first thing you need to do to use this library is get your marketplace uri and your api key. They can be found on the balanced dashboard under "settings".

### Signing up for Balanced

Test driving their api is as easy as going to [https://www.balancedpayments.com](https://www.balancedpayments.com) (Payments for Marketplaces) and clicking 'Try It Now'

### Getting an Api Key

* Go to the Balanced Payments Dashboard
* Click "settings"

To use the library you will use 'uri' and 'api key secret' underneath the marketplace section.

## Hello World

We tried to architect the library to follow the specs in the [Balanced Payments Rest Api](https://www.balancedpayments.com/docs/api). Everything stems from the BalancedService:

    BalancedService service = new BalancedService("/v1/marketplaces/MyMarketplace", "myapikey");

Then you can create accounts (We are dealing with Marketplaces here):

    var account = service.Account.Create();

Create cards:

    var card = service.Tokenize("xxxxxxxxxxxxxxxx", 2015, 2);

Attach card to account:

    var attach = service.Account.AddCard(account.Result.Id, card.Result.Id);

## Running the Tests

If unit testing is your thing just open Config.cs in the BalancedSharp.Test project and change the Marketplace Url and Api key. All tests are done in NUnit.

## Common Examples

### Adding Accounts

### Charging Credit Cards

## Api

BalancedSharp supports the following items from the Balanced Rest Api:

* Bank Accounts
* Bank Account Verifications
* Cards
* Accounts
* Credits
* Debits
* Holds
* Refunds
* Events

For more information on the Balanced Api go to the [Balanced Payments Rest Api](https://www.balancedpayments.com/docs/api)

## License

This software is licensed under the MIT License. View the LICENSE file for more information.
