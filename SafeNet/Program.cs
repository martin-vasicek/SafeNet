using SafeNet;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WinDivertSharp;

namespace SafeNet
{
    public class SafeNet
    {
        public static int nStopped = 0;

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("   ____         ___        _  __       __         ____                         _   __        \r\n  / __/ ___ _  / _/ ___   / |/ / ___  / /_       / __/ ___  ____ __ __  ____  (_) / /_  __ __\r\n _\\ \\  / _ `/ / _/ / -_) /    / / -_)/ __/      _\\ \\  / -_)/ __// // / / __/ / / / __/ / // /\r\n/___/  \\_,_/ /_/   \\__/ /_/|_/  \\__/ \\__/      /___/  \\__/ \\__/ \\_,_/ /_/   /_/  \\__/  \\_, / \r\n                                                                                      /___/  v1.0");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Status: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("PROTECTED");
            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Welcome in");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" SafeNet ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("security program!");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Your PC is protected from any type of local network remote control until you close this program.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;

            string filter = "(inbound && tcp.DstPort == 135) || (inbound && tcp.DstPort == 137) || (inbound && tcp.DstPort == 138) || (inbound && tcp.DstPort == 139) || (inbound && tcp.DstPort == 445)";

            IntPtr handle = WinDivert.WinDivertOpen(filter, WinDivertLayer.Network, 0, WinDivertOpenFlags.None);

            if (handle == IntPtr.Zero)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("ERROR - can't activate WinDivert.");
                return;
            }

            try
            {
                WinDivertBuffer packet = new WinDivertBuffer();
                WinDivertAddress addr = new WinDivertAddress();
                uint recvLen = 0;

                while (true)
                {
                    if (!WinDivert.WinDivertRecv(handle, packet, ref addr, ref recvLen))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("ERROR - can't catch packet.");
                        continue;
                    }             

                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    ClearCurrentConsoleLine();

                    nStopped++;
                    Console.WriteLine("Number of blocked connections: " + nStopped);
                }
            }
            finally
            {
                WinDivert.WinDivertClose(handle);
            }
        }
    }
}