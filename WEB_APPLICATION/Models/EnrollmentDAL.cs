using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Management;

namespace WEB_APPLICATION.Models
{
    public class EnrollmentDAL
    {
        private SqlConnection conn = UtilityDAL.createConnection() ; 

        // the method below takes a userId and a courseID and sets his enrollment status to false - soft delete - record still exists 
        public bool unEnroll(int userId , int courseId  ) // takes a user ID and removes them from the course they are enrolled too 
        {
            bool success = false ; 
            try { 
            conn.Open() ; 
            using (SqlCommand cmd = new SqlCommand("UPDATE  Enrollment SET activeStatus = 0  WHERE userId = @userId AND courseId = @courseId",conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId ) ; 
                cmd.Parameters.AddWithValue("@courseId", courseId  ) ; 
                if ( cmd.ExecuteNonQuery() > 0  )
                {
                    success = true ; 

                }  
                else 
                { 
                
                    success = false ;    
                }
               
            }
            } 
            catch (SqlException e ) 
            {
                
            }
            finally
            {
                conn.Close() ;     
            }
             return success ;  
        }
         
         // this method takes an enrollmentID and updates the compeltetion rate 
        public bool updateCompletionRate  (int enrollmentId, int rate )
        {
            bool success = false ; 
            try 
            { 
                conn.Open() ; 
                using (SqlCommand cmd = new SqlCommand("UPDATE  Enrollment SET completionRate = @rate  WHERE enrollmentId = @enrollmentId",conn))
                {
                    cmd.Parameters.AddWithValue("@enrollmentId", enrollmentId ) ; 
                    cmd.Parameters.AddWithValue("@rate", rate  ) ; 
                    if ( cmd.ExecuteNonQuery() > 0 )
                        {
                            success = true ; 
                        }

                }
            }
            catch (SqlException e ) 
            {
                        
            }
            finally
            {
                conn.Close() ;     
            }
            return success ; 
        }

    
    
    
    
    
    
    
    
    
    
    }
}