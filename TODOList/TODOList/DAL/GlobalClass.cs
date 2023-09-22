using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using TODOList.Models;

namespace TODOList.DAL
{
    public class GlobalClass
    {
        public DataSet GetQuestions()
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("usersSP", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Action", "Secquestions");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "Tasks");
            sqlConnection.Close();
            return dataSet;
        }

        public DataSet GetCategories()
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("usersSP", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Action", "Categ");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "Tasks");
            sqlConnection.Close();
            return dataSet;
        }

        public bool CheckDuplicate(RegisterModel register)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("usersSP", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            if(register.Email != null)
            {
                sqlCommand.Parameters.AddWithValue("@Username", register.Username);
                sqlCommand.Parameters.AddWithValue("@Pass", register.Pass);
                sqlCommand.Parameters.AddWithValue("@Email", register.Email);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Username", register.Username);
                sqlCommand.Parameters.AddWithValue("@Pass", register.Pass);
            }
            sqlCommand.Parameters.AddWithValue("@Action", "Check");
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            bool chk;
            if (dataReader.Read())
                chk = true;
            else
                chk = false;
            return chk;
        }

        public IdModel Getid(IdModel id)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("usersSP", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Username", id.Username);
            sqlCommand.Parameters.AddWithValue("@Action", "Getid");
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            //string uid = "0";
            IdModel id1 = new IdModel();
            if (dataReader.Read())
            {
                id1.UserId = int.Parse(dataReader[5].ToString());
                id1.Questions = dataReader[0].ToString();
                id1.passwords = dataReader[4].ToString();
                return id1;
            }
            else
                return id1;
        }

        public void InsertData(RegisterModel register)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("usersSP", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            if (register.Email != null)
            {
                sqlCommand.Parameters.AddWithValue("@Username", register.Username);
                sqlCommand.Parameters.AddWithValue("@Pass", register.Pass);
                sqlCommand.Parameters.AddWithValue("@Email", register.Email);
                sqlCommand.Parameters.AddWithValue("@Queid", register.Queid);
                sqlCommand.Parameters.AddWithValue("@Answers", register.Answers);
                sqlCommand.Parameters.AddWithValue("@Action", "InsertData");
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Username", register.Username);
                sqlCommand.Parameters.AddWithValue("@Pass", register.Pass);
                sqlCommand.Parameters.AddWithValue("@Action", "ChangePass");
            }
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void InsertTasks(int CatId, string Task, int u)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("usersSP", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Catid", CatId);
            sqlCommand.Parameters.AddWithValue("@Userid", u);
            sqlCommand.Parameters.AddWithValue("@Task", Task);
            sqlCommand.Parameters.AddWithValue("@Action", "AddTask");
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public DataSet GetTask(TaskModel task, int uid)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("usersSP", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Catid", task.CatId);
            sqlCommand.Parameters.AddWithValue("@Userid", uid);
            sqlCommand.Parameters.AddWithValue("@Action", "GetTask");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "Tasks");
            sqlConnection.Close();
            return dataSet;
        }
    }
}