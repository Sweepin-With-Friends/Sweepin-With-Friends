using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.MySqlClient;
using System.Data.SqlClient;
using TMPro;
using UnityEngine;

public class SubmitProfileData : MonoBehaviour
{
    public TextMeshProUGUI Username, Email, AvatarURL, Password;

    void SendInfoUp()
    {
       /* using (SqlConnection conn = new SqlConnection(DBHelperSweepin.GetConnectionString()))
        {
            // step 1
            // step 2
            string sql = "INSERT INTO User(Email, Username, AvatarURL, PasswordHash) "
                + "VALUES (@email, @username, @avatarURL, @passwordHash)";
            // step 3
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", Username);
            cmd.Parameters.AddWithValue("@email", Email);
            cmd.Parameters.AddWithValue("@avatarURL", AvatarURL);
            cmd.Parameters.AddWithValue("@passwordHash", Password);
            conn.Open();
            cmd.ExecuteNonQuery();
        }*/
    }
}
