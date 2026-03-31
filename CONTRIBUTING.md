# Contributing

Thanks for your interest in Frank.BedrockSlim.

## Getting started

1. Install the [.NET 10 SDK](https://dotnet.microsoft.com/download).
2. Clone the repository and run:

   ```bash
   dotnet build Frank.BedrockSlim.slnx
   dotnet test Frank.BedrockSlim.slnx
   ```

3. Samples live under `Samples/`; see `Samples/run-samples.ps1` for a quick local smoke test.

## Pull requests

- Prefer focused changes with clear intent.
- Run `dotnet build` and `dotnet test` on `Frank.BedrockSlim.slnx` before submitting.
- If you change public API or behavior, update `README.md` where it affects users.

## Security

If you believe you have found a security vulnerability, please report it responsibly (private channel to the maintainer or GitHub Security Advisories for this repository) rather than opening a public issue with exploit details.
