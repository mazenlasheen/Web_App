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
       
        public static  bool registerUser(string username , string password , User.Role role , string firstName , string lastName )  
        {
            SqlConnection  conn = UtilityDAL.createConnection() ; 
             try {
                
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password) ;
                
                conn.Open() ; 
                // we do not check first for the user name cause we alreadyt set the column to be UNIQUE In the data base 
                SqlCommand insert = new SqlCommand("INSERT INTO [User] (userName , [password],role , firstName, lastName , accountCreationDate ) VALUES (@userName , @password, @role , @firstName , @lastName , @accountCreationDate ) ", conn) ;
                insert.Parameters.AddWithValue("@userName" , username ) ;
                insert.Parameters.AddWithValue("@password",  hashedPassword ) ;
                insert.Parameters.AddWithValue("@role", User.roleToString(role)) ;
                insert.Parameters.AddWithValue("@firstName", firstName ) ;
                insert.Parameters.AddWithValue("@lastName",  lastName ) ;
                insert.Parameters.AddWithValue("@accountCreationDate", DateTime.Now) ;
                
                insert.ExecuteNonQuery()  ; 
                conn.Close() ; 
                return true ; 
             } catch (SqlException e ) {return false ; } 
             finally {conn.Close() ;}
        }
        public static int LoginAuthentication(string userName, string password)
        {
             SqlConnection conn = UtilityDAL.createConnection() ; 
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
            SqlConnection  conn = UtilityDAL.createConnection() ; 
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
                User.Role role  = UtilityDAL.parseRole(UtilityDAL.returnString(reader,"role")) ;
                string firstName = UtilityDAL.returnString(reader,"firstName") ;
                string lastName =  UtilityDAL.returnString(reader,"lastName") ;
                DateTime datetime = UtilityDAL.returnDateTime(reader,"accountCreationDate");
                User user = new User(id , userName , password , role , firstName , lastName , datetime );
                return user ; 
            }
            catch (SqlException e ) {return null ; }
            finally {conn.Close() ;}
        }  
        public static List<User> getAllUsers(string userRole)  // this is an Admin only method 
        {
            if (userRole == null ) {userRole = "student" ;} // defualt role is student   
            List<User> specifiedUserList = new List<User>(); 
            SqlConnection conn = UtilityDAL.createConnection() ; 
            SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE role = @wantedRole", conn );
            cmd.Parameters.AddWithValue("@wantedRole", userRole  ) ;
         
            
            try 
            {
                conn.Open()  ; 
                // connection must be open before execution 
                SqlDataReader reader = cmd.ExecuteReader() ; 
            
                while (reader.Read() ) // an automatic check for True and False is like done  
                {
                    int id = UtilityDAL.returnInt(reader, "userId" ) ; 
                    string userName = UtilityDAL.returnString(reader,"userName") ;
                    string  password = UtilityDAL.returnString(reader,"password") ; 
                    User.Role readRole  = UtilityDAL.parseRole(UtilityDAL.returnString(reader,"role")) ;
                    string firstName = UtilityDAL.returnString(reader,"firstName") ;
                    string lastName =  UtilityDAL.returnString(reader,"lastName") ;
                    DateTime datetime = UtilityDAL.returnDateTime(reader,"accountCreationDate");
                    User user = new User(id , userName , password , readRole , firstName , lastName , datetime );
                    specifiedUserList.Add(user) ;
                }
                
                return specifiedUserList ; 
            }
            catch (SqlException e ) {return null ; }
            finally {conn.Close() ;}
        }  
        public static bool updateUserProfile(int userId, string firstName = "", string lastName = "")
        {
            if (firstName == "" && lastName == "") { return false; }
            
            SqlConnection conn = null; // tries to Error Hanlde the Case if the connection is not created 
            try
            {
                conn = UtilityDAL.createConnection();
                string query;
                SqlCommand cmd;

                if (lastName == "")
                    query = "UPDATE [User] SET firstName = @firstName WHERE userId = @userId";
                else if (firstName == "")
                    query = "UPDATE [User] SET lastName = @lastName WHERE userId = @userId";
                else
                    query = "UPDATE [User] SET firstName = @firstName, lastName = @lastName WHERE userId = @userId";

                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);

                if (firstName != "") cmd.Parameters.AddWithValue("@firstName", firstName);
                if (lastName != "") cmd.Parameters.AddWithValue("@lastName", lastName);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException e) { return false; }
            finally { if (conn != null) conn.Close(); }
        }
        public static bool UpdatePassword(int userId, string newPassword)
        {
            SqlConnection conn = null;
            try
            {
                conn = UtilityDAL.createConnection();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                SqlCommand cmd = new SqlCommand("UPDATE [User] SET [password] = @hashedPassword WHERE userId = @userId", conn);
                cmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch (SqlException e) { return false; }
            finally { if (conn != null) conn.Close(); }
        }
        public static bool deleteUser(int userId ) 
        {
            SqlConnection conn = null;
            try
            {
                conn = UtilityDAL.createConnection();
                SqlCommand cmd = new SqlCommand("DELETE  FROM [User] WHERE   userId = @userId", conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch (SqlException e) { return false; }
            finally { if (conn != null) conn.Close(); }
        }

    }
    
}