using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserProfile : MonoBehaviour
{
    public int UserID { get; set; } 

    public string Email { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string AvatarUrl { get; set; } = string.Empty;

    public string Salt { get; set; }

    public string PasswordHash { get; set; }

    public int GameID { get; set; }


}
