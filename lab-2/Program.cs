// See https://aka.ms/new-console-template for more information

using Npgsql;

string Host = "localhost";
string User = "admin";
string DBname = "postgres";
string Password = "root";
string Port = "5432";

string connString =
                String.Format(
                    "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                    Host,
                    User,
                    DBname,
                    Port,
                    Password);


        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();
 
            string query = @"
                SELECT 
                    s.first_name || ' ' || s.last_name AS full_name,
                    sub.subject_name,
                    g.grade
                FROM 
                    students s
                JOIN 
                    grades g ON s.student_id = g.student_id
                JOIN 
                    subjects sub ON g.subject_id = sub.subject_id
                WHERE 
                    g.grade = (SELECT MIN(grade) FROM grades);
            ";
 
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetString(0)} - {reader.GetString(1)} - {reader.GetDecimal(2)}"); // Пример вывода данных
                    }
                }
            }     
        }