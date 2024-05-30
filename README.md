# SafeNet
A security programme for the blocking of ports that are used for the remote control of computers on the local network.
## Info
- Uses [WinDivert](https://github.com/basil00/Divert) .NET binding named [WinDivertSharp](https://github.com/BeyondDimension/WinDivertSharp).
- When running, blocks ports ```135, 137, 138, 139, 445``` for RPC Service, NetBIOS and TCP/IP SMB.
- Informs you of the number of packets being sent on these ports.
## Bulid
- The WinDivert binaries are located in ```SafeNet/windivert_binaries/```, you need to copy them to the location of the executable.