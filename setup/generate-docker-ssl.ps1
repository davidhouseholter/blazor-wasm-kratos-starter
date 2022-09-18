#create a SAN cert for both host.docker.internal and localhost
$cert = New-SelfSignedCertificate -DnsName "host.docker.internal", "localhost" -CertStoreLocation cert:\localmachine\my 
#export it for docker container to pick up later
$password = ConvertTo-SecureString -String "1234" -Force -AsPlainText
Export-PfxCertificate -Cert $cert -FilePath C:\Users\user\AppData\Roaming\ASP.NET\Https\Docker.KratosBlazorApp.Server.pfx -Password $password

# trust it on your host machine
$store = New-Object System.Security.Cryptography.X509Certificates.X509Store "TrustedPublisher","LocalMachine"
$store.Open("ReadWrite")
$store.Add($cert)
$store.Close()



$certPass = "password_here"
$certSubj = "host.docker.internal"
$certAltNames = "DNS:localhost,DNS:host.docker.internal,DNS:identity_server" # i believe you can also add individual IP addresses here like so: IP:127.0.0.1
$opensslPath="path\to\openssl\binaries" #assuming you can download OpenSSL, I believe no installation is necessary
$workDir="path\to\your\project" # i assume this will be your solution root
$dockerDir=Join-Path $workDir "ProjectApi" #you probably want to check if my assumptions about your folder structure are correct

#generate a self-signed cert with multiple domains
Start-Process -NoNewWindow -Wait -FilePath (Join-Path $opensslPath "openssl.exe") -ArgumentList "req -x509 -nodes -days 365 -newkey rsa:2048 -keyout ",
                                          (Join-Path $workDir aspnetapp.key),
                                          "-out", (Join-Path $dockerDir aspnetapp.crt),
                                          "-subj `"/CN=$certSubj`" -addext `"subjectAltName=$certAltNames`""

# this time round we convert PEM format into PKCS#12 (aka PFX) so .net core app picks it up
Start-Process -NoNewWindow -Wait -FilePath (Join-Path $opensslPath "openssl.exe") -ArgumentList "pkcs12 -export -in ", 
                                           (Join-Path $dockerDir aspnetapp.crt),
                                           "-inkey ", (Join-Path $workDir aspnetapp.key),
                                           "-out ", (Join-Path $workDir aspnetapp.pfx),
                                           "-passout pass:$certPass"

$password = ConvertTo-SecureString -String $certPass -Force -AsPlainText
$cert = Get-PfxCertificate -FilePath (Join-Path $workDir "aspnetapp.pfx") -Password $password

# and still, trust it on your host machine
$store = New-Object System.Security.Cryptography.X509Certificates.X509Store [System.Security.Cryptography.X509Certificates.StoreName]::Root,"LocalMachine"
$store.Open("ReadWrite")
$store.Add($cert)
$store.Close()