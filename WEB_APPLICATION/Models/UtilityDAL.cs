using System ; 
using System.SqlClient ; 
using System.DateTime ; 
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
    public static Role ParseRole(string role)
    {
        if (role == "Student") return Role.Student;
        if (role == "Instructor") return Role.Instructor;
        if (role == "Admin") return Role.Admin;
        return Role.Student; // default fallback
    }

}