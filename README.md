# Certificates

To generate a public key certificate for HTTPS requests to the servers, run the following commands in the root directory of the repository.

```
dotnet dev-certs https -ep conf.d\https\dev_cert.pfx -p { password here }
dotnet dev-certs https --trust
```

[Refer here for more info](https://docs.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-6.0)

# Running the Application

Run ```docker-compose up``` in the root directory of the repository.

# Requests

## Identity Server

### Registering an account
```
curl --request POST \
  --url https://localhost:7054/api/auth/register/username={username}&password={password}
```

### Logging in
```
curl --request GET \
  --url https://localhost:7054/api/auth/login/username=testAccount1%26password=ASFDkwEjiAWDMIW928@
```

## Account Server

### Adding Funds
```
curl --request POST \
  --url https://localhost:7050/api/Account/AddFunds/username={username}&amount={amount} \
  --header 'Authorization: Bearer {token}'
```

### Subtracting Funds
```
curl --request POST \
  --url https://localhost:7050/api/Account/SubtractFunds/username={username}&amount={amount} \
  --header 'Authorization: Bearer {token}'
```

## Store Server

### Get Products
```
curl --request GET \
  --url https://localhost:7160/api/store \
  --header 'Authorization: Bearer {token}'
```

### Buy Product
```
curl --request POST \
  --url https://localhost:7160/api/store/buy/{product id} \
  --header 'Authorization: Bearer {token}'
```
