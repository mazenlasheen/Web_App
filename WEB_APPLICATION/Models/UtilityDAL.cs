using System ; 
using System.SqlClient ; 
public class UtilityDAL 
{
    public static String  returnString(SqlDataReader reader , string column_name ) 
    {
        // first we get the index of the column 

        int index_in_row =  reader.GetOrdinal(column_name ) ; 
        

        // then checking the value using the index value 
        string string_value = reader.IsDBNull(index_in_row) ? null : reader.GetString(index_in_row)  ;

        return string_value  ;

    }
}