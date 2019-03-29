This is a simple console app for converting bitcoin's P2PKH addresses to P2SH-P2WPKH created as per request [here](https://bitcointalk.org/index.php?topic=5125843.0).  
Just enter your address starting with `1` and the app will automatically convert it to a SegWit address starting with `3`.  
Run by calling `dotnet AddrConverter.dll`
![ScreenShot](https://i.imgur.com/70Pq03L.jpg)

Download the correct version of .Netcore based on your OS from here: https://dotnet.microsoft.com/download

Disclaimer:  
The RIPEMD160 functions are copied from .Net framework's source code since .Net core no longer has these functions. Please refer to .Net framework's source code for the license.
