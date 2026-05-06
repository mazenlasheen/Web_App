using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient ; 
using System.Web;

namespace WEB_APPLICATION.Models
{
    public class CourseDAL
    {
        private SqlConnection conn = UtilityDAL.createConnection() ; 
        

        // the method below takes a course ID and deletes the course entry form the data base - through a soft delete not a hard delete 
        public bool deleteCourse(int courseId )
        {
            bool success = false;
            try
            {
                conn.Open();
                using (
                SqlCommand cmd = new SqlCommand(
                "UPDATE Course SET activeStatus= 0  WHERE  courseId = @courseId", conn)) {
                cmd.Parameters.AddWithValue("@courseId", courseId);
                if (cmd.ExecuteNonQuery() > 0)
                    success = true;
                }
            }
            catch (SqlException e) { Console.WriteLine(e.Message); }
            finally { conn.Close(); }
            return success;
        } 
        

        // the method below takes courseName and description and updates these fields using the courseID 
        public bool updateCourse(int courseId, String courseName ,  String courseDescription  )
        {
            bool success = false;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Course SET courseName = @courseName , courseDescription = @courseDescription  WHERE courseId = @courseId ", conn);
                cmd.Parameters.AddWithValue("@courseId", courseId );
                cmd.Parameters.AddWithValue("courseName", courseName );
                cmd.Parameters.AddWithValue("courseDescription", courseDescription );
                if (cmd.ExecuteNonQuery() > 0) 
                    success = true;
            }
            catch (SqlException e) { Console.WriteLine(e.Message); }
            finally { conn.Close(); }
            return success;
        }














    }
}