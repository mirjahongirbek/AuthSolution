namespace AuthService.ModelView.UserRole
{
  public  class AddUserRoleModel<TKey>
    {
        public TKey UserId { get; set; }
        public TKey RoleId { get; set; }

    }
}
