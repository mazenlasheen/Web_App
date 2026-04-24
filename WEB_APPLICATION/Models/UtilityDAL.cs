using System ; 
using System.Data.SqlClient ; 
using System.Configuration ; 
namespace  WEB_APPLICATION.Models {
public class UtilityDAL 
{
    public static String  returnString(SqlDataReader reader , string column_name ) 
    {
        // first we get the index of the column 
        int index_in_row =  reader.GetOrdinal(column_name ) ;  
        // then checking the value using the index value 
        string string_value = reader.IsDBNull(index_in_row) ? null : reader.GetString(index_in_row)  ;
        // the statement above basically says chekc if iti is null if so return null if not  return a string 

        return string_value  ;
    }
    public static int  returnInt  ( SqlDataReader reader , string columnName ) 
    {
        // getting the index of the column 
        int index_row = reader.GetOrdinal(columnName) ; 
        int value = reader.IsDBNull(index_row ) ? 0 : reader.GetInt32(index_row) ;
        return value ;
        // 0 is returned for null values 
    }
    public static DateTime returnDateTime (SqlDataReader reader , string columnName ) 
    {
        // index of the column 
        int index_r = reader.GetOrdinal(columnName) ;
        // assigning value 
        DateTime  value = reader.IsDBNull(index_r) ? DateTime.MinValue : reader.GetDateTime(index_r) ;
        return value ; 
    }
    public static User.Role ParseRole(string role)
    {
        if (role == "Student") return User.Role.Student;
        if (role == "Instructor") return User.Role.Instructor;
        if (role == "Admin") return Role.Admin;
        return User.Role.Student; // default fallback
    }
    public static SqlConnection createConnection () 
    {
        string connectionString = ConfigurationManager.ConnectionStrings["LearningPlatformDataBase"].ConnectionString ; 
        SqlConnection conn = new SqlConnection(connectionString )  ;
        return conn ;  
    }

}
}