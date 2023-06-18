# azure-functions-sample

## Run locally

Create an Azure Service Bus instance in Azure.

Fill a connectionstring value for "AzureWebServiceBus" in local.settings.json

Run the solution

## Test

Run a POST method against the 'SubmitPing' endpoint.
For example using powershell
```powershell
$body = @{ Id = New-Guid }
$uri = 'http://localhost:7071/api/SubmitPing'

Invoke-WebRequest -Uri $url - Method 'Post' -ContentType 'application/json' -Body $body | Write-Host
```