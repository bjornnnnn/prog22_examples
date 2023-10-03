using System.Security.Cryptography;


SymmetricEncryption();
await SymmetricDecryption();

static void SymmetricEncryption()
{
    Console.WriteLine("***Symmetric Encryption***\n");

    try
    {
        using (FileStream fileStream = new("TestData.txt", FileMode.OpenOrCreate))
        {
            using (Aes aes = Aes.Create())
            {
                byte[] key =
                {
                            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                };

                aes.Key = key;

                // Set initialization vector
                byte[] iv = aes.IV;

                fileStream.Write(iv, 0, iv.Length);

                using (var cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (var encryptWriter = new StreamWriter(cryptoStream))
                    {
                        encryptWriter.Write("Super hemligt meddelande!");
                    }
                }
            }
        }
        Console.WriteLine("The file was encrypted.");
    }
    catch (Exception)
    {

        throw;
    }
}
static async Task SymmetricDecryption()
{
    Console.WriteLine("\n\n**Symmetric Decryption**\n");

    try
    {
        using (FileStream fileStream = new("TestData.txt", FileMode.Open))
        {
            using (Aes aes = Aes.Create())
            {
                byte[] iv = new byte[aes.IV.Length];
                int numBytesToRead = aes.IV.Length;
                int numBytesRead = 0;
                while (numBytesToRead > 0)
                {
                    int n = fileStream.Read(iv, numBytesRead, numBytesToRead);

                    if (n == 0) break;

                    numBytesRead += n;
                    numBytesToRead -= n;
                }

                byte[] key =
                {
                            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                };


                using (var cryptoStream = new CryptoStream(fileStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    using (var decryptReader = new StreamReader(cryptoStream))
                    {
                        var decryptedMessage = await decryptReader.ReadToEndAsync();
                        await Console.Out.WriteLineAsync($"Decrypted original message: \n {decryptedMessage}");
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        await Console.Out.WriteLineAsync($"Decryption failed: {ex}");
    }
}