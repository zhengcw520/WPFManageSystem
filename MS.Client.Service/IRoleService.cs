namespace MS.Client.Service
{
    public interface IRoleService : IBaseService<RoleDto>
    {
        Task<FurApiResponse<List<RoleMenuDto>>> GetMenusByRoleIdAsync(int roleId);
        Task<FurApiResponse<List<UserTBDto>>> GetUsersByRoleIdAsync(int roleId);
        Task<FurApiResponse> BatchInsertAsync(RoleBatchModel model);
        //Task<FurApiResponse> BatchDeleteAsync(RoleBatchModel model);
        Task<FurApiResponse> BatchUpdateAsync(RoleBatchModel model);
        Task<FurApiResponse> BatchUpdateRoleMenuAsync(RoleBatchModel model);
    }
}
