using System;
using System.IO;

public static class DatabaseConfig
{
    public static string connectionString { get; private set; }

    static DatabaseConfig()
    {
        LoadSqlConfiguration();
    }

    private static void LoadSqlConfiguration()
    {
        string configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MyApp", "config.txt");


        //  connectionString = $"Data Source=Selim-pc;Initial Catalog=ReservationsDB;Integrated Security=True;Encrypt=False";

        connectionString = $"Data Source=Dell;Initial Catalog=ReservationsDB;Integrated Security=True;Encrypt=False";
       // connectionString = $"Data Source=192.168.50.5;Initial Catalog=ReservationsDB;User Id=sa;Password=comsys@123;Encrypt=False";
           
        }
       
    }

