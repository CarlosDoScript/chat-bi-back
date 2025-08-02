﻿namespace Chat.Bi.Core.Constantes.ChatConfig;

public static class ChatConfigCanais
{
    public const string WEB = "WEB";
    public const string WHATSAPP = "WHATSAPP";
    public const string TELEGRAM = "TELEGRAM";    

    public static readonly HashSet<string> Todos = new HashSet<string>
    {
        WEB,
        WHATSAPP,
        TELEGRAM
    };
}
