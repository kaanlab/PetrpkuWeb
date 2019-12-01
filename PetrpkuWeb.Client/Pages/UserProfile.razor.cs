using Blazor.FileReader;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PetrpkuWeb.Shared.Contracts.V1;
using PetrpkuWeb.Shared.ViewModels;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetrpkuWeb.Client.Pages
{
    public partial class UserProfile
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public IFileReaderService fileReaderService { get; set; }

        [Parameter]
        public string Id { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        //private Attachment Avatar { get; set; } = new Attachment();
        private FileInfoViewModel AvatarFileInfoVM { get; set; }

        AppUserViewModel appUser;
        string authUserId;
        bool dialogIsOpen;

        ElementReference inputElement;
        IFileReaderRef fileReaderReference;


        protected override void OnAfterRender(bool isFirstRender)
        {
            fileReaderReference = fileReaderService.CreateReference(inputElement);
        }

        protected override async Task OnInitializedAsync()
        {
            var authUser = (await AuthenticationStateTask).User;
            authUserId = authUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
        }

        protected override async Task OnParametersSetAsync()
        {
            appUser = await HttpClient.GetJsonAsync<AppUserViewModel>($"{ApiRoutes.Users.USER}/{Id}");
        }

        async Task ShowFileInfo()
        {
            foreach (var file in await fileReaderReference.EnumerateFilesAsync())
            {
                var fileInfo = await file.ReadFileInfoAsync();

                AvatarFileInfoVM = new FileInfoViewModel();
                AvatarFileInfoVM.Name = fileInfo.Name;
                AvatarFileInfoVM.Size = fileInfo.Size;
                AvatarFileInfoVM.Type = fileInfo.Type;
            }
        }

        async Task ClearFileInfo()
        {
            AvatarFileInfoVM = null;
            await fileReaderReference.ClearValue();
        }

        void OpenDialog()
        {
            dialogIsOpen = true;
        }

        async Task OkClick()
        {
            dialogIsOpen = false;
            await HttpClient.PutJsonAsync<ProfileViewModel>($"{ApiRoutes.Users.UPDATE}/{appUser.Id}", appUser);
        }

        async Task UploadFile()
        {
            var multipartFormDataContent = new MultipartFormDataContent();
            foreach (var file in await fileReaderReference.EnumerateFilesAsync())
            {
                multipartFormDataContent.Add(
                    new StreamContent(await file.OpenReadAsync(), 8192), "file", (await file.ReadFileInfoAsync()).Name);
            }

            var response = await HttpClient.PostAsync(requestUri: ApiRoutes.Upload.AVATAR, content: multipartFormDataContent);
            appUser.Avatar = await response.Content.ReadAsStringAsync();
            await ClearFileInfo();
        }
    }
}
