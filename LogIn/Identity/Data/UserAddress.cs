using System;
namespace Identity.Data;
public class UserAddress
{
	public int Id { get; set; }
	public User User { get; set; }
}