# IPN Integration examples


This is an examples of Lipisha IPN integration for ASP.NET MVC.

This assumes that you have configured an IPN URL for API callbacks.

IPN: Instant Payment Notification

## Usage

The bulk of the logic happens in the IPN controller.

    https://github.com/lipisha/lipisha-dotnet-sdk/tree/master/LipishaIPNMVC/LipishaIPNMVC/Controllers/HomeController.cs

For production usage, handling IPN callbacks should be mapped to records in permanent storage.

Adjust the controller settings to load your *API_KEY* and *API_SIGNATURE*.

```csharp
    ...
	public class HomeController : Controller
	{
		private const string API_KEY = "<YOUR API KEY>";
		private const string API_SIGNATURE = "<YOUR API SIGNATURE>";
		private const string API_VERSION = "1.3.0";
   ...
```


## Running

The example can be run by loading the Visual Studio solution in the *LipishaIPNMVC* directory.
This can also be run using *monodevelop*

Test HTTP requests may then be POSTED test parameters to

    http://localhost:8080/
