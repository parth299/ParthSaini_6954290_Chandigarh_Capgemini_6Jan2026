using System;
using System.Data;
using Microsoft.Data.SqlClient;

class Program {
    public static string connectionString =
            "Server=localhost,1433;" +
            "Database=UniversityDB;" +
            "User Id=sa;" +
            "Password=YourStrongPassw0rd@123;" +
            "TrustServerCertificate=True;";

    public static void InsertStudent()
    {
        using SqlConnection con =
            new SqlConnection(connectionString);

        SqlCommand cmd =
            new SqlCommand("sp_InsertStudent", con);

        cmd.CommandType = CommandType.StoredProcedure;

        Console.Write("First Name: ");
        cmd.Parameters.AddWithValue("@FirstName",
            Console.ReadLine());

        Console.Write("Last Name: ");
        cmd.Parameters.AddWithValue("@LastName",
            Console.ReadLine());

        Console.Write("Email: ");
        cmd.Parameters.AddWithValue("@Email",
            Console.ReadLine());

        Console.Write("DeptId: ");
        cmd.Parameters.AddWithValue("@DeptId",
            Convert.ToInt32(Console.ReadLine()));

        con.Open();
        cmd.ExecuteNonQuery();

        Console.WriteLine("Student Inserted!");
    }

    public static void GetStudents()
    {
        using SqlConnection con =
            new SqlConnection(connectionString);

        SqlCommand cmd =
            new SqlCommand("sp_GetAllStudents", con);

        cmd.CommandType =
            CommandType.StoredProcedure;

        con.Open();

        SqlDataReader reader =
            cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(
            $"{reader["StudentId"]} " +
            $"{reader["FirstName"]} " +
            $"{reader["LastName"]} " +
            $"{reader["DeptName"]}");
        }
        Console.WriteLine("Executed");
    }

    public static void UpdateStudent()
    {
        using SqlConnection con =
            new SqlConnection(connectionString);

        SqlCommand cmd =
            new SqlCommand("sp_UpdateStudent", con);

        cmd.CommandType =
            CommandType.StoredProcedure;

        Console.Write("StudentId: ");
        cmd.Parameters.AddWithValue("@StudentId",
            Convert.ToInt32(Console.ReadLine()));

        Console.Write("First Name: ");
        cmd.Parameters.AddWithValue("@FirstName",
            Console.ReadLine());

        Console.Write("Last Name: ");
        cmd.Parameters.AddWithValue("@LastName",
            Console.ReadLine());

        Console.Write("Email: ");
        cmd.Parameters.AddWithValue("@Email",
            Console.ReadLine());

        Console.Write("DeptId: ");
        cmd.Parameters.AddWithValue("@DeptId",
            Convert.ToInt32(Console.ReadLine()));

        con.Open();
        cmd.ExecuteNonQuery();

        Console.WriteLine("Updated Successfully!");
    }

    public static void DeleteStudent()
    {
        using SqlConnection con =
            new SqlConnection(connectionString);

        SqlCommand cmd =
            new SqlCommand("sp_DeleteStudent", con);

        cmd.CommandType =
            CommandType.StoredProcedure;

        Console.Write("StudentId: ");
        cmd.Parameters.AddWithValue("@StudentId",
            Convert.ToInt32(Console.ReadLine()));

        con.Open();
        cmd.ExecuteNonQuery();

        Console.WriteLine("Deleted Successfully!");
    }

    public static void Main(string[] args) {

        while (true)
        {
            Console.WriteLine("\n---- STUDENT MENU ----");
            Console.WriteLine("1. Insert Student");
            Console.WriteLine("2. View Students");
            Console.WriteLine("3. Update Student");
            Console.WriteLine("4. Delete Student");
            Console.WriteLine("5. Exit");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1: InsertStudent(); break;
                case 2: GetStudents(); break;
                case 3: UpdateStudent(); break;
                case 4: DeleteStudent(); break;
                case 5: return;
            }
        }
    }

}