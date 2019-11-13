using Blazor.FileReader;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PetrpkuWeb.Shared.Models;
using PetrpkuWeb.Shared.ViewModels;
using System;
using System.Collections.Generic;
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
        public int AppUserId { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        //private Attachment Avatar { get; set; } = new Attachment();
        private FileInfoViewModel AvatarFileInfoVM { get; set; }

        AppUser user;
        int identityUserId;
        bool dialogIsOpen;

        ElementReference inputElement;
        IFileReaderRef fileReaderReference;


        protected override void OnAfterRender(bool isFirstRender)
        {
            fileReaderReference = fileReaderService.CreateReference(inputElement);
        }

        protected override async Task OnInitializedAsync()
        {
            var identityUser = (await AuthenticationStateTask).User;
            identityUserId = Int32.Parse(identityUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value);
        }

        protected override async Task OnParametersSetAsync()
        {
            user = await HttpClient.GetJsonAsync<AppUser>($"api/users/user/{AppUserId}");
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
            await HttpClient.PutJsonAsync<AppUser>($"api/users/update/{user.AppUserId}", user);
        }

        async Task UploadFile()
        {
            var multipartFormDataContent = new MultipartFormDataContent();
            foreach (var file in await fileReaderReference.EnumerateFilesAsync())
            {
                multipartFormDataContent.Add(
                    new StreamContent(await file.OpenReadAsync(), 8192), "file", (await file.ReadFileInfoAsync()).Name);
            }

            var response = await HttpClient.PostAsync(requestUri: "api/upload/avatar", content: multipartFormDataContent);
            user.Avatar = await response.Content.ReadAsStringAsync();
            await ClearFileInfo();
        }
    }
}
