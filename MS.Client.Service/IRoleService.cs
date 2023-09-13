namespace MS.Client.Service
{
    public interface IRoleService : IBaseService<RoleDto>
    {
        Task<FurApiResponse> GetMenusByRoleIdAsync(int roleId);
        Task<FurApiResponse> GetUsersByRoleIdAsync(int roleId);
        Task<FurApiResponse> BatchInsertAsync(RoleBatchModel model);
        //Task<FurApiResponse> BatchDeleteAsync(RoleBatchModel model);
        //Task<FurApiResponse> BatchUpdateAsync(RoleBatchModel model);
        Task<FurApiResponse> BatchUpdateRoleInfoAsync(RoleBatchModel model);
    }
}
