
class MyWebsocketClass
{

using HttpClientHandler handler = new()
{
    CookieContainer = cookies;
UseCookies = cookies != null;
ServerCertificateCustomValidationCallback = remoteCertificateValidationCallback;
Credentials = useDefaultCredentials
    ? CredentialCache.DefaultCredentials
    : credentials;
};
if (proxy is null)
{
    handler.UseProxy = false;
}
else
{
    handler.Proxy = proxy;
}
if (clientCertificates?.Count > 0)
{
    handler.ClientCertificates.AddRange(clientCertificates);
}
HttpMessageInvoker invoker = new(handler);
using ClientWebSocket cws = new();
await cws.ConnectAsync(uri, invoker, cancellationToken);

}