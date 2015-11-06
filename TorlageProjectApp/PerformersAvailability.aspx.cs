﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TorlageProjectApp
{
    public partial class PerformersAvailability : System.Web.UI.Page
    {
        bool available = false;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CalendarChangeAvailability_SelectionChanged(object sender, EventArgs e)
        {
            TextBoxChangeAvailability.Text = CalendarChanageAvailability.SelectedDate.ToString();
            LabelUserAlreadyClickedAvailability.Text = "";
            //establish an connection to the SQL server 
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ToConnectionString"].ConnectionString;
            string selectCommand = "SELECT PerformersAvailable.AvailableID, Performers.PerformerName, PerformersAvailable.ScheduleDate, " +
                "PerformersAvailable.Available " +
                "FROM PerformersAvailable " +
                "INNER JOIN Performers " +
                "ON PerformersAvailable.PerformerID = Performers.PerformerID " +
                "WHERE PerformersAvailable.ScheduleDate = '" + TextBoxChangeAvailability.Text + "' And Performers.PerformerName = '"
                + TextBoxUser.Text + "'";
            SqlCommand command = new SqlCommand(selectCommand, connection);
            connection.Open();
            SqlDataReader reader = null;
            try
            {
                reader = command.ExecuteReader();


                while (reader.Read())
                {
                    byte value = (byte)reader["Available"];
                    if (value == 1)
                    {
                        LabelUserAlreadyClickedAvailability.Text = "You already entered that you were available. Are you available?";
                    }
                    else
                    {
                        LabelUserAlreadyClickedAvailability.Text = "You already entered that you were not available. Are you available?";
                    }
                }

            }
            catch (Exception ex)
            {
                LabelUserAlreadyClickedAvailability.Text = "Caught Exception " + ex.ToString();
            }
            reader.Close();
            connection.Close();
        }

        protected void ButtonYes_Click(object sender, EventArgs e)
        {
            bool foundRecordOnDate = false;
            TextBoxChangeAvailability.Text = CalendarChanageAvailability.SelectedDate.ToString();

            // need to find if record exists
            //establish an connection to the SQL server 
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ToConnectionString"].ConnectionString;
            string selectCommand = "SELECT PerformersAvailable.AvailableID, Performers.PerformerName, PerformersAvailable.ScheduleDate, " +
                "PerformersAvailable.Available " +
                "FROM PerformersAvailable " +
                "INNER JOIN Performers " +
                "ON PerformersAvailable.PerformerID = Performers.PerformerID " +
                "WHERE PerformersAvailable.ScheduleDate = '" + TextBoxChangeAvailability.Text + "' And Performers.PerformerName = '"
                + TextBoxUser.Text + "'";
            SqlCommand command = new SqlCommand(selectCommand, connection);
            connection.Open();
            SqlDataReader reader = null;
            try
            {
                reader = command.ExecuteReader();


                while (reader.Read())
                {
                    foundRecordOnDate = true;
                    byte value = (byte)reader["Available"];
                    if (value == 1)
                    {
                        available = true;
                    }
                    else
                    {
                        available = false;
                    }
                }

            }
            catch (Exception ex)
            {
                LabelUserAlreadyClickedAvailability.Text = "Caught Exception " + ex.ToString();
            }

            reader.Close();
            connection.Close();

            if (foundRecordOnDate && !available)
            {
                // do update of record

                //establish an connection to the SQL server 
                SqlConnection connection2 = new SqlConnection();
                connection2.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ToConnectionString"].ConnectionString;
                string updateCommand = "UPDATE PerformersAvailable " +
                    "SET PerformersAvailable.Available = 1 " +
                    "FROM PerformersAvailable " +
                    "INNER JOIN Performers " +
                    "ON PerformersAvailable.PerformerID = Performers.PerformerID " +
                    "WHERE ScheduleDate = '" + TextBoxChangeAvailability.Text + "' AND Performers.PerformerName = '"
                    + TextBoxUser.Text + "'";
                SqlCommand command2 = new SqlCommand(updateCommand, connection2);
                connection2.Open();
                command2.ExecuteNonQuery();
                connection2.Close();

            } else if (!foundRecordOnDate)
            {
                // do insert of the record

                //establish an connection to the SQL server 
                SqlConnection connection4 = new SqlConnection();
                connection4.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ToConnectionString"].ConnectionString;
                string selectQueryForUserName = "(SELECT PerformerID FROM Performers WHERE PerformerName = '" + TextBoxUser.Text + "')";
                string insertCommand = "INSERT INTO PerformersAvailable (ScheduleDate, PerformerID, Available, TentativeShow) VALUES('" + TextBoxChangeAvailability.Text + "', "
                    + selectQueryForUserName + ", 1, 0)";
                SqlCommand command4 = new SqlCommand(insertCommand, connection4);
                connection4.Open();
                command4.ExecuteNonQuery();
                connection4.Close();
            }

            LabelUserAlreadyClickedAvailability.Text = "You entered that you were available. Are you available?";
        }

        protected void ButtonNo_Click(object sender, EventArgs e)
        {
            bool foundRecordOnDate = false;
            TextBoxChangeAvailability.Text = CalendarChanageAvailability.SelectedDate.ToString();

            // need to find if record exists
            //establish an connection to the SQL server 
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ToConnectionString"].ConnectionString;
            string selectCommand = "SELECT PerformersAvailable.AvailableID, Performers.PerformerName, PerformersAvailable.ScheduleDate, " +
                "PerformersAvailable.Available " +
                "FROM PerformersAvailable " +
                "INNER JOIN Performers " +
                "ON PerformersAvailable.PerformerID = Performers.PerformerID " +
                "WHERE PerformersAvailable.ScheduleDate = '" + TextBoxChangeAvailability.Text + "' And Performers.PerformerName = '"
                + TextBoxUser.Text + "'";
            SqlCommand command = new SqlCommand(selectCommand, connection);
            connection.Open();
            SqlDataReader reader = null;
            try
            {
                reader = command.ExecuteReader();


                while (reader.Read())
                {
                    foundRecordOnDate = true;
                    byte value = (byte)reader["Available"];
                    if (value == 1)
                    {
                        available = true;
                    }
                    else
                    {
                        available = false;
                    }
                }

            }
            catch (Exception ex)
            {
                LabelUserAlreadyClickedAvailability.Text = "Caught Exception " + ex.ToString();
            }

            reader.Close();
            connection.Close();

            if (foundRecordOnDate && available)
            {
                // do update of record

                //establish an connection to the SQL server 
                SqlConnection connection2 = new SqlConnection();
                connection2.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ToConnectionString"].ConnectionString;
                string updateCommand = "UPDATE PerformersAvailable " +
                    "SET PerformersAvailable.Available = 0 " +
                    "FROM PerformersAvailable " +
                    "INNER JOIN Performers " +
                    "ON PerformersAvailable.PerformerID = Performers.PerformerID " +
                    "WHERE ScheduleDate = '" + TextBoxChangeAvailability.Text + "' AND Performers.PerformerName = '"
                    + TextBoxUser.Text + "'";
                SqlCommand command2 = new SqlCommand(updateCommand, connection2);
                connection2.Open();
                command2.ExecuteNonQuery();
                connection2.Close();

            }
            else if (!foundRecordOnDate)
            {
                // do insert of the record

                //establish an connection to the SQL server 
                SqlConnection connection4 = new SqlConnection();
                connection4.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ToConnectionString"].ConnectionString;
                string selectQueryForUserName = "(SELECT PerformerID FROM Performers WHERE PerformerName = '" + TextBoxUser.Text + "')";
                string insertCommand = "INSERT INTO PerformersAvailable (ScheduleDate, PerformerID, Available, TentativeShow) VALUES('" + TextBoxChangeAvailability.Text + "', "
                    + selectQueryForUserName + ", 0, 0)";
                SqlCommand command4 = new SqlCommand(insertCommand, connection4);
                connection4.Open();
                command4.ExecuteNonQuery();
                connection4.Close();
            }

            LabelUserAlreadyClickedAvailability.Text = "You entered that you were not available. Are you available?";

        }
    }


}