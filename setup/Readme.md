
dotnet dev-certs https -ep ./localhost.pfx -p "1234"

Run .\generate-docker-ssl.ps1 (Admin)

install C:\Users\user\AppData\Roaming\ASP.NET\Https\Docker.KratosBlazorApp.Server.pfx into Trusted Root Certification

Extracts the private key form a PFX to a PEM file:
openssl pkcs12 -in Docker.KratosBlazorApp.Server.pfx -nocerts -out Docker.KratosBlazorApp.Server.key.pem

Exports the certificate (includes the public key only):
openssl pkcs12 -in Docker.KratosBlazorApp.Server.pfx -clcerts -nokeys -out Docker.KratosBlazorApp.Server.pem

Removes the password (paraphrase) from the extracted private key (optional):
openssl rsa -in Docker.KratosBlazorApp.Server.key.pem -out Docker.KratosBlazorApp.Server.key

SERVE_PUBLIC_TLS_CERT_BASE64 = 
base64 -i Docker.KratosBlazorApp.Server.pem

SERVE_PUBLIC_TLS_KEY_BASE64 = 
base64 -i Docker.KratosBlazorApp.Server.key