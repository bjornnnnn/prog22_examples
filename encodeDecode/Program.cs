using System.Text;

string name = "Björn Næslund";
Console.WriteLine("1. Plain text name is: {0} ",  name );

Console.WriteLine("Now ENCODING string to Byte Array using codepage UTF8.");
byte[] msg = System.Text.Encoding.UTF8.GetBytes(name);
Console.WriteLine("2. Byte encoded name is: ");
Console.WriteLine("{{{0}}}", string.Join(", ",  msg));


Console.WriteLine("----------------------------------------------------------");


Console.WriteLine("Now DECODING byte array to string using codepage UTF8.");
string nameDecoded = System.Text.Encoding.UTF8.GetString(msg);
Console.WriteLine("3. Encoded-Decoded string is:" + nameDecoded);


