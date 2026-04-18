using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient ; 

namespace WEB_APPLICATION.Models
{
    public class UserDAL // DAL basically stands for Data Access Layer 
    {  
         
        // THE METHOD BELOW RETURNS FALSE IF THE USERNAME ALREADY EXISTS 
        public static  bool registerUser(string username , string password , Role role , string firstName , string lastName )  
        {
            
            
             try {
                string conn_string = ConfigurationManager.ConnectionStrings["LearningPlatformDB"].ConnectionString;
                
                SqlConnection conn = new SqlConnection(conn_string) ; 
                conn.Open() ; 
                // we do not check first for the user name cause we alreadyt set the column to be UNIQUE In the data base 
                SqlCommand insert = new SqlCommand("INSERT INTO [User] (userName , [password],role , firstName, lastName , accountCreationDate ) VALUES (@userName , @password, @role , @firstName , @lastName , @accountCreationDate ) ", conn) ;
                insert.Parameters.AddWithValue("@userName" , username ) ;
                insert.Parameters.AddWithValue("@password",  password) ;
                insert.Parameters.AddWithValue("@role", User.role_toString(role)) ;
                insert.Parameters.AddWithValue("@firstName", firstName ) ;
                insert.Parameters.AddWithValue("@lastName",  lastName ) ;
                insert.Parameters.AddWithValue("@accountCreationDate", DateTime.Now) ;
                
                insert.ExecuteNonQuery()  ; 
                conn.Close() ; 
                return true ; 
             } catch (SqlException e ) {return false ; } 


        }

    
}

}