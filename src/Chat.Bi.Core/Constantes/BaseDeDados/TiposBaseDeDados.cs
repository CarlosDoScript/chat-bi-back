namespace Chat.Bi.Core.Constantes.BaseDeDados;

public static class TiposBaseDeDados
{
    public const string SqlServer = "SqlServer";
    public const string MySql = "MySql"; 
    public const string Postgre = "Postgre";

    public static readonly HashSet<string> Todos = new HashSet<string>
    {
        SqlServer,
        MySql,
        Postgre
    };
}