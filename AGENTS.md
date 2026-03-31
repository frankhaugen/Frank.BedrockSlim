# Agent notes — Frank.BedrockSlim

## Purpose

Lightweight TCP client/server helpers built on **Kestrel** (server) and `System.Net.Sockets.TcpClient` (client), plus optional **AES** helpers in `Frank.BedrockSlim.Cryptography`.

## Build and test

- **SDK**: .NET 10 (`global.json` requires 10.0.100+; `rollForward` allows newer feature bands on the same major).
- **Solution**: `Frank.BedrockSlim.slnx` (preferred). Build with `dotnet build Frank.BedrockSlim.slnx`, test with `dotnet test Frank.BedrockSlim.slnx`.
- **Samples**: under `Samples/`; `Samples/run-samples.ps1` starts server and client samples.

## Layout

| Project | Role |
|--------|------|
| `Frank.BedrockSlim.Server` | ASP.NET Core / Kestrel TCP hosting extensions |
| `Frank.BedrockSlim.Client` | `ITcpClient` / DI registration |
| `Frank.BedrockSlim.Cryptography` | AES encrypt/decrypt + DI |
| `Frank.BedrockSlim.Tests` | Unit tests |

## Conventions

- **Target framework**: `net10.0` from root `Directory.Build.props`; samples use `Samples/Directory.Build.props`.
- **Nullable** and **implicit usings** are enabled repo-wide.
- **InternalsVisibleTo** includes `$(AssemblyName).Tests` and `LINQPadQuery` (see `Directory.Build.props`).

## Security expectations

- **Transport**: APIs are **plain TCP**, not TLS. Callers who need confidentiality or integrity on the wire must add TLS (or another layer) themselves.
- **Cryptography**: `AdvancedEncryptionOptions` ships **sample defaults**; production code must supply cryptographically random keys/IVs and secure storage. `AdvancedEncryptionFactory` creates a **new** `Aes` instance per operation; do not reintroduce caching of `Aes` instances that callers dispose.
- **Dependencies**: `NuGetAuditMode` is `all` at moderate level in `Directory.Build.props`; run `dotnet list package --vulnerable` when changing packages.

## CI

Workflows in `.github/workflows/` call reusable workflows from `frankhaugen/Workflows`; keep SDK alignment with `global.json` when adjusting those pipelines.
