using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient ;
using BCrypt.Net; 

namespace WEB_APPLICATION.Models
{
    public class UserDAL // DAL basically stands for Data Access Layer 
    {   // THE METHOD BELOW RETURNS FALSE IF THE USERNAME ALREADY EXISTS 
       
        public static  bool registerUser(string username , string password , Role role , string firstName , string lastName )  
        {
            
             try {
                string conn_string = ConfigurationManager.ConnectionStrings["LearningPlatformDataBase"].ConnectionString;
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password) ;
                SqlConnection conn = new SqlConnection(conn_string) ; 
                conn.Open() ; 
                // we do not check first for the user name cause we alreadyt set the column to be UNIQUE In the data base 
                SqlCommand insert = new SqlCommand("INSERT INTO [User] (userName , [password],role , firstName, lastName , accountCreationDate ) VALUES (@userName , @password, @role , @firstName , @lastName , @accountCreationDate ) ", conn) ;
                insert.Parameters.AddWithValue("@userName" , username ) ;
                insert.Parameters.AddWithValue("@password",  hashedPassword ) ;
                insert.Parameters.AddWithValue("@role", User.role_toString(role)) ;
                insert.Parameters.AddWithValue("@firstName", firstName ) ;
                insert.Parameters.AddWithValue("@lastName",  lastName ) ;
                insert.Parameters.AddWithValue("@accountCreationDate", DateTime.Now) ;
                
                insert.ExecuteNonQuery()  ; 
                conn.Close() ; 
                return true ; 
             } catch (SqlException e ) {return false ; } 
        }
        public static int  loginAuthentication (string username , string password ) 
        {   //  Authentication part of the login
                    // creting the connection string 
            bool first_stage = false ; 
            bool second_stage = false ; 
            string connectionString = ConfigurationManager.ConnectionStrings["LearningPlatformDataBase"].ConnectionString ; 

            // Creating the connection and the sql command 
            SqlConnection conn = new SqlConnection(connectionString) ; 
             try 
            {         
                SqlCommand cmd = new SqlCommand("SELECT userName , [password]  FROM [User] WHERE userName = @userName" , conn ) ;
                conn.Open() ; 
                SqlDataReader reader = cmd.ExecuteReader() ;           
                string userReturned = " "; 
                string passwordReturned = "" ; 
                while (reader.Read()) 
            {
                userReturned = UtilityDAL.returnString(reader,"userName");
                passwordReturned = UtilityDAL.returnString(reader,"password");
                bool correctPassword  = BCrypt.Net.BCrypt.Verify(password , passwordReturned ) ;
                if ( userReturned !=  null  ) {first_stage = true ; } // meaning we found the userName 
                if (  passwordReturned != null   && ( correctPassword) ) {second_stage = true ;} // meaning the pass word is also correct 
            }
            
            // the 4 conditions that might occur 
            if ( (first_stage == true )  &&  (second_stage == true )  )  {return 0 ; }  // successful authentication 
            else if (first_stage == true && (second_stage == false )) {return 1 ;}  // correct username wrong passwprd
            else if (first_stage == false  ) {return 2 ;} // wrong username - not found 
            else {return -1 ; }
            } 
            catch (SqlException e ) 
                { 
                    return -1 ; 
                }
            finally 
            {
                conn.Close()  ; 
            } // to ensure that even if an issue occured the connection is closed 

        }
    }
}