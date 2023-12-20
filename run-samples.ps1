# Run the server sample then the client sample

# Set the working directory to the script directory
$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptPath

# Run the server sample in new window
Start-Process -FilePath dotnet -ArgumentList "run --project .\Frank.BedrockSlim.Server.Sample\Frank.BedrockSlim.Server.Sample.csproj" -WindowStyle Normal

# Wait for the server to start and port to be ready
Start-Sleep -Seconds 5

# Run the client sample
Start-Process -FilePath dotnet -ArgumentList "run --project .\Frank.BedrockSlim.Client.Sample\Frank.BedrockSlim.Client.Sample.csproj" -WindowStyle Normal

# Wait for the client to finish
Start-Sleep -Seconds 15

# Kill the client then the server
Stop-Process -Name Frank.BedrockSlim.Client.Sample
Stop-Process -Name Frank.BedrockSlim.Server.Sample