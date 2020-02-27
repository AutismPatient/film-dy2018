using Macrocosm.Models;
using Macrocosm.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Service
{
    public interface ISystemService
    {
        Task<bool> Additive(string adminId, AdditiveAdminModel additive);
        Task<bool> Forbidden(string adminId, string userId);
        Task<bool> UnForbidden(string adminId, string userId);
        Task<bool> Delete(string adminId, string userId);
        Task<bool> ChangePassWord(string adminId, string password);
        Task<WorkbenchInfo> Workbench();
        Task<List<Movie>> WorkMovies(int page);
        Task<List<System_User>> GetUsersAsync(int page, int page_size);
        Task<System_User> UserDetailAsync(int id);
        Task<int> GetUsersCountAsync();
        Task<RedisListModel> RedisListsAsync(int page = 1, int page_size = 10);
        Task<bool> DeleteKeyAsync(string key);
        Task<List<System_Config>> Settings();
        Task<bool> AddConfigAsync(AddConfigModel model);
        Task<bool> DeleteConfigAsync(string Id);
        Task<bool> EditConfigAsync(string id, string val);
        Task<List<Action_logs>> Action_LogsAsync(int page = 1, int page_size = 10);
        Task<int> LogCountAsync();
        Task<bool> DeleteLogAsync(string id);
    }
}
