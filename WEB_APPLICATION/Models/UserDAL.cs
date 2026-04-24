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
        public static int LoginAuthentication(string userName, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LearningPlatformDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT [password] FROM [User] WHERE userName = @userName", conn);
                cmd.Parameters.AddWithValue("@userName", userName);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read()) { return 2; } // username not found

                string passwordReturned = reader["password"].ToString();
                bool correctPassword = BCrypt.Net.BCrypt.Verify(password, passwordReturned);

                if (!correctPassword) { return 1; } // wrong password
                return 0; // success
            }
            catch (SqlException e)
            {
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        public static User getUserById ( int userId ) 
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LearningPlatformDataBase"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString );  
            try 
            {
                
                SqlCommand cmd = new SqlCommand ("SELECT * FROM [User] WHERE userId = @userId ",conn) ;
                cmd.Parameters.AddWithValue("@userId",userId) ; 
                conn.Open() ; 
                SqlDataReader reader = cmd.ExecuteReader() ; // basiically reader is used to access values 
                if (reader.Read() == false ) {return null ; }
                int id = UtilityDAL.returnInt(reader, "userId" ) ; 
                string userName = UtilityDAL.returnString(reader,"userName") ;
                string  password = UtilityDAL.returnString(reader,"password") ; 
                User.role role  = UtilityDAL.parseRole(UtilityDAL.returnString(reader,"role")) ;
                string firstName = UtilityDAL.returnString(reader,"firstName") ;
                string lastName =  UtilityDAL.returnString(reader,"lastName") ;
                DateTime datetime = UtilityDAL.returnDateTime(reader,"accountCreationDate");
                User user = new User(id , userName , password , role , firstName , lastName , datetime );
                return user ; 
            }
            catch (SqlException e ) {return null ; }
            finally {conn.Close() ;}
        }  
    }
}