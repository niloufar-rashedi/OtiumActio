using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Data.SqlClient;
using System.Data;
using OtiumActio.Models;

namespace OtiumActio
{
    public class Repository<T> : IRepository<T> where T : class
    {


        private readonly OtiumActioContext _context;
        private readonly DbSet<T> _table;
        public Repository(OtiumActioContext otiumActioContext)
        {
            _context = otiumActioContext;
            _table = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _table.Add(entity);
            Save();
        }

        public void Delete(object id)
        {
            T existing = _table.Find(id);
            _table.Remove(existing);
            Save();

        }

        public void Update(T entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            Save();

        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        T IRepository<T>.GetById(object id)
        {
            return _table.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public string AddNewActivity(Activity activity)
        {
                string connectionString = GetSrting();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("AddActivityUpdateActCat", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@id", activity.Id);
                        cmd.Parameters.AddWithValue("@category", activity.AcCategoryId);
                        cmd.Parameters.AddWithValue("@description", activity.AcDescription);
                        cmd.Parameters.AddWithValue("@participants", activity.AcParticipants);
                        cmd.Parameters.AddWithValue("@date", activity.AcDate);
                        //AddActivityCategory(activity);
                        cmd.ExecuteNonQuery();

                        return null; // success   
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message); // return error message  
                    }
                    finally
                    {
                        con.Close();
                    }

                }

            }
        public string UpdateActivity(Activity activity)
        {
            string connectionString = GetSrting();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UpdateActivity", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@activityId", activity.AcId);
                    cmd.Parameters.AddWithValue("@categoryId", activity.AcCategoryId);
                    cmd.Parameters.AddWithValue("@description", activity.AcDescription);
                    cmd.Parameters.AddWithValue("@participants", activity.AcParticipants);
                    cmd.Parameters.AddWithValue("@date", activity.AcDate);
                    //AddActivityCategory(activity);
                    cmd.ExecuteNonQuery();

                    return null; // success   
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message); // return error message  
                }
                finally
                {
                    con.Close();
                }

            }

        }

        public static string GetSrting()
        {
            return ConnectionStringSetting.GetConnectionString();
        }
    }
}

