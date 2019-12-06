using AutoMapper;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;
using PetrpkuWeb.Shared.Contracts.V1;
using PetrpkuWeb.Shared.Models;
using PetrpkuWeb.Shared.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetrpkuWeb.Client.Pages
{
    public partial class UserManager
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public IMatToaster Toaster { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        private LdapUser LdapUser { get; set; } = new LdapUser();

        string SearchTerm { get; set; } = String.Empty;

        List<LdapUser> ldapUsers;
        ClaimsPrincipal authUser;
        List<AppUserDepartmentBuildingView> appUsersList;
        AppUserDepartmentBuildingView editAppUser;
        AppUserRoleView appUserRole;
        List<RoleView> roles;
        List<DepartmentView> departments;
        List<BuildingView> buildings;
        List<AppUserDepartmentBuildingView> filtredAppUsers;

        bool editAppUserDialogIsOpen = false;
        bool newUserDialogIsOpen = false;
        bool appUserRolesDialogIsOpen = false;

        protected override async Task OnInitializedAsync()
        {
            authUser = (await AuthenticationStateTask).User;

            appUsersList = await HttpClient.GetJsonAsync<List<AppUserDepartmentBuildingView>>(ApiRoutes.Users.ALL);
            departments = await HttpClient.GetJsonAsync<List<DepartmentView>>(ApiRoutes.Departments.ALL);
            buildings = await HttpClient.GetJsonAsync<List<BuildingView>>(ApiRoutes.Buildings.ALL);
            ldapUsers = await HttpClient.GetJsonAsync<List<LdapUser>>(ApiRoutes.Account.ALL_LDAPUSERS);

        }

        async Task AddAccount()
        {
            newUserDialogIsOpen = false;

            var newUser = ldapUsers.FirstOrDefault(u => u.UserName == LdapUser.UserName);

            // Remove from dropdown list of LdapUsers collection
            ldapUsers.Remove(newUser);

            var jsonContent = JsonConvert.SerializeObject(newUser);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync(ApiRoutes.Account.ADD_IDENTITY, byteContent);
            var content = await response.Content.ReadAsStringAsync();

            //Add to UsersIdentity collection
            var appUser = JsonConvert.DeserializeObject<AppUserDepartmentBuildingView>(content);
            appUsersList.Add(appUser);
            appUsersList = appUsersList.OrderBy(d => d.DisplayName).ToList();
            LdapUser = new LdapUser();

            Toaster.Add($"Пользователь успешно добавлен в систему", MatToastType.Success, "Успех!");
        }

        async Task OpenAppUserRolesDialog(AppUserDepartmentBuildingView user)
        {
            appUserRole = Mapper.Map(user, appUserRole);
            roles = await HttpClient.GetJsonAsync<List<RoleView>>(ApiRoutes.Roles.ALL);
            appUserRolesDialogIsOpen = true;
 
        }

        async Task AddAppUserToRole()
        {
            var response = await HttpClient.PostJsonAsync<AppUserRoleView>(ApiRoutes.Users.ADD_TO_ROLE, appUserRole);
            appUserRolesDialogIsOpen = false;
        }

        async Task RemoveAppUserToRole()
        {
            var response = await HttpClient.PostJsonAsync<AppUserRoleView>(ApiRoutes.Users.REMOVE_FROM_ROLE, appUserRole);
            appUserRolesDialogIsOpen = false;
        }

        void OpenEditAppUserDialog(string appUserId)
        {
            editAppUserDialogIsOpen = true;
            editAppUser = appUsersList.FirstOrDefault(u => u.Id == appUserId);
        }

        async Task UpdateAppUser(string appUserId)
        {
            editAppUserDialogIsOpen = false;
            var response = await HttpClient.PutJsonAsync<AppUserDepartmentBuildingView>($"{ApiRoutes.Users.UPDATE}/{appUserId}", editAppUser);
            var index = appUsersList.FindIndex(u => u.Id == response.Id);
            appUsersList[index] = response;
            Toaster.Add($"Информация о пользователе {response.DisplayName} успешно обновлена", MatToastType.Success, "Успех!");
        }

        void SearchKeyDown(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                SearchCommand();
            }
        }

        void SearchButton()
        {
            SearchCommand();
        }

        void ClearButton()
        {
            SearchTerm = String.Empty;
            filtredAppUsers = null;
        }

        void SearchCommand()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                string filter = SearchTerm.ToLower();
                filtredAppUsers = appUsersList
                    .Where(u => (u.LastName ?? "").ToLower().Contains(filter) || (u.IntPhone ?? "").ToLower().Contains(filter) || (u.DepartmentView.Name ?? "").ToLower().Contains(filter)).ToList();
                if (filtredAppUsers.Count < 1)
                    Toaster.Add($"Пользователь не найден. Попробуйте другой критерий поиска", MatToastType.Danger, "Ошибка!");
            }
            else
            {
                filtredAppUsers = null;
            }
        }
    }
}
