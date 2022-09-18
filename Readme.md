<h1 >A starter for Blazor WebAssembly .NET 6 and the Ory Kratos Identity Server</h1>

This project aims to be a simple example of using Ory Kratos as the Identity Server for a Blazor WebAssembly Project.

## Features

- [x] Blazor Web-Assembly: ASP.NET Core Hosted
- [x] Entity Framework 
- [x] Authentication Middleware
- [x] Docker
- [x] All account flows:
	- [x] Registration
	- [x] Login
	- [ ] Account verification
	- [ ] Password reset
	- [ ] Email change
	- [ ] Account deletion by user
- [ ] Social login/OpenID Connect (example is GitHub login)
- [ ] External API authorization example with [Ory Oathkeeper](https://github.com/ory/oathkeeper)
- [ ] RBAC example with [Ory Keto](https://github.com/ory/keto)
- [ ] Custom email template example
- [ ] Two/Multi-Factor authentication ([TOTP](https://en.wikipedia.org/wiki/Time-based_One-Time_Password)  & security codes)
- [ ] [WebAuthn](https://en.wikipedia.org/wiki/WebAuthn)

# Template

### Single Page Application

This template was built as a single page template, the user is redirected to the login page unless they are authenticated with the server via OryKratos Middleware.

# Usage

ToDo

# Notes
- due to the SetCookieHandler Http Delegate Handler in Web-Assembly, Cors is enfoces beacause * is not allowed.
- Blazor Web-Assembly does not work with Ory Kratos C# SDK (RestSharp)
