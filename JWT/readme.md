# JWT

Skapa en JWT-token för din användare genom att använda det inbygda kommandot "dotnet user-jwts create":

    $ dotnet user-jwts create 

    New JWT saved with ID '2e3e9370'.
    Name: bjornn
    
    Token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImJqb3JubiIsInN1YiI6ImJqb3JubiIsImp0aSI6IjJlM2U5MzcwIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NDAxNDQiLCJodHRwczovL2xvY2FsaG9zdDo0NDM2NiIsImh0dHA6Ly9sb2NhbGhvc3Q6NTAzOCIsImh0dHBzOi8vbG9jYWxob3N0OjcyNjIiXSwibmJmIjoxNjk2MTcwNjM0LCJleHAiOjE3MDQxMTk0MzQsImlhdCI6MTY5NjE3MDYzNSwiaXNzIjoiZG90bmV0LXVzZXItand0cyJ9.SapbJ7OhyRl-ZA1kHIs9dgjsnxt_isSsbhJoIIP8r9o

Lista information om den skapade JWT-token såhär:

    $ dotnet user-jwts print 2e3e9370 --show-all

    Found JWT with ID '2e3e9370'.
    ID: 2e3e9370
    Name: bjornn
    Scheme: Bearer
    Audience(s): http://localhost:40144, https://localhost:44366, http://localhost:5038, https://localhost:7262
    Not Before: 2023-10-01T14:30:34.0000000+00:00
    Expires On: 2024-01-01T14:30:34.0000000+00:00
    Issued On: 2023-10-01T14:30:35.0000000+00:00
    Scopes: none
    Roles: [none]
    Custom Claims: [none]
    Token Header: {"alg":"HS256","typ":"JWT"}
    Token Payload: {"unique_name":"bjornn","sub":"bjornn","jti":"2e3e9370","aud":["http://localhost:40144","https://localhost:44366","http://localhost:5038","https://localhost:7262"],"nbf":1696170634,"exp":1704119434,"iat":1696170635,"iss":"dotnet-user-jwts"}
    Compact Token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImJqb3JubiIsInN1YiI6ImJqb3JubiIsImp0aSI6IjJlM2U5MzcwIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NDAxNDQiLCJodHRwczovL2xvY2FsaG9zdDo0NDM2NiIsImh0dHA6Ly9sb2NhbGhvc3Q6NTAzOCIsImh0dHBzOi8vbG9jYWxob3N0OjcyNjIiXSwibmJmIjoxNjk2MTcwNjM0LCJleHAiOjE3MDQxMTk0MzQsImlhdCI6MTY5NjE3MDYzNSwiaXNzIjoiZG90bmV0LXVzZXItand0cyJ9.SapbJ7OhyRl-ZA1kHIs9dgjsnxt_isSsbhJoIIP8r9o

## I koden:
Kontrollera om följande instruktion redan finns. Annars lägg till den i middleware-klassen: 

    app.UseHttpsRedirection();
 
## För att köra:

David kör Visual Studio och för honom funkar HTTPS i denna kod automagiskt.

Om man som Björn använder Visual Studio *Code* måste man välja profil som koden startas med själv. Profilerna finns i filen properties/launchSettings.json. Kör följande kommando för att starta koden med HTTPS-kommunikation enabled:

    $ dotnet run --launch-profile https

För att göra anrop mot en http-endpoint som kräver JWT-autentisering gör man ett anrop med JWT-token skickad med i en HTTP-Header som heter Authorization.

Använd token genom att skicka den i en http-header med ett https-get-anrop exempelvis såhär:

    curl -i -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImJqb3JubiIsInN1YiI6ImJqb3JubiIsImp0aSI6IjJlM2U5MzcwIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NDAxNDQiLCJodHRwczovL2xvY2FsaG9zdDo0NDM2NiIsImh0dHA6Ly9sb2NhbGhvc3Q6NTAzOCIsImh0dHBzOi8vbG9jYWxob3N0OjcyNjIiXSwibmJmIjoxNjk2MTcwNjM0LCJleHAiOjE3MDQxMTk0MzQsImlhdCI6MTY5NjE3MDYzNSwiaXNzIjoiZG90bmV0LXVzZXItand0cyJ9.SapbJ7OhyRl-ZA1kHIs9dgjsnxt_isSsbhJoIIP8r9o" https://localhost:7262/secret