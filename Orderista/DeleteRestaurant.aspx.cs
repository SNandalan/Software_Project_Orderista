﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DeleteRestaurant : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["OrderistaConnectionString"].ConnectionString);
    SqlCommand comm;
    SqlDataReader reader;
    SqlDataAdapter adapter;
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        string useradmin = Session["UserAdmin"].ToString();

        UserAdmin.Text = "Hello, " + useradmin;

            BindGrid();
    }
    protected void DelRestaurant(object sender, GridViewDeleteEventArgs e)
    {
        //string userName = GridDeleteRestaurnt.Columns[2].
        GridViewRow row = (GridViewRow)GridDeleteRestaurant.Rows[e.RowIndex];
        adapter = new SqlDataAdapter(comm);
        //string connString = "Server=LAPTOP-I8AD7C8G\\MSSQLSERVER2017;Initial Catalog=SwiftServe;Integrated Security=True";
        //conn = new SqlConnection(connString);
        // comm.Parameters.AddWithValue("@res", row.Cells[2].Text);
        string sql = "Delete From RESTAURANTS Where Username = '" + row.Cells[2].Text + "'";

        comm = new SqlCommand(sql, conn);

        try
        {
            conn.Open();
            adapter.DeleteCommand = new SqlCommand(sql, conn);
            adapter.DeleteCommand.ExecuteNonQuery();
            BindGrid();

            Response.Write("Restaurant deleted");
            Page_Load(sender, e);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            Response.Write("Connection Failed: Check Connexion String");
        }
        finally
        {
            conn.Close();
        }
    }
    void BindGrid()
    {
        //Change connSTRING!!!
        //=LAPTOP-I8AD7C8G\\MSSQLSERVER2017;Initial Catalog=SwiftServe;Integrated Security=True;
        //string connString = "Server=LAPTOP-I8AD7C8G\\MSSQLSERVER2017;Initial Catalog=SwiftServe;Integrated Security=True";
        //conn = new SqlConnection(connString); Select Restaurant_Name,Username, Password,Email, Contact from Restaurants
      //  comm = new SqlCommand("Select Restaurant_Name,Username, Password,Email, Contact from Restaurants", conn);
        comm = new SqlCommand("Select * from Restaurants", conn);

        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            GridDeleteRestaurant.DataSource = reader;
            GridDeleteRestaurant.DataBind();
        }
        catch
        {
            Response.Write("ERROR");
        }
        finally
        {
            conn.Close();
        }
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminPage.aspx");
    }
}